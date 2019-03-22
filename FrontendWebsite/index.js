let express = require('express');
let path = require('path');
let bodyParser = require('body-parser');
let app = express();

let PORT = process.env.PORT || 3000;

app.use(bodyParser.json());
app.use(bodyParser.urlencoded({ extended: true }));
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



app.listen(PORT, () => {
    console.log(`Server is listening on port ${PORT}`);
});