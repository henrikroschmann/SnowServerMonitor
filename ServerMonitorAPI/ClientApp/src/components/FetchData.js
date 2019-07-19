
import React, { Component } from 'react';
import Chart from "react-apexcharts";

export class FetchData extends Component {
  static displayName = FetchData.name;

  constructor(props) {
    super(props);

    const newDataSeries = [];

    this.state = {
      serverlogs: [],
      loading: true,
      chartloading: true,
      options: {
        chart: {
          id: 'DUJ Times last 5'
        },
        xaxis: {
          categories: [1, 2, 3, 4, 5]
        }
      },
      series: [{
        name: 'series-1',
        data: [0, 0, 0, 0, 0]
      }]
    };

    fetch('/api/serverlog/search' + this.props.location.search)
      .then(response => response.json())
      .then(data => {
        this.setState({ serverlogs: data, loading: false });
      });

    fetch('/api/serverlog/getChart' + this.props.location.search)
      .then(response => response.json())
      .then(data => {
        this.state.series.map((s) => {
          s.data.map(() => {
            return data
          })
          newDataSeries.push({ data, name: s.name })

        })
        this.setState({ series: newDataSeries, chartloading: false });
      });

  }

  static renderServerLogsTable(serverlogs) {
    return (
      <div>

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

  static renderChart(options, series) {
    return (
      <div className="app">
        <div className="row">
          <div className="mixed-chart">
            <Chart
              options={options}
              series={series}
              type="line"
              width="500"
            />
          </div>
        </div>
      </div>
    );
  }

  render() {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
      : FetchData.renderServerLogsTable(this.state.serverlogs);
    let chart = this.state.chartloading
      ? <p><em>Loading Chart...</em></p>
      : FetchData.renderChart(this.state.options, this.state.series);

    return (
      <div>
        <script src="https://cdn.jsdelivr.net/npm/apexcharts"></script>
        <script src="https://cdn.jsdelivr.net/npm/react-apexcharts"></script>
        <h1>ServerMonitor</h1>
        {chart}
        {contents}
      </div>
    );
  }
}