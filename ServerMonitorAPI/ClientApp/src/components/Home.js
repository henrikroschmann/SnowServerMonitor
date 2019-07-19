import React, { Component } from 'react';
import './default.css';
import Moment from 'react-moment';
import 'moment-timezone';
import { ServerResponse } from 'http';

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
                        <h2>{server.servername} <i><Moment format="YYYY-MM-DD">{server.date}</Moment></i></h2>
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
