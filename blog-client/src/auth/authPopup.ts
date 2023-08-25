import * as msal from '@azure/msal-browser';
import { msalConfig, loginRequest, tokenRequest } from './authConfig';

const myMSALObj = new msal.PublicClientApplication(msalConfig);

let accountId = '';

async function signIn(): Promise<any> {
  const response = await myMSALObj.loginPopup(loginRequest);

  if (response !== null) {
    if (response.account) {
      const token = await getTokenPopup(tokenRequest, response.account);
      accountId = response.account.localAccountId;

      return {
        token: token?.accessToken,
        user: {
          email: response.account.username,
          firstname: token?.idTokenClaims?.given_name,
          lastname: token?.idTokenClaims?.family_name,
          identityAccountId: response.account.localAccountId,
        },
      };
    }
  }

  return null;
}

function signOut() {
  const logoutRequest = {
    postLogoutRedirectUri: msalConfig.auth.redirectUri,
    mainWindowRedirectUri: msalConfig.auth.redirectUri,
  };

  myMSALObj.logoutPopup(logoutRequest);
}

async function getTokenPopup(
  request: any,
  account?: any,
): Promise<msal.AuthenticationResult | null> {
  request.account = account ?? myMSALObj.getAccountByHomeId(accountId);

  try {
    const response = await myMSALObj.acquireTokenSilent(request);

    if (!response.idToken || response.idToken === '') {
      throw new msal.InteractionRequiredAuthError();
    }
    return response;
  } catch (error) {
    console.log('Silent token acquisition fails. Acquiring token using popup. \n', error);

    if (error instanceof msal.InteractionRequiredAuthError) {
      return myMSALObj
        .acquireTokenPopup(request)
        .then((response_1) => {
          return response_1;
        })
        .catch((error_1) => {
          console.log(error_1);
          return null;
        });
    } else {
      return null;
    }
  }
}

export { signIn, signOut };
