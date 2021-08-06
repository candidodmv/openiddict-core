using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Mvc.Server.Models;
using OpenIddict.Abstractions;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace Mvc.Server
{
    public class Worker : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;

        public Worker(IServiceProvider serviceProvider)
            => _serviceProvider = serviceProvider;

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            await context.Database.EnsureCreatedAsync(cancellationToken);

            await RegisterApplicationsAsync(scope.ServiceProvider);
            await RegisterScopesAsync(scope.ServiceProvider);

            static async Task RegisterApplicationsAsync(IServiceProvider provider)
            {
                var applicationManager = provider.GetRequiredService<IOpenIddictApplicationManager>();

                if (await applicationManager.FindByClientIdAsync("react_auth_client2") is null)
                {
                    await applicationManager.CreateAsync(new OpenIddictApplicationDescriptor
                    {
                        ClientId = "react_auth_client2",
                        //ClientSecret = "EB0CEDE8-8F7A-4C8C-AE03-34525E174C21",
                        //ConsentType = ConsentTypes.Explicit,
                        DisplayName = "SPA React client application 2",
                        PostLogoutRedirectUris =
                        {
                            new Uri($"{SharedLibrary.Constants.SpaClientAddress2}/logout/callback")
                        },
                        Type = ClientTypes.Public,
                        RedirectUris =
                        {
                            new Uri($"{SharedLibrary.Constants.SpaClientAddress2}/signin-oidc"),
                            new Uri($"{SharedLibrary.Constants.SpaClientAddress2}/silentrenew"),
                        },
                        Permissions =
                        {
                            Permissions.Endpoints.Authorization,
                            Permissions.Endpoints.Logout,
                            Permissions.Endpoints.Token,
                            Permissions.GrantTypes.AuthorizationCode,
                            Permissions.GrantTypes.RefreshToken,
                            Permissions.GrantTypes.Implicit,
                            Permissions.ResponseTypes.Code,
                            Permissions.ResponseTypes.IdToken,
                            Permissions.ResponseTypes.IdTokenToken,
                            Permissions.ResponseTypes.Token,
                            Permissions.Scopes.Email,
                            Permissions.Scopes.Profile,
                            Permissions.Scopes.Roles,
                            Permissions.Prefixes.Scope + "basic_scope"
                        },
                        Requirements =
                        {
                            Requirements.Features.ProofKeyForCodeExchange
                        }
                    });
                }

                if (await applicationManager.FindByClientIdAsync("react_auth_client") is null)
                {
                    await applicationManager.CreateAsync(new OpenIddictApplicationDescriptor
                    {
                        ClientId = "react_auth_client",
                        //ClientSecret = "EB0CEDE8-8F7A-4C8C-AE03-34525E174C21",
                        //ConsentType = ConsentTypes.Explicit,
                        DisplayName = "SPA React client application",
                        PostLogoutRedirectUris =
                        {
                            new Uri($"{SharedLibrary.Constants.SpaClientAddress}/signout-callback-oidc")
                        },
                        Type = ClientTypes.Public,
                        RedirectUris =
                        {
                            new Uri($"{SharedLibrary.Constants.SpaClientAddress}/signin-oidc"),
                            new Uri($"{SharedLibrary.Constants.SpaClientAddress}/auth-callback"),


                        },
                        Permissions =
                        {
                            Permissions.Endpoints.Authorization,
                            Permissions.Endpoints.Logout,
                            Permissions.Endpoints.Token,
                            Permissions.GrantTypes.AuthorizationCode,
                            Permissions.GrantTypes.RefreshToken,
                            Permissions.GrantTypes.Implicit,
                            Permissions.ResponseTypes.Code,
                            Permissions.ResponseTypes.IdToken,
                            Permissions.ResponseTypes.IdTokenToken,
                            Permissions.ResponseTypes.Token,
                            Permissions.Scopes.Email,
                            Permissions.Scopes.Profile,
                            Permissions.Scopes.Roles,
                            Permissions.Prefixes.Scope + "basic_scope"
                        },
                        Requirements =
                        {
                            Requirements.Features.ProofKeyForCodeExchange
                        }
                    });
                }

                if (await applicationManager.FindByClientIdAsync("api_client") is null)
                {
                    await applicationManager.CreateAsync(new OpenIddictApplicationDescriptor
                    {
                        ClientId = "api_client",
                        ClientSecret = "6AC7EB4F-3BB9-45BA-BCDF-DAD9299501E5",
                        //ConsentType = ConsentTypes.Implicit,
                        //DisplayName = "Demo API client application",
                        //PostLogoutRedirectUris =
                        //{
                        //    new Uri("https://localhost:44381/signout-callback-oidc")
                        //},
                        //RedirectUris =
                        //{
                        //    new Uri("https://localhost:44381/signin-oidc")
                        //},
                        Permissions =
                        {
                            //Permissions.Endpoints.Authorization,
                            //Permissions.Endpoints.Logout,
                            //Permissions.Endpoints.Token,
                            //Permissions.GrantTypes.AuthorizationCode,
                            //Permissions.GrantTypes.RefreshToken,
                            //Permissions.ResponseTypes.Code,
                            //Permissions.Scopes.Email,
                            //Permissions.Scopes.Profile,
                            //Permissions.Scopes.Roles
                            Permissions.Endpoints.Introspection
                        }
                        //,Requirements =
                        //{
                        //    Requirements.Features.ProofKeyForCodeExchange
                        //}
                    });
                }

                if (await applicationManager.FindByClientIdAsync("mvc") is null)
                {
                    await applicationManager.CreateAsync(new OpenIddictApplicationDescriptor
                    {
                        ClientId = "mvc",
                        ClientSecret = "901564A5-E7FE-42CB-B10D-61EF6A8F3654",
                        ConsentType = ConsentTypes.Explicit,
                        DisplayName = "MVC client application",
                        DisplayNames =
                        {
                            [CultureInfo.GetCultureInfo("fr-FR")] = "Application cliente MVC"
                        },
                        PostLogoutRedirectUris =
                        {
                            new Uri("https://localhost:44381/signout-callback-oidc")
                        },
                        RedirectUris =
                        {
                            new Uri("https://localhost:44381/signin-oidc")
                        },
                        Permissions =
                        {
                            Permissions.Endpoints.Authorization,
                            Permissions.Endpoints.Logout,
                            Permissions.Endpoints.Token,
                            Permissions.GrantTypes.AuthorizationCode,
                            Permissions.GrantTypes.RefreshToken,
                            Permissions.ResponseTypes.Code,
                            Permissions.Scopes.Email,
                            Permissions.Scopes.Profile,
                            Permissions.Scopes.Roles,
                            Permissions.Prefixes.Scope + "demo_api"
                        },
                        Requirements =
                        {
                            Requirements.Features.ProofKeyForCodeExchange
                        }
                    });
                }

                // To test this sample with Postman, use the following settings:
                //
                // * Authorization URL: https://localhost:44395/connect/authorize
                // * Access token URL: https://localhost:44395/connect/token
                // * Client ID: postman
                // * Client secret: [blank] (not used with public clients)
                // * Scope: openid email profile roles
                // * Grant type: authorization code
                // * Request access token locally: yes
                if (await applicationManager.FindByClientIdAsync("postman") is null)
                {
                    await applicationManager.CreateAsync(new OpenIddictApplicationDescriptor
                    {
                        ClientId = "postman",
                        ConsentType = ConsentTypes.Systematic,
                        DisplayName = "Postman",
                        RedirectUris =
                        {
                            new Uri("urn:postman")
                        },
                        Permissions =
                        {
                            Permissions.Endpoints.Authorization,
                            Permissions.Endpoints.Device,
                            Permissions.Endpoints.Token,
                            Permissions.GrantTypes.AuthorizationCode,
                            Permissions.GrantTypes.DeviceCode,
                            Permissions.GrantTypes.Password,
                            Permissions.GrantTypes.RefreshToken,
                            Permissions.ResponseTypes.Code,
                            Permissions.Scopes.Email,
                            Permissions.Scopes.Profile,
                            Permissions.Scopes.Roles
                        }
                    });
                }
            }

            static async Task RegisterScopesAsync(IServiceProvider provider)
            {
                var scopeManager = provider.GetRequiredService<IOpenIddictScopeManager>();

                if (await scopeManager.FindByNameAsync("demo_api") is null)
                {
                    await scopeManager.CreateAsync(new OpenIddictScopeDescriptor
                    {
                        DisplayName = "Demo API access",
                        DisplayNames =
                        {
                            [CultureInfo.GetCultureInfo("fr-FR")] = "Accès à l'API de démo"
                        },
                        Name = "demo_api",
                        Resources =
                        {
                            "resource_server"
                        }
                    });
                }

                if (await scopeManager.FindByNameAsync("basic_scope") is null)
                {
                    await scopeManager.CreateAsync(new OpenIddictScopeDescriptor
                    {
                        DisplayName = "Escopo básico de utilização da aplicação",
                        Name = "basic_scope",
                        Resources =
                        {
                            "api_client"
                        }
                    });
                }
            }
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
