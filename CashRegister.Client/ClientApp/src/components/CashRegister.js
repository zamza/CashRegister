import React, { Component } from 'react';
import { Amounts } from './Amounts';
import { TakePayment } from './TakePayment';
import DenominationTypes from '../constants/DenominationTypes';

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
          <div class="flex">
            <TakePayment key={this.state.denominations} denominations = {this.state.denominations} callback = {this.amountChangeCallback}/>
            <div class="panel">
              <TakePayment key={this.state.denominations} denominations = {this.state.denominations} callback = {this.amountChangeCallback}/>
            </div>
          </div>
      </div>
    );
  }

  async populateAmounts() {
    const response = await fetch('cashregister/amounts');
    const data = await response.json();
    this.setState(
      { 
        amounts: data.amounts.map(currencyAmount => {
          return {
            denomination: DenominationTypes.find(x => x.value === currencyAmount.denomination)?.display,
            amount: currencyAmount?.amount
          }
        }), 
      });
  }

  async populateDenominations() {
    const response = await fetch('cashregister/denominations');
    const data = await response.json();
    this.setState(
      { 
        denominations: data.denominations.map(denomination => {
          return {
            name: denomination.name,
            value: denomination.value, 
            display: DenominationTypes.find(x => x.value === denomination.name)?.display
          }
        }),
      });
  }
}
