using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Http.Json;
using Newtonsoft.Json;

namespace Web.Backend
{
    public class ApiCaller
    {
        public bool UseHttpMethodOverride { get; set; }
        public HttpClient httpClient { get; set; }

        public async Task<TValue?> GetFromJsonAsync<TValue>([StringSyntax(StringSyntaxAttribute.Uri)] string? requestUri, CancellationToken cancellationToken = default)
        {
            if (UseHttpMethodOverride && httpClient.DefaultRequestHeaders.Any(e => e.Key == "X-HTTP-Method-Override"))
                httpClient.DefaultRequestHeaders.Remove("X-HTTP-Method-Override");
            //return await httpClient.GetFromJsonAsync<TValue>(requestUri, cancellationToken);
            var response = await httpClient.GetAsync(requestUri);
            if (response.IsSuccessStatusCode)
            {
                var stringData = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<TValue>(stringData);
                return result;
            }
            else
            {
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                    throw new UnauthorizedAccessException();
                return default(TValue);
            }
        }

        public async Task<HttpResponseMessage> PostAsJsonAsync<TValue>([StringSyntax(StringSyntaxAttribute.Uri)] string? requestUri, TValue value, CancellationToken cancellationToken = default)
        {
            if (UseHttpMethodOverride && httpClient.DefaultRequestHeaders.Any(e => e.Key == "X-HTTP-Method-Override"))
                httpClient.DefaultRequestHeaders.Remove("X-HTTP-Method-Override");
            return await httpClient.PostAsJsonAsync(requestUri, value, cancellationToken);
        }

        //public async Task<HttpResponseMessage> PostAsJsonAsync<TValue>([StringSyntax(StringSyntaxAttribute.Uri)] string? requestUri, TValue value, JsonSerializerOptions? options = null, CancellationToken cancellationToken = default)
        //{
        //    JsonContent content = JsonContent.Create(value, mediaType: null, options);
        //    return await httpClient.PostAsync(requestUri, content, cancellationToken);
        //}
        public async Task<HttpResponseMessage> PostAsync([StringSyntax(StringSyntaxAttribute.Uri)] string? requestUri, HttpContent? content, CancellationToken cancellationToken = default)
        {
            if (UseHttpMethodOverride && httpClient.DefaultRequestHeaders.Any(e => e.Key == "X-HTTP-Method-Override"))
                httpClient.DefaultRequestHeaders.Remove("X-HTTP-Method-Override");
            return await httpClient.PostAsync(requestUri, content, cancellationToken);
        }

        //public async Task<HttpResponseMessage> PutAsJsonAsync<TValue>([StringSyntax(StringSyntaxAttribute.Uri)] string? requestUri, TValue value, JsonSerializerOptions? options = null, CancellationToken cancellationToken = default)
        //{
        //    if (UseHttpMethodOverride)
        //    {
        //        httpClient.DefaultRequestHeaders.Add("X-HTTP-Method-Override", "PUT");
        //        var result = await httpClient.PostAsJsonAsync(requestUri, value, options, cancellationToken);
        //        httpClient.DefaultRequestHeaders.Remove("X-HTTP-Method-Override");
        //        return result;
        //    }
        //    else
        //    {
        //        JsonContent content = JsonContent.Create(value, mediaType: null, options);
        //        return await httpClient.PutAsJsonAsync(requestUri, content, cancellationToken);
        //    }
        //}

        public async Task<HttpResponseMessage> PutAsJsonAsync<TValue>([StringSyntax(StringSyntaxAttribute.Uri)] string? requestUri, TValue value, CancellationToken cancellationToken = default)
        {
            if (UseHttpMethodOverride)
            {
                if (httpClient.DefaultRequestHeaders.Any(e => e.Key == "X-HTTP-Method-Override"))
                    httpClient.DefaultRequestHeaders.Remove("X-HTTP-Method-Override");
                httpClient.DefaultRequestHeaders.Add("X-HTTP-Method-Override", "PUT");
                var result = await httpClient.PostAsJsonAsync(requestUri, value, cancellationToken);
                httpClient.DefaultRequestHeaders.Remove("X-HTTP-Method-Override");
                return result;
            }
            else
            {
                JsonContent content = JsonContent.Create(value, mediaType: null);
                return await httpClient.PutAsJsonAsync(requestUri, content, cancellationToken);
            }
        }

        public async Task<HttpResponseMessage> PutAsync([StringSyntax(StringSyntaxAttribute.Uri)] string? requestUri, HttpContent? content)
        {
            if (UseHttpMethodOverride)
            {
                if (httpClient.DefaultRequestHeaders.Any(e => e.Key == "X-HTTP-Method-Override"))
                    httpClient.DefaultRequestHeaders.Remove("X-HTTP-Method-Override");
                httpClient.DefaultRequestHeaders.Add("X-HTTP-Method-Override", "PUT");
                var result = await httpClient.PostAsync(requestUri, content);
                httpClient.DefaultRequestHeaders.Remove("X-HTTP-Method-Override");
                return result;
            }   
            else return await httpClient.PutAsync(requestUri, content);
        }

        public async Task<HttpResponseMessage> DeleteAsync([StringSyntax(StringSyntaxAttribute.Uri)] string? requestUri, CancellationToken cancellationToken = default)
        {
            if (UseHttpMethodOverride)
            {
                if (httpClient.DefaultRequestHeaders.Any(e => e.Key == "X-HTTP-Method-Override"))
                    httpClient.DefaultRequestHeaders.Remove("X-HTTP-Method-Override");
                httpClient.DefaultRequestHeaders.Add("X-HTTP-Method-Override", "DELETE");
                var result = await httpClient.PostAsync(requestUri, null, cancellationToken);
                httpClient.DefaultRequestHeaders.Remove("X-HTTP-Method-Override");
                return result;
            }
            else return await httpClient.DeleteAsync(requestUri, cancellationToken);
        }
    }
}
