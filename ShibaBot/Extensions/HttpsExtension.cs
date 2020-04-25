using System.Net;
using System.Threading.Tasks;

namespace ShibaBot.Extensions {
    internal class HttpsExtension {
        internal async Task<bool> IsImageAsync(string url) {
            try {
                WebRequest request = WebRequest.Create(url);
                request.Method = "HEAD";
                using WebResponse response = await request.GetResponseAsync();
                return response.ContentType.ToLowerInvariant().StartsWith("image/");
            }
            catch {
                return false;
            }
        }
    }
}
