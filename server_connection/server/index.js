const express = require('express');
//const http = require('http');

const logger = require("morgan") //logging events upon connection
const path = require("path");
const websockets = require("./websockets.js"); //everything connected to websockets

//all config parameters

const CONFIG = require('../config.json');
const port = process.env.PORT || parseInt(CONFIG.PortServer);
const localIP = CONFIG.LocalIP;
const host = CONFIG.HostIP;
const portWs = CONFIG.PortWs;

//create the server with express
const app = new express;
//const server = http.createServer(app);

app.engine('html', require('ejs').renderFile);
app.set('view engine', 'html');
app.use(logger("short"))
app.get('/', (req, res) => {
    res.render(path.join(__dirname,  '..',  'app',  'index.html'), {host:host, port:portWs});
});
app.use(express.static(path.join(__dirname, '..', 'app'))); // This sends all static files in app upon request

websockets.Start(port)

//start the server listening on local IP

server.listen(port, () => {
    console.log('listening on http://' + localIP + ':' + port);
    console.log('Go to http://' + host + ':' + port);
});
