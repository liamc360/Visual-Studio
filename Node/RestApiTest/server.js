//'use strict';

var express = require('express');        // call express
var app = express();                 // define our app using express
var bodyParser = require('body-parser');
const port = 25565;        // set our port
var requestTypes = ["/api/testget", "/api/testpost", "/api/getfile", "api/playsound"];

process.chdir('E:\\RestAPI\\');

app.use(function (req, res, next) {
    var ip = req.connection.remoteAddress
    console.log(ip.split(":").pop());
    next();
});

app.get('/', function (req, res) {
    var responseString = "Help = <br>";
    for (var i = 0; i < requestTypes.length; i++) {
        responseString += requestTypes[i] + '<br>';
    }
});

app.get('/api/getfile/:file', function (req, res) {
    var file = req.params.file;
    res.download(file);
});

app.get('/api/testget', function (req, res) {
    res.send('testget goes here');
});

app.get('/sound', function (req, res) {

    var player = require('play-sound')({ player: "mpv" });
    player.play('new.flac', function (err) {
        res.send('neve is a loser');
    });
});

app.get('/neve', function (req, res) {

    var player = require('play-sound')({ player: "mpv" });
    player.play('neve.mp3', function (err) {
        res.send('neve is a loser');
    });
});

app.listen(port);
console.log('Listening on ' + port);

