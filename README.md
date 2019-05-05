TODO: 

- ~~Working on: function which will save receive the basic student Json object and store it in cosmosdb full student record~~
    - ~~Create method to get count of how many items are there~~
        - ~~If zero, then its genesis block and create the initial hash for genesis block~~
    - ~~Create method to get the last student record added~~
    - ~~Map basic student object to full student object~~
    - ~~Create method which will hash the basic student object (maybe the Json object string format)~~
    - ~~Hashing will use an established algorithm such as SHA-512 or the newer SHA3 algorithm rather than hand rolling my own~~
- ~~Create the initial function which will take the submitted name from the client side and call the function which will retrieve the user record and respond back with the object containing the basic student info~~
- ~~**Extra Work** generate a pdf server side and send back a base64 encoded string which can be used client side to download the pdf~~
- **Extra Work**Create Azure API gateway endpoints which I can call instead of using the function http url
- ~~Create the end user website and host it~~
    - ~~Display received pdf~~
- Write unit tests
- Finish the debug student tool used to test the flow/ something administration might use
  - Add ability to generate records, view records, add records, delete records, verify records
- Deploy to live Azure resources