using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RestOn.Http
{
    public class HttpClientDecorator : IHttpClientDecorator
    {
        private readonly HttpClient client;

        public HttpClientDecorator()
        {
            this.client = new HttpClient();
        }

        //
        // Resumo:
        //     Gets or sets the global Http proxy.
        //
        // Devoluções:
        //     A proxy used by every call that instantiates a System.Net.HttpWebRequest.
        //
        // Exceções:
        //   T:System.ArgumentNullException:
        //     The value passed cannot be null.
        public static IWebProxy DefaultProxy
        {
            get => HttpClient.DefaultProxy;
            set => HttpClient.DefaultProxy = value;
        }

        //
        // Resumo:
        //     Gets or sets the default HTTP version used on subsequent requests made by this
        //     System.Net.Http.HttpClient instance.
        //
        // Devoluções:
        //     The default version to use for any requests made with this System.Net.Http.HttpClient
        //     instance.
        //
        // Exceções:
        //   T:System.ArgumentNullException:
        //     In a set operation, DefaultRequestVersion is null.
        //
        //   T:System.InvalidOperationException:
        //     The System.Net.Http.HttpClient instance has already started one or more requests.
        //
        //   T:System.ObjectDisposedException:
        //     The System.Net.Http.HttpClient instance has already been disposed.
        public Version DefaultRequestVersion
        {
            get => client.DefaultRequestVersion;
            set => client.DefaultRequestVersion = value;
        }

        //
        // Resumo:
        //     Gets the headers which should be sent with each request.
        //
        // Devoluções:
        //     The headers which should be sent with each request.
        public HttpRequestHeaders DefaultRequestHeaders
        {
            get => client.DefaultRequestHeaders;
        }

        //
        // Resumo:
        //     Gets or sets the base address of Uniform Resource Identifier (URI) of the Internet
        //     resource used when sending requests.
        //
        // Devoluções:
        //     The base address of Uniform Resource Identifier (URI) of the Internet resource
        //     used when sending requests.
        public Uri BaseAddress
        {
            get => client.BaseAddress;
            set => client.BaseAddress = value;
        }

        //
        // Resumo:
        //     Gets or sets the maximum number of bytes to buffer when reading the response
        //     content.
        //
        // Devoluções:
        //     The maximum number of bytes to buffer when reading the response content. The
        //     default value for this property is 2 gigabytes.
        //
        // Exceções:
        //   T:System.ArgumentOutOfRangeException:
        //     The size specified is less than or equal to zero.
        //
        //   T:System.InvalidOperationException:
        //     An operation has already been started on the current instance.
        //
        //   T:System.ObjectDisposedException:
        //     The current instance has been disposed.
        public long MaxResponseContentBufferSize
        {
            get => client.MaxResponseContentBufferSize;
            set => client.MaxResponseContentBufferSize = value;
        }

        //
        // Resumo:
        //     Gets or sets the timespan to wait before the request times out.
        //
        // Devoluções:
        //     The timespan to wait before the request times out.
        //
        // Exceções:
        //   T:System.ArgumentOutOfRangeException:
        //     The timeout specified is less than or equal to zero and is not System.Threading.Timeout.InfiniteTimeSpan.
        //
        //   T:System.InvalidOperationException:
        //     An operation has already been started on the current instance.
        //
        //   T:System.ObjectDisposedException:
        //     The current instance has been disposed.
        public TimeSpan Timeout
        {
            get => client.Timeout;
            set => client.Timeout = value;
        }

        //
        // Resumo:
        //     Cancel all pending requests on this instance.
        public void CancelPendingRequests()
        {
            client.CancelPendingRequests();
        }

        //
        // Resumo:
        //     Send a DELETE request to the specified Uri as an asynchronous operation.
        //
        // Parâmetros:
        //   requestUri:
        //     The Uri the request is sent to.
        //
        // Devoluções:
        //     The task object representing the asynchronous operation.
        //
        // Exceções:
        //   T:System.ArgumentNullException:
        //     The requestUri is null.
        //
        //   T:System.InvalidOperationException:
        //     The request message was already sent by the System.Net.Http.HttpClient instance.
        //
        //   T:System.Net.Http.HttpRequestException:
        //     The request failed due to an underlying issue such as network connectivity, DNS
        //     failure, server certificate validation or timeout.
        public async Task<HttpResponseMessage> DeleteAsync(Uri requestUri)
        {
            return await client.DeleteAsync(requestUri);
        }

        //
        // Resumo:
        //     Send a DELETE request to the specified Uri as an asynchronous operation.
        //
        // Parâmetros:
        //   requestUri:
        //     The Uri the request is sent to.
        //
        // Devoluções:
        //     The task object representing the asynchronous operation.
        //
        // Exceções:
        //   T:System.ArgumentNullException:
        //     The requestUri is null.
        //
        //   T:System.InvalidOperationException:
        //     The request message was already sent by the System.Net.Http.HttpClient instance.
        //
        //   T:System.Net.Http.HttpRequestException:
        //     The request failed due to an underlying issue such as network connectivity, DNS
        //     failure, server certificate validation or timeout.
        public async Task<HttpResponseMessage> DeleteAsync(string requestUri)
        {
            return await client.DeleteAsync(requestUri);
        }

        //
        // Resumo:
        //     Send a DELETE request to the specified Uri with a cancellation token as an asynchronous
        //     operation.
        //
        // Parâmetros:
        //   requestUri:
        //     The Uri the request is sent to.
        //
        //   cancellationToken:
        //     A cancellation token that can be used by other objects or threads to receive
        //     notice of cancellation.
        //
        // Devoluções:
        //     The task object representing the asynchronous operation.
        //
        // Exceções:
        //   T:System.ArgumentNullException:
        //     The requestUri is null.
        //
        //   T:System.InvalidOperationException:
        //     The request message was already sent by the System.Net.Http.HttpClient instance.
        //
        //   T:System.Net.Http.HttpRequestException:
        //     The request failed due to an underlying issue such as network connectivity, DNS
        //     failure, server certificate validation or timeout.
        public async Task<HttpResponseMessage> DeleteAsync(Uri requestUri, CancellationToken cancellationToken)
        {
            return await client.DeleteAsync(requestUri, cancellationToken);
        }

        //
        // Resumo:
        //     Send a DELETE request to the specified Uri with a cancellation token as an asynchronous
        //     operation.
        //
        // Parâmetros:
        //   requestUri:
        //     The Uri the request is sent to.
        //
        //   cancellationToken:
        //     A cancellation token that can be used by other objects or threads to receive
        //     notice of cancellation.
        //
        // Devoluções:
        //     The task object representing the asynchronous operation.
        //
        // Exceções:
        //   T:System.ArgumentNullException:
        //     The requestUri is null.
        //
        //   T:System.InvalidOperationException:
        //     The request message was already sent by the System.Net.Http.HttpClient instance.
        //
        //   T:System.Net.Http.HttpRequestException:
        //     The request failed due to an underlying issue such as network connectivity, DNS
        //     failure, server certificate validation or timeout.
        public async Task<HttpResponseMessage> DeleteAsync(string requestUri, CancellationToken cancellationToken)
        {
            return await client.DeleteAsync(requestUri, cancellationToken);
        }

        //
        // Resumo:
        //     Send a GET request to the specified Uri with a cancellation token as an asynchronous
        //     operation.
        //
        // Parâmetros:
        //   requestUri:
        //     The Uri the request is sent to.
        //
        //   cancellationToken:
        //     A cancellation token that can be used by other objects or threads to receive
        //     notice of cancellation.
        //
        // Devoluções:
        //     The task object representing the asynchronous operation.
        //
        // Exceções:
        //   T:System.ArgumentNullException:
        //     The requestUri is null.
        //
        //   T:System.Net.Http.HttpRequestException:
        //     The request failed due to an underlying issue such as network connectivity, DNS
        //     failure, server certificate validation or timeout.
        public async Task<HttpResponseMessage> GetAsync(Uri requestUri, CancellationToken cancellationToken)
        {
            return await client.GetAsync(requestUri, cancellationToken);
        }

        //
        // Resumo:
        //     Send a GET request to the specified Uri as an asynchronous operation.
        //
        // Parâmetros:
        //   requestUri:
        //     The Uri the request is sent to.
        //
        // Devoluções:
        //     The task object representing the asynchronous operation.
        //
        // Exceções:
        //   T:System.ArgumentNullException:
        //     The requestUri is null.
        //
        //   T:System.Net.Http.HttpRequestException:
        //     The request failed due to an underlying issue such as network connectivity, DNS
        //     failure, server certificate validation or timeout.
        public async Task<HttpResponseMessage> GetAsync(string requestUri)
        {
            return await client.GetAsync(requestUri);
        }

        //
        // Resumo:
        //     Send a GET request to the specified Uri with an HTTP completion option as an
        //     asynchronous operation.
        //
        // Parâmetros:
        //   requestUri:
        //     The Uri the request is sent to.
        //
        //   completionOption:
        //     An HTTP completion option value that indicates when the operation should be considered
        //     completed.
        //
        // Devoluções:
        //     The task object representing the asynchronous operation.
        //
        // Exceções:
        //   T:System.ArgumentNullException:
        //     The requestUri is null.
        //
        //   T:System.Net.Http.HttpRequestException:
        //     The request failed due to an underlying issue such as network connectivity, DNS
        //     failure, server certificate validation or timeout.
        public async Task<HttpResponseMessage> GetAsync(Uri requestUri, HttpCompletionOption completionOption)
        {
            return await client.GetAsync(requestUri, completionOption);
        }

        //
        // Resumo:
        //     Send a GET request to the specified Uri as an asynchronous operation.
        //
        // Parâmetros:
        //   requestUri:
        //     The Uri the request is sent to.
        //
        // Devoluções:
        //     The task object representing the asynchronous operation.
        //
        // Exceções:
        //   T:System.ArgumentNullException:
        //     The requestUri is null.
        //
        //   T:System.Net.Http.HttpRequestException:
        //     The request failed due to an underlying issue such as network connectivity, DNS
        //     failure, server certificate validation or timeout.
        public async Task<HttpResponseMessage> GetAsync(Uri requestUri)
        {
            return await client.GetAsync(requestUri);
        }

        //
        // Resumo:
        //     Send a GET request to the specified Uri with a cancellation token as an asynchronous
        //     operation.
        //
        // Parâmetros:
        //   requestUri:
        //     The Uri the request is sent to.
        //
        //   cancellationToken:
        //     A cancellation token that can be used by other objects or threads to receive
        //     notice of cancellation.
        //
        // Devoluções:
        //     The task object representing the asynchronous operation.
        //
        // Exceções:
        //   T:System.ArgumentNullException:
        //     The requestUri is null.
        //
        //   T:System.Net.Http.HttpRequestException:
        //     The request failed due to an underlying issue such as network connectivity, DNS
        //     failure, server certificate validation or timeout.
        public async Task<HttpResponseMessage> GetAsync(string requestUri, CancellationToken cancellationToken)
        {
            return await client.GetAsync(requestUri, cancellationToken);
        }

        //
        // Resumo:
        //     Send a GET request to the specified Uri with an HTTP completion option and a
        //     cancellation token as an asynchronous operation.
        //
        // Parâmetros:
        //   requestUri:
        //     The Uri the request is sent to.
        //
        //   completionOption:
        //     An HTTP completion option value that indicates when the operation should be considered
        //     completed.
        //
        //   cancellationToken:
        //     A cancellation token that can be used by other objects or threads to receive
        //     notice of cancellation.
        //
        // Devoluções:
        //     The task object representing the asynchronous operation.
        //
        // Exceções:
        //   T:System.ArgumentNullException:
        //     The requestUri is null.
        //
        //   T:System.Net.Http.HttpRequestException:
        //     The request failed due to an underlying issue such as network connectivity, DNS
        //     failure, server certificate validation or timeout.
        public async Task<HttpResponseMessage> GetAsync(string requestUri, HttpCompletionOption completionOption, CancellationToken cancellationToken)
        {
            return await client.GetAsync(requestUri, completionOption, cancellationToken);
        }

        //
        // Resumo:
        //     Send a GET request to the specified Uri with an HTTP completion option as an
        //     asynchronous operation.
        //
        // Parâmetros:
        //   requestUri:
        //     The Uri the request is sent to.
        //
        //   completionOption:
        //     An HTTP completion option value that indicates when the operation should be considered
        //     completed.
        //
        // Devoluções:
        //     The task object representing the asynchronous operation.
        //
        // Exceções:
        //   T:System.ArgumentNullException:
        //     The requestUri is null.
        //
        //   T:System.Net.Http.HttpRequestException:
        //     The request failed due to an underlying issue such as network connectivity, DNS
        //     failure, server certificate validation or timeout.
        public async Task<HttpResponseMessage> GetAsync(string requestUri, HttpCompletionOption completionOption)
        {
            return await client.GetAsync(requestUri, completionOption);
        }

        //
        // Resumo:
        //     Send a GET request to the specified Uri with an HTTP completion option and a
        //     cancellation token as an asynchronous operation.
        //
        // Parâmetros:
        //   requestUri:
        //     The Uri the request is sent to.
        //
        //   completionOption:
        //     An HTTP completion option value that indicates when the operation should be considered
        //     completed.
        //
        //   cancellationToken:
        //     A cancellation token that can be used by other objects or threads to receive
        //     notice of cancellation.
        //
        // Devoluções:
        //     The task object representing the asynchronous operation.
        //
        // Exceções:
        //   T:System.ArgumentNullException:
        //     The requestUri is null.
        //
        //   T:System.Net.Http.HttpRequestException:
        //     The request failed due to an underlying issue such as network connectivity, DNS
        //     failure, server certificate validation or timeout.
        public async Task<HttpResponseMessage> GetAsync(Uri requestUri, HttpCompletionOption completionOption, CancellationToken cancellationToken)
        {
            return await client.GetAsync(requestUri, completionOption, cancellationToken);
        }

        //
        // Resumo:
        //     Send a GET request to the specified Uri and return the response body as a byte
        //     array in an asynchronous operation.
        //
        // Parâmetros:
        //   requestUri:
        //     The Uri the request is sent to.
        //
        // Devoluções:
        //     The task object representing the asynchronous operation.
        //
        // Exceções:
        //   T:System.ArgumentNullException:
        //     The requestUri is null.
        //
        //   T:System.Net.Http.HttpRequestException:
        //     The request failed due to an underlying issue such as network connectivity, DNS
        //     failure, server certificate validation or timeout.
        public async Task<byte[]> GetByteArrayAsync(Uri requestUri)
        {
            return await client.GetByteArrayAsync(requestUri);
        }

        //
        // Resumo:
        //     Sends a GET request to the specified Uri and return the response body as a byte
        //     array in an asynchronous operation.
        //
        // Parâmetros:
        //   requestUri:
        //     The Uri the request is sent to.
        //
        // Devoluções:
        //     The task object representing the asynchronous operation.
        //
        // Exceções:
        //   T:System.ArgumentNullException:
        //     The requestUri is null.
        //
        //   T:System.Net.Http.HttpRequestException:
        //     The request failed due to an underlying issue such as network connectivity, DNS
        //     failure, server certificate validation or timeout.
        public async Task<byte[]> GetByteArrayAsync(string requestUri)
        {
            return await client.GetByteArrayAsync(requestUri);
        }

        //
        // Resumo:
        //     Send a GET request to the specified Uri and return the response body as a stream
        //     in an asynchronous operation.
        //
        // Parâmetros:
        //   requestUri:
        //     The Uri the request is sent to.
        //
        // Devoluções:
        //     The task object representing the asynchronous operation.
        //
        // Exceções:
        //   T:System.ArgumentNullException:
        //     The requestUri is null.
        //
        //   T:System.Net.Http.HttpRequestException:
        //     The request failed due to an underlying issue such as network connectivity, DNS
        //     failure, server certificate validation or timeout.
        public async Task<Stream> GetStreamAsync(string requestUri)
        {
            return await client.GetStreamAsync(requestUri);
        }

        //
        // Resumo:
        //     Send a GET request to the specified Uri and return the response body as a stream
        //     in an asynchronous operation.
        //
        // Parâmetros:
        //   requestUri:
        //     The Uri the request is sent to.
        //
        // Devoluções:
        //     The task object representing the asynchronous operation.
        //
        // Exceções:
        //   T:System.ArgumentNullException:
        //     The requestUri is null.
        //
        //   T:System.Net.Http.HttpRequestException:
        //     The request failed due to an underlying issue such as network connectivity, DNS
        //     failure, server certificate validation or timeout.
        public async Task<Stream> GetStreamAsync(Uri requestUri)
        {
            return await client.GetStreamAsync(requestUri);
        }

        //
        // Resumo:
        //     Send a GET request to the specified Uri and return the response body as a string
        //     in an asynchronous operation.
        //
        // Parâmetros:
        //   requestUri:
        //     The Uri the request is sent to.
        //
        // Devoluções:
        //     The task object representing the asynchronous operation.
        //
        // Exceções:
        //   T:System.ArgumentNullException:
        //     The requestUri is null.
        //
        //   T:System.Net.Http.HttpRequestException:
        //     The request failed due to an underlying issue such as network connectivity, DNS
        //     failure, server certificate validation or timeout.
        public async Task<string> GetStringAsync(Uri requestUri)
        {
            return await client.GetStringAsync(requestUri);
        }

        //
        // Resumo:
        //     Send a GET request to the specified Uri and return the response body as a string
        //     in an asynchronous operation.
        //
        // Parâmetros:
        //   requestUri:
        //     The Uri the request is sent to.
        //
        // Devoluções:
        //     The task object representing the asynchronous operation.
        //
        // Exceções:
        //   T:System.ArgumentNullException:
        //     The requestUri is null.
        //
        //   T:System.Net.Http.HttpRequestException:
        //     The request failed due to an underlying issue such as network connectivity, DNS
        //     failure, server certificate validation or timeout.
        public async Task<string> GetStringAsync(string requestUri)
        {
            return await client.GetStringAsync(requestUri);
        }

        //
        // Resumo:
        //     Sends a PATCH request with a cancellation token as an asynchronous operation.
        //
        // Parâmetros:
        //   requestUri:
        //     The Uri the request is sent to.
        //
        //   content:
        //     The HTTP request content sent to the server.
        //
        //   cancellationToken:
        //     A cancellation token that can be used by other objects or threads to receive
        //     notice of cancellation.
        //
        // Devoluções:
        //     The task object representing the asynchronous operation.
        public async Task<HttpResponseMessage> PatchAsync(Uri requestUri, HttpContent content, CancellationToken cancellationToken)
        {
            return await client.PatchAsync(requestUri, content, cancellationToken);
        }

        //
        // Resumo:
        //     Sends a PATCH request to a Uri designated as a string as an asynchronous operation.
        //
        // Parâmetros:
        //   requestUri:
        //     The Uri the request is sent to.
        //
        //   content:
        //     The HTTP request content sent to the server.
        //
        // Devoluções:
        //     The task object representing the asynchronous operation.
        public async Task<HttpResponseMessage> PatchAsync(string requestUri, HttpContent content)
        {
            return await client.PatchAsync(requestUri, content);
        }

        //
        // Resumo:
        //     Sends a PATCH request with a cancellation token to a Uri represented as a string
        //     as an asynchronous operation.
        //
        // Parâmetros:
        //   requestUri:
        //     The Uri the request is sent to.
        //
        //   content:
        //     The HTTP request content sent to the server.
        //
        //   cancellationToken:
        //     A cancellation token that can be used by other objects or threads to receive
        //     notice of cancellation.
        //
        // Devoluções:
        //     The task object representing the asynchronous operation.
        public async Task<HttpResponseMessage> PatchAsync(string requestUri, HttpContent content, CancellationToken cancellationToken)
        {
            return await client.PatchAsync(requestUri, content, cancellationToken);
        }

        //
        // Resumo:
        //     Sends a PATCH request as an asynchronous operation.
        //
        // Parâmetros:
        //   requestUri:
        //     The Uri the request is sent to.
        //
        //   content:
        //     The HTTP request content sent to the server.
        //
        // Devoluções:
        //     The task object representing the asynchronous operation.
        public async Task<HttpResponseMessage> PatchAsync(Uri requestUri, HttpContent content)
        {
            return await client.PatchAsync(requestUri, content);
        }

        //
        // Resumo:
        //     Send a POST request to the specified Uri as an asynchronous operation.
        //
        // Parâmetros:
        //   requestUri:
        //     The Uri the request is sent to.
        //
        //   content:
        //     The HTTP request content sent to the server.
        //
        // Devoluções:
        //     The task object representing the asynchronous operation.
        //
        // Exceções:
        //   T:System.ArgumentNullException:
        //     The requestUri is null.
        //
        //   T:System.Net.Http.HttpRequestException:
        //     The request failed due to an underlying issue such as network connectivity, DNS
        //     failure, server certificate validation or timeout.
        public async Task<HttpResponseMessage> PostAsync(string requestUri, HttpContent content)
        {
            return await client.PostAsync(requestUri, content);
        }

        //
        // Resumo:
        //     Send a POST request with a cancellation token as an asynchronous operation.
        //
        // Parâmetros:
        //   requestUri:
        //     The Uri the request is sent to.
        //
        //   content:
        //     The HTTP request content sent to the server.
        //
        //   cancellationToken:
        //     A cancellation token that can be used by other objects or threads to receive
        //     notice of cancellation.
        //
        // Devoluções:
        //     The task object representing the asynchronous operation.
        //
        // Exceções:
        //   T:System.ArgumentNullException:
        //     The requestUri is null.
        //
        //   T:System.Net.Http.HttpRequestException:
        //     The request failed due to an underlying issue such as network connectivity, DNS
        //     failure, server certificate validation or timeout.
        public async Task<HttpResponseMessage> PostAsync(Uri requestUri, HttpContent content, CancellationToken cancellationToken)
        {
            return await client.PostAsync(requestUri, content, cancellationToken);
        }

        //
        // Resumo:
        //     Send a POST request to the specified Uri as an asynchronous operation.
        //
        // Parâmetros:
        //   requestUri:
        //     The Uri the request is sent to.
        //
        //   content:
        //     The HTTP request content sent to the server.
        //
        // Devoluções:
        //     The task object representing the asynchronous operation.
        //
        // Exceções:
        //   T:System.ArgumentNullException:
        //     The requestUri is null.
        //
        //   T:System.Net.Http.HttpRequestException:
        //     The request failed due to an underlying issue such as network connectivity, DNS
        //     failure, server certificate validation or timeout.
        public async Task<HttpResponseMessage> PostAsync(Uri requestUri, HttpContent content)
        {
            return await client.PostAsync(requestUri, content);
        }

        //
        // Resumo:
        //     Send a POST request with a cancellation token as an asynchronous operation.
        //
        // Parâmetros:
        //   requestUri:
        //     The Uri the request is sent to.
        //
        //   content:
        //     The HTTP request content sent to the server.
        //
        //   cancellationToken:
        //     A cancellation token that can be used by other objects or threads to receive
        //     notice of cancellation.
        //
        // Devoluções:
        //     The task object representing the asynchronous operation.
        //
        // Exceções:
        //   T:System.ArgumentNullException:
        //     The requestUri is null.
        //
        //   T:System.Net.Http.HttpRequestException:
        //     The request failed due to an underlying issue such as network connectivity, DNS
        //     failure, server certificate validation or timeout.
        public async Task<HttpResponseMessage> PostAsync(string requestUri, HttpContent content, CancellationToken cancellationToken)
        {
            return await client.PostAsync(requestUri, content, cancellationToken);
        }

        //
        // Resumo:
        //     Send a PUT request to the specified Uri as an asynchronous operation.
        //
        // Parâmetros:
        //   requestUri:
        //     The Uri the request is sent to.
        //
        //   content:
        //     The HTTP request content sent to the server.
        //
        // Devoluções:
        //     The task object representing the asynchronous operation.
        //
        // Exceções:
        //   T:System.ArgumentNullException:
        //     The requestUri is null.
        //
        //   T:System.Net.Http.HttpRequestException:
        //     The request failed due to an underlying issue such as network connectivity, DNS
        //     failure, server certificate validation or timeout.
        public async Task<HttpResponseMessage> PutAsync(Uri requestUri, HttpContent content)
        {
            return await client.PutAsync(requestUri, content);
        }

        //
        // Resumo:
        //     Send a PUT request with a cancellation token as an asynchronous operation.
        //
        // Parâmetros:
        //   requestUri:
        //     The Uri the request is sent to.
        //
        //   content:
        //     The HTTP request content sent to the server.
        //
        //   cancellationToken:
        //     A cancellation token that can be used by other objects or threads to receive
        //     notice of cancellation.
        //
        // Devoluções:
        //     The task object representing the asynchronous operation.
        //
        // Exceções:
        //   T:System.ArgumentNullException:
        //     The requestUri is null.
        //
        //   T:System.Net.Http.HttpRequestException:
        //     The request failed due to an underlying issue such as network connectivity, DNS
        //     failure, server certificate validation or timeout.
        public async Task<HttpResponseMessage> PutAsync(Uri requestUri, HttpContent content, CancellationToken cancellationToken)
        {
            return await client.PutAsync(requestUri, content, cancellationToken);
        }

        //
        // Resumo:
        //     Send a PUT request to the specified Uri as an asynchronous operation.
        //
        // Parâmetros:
        //   requestUri:
        //     The Uri the request is sent to.
        //
        //   content:
        //     The HTTP request content sent to the server.
        //
        // Devoluções:
        //     The task object representing the asynchronous operation.
        //
        // Exceções:
        //   T:System.ArgumentNullException:
        //     The requestUri is null.
        //
        //   T:System.Net.Http.HttpRequestException:
        //     The request failed due to an underlying issue such as network connectivity, DNS
        //     failure, server certificate validation or timeout.
        public async Task<HttpResponseMessage> PutAsync(string requestUri, HttpContent content)
        {
            return await client.PutAsync(requestUri, content);
        }

        //
        // Resumo:
        //     Send a PUT request with a cancellation token as an asynchronous operation.
        //
        // Parâmetros:
        //   requestUri:
        //     The Uri the request is sent to.
        //
        //   content:
        //     The HTTP request content sent to the server.
        //
        //   cancellationToken:
        //     A cancellation token that can be used by other objects or threads to receive
        //     notice of cancellation.
        //
        // Devoluções:
        //     The task object representing the asynchronous operation.
        //
        // Exceções:
        //   T:System.ArgumentNullException:
        //     The requestUri is null.
        //
        //   T:System.Net.Http.HttpRequestException:
        //     The request failed due to an underlying issue such as network connectivity, DNS
        //     failure, server certificate validation or timeout.
        public async Task<HttpResponseMessage> PutAsync(string requestUri, HttpContent content, CancellationToken cancellationToken)
        {
            return await client.PutAsync(requestUri, content, cancellationToken);
        }

        //
        // Resumo:
        //     Send an HTTP request as an asynchronous operation.
        //
        // Parâmetros:
        //   request:
        //     The HTTP request message to send.
        //
        //   completionOption:
        //     When the operation should complete (as soon as a response is available or after
        //     reading the whole response content).
        //
        // Devoluções:
        //     The task object representing the asynchronous operation.
        //
        // Exceções:
        //   T:System.ArgumentNullException:
        //     The request is null.
        //
        //   T:System.InvalidOperationException:
        //     The request message was already sent by the System.Net.Http.HttpClient instance.
        //
        //   T:System.Net.Http.HttpRequestException:
        //     The request failed due to an underlying issue such as network connectivity, DNS
        //     failure, server certificate validation or timeout.
        public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, HttpCompletionOption completionOption)
        {
            return await client.SendAsync(request, completionOption);
        }

        //
        // Resumo:
        //     Send an HTTP request as an asynchronous operation.
        //
        // Parâmetros:
        //   request:
        //     The HTTP request message to send.
        //
        //   completionOption:
        //     When the operation should complete (as soon as a response is available or after
        //     reading the whole response content).
        //
        //   cancellationToken:
        //     The cancellation token to cancel operation.
        //
        // Devoluções:
        //     The task object representing the asynchronous operation.
        //
        // Exceções:
        //   T:System.ArgumentNullException:
        //     The request is null.
        //
        //   T:System.InvalidOperationException:
        //     The request message was already sent by the System.Net.Http.HttpClient instance.
        //
        //   T:System.Net.Http.HttpRequestException:
        //     The request failed due to an underlying issue such as network connectivity, DNS
        //     failure, server certificate validation or timeout.
        public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, HttpCompletionOption completionOption, CancellationToken cancellationToken)
        {
            return await client.SendAsync(request, completionOption, cancellationToken);
        }

        //
        // Resumo:
        //     Send an HTTP request as an asynchronous operation.
        //
        // Parâmetros:
        //   request:
        //     The HTTP request message to send.
        //
        //   cancellationToken:
        //     The cancellation token to cancel operation.
        //
        // Devoluções:
        //     The task object representing the asynchronous operation.
        //
        // Exceções:
        //   T:System.ArgumentNullException:
        //     The request is null.
        //
        //   T:System.InvalidOperationException:
        //     The request message was already sent by the System.Net.Http.HttpClient instance.
        //
        //   T:System.Net.Http.HttpRequestException:
        //     The request failed due to an underlying issue such as network connectivity, DNS
        //     failure, server certificate validation or timeout.
        public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return await client.SendAsync(request, cancellationToken);
        }

        //
        // Resumo:
        //     Send an HTTP request as an asynchronous operation.
        //
        // Parâmetros:
        //   request:
        //     The HTTP request message to send.
        //
        // Devoluções:
        //     The task object representing the asynchronous operation.
        //
        // Exceções:
        //   T:System.ArgumentNullException:
        //     The request is null.
        //
        //   T:System.InvalidOperationException:
        //     The request message was already sent by the System.Net.Http.HttpClient instance.
        //
        //   T:System.Net.Http.HttpRequestException:
        //     The request failed due to an underlying issue such as network connectivity, DNS
        //     failure, server certificate validation or timeout.
        public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
        {
            return await client.SendAsync(request);
        }

        //
        // Resumo:
        //     Releases the unmanaged resources used by the System.Net.Http.HttpClient and optionally
        //     disposes of the managed resources.
        //
        // Parâmetros:
        //   disposing:
        //     true to release both managed and unmanaged resources; false to releases only
        //     unmanaged resources.
        protected void Dispose()
        {
            client.Dispose();
        }
    }
}
