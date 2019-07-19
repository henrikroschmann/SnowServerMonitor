
import React, { Component } from 'react';
import Moment from 'react-moment';
import 'moment-timezone';

export class FetchData extends Component {
  static displayName = FetchData.name;

  constructor(props) {
    super(props);
    this.state = { serverlogs: [], loading: true };

    fetch('api/serverlog/search' + this.props.location.search)
      .then(response => response.json())
      .then(data => {
        this.setState({ serverlogs: data, loading: false });
      });
  }
  static renderServerLogsTable(serverlogs) {
    return (

      <table className='table table-striped'>
        <thead>
          <tr>
            <th>Date</th>
            <th>Service</th>
            <th>LineNumber</th>
            <th>Line</th>
          </tr>
        </thead>
        <tbody>
          {serverlogs.map(serverlog =>
            <tr key={serverlog.serverName}>
              <td><Moment format="YYYY-MM-DD">{serverlog.date}</Moment></td>
              <td>{serverlog.service}</td>
              <td>{serverlog.lineNumber}</td>
              <td>{serverlog.line}</td>
            </tr>
          )}
        </tbody>
      </table>
    );
  }

  render() {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
      : FetchData.renderServerLogsTable(this.state.serverlogs);
    return (
      <div>
        <h1>ServerMonitor</h1>

        {contents}
      </div>
    );
  }
}
