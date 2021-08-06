import { Log } from "oidc-client";

const environment = {
    urls: {
        authority: "https://localhost:44395",
        host: "https://localhost:44303"
    }
};

export const oidcConfig = {
    loginRedirectRoute: "/home",
    logoutRedirectRoute: "/home",
    unauthorizedRedirectRoute: "/home",
    logLevel: Log.DEBUG,
    userManagerSettings:{
        // number of seconds in advance of access token expiry
        // to raise the access token expiring event
        accessTokenExpiringNotificationTime: 3585,
        automaticSilentRenew: true,
        // interval in milliseconds to check the user's session
        checkSessionInterval: 10000,
        filterProtocolClaims: true,
        loadUserInfo: false,
        // number of millisecods to wait for the authorization
        // server to response to silent renew request
        silentRequestTimeout: 10000,
        silent_redirect_uri: `${environment.urls.host}/auth-callback`,
        authority: environment.urls.authority,
        client_id: "react_auth_client",
        redirect_uri: `${environment.urls.host}/auth-callback`,
        response_type: "code",
        scope: "openid email roles profile offline_access basic_scope",
        post_logout_redirect_uri: `${environment.urls.host}/signout-oidc`
    },
};

