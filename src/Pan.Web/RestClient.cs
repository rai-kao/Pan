using System;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Pan.Web
{
    public class RestClient : IRestClient
    {
        protected readonly IHttpClientFactory HttpClientFactory;

        private Uri _host;

        public RestClient(IHttpClientFactory httpClientFactory)
        {
            HttpClientFactory = httpClientFactory;
        }

        public void Host(Uri host)
        {
            _host = host;
        }

        public void Host(string domain)
        {
            _host = new Uri(domain);
        }

        public Uri Host()
        {
            return _host;
        }

        public virtual async Task<TResponse> Get<TResponse>(string path)
        {
            return await Get<TResponse>(path, default, default, CancellationToken.None);
        }

        public virtual async Task<TResponse> Get<TResponse>(string path, object request)
        {
            return await Get<TResponse>(path, request, default, CancellationToken.None);
        }

        public virtual async Task<TResponse> Get<TResponse>(string path, object request, MethodOptions options)
        {
            return await Get<TResponse>(path, request, options, CancellationToken.None);
        }

        public virtual async Task<TResponse> Get<TResponse>(string path, object request, MethodOptions options,
            CancellationToken cancellationToken)
        {
            var response = await Get(path, request, options, cancellationToken);
            return Deserialize<TResponse>(response, options);
        }

        public virtual async Task<string> Get(string path, object request, MethodOptions options,
            CancellationToken cancellationToken)
        {
            var uri = new Uri(_host, path);
            var client = CreateClient(options);
            var response = await client.GetAsync($"{uri}{request.ToQueryString()}",
                cancellationToken == default ? CancellationToken.None : cancellationToken);
            return await response.Content.ReadAsStringAsync();
        }

        public virtual async Task<TResponse> Post<TRequest, TResponse>(string path, TRequest request)
        {
            return await Post<TRequest, TResponse>(path, request, default, CancellationToken.None);
        }

        public virtual async Task<TResponse> Post<TRequest, TResponse>(string path, TRequest request,
            MethodOptions options)
        {
            return await Post<TRequest, TResponse>(path, request, options, CancellationToken.None);
        }

        public virtual async Task<TResponse> Post<TRequest, TResponse>(string path, TRequest request,
            MethodOptions options, CancellationToken cancellationToken)
        {
            var data = await Post(path, request, options, cancellationToken);
            return Deserialize<TResponse>(data, options);
        }

        public virtual async Task<string> Post(string path, object request, MethodOptions options,
            CancellationToken cancellationToken)
        {
            var uri = new Uri(_host, path);
            var payload = Serialize(request, options);
            var client = CreateClient(options);
            var content = new StringContent(payload, Encoding.UTF8, MediaTypeNames.Application.Json);
            var response = await client.PostAsync(uri, content, cancellationToken);
            return await response.Content.ReadAsStringAsync();
        }

        public virtual async Task<bool> Put<TRequest>(string path, TRequest request)
        {
            return await Put(path, request, default, CancellationToken.None);
        }

        public virtual async Task<bool> Put<TRequest>(string path, TRequest request,
            MethodOptions options)
        {
            return await Put(path, request, options, CancellationToken.None);
        }

        public virtual async Task<bool> Put<TRequest>(string path, TRequest request,
            MethodOptions options, CancellationToken cancellationToken)
        {
            var uri = new Uri(_host, path);
            var payload = Serialize(request, options);
            var client = CreateClient(options);
            var content = new StringContent(payload, Encoding.UTF8, MediaTypeNames.Application.Json);
            var response = await client.PutAsync(uri, content, cancellationToken);
            return response.StatusCode == HttpStatusCode.OK;
        }

        public virtual async Task<bool> Delete(string path, object request)
        {
            return await Delete(path, request, default, CancellationToken.None);
        }

        public virtual async Task<bool> Delete(string path, object request, MethodOptions options)
        {
            return await Delete(path, request, options, CancellationToken.None);
        }

        public virtual async Task<bool> Delete(string path, object request, MethodOptions options,
            CancellationToken cancellationToken)
        {
            var uri = new Uri(_host, path);
            var client = CreateClient(options);
            var response = await client.DeleteAsync($"{uri}{request.ToQueryString()}", cancellationToken);
            return response.StatusCode == HttpStatusCode.OK;
        }

        private HttpClient CreateClient(MethodOptions options)
        {
            var httpClient = HttpClientFactory.CreateClient();
            return options?.Client != default ? options.Client(httpClient) : httpClient;
        }

        private string Serialize(object source, MethodOptions options)
        {
            return options?.Serializer != default ? options.Serializer(source) : JsonConvert.SerializeObject(source);
        }

        private T Deserialize<T>(string source, MethodOptions options)
        {
            if (options?.Deserializer != default) return (T) options.Deserializer(source);

            return JsonConvert.DeserializeObject<T>(source);
        }
    }
}