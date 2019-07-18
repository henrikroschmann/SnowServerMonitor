import React, { Component } from 'react';
import './default.css';

export class Home extends Component {
  static displayName = Home.name;

  render () {
    return (
      <div class="dashboard">
      <div class="col">
        <div class="card flat small">
          <h1>ServerMonitor<span class="highlight">.</span></h1>
         
        </div>
        
      </div>
      <div class="col">
        <div class="card">
          <h3>Supmaint03
              <p>Exception - LicenseLog <br/>
              Error - Compliance</p>
          </h3>
        </div>
        <div class="card">
          <h2>Supmaint01</h2>
        </div>
        <div class="card">
          <h2>Supmaint04</h2>
        </div>
      </div>
      <div class="col">
        <div class="card">
          <h2>Supmaint10</h2>
        </div>
        <div class="card">
          <h2>Supmaint14</h2>
        </div>
       
      </div>
    </div>
    );
  }
}
