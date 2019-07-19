import React, { Component } from 'react';
import './default.css';

export class Home extends Component {
    static displayName = Home.name;

    constructor(props) {
        super(props);
        this.state = { servers: [], loading: true };

        fetch('api/serverlog/Servers')
            .then(response => response.json())
            .then(data => {
                this.setState({ servers: data, loading: false });
            });
    }
    static renderServerList(servers) {
        return (
            <div className="card">
                {servers.map(server =>
                    <a href={'/fetch-data/search?server=' + server.servername + '&date=' + server.date}>
                        <h2>{server.servername} <i>{server.date}</i></h2>
                        <p>{server.result}</p>
                    </a>

                )}
            </div>
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : Home.renderServerList(this.state.servers);

        return (
            <div>
                <h1>ServerMonitor</h1>
                <div className="dashboard">
                    <div className="col"></div>
                    <div className="col">
                        {contents}
                    </div>
                </div>
            </div>
        );
    }
}
