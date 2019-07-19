
import React, { Component } from 'react';
import Chart from "react-apexcharts";

export class FetchData extends Component {
  static displayName = FetchData.name;

  constructor(props) {
    super(props);
      this.state = { serverlogs: [], loading: true };



    this.state = {
      serverlogs: [], loading: true,
      options: {
        chart: {
          id: "basic-bar"
        },
        xaxis: {
          categories: [1991, 1992, 1993, 1994, 1995, 1996, 1997, 1998, 1999]
        }
      },
      series: [
        {
          name: "series-1",
          data: [30, 40, 45, 50, 49, 60, 70, 91]
        }
      ]
    };


    fetch('api/serverlog')
      .then(response => response.json())
      .then(data => {
        this.setState({ serverlogs: data, loading: false });
      });
  }
  static renderServerLogsTable(serverlogs) {
    return (
      <div>
        <div className="app">
          <div className="row">
            <div className="mixed-chart">
              <Chart
                options={this.state.options}
                series={this.state.series}
                type="bar"
                width="500"
              />
            </div>
          </div>
        </div>

        <table className='table table-striped'>
          <thead>
            <tr>
              <th>serverName</th>
              <th>Date</th>
              <th>Service</th>
              <th>LineNumber</th>
              <th>Line</th>
            </tr>
          </thead>
          <tbody>
            {serverlogs.map(serverlog =>
              <tr key={serverlog.serverName}>
                <td>{serverlog.date}</td>
                <td>{serverlog.service}</td>
                <td>{serverlog.lineNumber}</td>
                <td>{serverlog.line}</td>
              </tr>
            )}
          </tbody>
        </table>
      </div>
    );
  }

  render() {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
      : FetchData.renderServerLogsTable(this.state.serverlogs);

    return (

      <div>
        <script src="https://cdn.jsdelivr.net/npm/apexcharts"></script>
        <script src="https://cdn.jsdelivr.net/npm/react-apexcharts"></script>
        <h1>Server</h1>

        {contents}
      </div>
    );
  }
}
