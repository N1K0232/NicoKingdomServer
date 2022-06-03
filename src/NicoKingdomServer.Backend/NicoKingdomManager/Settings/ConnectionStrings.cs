using System.Text;

namespace NicoKingdomManager.Settings;

public sealed class ConnectionStrings
{
    private readonly Dictionary<string, string> connectionStrings = new();

    public ConnectionStrings(IConfiguration configuration)
    {
        SetString("AzureConnection", configuration.GetConnectionString("AzureConnection"));
        SetString("SqlConnection", configuration.GetConnectionString("SqlConnection"));
    }

    public string AzureConnection => GetString(nameof(AzureConnection));
    public string SqlConnection => GetString(nameof(SqlConnection));

    private string GetString(string key)
    {
        string hash = connectionStrings[key];
        byte[] bytes = Convert.FromBase64String(hash);
        return Encoding.UTF8.GetString(bytes);
    }
    private void SetString(string key, string value)
    {
        if (string.IsNullOrEmpty(key))
        {
            throw new ArgumentNullException(nameof(key), "you must specify the key");
        }
        if (string.IsNullOrEmpty(value))
        {
            throw new ArgumentNullException(nameof(value), "you must specify the connection string");
        }

        connectionStrings.Add(key, value);
    }
}