import React, { Component } from 'react';
import denominationDisplayName from '../helpers/denominationDisplayName';

export class MakeChange extends Component {
static displayName = MakeChange.name;

  getTotalValueOfDenomination(denomination) {
      let value = this.props.currencyAmounts.find(x => x.denomination === denomination.denomination)?.value;
      return denomination.amount * value;
  }

  sumTotalValues(currencyTotals) {
    return currencyTotals.reduce((rollingTotal, currencyAmount) => {
        return rollingTotal += this.getTotalValueOfDenomination(currencyAmount);
      }, 0).toFixed(2);
  }

  render() {
    const currencyTotals = (amounts) => amounts.filter(x => x.amount > 0).map((currencyAmount) =>
        <tr key={currencyAmount.denomination}>
            <td></td>
            <td>{denominationDisplayName(currencyAmount.denomination)}</td>
            <td>x {currencyAmount.amount}</td>
            <td>${this.getTotalValueOfDenomination(currencyAmount)}</td>
        </tr>
    );

    const table = 
        <table className='table table-striped' aria-labelledby="tabelLabel">
            <thead>
            <tr>
                <th>Type</th>
                <th>Description</th>
                <th>Amount</th>
                <th>Value</th>
            </tr>
            </thead>
            <tbody>
            <tr className="total-rows">
                <td><label>Cost:</label></td>
                <td colSpan="2"></td>
                <td>${this.props.purchaseCost}</td>
            </tr>
            <tr>
                <td><label>Amount Spent:</label></td>
                <td colSpan="3"></td>
            </tr>
            {currencyTotals(this.props.amountsProvided)}
            <tr className="total-rows">
                <td colSpan="2"></td>
                <td><b>Total</b></td>
                <td>${this.sumTotalValues(this.props.amountsProvided)}</td>
            </tr>
            <tr>
                <td><label>Change Tendered:</label></td>
                <td colSpan="3"></td>
            </tr>
            {currencyTotals(this.props.changeTendered)}
            <tr className="total-rows">
                <td colSpan="2"></td>
                <td><b>Total</b></td>
                <td>${this.sumTotalValues(this.props.changeTendered)}</td>
            </tr>
            </tbody>
        </table>

    return (
        <div className="panel make-change">
            <div className="panel__header">
                <h2>Change To Be Returned</h2>
            </div>
            <div className="panel__content">
                {this.props.amountsProvided.length > 0 ? table : ""}
            </div>
        </div>
    );
  }
}
