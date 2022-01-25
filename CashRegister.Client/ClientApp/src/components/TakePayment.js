import React, { Component } from 'react';
import denominationDisplayName from '../helpers/denominationDisplayName';

export class TakePayment extends Component {
static displayName = TakePayment.name;

  constructor(props) {
    super(props);

    this.state = {
        loading: true,
        currencyAmounts: [],
        cost: 0,
        total: 0,
        errorMessage: '',
    };

    this.handleCostUpdate = this.handleCostUpdate.bind(this);
    this.makePayment = this.makePayment.bind(this);
    this.updateAmount = this.updateAmount.bind(this);
    this.clearAmounts = this.clearAmounts.bind(this);
  }

  componentDidMount() {
    this.populateDenominations();
  }

  calculateTotals() {
    let total = this.state.currencyAmounts.reduce((rollingTotal, currencyAmount) => {
      let currencyPaid = currencyAmount.amount * currencyAmount.value;
      return rollingTotal += currencyPaid;
    }, 0).toFixed(2);
    this.setState({total: total})
  }

  clearAmounts() {
    let amounts = this.state.currencyAmounts;
    amounts.forEach(currencyAmount => {
      currencyAmount.amount = 0
    })
    this.setState({currencyAmounts: amounts, total: 0, cost: 0})
  }

  handleCostUpdate(event){
      this.setState({cost: event.target.value})
  }

  updateAmount(currencyDenomination, adjustment) {
    let amounts = this.state.currencyAmounts;
    let amountToUpdate = amounts.find(amount => amount.denomination === currencyDenomination.denomination);
    if (amountToUpdate.amount + adjustment >= 0)
    {
        amountToUpdate.amount += adjustment;
        this.setState({currencyAmounts: amounts});
        this.calculateTotals();
    }
  }

  async populateDenominations() {
    const response = await fetch('cashregister/denominations');
    const data = await response.json();
    this.setState(
      { 
        currencyAmounts: data.denominations.map(denomination => {
          return {
            denomination: denomination.name,
            value: denomination.value,
            amount: 0
          }
        }),
      });
  }

  async makePayment() {
    this.setState({errorMessage: ''});
    let amountsPaid = this.state.currencyAmounts.map(currencyType => {return {denomination: currencyType.denomination, amount: currencyType.amount}});
    let request = {
        method: 'POST',
        headers: { 'Content-Type': 'application/json'},
        body: JSON.stringify({transaction: {
            amountsPaid,
            cost: this.state.cost 
        }})
    };
    await fetch('cashregister/payment', request).then(response => {
      if (!response.ok) { throw response }
      response.json().then(data => {
        this.props.callback(amountsPaid, data.currencyAmounts.amounts);
      })
      this.clearAmounts();
    }).catch((error) => {
      error.text().then(errorMessage => {
        this.setState({errorMessage: errorMessage})
      })
    });
  }

  render() {
    return (
      <div class="panel">
          <div class="panel__header">
              <h2>Enter Payment</h2>
          </div>
          <div class="panel__content">
            <table className='table table-striped' aria-labelledby="tabelLabel">
              <thead>
                <tr>
                  <th>Denomination</th>
                  <th class="table-column">Remove One</th>
                  <th class="table-column">#</th>
                  <th class="table-column">Add One</th>
                </tr>
              </thead>
              <tbody>
              {this.state.currencyAmounts.map(currencyDenomination => 
                <tr key={denominationDisplayName(currencyDenomination.denomination)}>
                  <td><b>{denominationDisplayName(currencyDenomination.denomination)} (${currencyDenomination.value}):</b></td>
                  <td class="table-column"><button type="button" className="button button__increase" disabled={currencyDenomination.amount < 1} onClick={() => this.updateAmount(currencyDenomination, -1)}>-${currencyDenomination.value}</button></td>
                  <td class="table-column">{currencyDenomination.amount}</td>
                  <td class="table-column"><button type="button" className="button button__decrease" onClick={() => this.updateAmount(currencyDenomination, +1)}>+${currencyDenomination.value}</button></td>
                </tr>
              )}
                <tr class="total-rows">
                  <td colSpan="2"></td>
                  <td><label for="cost">Total Paid:</label></td>
                  <td>&nbsp;&nbsp;$ &nbsp;&nbsp;{this.state.total}</td>
                </tr>
                <tr>
                  <td colSpan="2"></td>
                  <td><label for="cost">Cost:</label></td>
                  <td><div class="input-icon"><input class="cost-input" id="cost" type="number" min="0" value={this.state.cost} onChange={this.handleCostUpdate} min=".01" /><i>$</i></div></td>
                </tr>
              </tbody>
            </table>
            <span class="error-message">{this.state.errorMessage}</span>
            <br/>
            <div class="form-submit">
              <button class="button button__submit" type="submit" disabled={(parseFloat(this.state.cost) > parseFloat(this.state.total)) || this.state.cost < .01} onClick={this.makePayment}>Make Payment</button>
            </div>
          </div>          
      </div>
    );
  }
}
