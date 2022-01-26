import React, { Component } from 'react';
import denominationDisplayName from '../helpers/denominationDisplayName';
import { toast, ToastContainer } from 'react-toastify';

export class TakePayment extends Component {
static displayName = TakePayment.name;

  constructor(props) {
    super(props);

    this.state = {
        loading: true,
        cost: '',
        total: 0,
    };

    this.handleCostUpdate = this.handleCostUpdate.bind(this);
    this.makePayment = this.makePayment.bind(this);
    this.updateAmount = this.updateAmount.bind(this);
    this.clearAmounts = this.clearAmounts.bind(this);
  }

  calculateTotals() {
    let total = this.props.currencyAmounts.reduce((rollingTotal, currencyAmount) => {
      let currencyPaid = currencyAmount.amount * currencyAmount.value;
      return rollingTotal += currencyPaid;
    }, 0).toFixed(2);
    
    if (isNaN(total)) total = 0;

    this.setState({total: total})
  }

  clearAmounts() {
    let amounts = this.props.currencyAmounts;
    amounts.forEach(currencyAmount => {
      currencyAmount.amount = ''
    })
    this.setState({currencyAmounts: amounts, total: 0, cost: 0})
  }

  handleCostUpdate(event){
    let inputValue = event.target.value;
    let currencyParts = inputValue.split(".");
    if (currencyParts.length > 1 && currencyParts[1].length > 2) return;
    
    const currencyRegex = /[0-9]+(\.[0-9]{1,2})?$/;

    if (inputValue === '' || currencyRegex.test(inputValue)) {
      let inputValue = parseFloat(event.target.value);
      if (isNaN(inputValue)) inputValue = "";
      this.setState({cost: inputValue})
    }
  }

  updateAmount(index, event) {
    let inputValue = event.target.value;
    const currencyRegex = /^[0-9]+(\.[0-9]{1,2})?$/;

    if ((inputValue === '' || currencyRegex.test(inputValue) && inputValue.length < 5))
    {
      let amounts = this.props.currencyAmounts.slice();

      inputValue = parseInt(inputValue);
      if (isNaN(inputValue)) inputValue = "";
      amounts[index].amount = inputValue;
      
      this.setState({currencyAmounts: amounts});
      this.calculateTotals();
    }
  }

  async makePayment() {
    let amountsPaid = this.props.currencyAmounts.map(currencyType => {return {denomination: currencyType.denomination, amount: parseInt(currencyType.amount) || 0}});
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
      toast.success("Payment accepted");
      response.json().then(data => {
        debugger;
        this.props.callback(this.state.cost, amountsPaid, data.currencyAmounts.amounts);
        this.clearAmounts();
      })
    }).catch((error) => {
      error.text().then(errorMessage => {
        toast.error(errorMessage);
      })
    });
  }

  render() {
    return (
      <div className="panel take-payment">
        <ToastContainer></ToastContainer>
          <div className="panel__header">
              <h2>Enter Payment</h2>
          </div>
          <div className="panel__content">
            <table className='table table-striped' aria-labelledby="tabelLabel">
              <thead>
                <tr>
                  <th>Denomination</th>
                  <th></th>
                  <th>Amount</th>
                </tr>
              </thead>
              <tbody>
              <tr>
                <td></td>
                <td><label>Cost:</label></td>
                <td><div className="input-icon">
                  <input 
                    className="table-input" 
                    id="cost" 
                    type="number" 
                    value={this.state.cost} 
                    onChange={this.handleCostUpdate} 
                    min=".01" 
                  /><i>$</i></div></td>
              </tr>
              {this.props.currencyAmounts.map((currencyDenomination, index) => 
                <tr key={denominationDisplayName(currencyDenomination.denomination)}>
                  <td><b>{denominationDisplayName(currencyDenomination.denomination)} (${currencyDenomination.value}):</b></td>
                  <td>x</td>
                  <td><div className="input-icon">
                    <input className="table-input" 
                      type="number" 
                      value={currencyDenomination.amount} 
                      onChange={this.updateAmount.bind(this, index)} min="0" />
                    </div>
                  </td>
                </tr>
              )}
                <tr className="total-rows">
                  <td></td>
                  <td><label>Total Paid:</label></td>
                  <td>&nbsp;&nbsp;$ &nbsp;&nbsp;{this.state.total}</td>
                </tr>
              </tbody>
            </table>
            <br/>
            <div className="form-submit">
              <button className="button button__submit" type="submit" disabled={(parseFloat(this.state.cost) > parseFloat(this.state.total)) || this.state.cost < .01}  onClick={this.makePayment}>Make Payment</button>
            </div>
          </div>          
      </div>
    );
  }
}
