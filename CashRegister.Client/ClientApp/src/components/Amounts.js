import React, { Component } from 'react';
import denominationDisplayName from '../helpers/denominationDisplayName';

export class Amounts extends Component {
static displayName = Amounts.name;

  constructor(props) {
    super(props);
    this.state = {
        amounts: this.props.amounts,
        loading: true
    };
  }

  render() {
    return (
        <div className="panel">
            <div className="panel__header">
                <h2>Current Amounts</h2>
            </div>
            <div className="panel__content">
                <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>Denomination</th>
                        <th>Amount</th>
                    </tr>
                </thead>
                <tbody>
                {this.props.amounts.map(amount =>
                    <tr key={amount.denomination}>
                        <td><b>{denominationDisplayName(amount.denomination)}:</b></td>
                        <td key={amount.denomination}>{amount.amount}</td>
                    </tr>
                )}
                </tbody>
                </table>
            </div>
        </div>
    );
  }
}
