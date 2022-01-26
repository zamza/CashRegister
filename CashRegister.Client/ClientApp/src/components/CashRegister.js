import React, { Component } from 'react';
import { Amounts } from './Amounts';
import { MakeChange} from './MakeChange';
import { TakePayment } from './TakePayment';

export class CashRegister extends Component {
static displayName = CashRegister.name;

  constructor(props) {
    super(props);
    this.state = {
        amounts: [],
        changeTendered: [],
        currencyAmounts: [],
        amountsProvided: [],
        purchaseCost: null,
    };
    this.amountChangeCallback = this.amountChangeCallback.bind(this);
  }

  componentDidMount() {
    this.populateAmounts();
    this.populateDenominations();
  }

  amountChangeCallback(purchaseCost, amountsProvided, changeTendered) {
    this.populateAmounts();
    this.setState({ purchaseCost: purchaseCost, amountsProvided: amountsProvided, changeTendered: changeTendered })
  }

  render() {
    return (
      <div>
          <div className="flex">
            <TakePayment currencyAmounts = {this.state.currencyAmounts} callback = {this.amountChangeCallback}/>
            <MakeChange key={this.state.changeTendered} currencyAmounts = {this.state.currencyAmounts} purchaseCost = {this.state.purchaseCost} changeTendered = {this.state.changeTendered} amountsProvided = { this.state.amountsProvided }/>
            <Amounts key={this.state.totalAmounts} amounts = {this.state.amounts}></Amounts>
          </div>
      </div>
    );
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
            amount: ''
          }
        }),
      });
  }

  async populateAmounts() {
    const response = await fetch('cashregister/amounts');
    const data = await response.json();
    this.setState(
      { 
        amounts: data.amounts.map(currencyAmount => {
          return {
            denomination: currencyAmount.denomination,
            amount: currencyAmount?.amount
          }
        }),
      });
  }
}
