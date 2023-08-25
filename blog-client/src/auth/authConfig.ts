import * as msal from '@azure/msal-browser';
import { b2cPolicies } from './policies';
import { apiConfig } from './apiConfig';

const msalConfig = {
  auth: {
    clientId: '505dae46-d10f-470d-b9ec-5730abc7dc20',
    authority: b2cPolicies.authorities.signUpSignIn.authority,
    knownAuthorities: [b2cPolicies.authorityDomain],
    redirectUri: 'https://localhost:5173',
  },
  cache: {
    cacheLocation: 'sessionStorage',
    storeAuthStateInCookie: false,
  },
  system: {
    loggerOptions: {
      loggerCallback: (level: any, message: any, containsPii: any) => {
        if (containsPii) {
          return;
        }
        switch (level) {
          case msal.LogLevel.Error:
            console.error(message);
            return;
          case msal.LogLevel.Info:
            console.info(message);
            return;
          case msal.LogLevel.Verbose:
            console.debug(message);
            return;
          case msal.LogLevel.Warning:
            console.warn(message);
            return;
        }
      },
    },
  },
};

const loginRequest = {
  scopes: [...apiConfig.scopes],
};

const tokenRequest = {
  scopes: [...apiConfig.scopes],
  forceRefresh: false,
  secret: '6LY8Q~mnLZQUAYMQZdZg1qR6kNgIWt9._XCJ0bz~',
};

export { msalConfig, loginRequest, tokenRequest };
