export const b2cPolicies = {
  names: {
    signUpSignIn: 'B2C_1_SignInSignUp',
    forgotPassword: 'B2C_1_Reset',
  },

  authorities: {
    signUpSignIn: {
      authority: 'https://msheggar.b2clogin.com/msheggar.onmicrosoft.com/B2C_1_SignInSignUp',
    },
    forgotPassword: {
      authority: 'https://msheggar.b2clogin.com/msheggar.onmicrosoft.com/B2C_1_Reset',
    },
  },

  authorityDomain: 'msheggar.b2clogin.com',
};
