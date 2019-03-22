var express = require('express');
var bodyParser = require('body-parser');
var path = require('path');
var app = express();

let port = process.env.PORT || 1337;

app.use(bodyParser.json());
app.use(bodyParser.urlencoded({ extended: true }));
app.use(express.static(path.join(__dirname, 'public')));


app.get('/', (request, response) => {
    response.sendFile(path.join(__dirname, 'public', 'index.html'));

});

app.post('/submit', (request, response) => {

    let student =
    {
        FirstName: request.body.firstName,
        MiddleName: request.body.middleName,
        LastName: request.body.lastName
    };
    let data = JSON.stringify(student);
    let url = 'http://localhost:7071/api/GetPdf';
    console.log(`Make a request to ${url} with ${data}`);

    var http = require("http");
    var options = {
        hostname: 'localhost',
        port: 7071,
        path: '/api/GetPdf',
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        }
    };
    var req = http.request(options, function (res) {
        console.log('Status: ' + res.statusCode);
        console.log('Headers: ' + JSON.stringify(res.headers));
        res.setEncoding('utf8');
        res.on('data', function (body) {
            console.log('Body: ' + body);
            let mydata = JSON.parse(body);
            let baseString = mydata.Pdf;
            var buf = Buffer.from(baseString, 'base64'); // Ta-da

            response.setHeader('Content-Type', 'application/pdf');
            response.send(buf);

            //response.send(mydata.Pdf);
        });
    });
    req.on('error', function (e) {
        console.log('problem with request: ' + e.message);
    });
    // write data to request body
    req.write(data);
    req.end();

});



app.get('*', (request, response) => {
    response.sendFile(path.join(__dirname, 'public', '404.html'));

});

app.post('*', (request, response) => {
    response.sendFile(path.join(__dirname, 'public', '404.html'));

});


app.listen(port, () => {
    console.log(`Server is listening on port ${port}`);
});