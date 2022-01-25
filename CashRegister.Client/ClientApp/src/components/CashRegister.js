import React, { Component } from 'react';
import { Amounts } from './Amounts';
import { MakeChange} from './MakeChange';
import { TakePayment } from './TakePayment';
import DenominationTypes from '../constants/DenominationTypes';

export class CashRegister extends Component {
static displayName = CashRegister.name;

  constructor(props) {
    super(props);
    this.state = {
        amounts: [],
        changeTendered: [],
        amountsProvided: [],
    };
    this.amountChangeCallback = this.amountChangeCallback.bind(this);
  }

  componentDidMount() {
    this.populateAmounts();
  }

  amountChangeCallback(amountsProvided, changeTendered) {
    this.populateAmounts();
    this.setState({ amountsProvided: amountsProvided, changeTendered: changeTendered })
  }

  render() {
    return (
      <div>
          <Amounts key={this.state.totalAmounts} amounts = {this.state.amounts} />
          <div class="flex">
            <TakePayment callback = {this.amountChangeCallback}/>
            <MakeChange key={this.state.changeTendered} changeTendered = {this.state.changeTendered} amountsProvided = { this.state.amountsProvided }/>
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
}
