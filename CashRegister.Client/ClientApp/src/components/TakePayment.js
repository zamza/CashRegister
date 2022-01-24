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
    };

    this.handleCostUpdate = this.handleCostUpdate.bind(this);
    this.makePayment = this.makePayment.bind(this);
    this.updateAmount = this.updateAmount.bind(this);
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
        this.setState({currencyAmounts: amounts})
    }
  }

  async makePayment() {
    let request = {
        method: 'POST',
        headers: { 'Content-Type': 'application/json'},
        body: JSON.stringify({transaction: {
            amountsPaid: this.state.currencyAmounts.map(currencyType => {return {denomination: currencyType.denomination, amount: currencyType.amount}}),
            cost: this.state.cost 
        }})
    };
    const response = await fetch('cashregister/payment', request);
    this.props.callback();
  }

  render() {
    return (
      <div class="panel">
          <div class="panel__header">
              <h2>Enter Payment</h2>
          </div>
          <div class="panel__content">
            {this.state.currencyAmounts.map(denomination => 
                <div key={denomination.details.name}>
                    {denomination.details.name} (${denomination.details.value}):
                    <button className="btn btn-primary" onClick={() => this.updateAmount(denomination, -1)}>-1</button>
                    {denomination.amount}
                    <button className="btn btn-primary" onClick={() => this.updateAmount(denomination, +1)}>+1</button>
                </div>
            )}
            <label>
                Cost of Purchase: 
                <input id="cost" type="number" min="0" value={this.state.cost} onChange={this.handleCostUpdate} min=".01" />
            </label>
            <button type="submit" onClick={this.makePayment}>Make Payment</button>
          </div>          
      </div>
    );
  }
}
