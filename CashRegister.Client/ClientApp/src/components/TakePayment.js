import React, { Component } from 'react';

export class TakePayment extends Component {
static displayName = TakePayment.name;

  constructor(props) {
    super(props);

    let amounts = this.props.denominations.map(denomination => {
        return {
            details: denomination,
            denomination: denomination.name,
            amount: 0,
        }
    });

    this.state = {
        denominations: this.props.denominations,
        loading: true,
        currencyAmounts: amounts,
        cost: 0,
        total: 0,
        errorMessage: '',
    };

    this.handleCostUpdate = this.handleCostUpdate.bind(this);
    this.makePayment = this.makePayment.bind(this);
    this.updateAmount = this.updateAmount.bind(this);
    this.clearAmounts = this.clearAmounts.bind(this);
  }

  calculateTotals() {
    let total = this.state.currencyAmounts.reduce((rollingTotal, currencyAmount) => {
      let currencyPaid = currencyAmount.amount * currencyAmount.details.value;
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

  updateAmount(denomination, adjustment) {
    let amounts = this.state.currencyAmounts;
    let amountToUpdate = amounts.find(amount => amount.denomination === denomination.details.name);
    if (amountToUpdate.amount + adjustment >= 0)
    {
        amountToUpdate.amount += adjustment;
        this.setState({currencyAmounts: amounts});
        this.calculateTotals();
    }
  }

  async makePayment() {
    this.setState({errorMessage: ''});
    let request = {
        method: 'POST',
        headers: { 'Content-Type': 'application/json'},
        body: JSON.stringify({transaction: {
            amountsPaid: this.state.currencyAmounts.map(currencyType => {return {denomination: currencyType.denomination, amount: currencyType.amount}}),
            cost: this.state.cost 
        }})
    };
    await fetch('cashregister/payment', request).then(response => {
      if (!response.ok) { throw response }
      this.props.callback();
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
              {this.state.currencyAmounts.map(denomination => 
                <tr key={denomination.details.display}>
                  <td><b>{denomination.details.display} (${denomination.details.value}):</b></td>
                  <td class="table-column"><button type="button" className="button button__increase" disabled={denomination.amount < 1} onClick={() => this.updateAmount(denomination, -1)}>-${denomination.details.value}</button></td>
                  <td class="table-column">{denomination.amount}</td>
                  <td class="table-column"><button type="button" className="button button__decrease" onClick={() => this.updateAmount(denomination, +1)}>+${denomination.details.value}</button></td>
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
              <button class="button button__submit" type="submit" disabled={(this.state.cost > this.state.total) || this.state.cost < .01} onClick={this.makePayment}>Make Payment</button>
            </div>
          </div>          
      </div>
    );
  }
}
