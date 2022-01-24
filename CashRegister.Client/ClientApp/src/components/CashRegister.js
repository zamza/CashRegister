import React, { Component } from 'react';
import { Amounts } from './Amounts';
import { TakePayment } from './TakePayment';

export class CashRegister extends Component {
static displayName = CashRegister.name;

  constructor(props) {
    super(props);
    this.state = {
        amounts: [],
        denominations: [],
        totalAmounts: 0
    };
    this.amountChangeCallback = this.amountChangeCallback.bind(this);
  }

  componentDidMount() {
    this.populateAmounts();
    this.populateDenominations();
  }

  amountChangeCallback() {
    this.populateAmounts();
  }

  render() {
    return (
      <div>
          <Amounts key={this.state.totalAmounts} amounts = {this.state.amounts} />
          <TakePayment key={this.state.denominations} denominations = {this.state.denominations} callback = {this.amountChangeCallback}/>
      </div>
    );
  }

  async populateAmounts() {
    const response = await fetch('cashregister/amounts');
    const data = await response.json();
      this.setState(
        { 
          amounts: data.amounts, 
        });
  }

  async populateDenominations() {
    const response = await fetch('cashregister/denominations');
    const data = await response.json();
      this.setState(
        { 
          denominations: data.denominations,
        });
  }
}
