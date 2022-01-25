import React, { Component } from 'react';
import denominationDisplayName from '../helpers/denominationDisplayName';

export class MakeChange extends Component {
static displayName = MakeChange.name;

  render() {
    const currencyAmountsProvided = this.props.amountsProvided?.filter(x => x.amount > 0).map((currencyAmount) =>
        <li key={currencyAmount.denomination}>{denominationDisplayName(currencyAmount.denomination)} x {currencyAmount.amount}</li>
    );
    const changeTendered = this.props.changeTendered?.filter(x => x.amount > 0).map((change) => 
        <li key={change.denomination}>{denominationDisplayName(change.denomination)} x {change.amount}</li>
    );

    return (
        <div class="panel full-width">
            <div class="panel__header">
                <h2>Change To Be Returned</h2>
            </div>
            <div class="panel__content">
                <div class="flex flex-list">
                    <div>
                        <h4>Cash Provided</h4>
                        <ul>
                            {currencyAmountsProvided}
                        </ul>
                    </div>
                    <div>
                        <h4>Change Returned</h4>
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
