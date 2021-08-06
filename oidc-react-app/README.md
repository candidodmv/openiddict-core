This project add the implementation of [Oidc-client](https://github.com/IdentityModel/oidc-client-js) to [Create React App](https://github.com/facebook/create-react-app).

## Installation

You have to configure this variables IDENTITY_CONFIG and METADATA_OIDC and the environment variables inside of the env.development and .evn.production

After that run

### `npm start`

or

### `npm run build`

### Test

`fetch("https://localhost:44360/ApiContent/Private", {mode: 'cors', headers: {
    'Authorization': 'Bearer {TOKEN',
}}).then(r => r.text()).then(t => console.log(t))`
