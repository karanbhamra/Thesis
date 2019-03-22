let express = require('express');
let path = require('path');
let bodyParser = require('body-parser');
let app = express();

let PORT = process.env.PORT || 3000;

app.use(bodyParser.json());
app.use(bodyParser.urlencoded({ extended: true }));
app.use(express.static(path.join(__dirname, 'public')));

app.get('/', (request, response) => {
    response.send('Welcome to the main website.');

});

app.get('*', (request, response) => {
    response.send('Not found');
});


app.listen(PORT, () => {
    console.log(`Server is listening on port ${PORT}`);
});