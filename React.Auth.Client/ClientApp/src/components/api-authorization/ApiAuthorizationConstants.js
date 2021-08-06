

export const ApplicationName = 'react_auth_client';

export const QueryParameterNames = {
  ReturnUrl: 'returnUrl',
  Message: 'message'
};

export const LogoutActions = {
  LogoutCallback: 'logout-callback',
  Logout: 'logout',
  LoggedOut: 'logged-out'
};

export const LoginActions = {
  Login: 'login',
  LoginCallback: 'login-callback',
  LoginFailed: 'login-failed',
  Profile: 'profile',
  Register: 'register'
};

const clientPrefix = '/authentication';
const authorityPrefix = '/connect';

export const ApplicationPaths = {
    DefaultLoginRedirectPath: '/',
    //ApiAuthorizationClientConfigurationUrl: `_configuration/${ApplicationName}`, // used by IdentyServer to generate on the fly the configuration parameters
    ApiAuthorizationPrefix: clientPrefix,
    Login: `${clientPrefix}/${LoginActions.Login}`,
    LoginFailed: `${clientPrefix}/${LoginActions.LoginFailed}`,
    LoginCallback: `${clientPrefix}/${LoginActions.LoginCallback}`,
    Register: `${clientPrefix}/${LoginActions.Register}`,
    Profile: `${clientPrefix}/${LoginActions.Profile}`,
    LogOut: `${clientPrefix}/${LogoutActions.Logout}`,
    LoggedOut: `${clientPrefix}/${LogoutActions.LoggedOut}`,
    LogOutCallback: `${clientPrefix}/${LogoutActions.LogoutCallback}`,
    IdentityRegisterPath: 'Identity/Account/Register',
    IdentityManagePath: 'Identity/Account/Manage'
};
