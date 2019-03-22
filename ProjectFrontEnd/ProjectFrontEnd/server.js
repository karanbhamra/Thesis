var express = require('express');
var bodyParser = require('body-parser');
var path = require('path');
var app = express();

let port = process.env.PORT || 1337;

app.use(bodyParser.json());
app.use(bodyParser.urlencoded({ extended: false }));
app.use(express.static(path.join(__dirname, 'public')));


app.get('/', (request, response) => {
    response.sendFile(path.join(__dirname, 'public', 'index.html'));

});

app.post('/submit', (request, response) => {
    response.send(`Your file will be here for`);

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