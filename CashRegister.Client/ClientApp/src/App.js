import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { CashRegister } from './components/CashRegister';

import './custom.css'
import 'react-toastify/dist/ReactToastify.css';

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
        <Route exact path='/' component={CashRegister} />
      </Layout>
    );
  }
}
