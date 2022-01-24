import React, { Component } from 'react';
import { Container } from 'reactstrap';

export class Layout extends Component {
  static displayName = Layout.name;

  render () {
    return (
      <div>
        <header className="navbar-expand-sm navbar-toggleable-sm ng-white border-bottom box-shadow mb-3">
          <h1 id="tabelLabel" >Cash Register App</h1>
        </header>
        <Container>
          {this.props.children}
        </Container>
      </div>
    );
  }
}
