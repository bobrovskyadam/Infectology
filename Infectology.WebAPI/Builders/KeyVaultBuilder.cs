using Azure.Identity;

namespace Infectology.WebAPI.Builders;

public static class KeyVaultBuilder
{
    public static WebApplicationBuilder AddAzureKeyVault(this WebApplicationBuilder builder)
    {
        var vaultUri = builder.Configuration["KeyVault:Uri"];

        if (!string.IsNullOrWhiteSpace(vaultUri) && Uri.TryCreate(vaultUri, UriKind.Absolute, out var keyVaultUri))
        {
            var tenantId = builder.Configuration["KeyVault:TenantId"];
            var clientId = builder.Configuration["KeyVault:ClientId"];
            var clientSecret = builder.Configuration["KeyVault:ClientSecret"];

            if (!string.IsNullOrWhiteSpace(tenantId) && !string.IsNullOrWhiteSpace(clientId) && !string.IsNullOrWhiteSpace(clientSecret))
            {
                var credential = new ClientSecretCredential(tenantId, clientId, clientSecret);
                builder.Configuration.AddAzureKeyVault(keyVaultUri, credential);
            }
            else
            {
                Console.WriteLine("Azure Key Vault credentials (TenantId/ClientId/ClientSecret) are not fully configured.");
            }
        }
        else
        {
            Console.WriteLine("KeyVault:Uri is not configured or invalid.");
        }

        return builder;
    }
}