using System;
using System.Threading;
using System.Threading.Tasks;

namespace Pan.Web
{
    /// <summary>
    ///     REST API client
    /// </summary>
    public interface IRestClient
    {
        void Host(Uri host);

        void Host(string domain);

        Uri Host();

        /// <summary>
        ///     HTTP GET
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="path"></param>
        /// <returns></returns>
        Task<TResponse> Get<TResponse>(string path);

        /// <summary>
        ///     HTTP GET using query string
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="path"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<TResponse> Get<TResponse>(string path, object request);

        /// <summary>
        ///     HTTP GET using custom options
        /// </summary>
        /// <param name="path"></param>
        /// <param name="request"></param>
        /// <param name="options"></param>
        /// <typeparam name="TResponse"></typeparam>
        /// <returns></returns>
        Task<TResponse> Get<TResponse>(string path, object request, MethodOptions options);

        /// <summary>
        ///     HTTP GET using custom options with cancellation token
        /// </summary>
        /// <param name="path"></param>
        /// <param name="request"></param>
        /// <param name="options"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="TResponse"></typeparam>
        /// <returns></returns>
        Task<TResponse> Get<TResponse>(string path, object request, MethodOptions options,
            CancellationToken cancellationToken);

        /// <summary>
        ///     HTTP GET using custom options with cancellation token
        /// </summary>
        /// <param name="path"></param>
        /// <param name="request"></param>
        /// <param name="options"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<string> Get(string path, object request, MethodOptions options, CancellationToken cancellationToken);

        /// <summary>
        ///     HTTP POST
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="path"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<TResponse> Post<TRequest, TResponse>(string path, TRequest request);

        /// <summary>
        ///     HTTP POST using custom options
        /// </summary>
        /// <param name="path"></param>
        /// <param name="request"></param>
        /// <param name="options"></param>
        /// <typeparam name="TRequest"></typeparam>
        /// <typeparam name="TResponse"></typeparam>
        /// <returns></returns>
        Task<TResponse> Post<TRequest, TResponse>(string path, TRequest request, MethodOptions options);

        /// <summary>
        ///     HTTP POST using custom options with cancellation token
        /// </summary>
        /// <param name="path"></param>
        /// <param name="request"></param>
        /// <param name="options"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="TRequest"></typeparam>
        /// <typeparam name="TResponse"></typeparam>
        /// <returns></returns>
        Task<TResponse> Post<TRequest, TResponse>(string path, TRequest request, MethodOptions options,
            CancellationToken cancellationToken);

        /// <summary>
        ///     HTTP POST using custom options with cancellation token
        /// </summary>
        /// <param name="path"></param>
        /// <param name="request"></param>
        /// <param name="options"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<string> Post(string path, object request, MethodOptions options, CancellationToken cancellationToken);

        /// <summary>
        ///     HTTP Put
        /// </summary>
        /// <param name="path"></param>
        /// <param name="request"></param>
        /// <typeparam name="TRequest"></typeparam>
        /// <returns></returns>
        Task<bool> Put<TRequest>(string path, TRequest request);

        /// <summary>
        ///     HTTP PUT using custom options
        /// </summary>
        /// <param name="path"></param>
        /// <param name="request"></param>
        /// <param name="options"></param>
        /// <typeparam name="TRequest"></typeparam>
        /// <returns></returns>
        Task<bool> Put<TRequest>(string path, TRequest request, MethodOptions options);

        /// <summary>
        ///     HTTP PUT using custom options with cancellation token
        /// </summary>
        /// <param name="path"></param>
        /// <param name="request"></param>
        /// <param name="options"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="TRequest"></typeparam>
        /// <returns></returns>
        Task<bool> Put<TRequest>(string path, TRequest request, MethodOptions options,
            CancellationToken cancellationToken);

        /// <summary>
        ///     HTTP Delete
        /// </summary>
        /// <param name="path"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> Delete(string path, object request);

        /// <summary>
        ///     HTTP Delete using custom options
        /// </summary>
        /// <param name="path"></param>
        /// <param name="request"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        Task<bool> Delete(string path, object request, MethodOptions options);

        /// <summary>
        ///     HTTP Delete using custom options with cancellation token
        /// </summary>
        /// <param name="path"></param>
        /// <param name="request"></param>
        /// <param name="options"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> Delete(string path, object request, MethodOptions options, CancellationToken cancellationToken);
    }
}