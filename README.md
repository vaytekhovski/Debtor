# Debtor

I started a long-conceived project. Here you can manage your debts and loans and also, make sure to get the money back on time...

## Local Back-end deployment

Just build and run backend part and make sure app is working well [https://localhost:7117/index.html](https://localhost:7117/index.html)

## Local Front-end deployment

Create ```.env``` file and fill with your properties.

| Field | Value |
| ------ | ------ |
| REACT_APP_AUTH0_DOMAIN | debtor.us.auth0.com |
| REACT_APP_AUTH0_CLIENT_ID | your client id key |
| REACT_APP_MY_TEST_USER_ID | your user key for local testing |


Install the dependencies and devDependencies and start the server.

```sh
cd Debtor.FrontEnd/debtor
npm i
npm run build
npm run start
```

