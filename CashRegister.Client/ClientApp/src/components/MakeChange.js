import React, { Component } from 'react';

export class MakeChange extends Component {
static displayName = MakeChange.name;
  render() {
    const currencyAmountsProvided = this.props.amountsProvided?.filter(x => x.amount > 0).map((currencyAmount) =>
        <li key={currencyAmount.denomination}>{currencyAmount.denomination} x {currencyAmount.amount}</li>
    );
    const changeTendered = this.props.changeTendered?.filter(x => x.amount > 0).map((change) => 
        <li key={change.denomination}>{change.denomination} x {change.amount}</li>
    );

    return (
        <div class="panel">
            <div class="panel__header">
                <h2>Change To Be Returned</h2>
            </div>
            <div class="panel__content">
                <div class="flex">
                    <div>
                        Cash Provided
                        <ul>
                            {currencyAmountsProvided}
                        </ul>
                    </div>
                    <div>
                        Change to return
                        <ul>
                            {changeTendered}
                        </ul>
                    </div>
                </div>
            </div>
        </div>
    );
  }
}
