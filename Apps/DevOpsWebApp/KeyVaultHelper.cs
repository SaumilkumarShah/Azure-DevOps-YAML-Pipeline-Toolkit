using Microsoft.Azure.KeyVault;
using Microsoft.Azure.KeyVault.Models;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using static Microsoft.Azure.KeyVault.KeyVaultClient;

namespace DevOpsWebApp
{
    public static class KeyVaultHelper
    {
		public async static Task<string> GetSecretValue(string secretIdentifier)
		{
			AuthenticationCallback keyVaultCallback = new AuthenticationCallback(GetAccessTokenAsync);
			KeyVaultClient keyVaultClient = new KeyVaultClient(keyVaultCallback);
			string keyValueSecretValue = keyVaultClient.GetSecretAsync(secretIdentifier).Result.Value;
			return keyValueSecretValue;
		}

		public static async Task<string> GetAccessTokenAsync(string authority, string resource, string scope)
		{
			var clientId = ConfigurationManager.AppSettings["ClientID"];
			var clientSecret = ConfigurationManager.AppSettings["ClientSecret"];

			var context = new AuthenticationContext(authority);
			ClientCredential credential = new ClientCredential(clientId, clientSecret);
			AuthenticationResult result = await context.AcquireTokenAsync(resource, credential);

			if (result == null)
				throw new InvalidOperationException("Failed to obtain the JWT token");

			return result.AccessToken;
		}
	}
}