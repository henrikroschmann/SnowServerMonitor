import React, { Component } from 'react';
import './default.css';

export class Home extends Component {
    static displayName = Home.name;

    constructor(props) {
        super(props);
        this.state = { servers: [], loading: true };

        fetch('api/serverlog')
            .then(response => response.json())
            .then(data => {
                this.setState({ servers: data, loading: false });
            });
    }
    static renderServerList(servers) {
        return (
            <div className="dashboard">
                <div className="col">
                    <div class="card flat small">
                        <h1>ServerMonitor<span className="highlight">.</span></h1>

                    </div>

                </div>
                <div className="col">
                    
                    <div className="card">
                        
                        {servers.map(server =>
                             server.warnings  < 0 ?
                                <h2>{server.name}   </h2> :
                                <h3>{server.name} </h3>                                                      
                        )}

                    </div>
                </div>
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
                
                {contents}
            </div>
        );
    }
}
