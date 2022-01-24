import React, { Component } from 'react';

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
        <div class="panel">
            <div class="panel__header">
                <h2>Current Amounts</h2>
            </div>
            <div class="panel__content">
                <table className='table' aria-labelledby="tabelLabel">
                    <thead>
                        <tr>
                            <th>Denomination:</th>
                            {this.props.amounts.map(amount =>
                                <th key={amount.denomination}>{amount.denomination}</th>
                            )}
                        </tr>
                    </thead>
                    <tbody>
                        <tr> 
                        <td><b>Quantity:</b></td>
                        {this.props.amounts.map(amount =>
                            <td key={amount.denomination}>{amount.amount}</td>
                        )}
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    );
  }
}
