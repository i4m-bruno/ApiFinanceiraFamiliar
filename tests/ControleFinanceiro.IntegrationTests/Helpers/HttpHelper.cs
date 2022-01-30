using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ControleFinanceiro.IntegrationTests.Helpers;

public static class HttpHelper
{
    public static async Task<T> DeserializeContent<T>(this HttpResponseMessage response)
    {
        return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
    }

    public static StringContent GetStringContent<T>(this T obj)
    {
        return new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
    }
}
