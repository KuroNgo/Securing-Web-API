const express = require('express');
const cors = require('cors')
const app = express();

app.use(cors());

app.use('/api/1.0/User/Login', (req, res) => {
  res.send({
    token: 'test123'
  });
});

app.listen(44358, () =>
 console.log('API is running on https://localhost:44358/api/1.0/User/Login'));