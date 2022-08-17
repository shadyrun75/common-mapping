using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace common_mapping.Mappings.API
{
    /// <summary>
    /// REST Client for integration with common-mapping.API
    /// </summary>
    public class Client
    {
        private Settings.API _setup;
        private static readonly System.Text.Encoding encoding = Encoding.UTF8;
        private readonly ILogger _logger;
        private JsonSerializerOptions options = new JsonSerializerOptions
        {
            WriteIndented = true,
            Converters = { new JsonStringEnumConverter() }
        };

        public Client(Settings.API setup, ILogger logger)
        {
            _setup = setup;
            _logger = logger;
        }

        /// <summary>
        /// Async method of execution request
        /// </summary>
        /// <typeparam name="T">Type of waited model of response</typeparam>
        /// <param name="urlQuery">Url connection without destination and filters</param>
        /// <param name="httpMethod">Type of HTTP method</param>
        /// <param name="body">Object for body request</param>
        /// <returns>Response of T model</returns>
        /// <exception cref="ApiException"></exception>
        public T DoRequest<T>(string urlQuery, HttpMethod httpMethod, Dictionary<string, string> queryParams = null, object? body = null)
            where T : class
        {
            string jsonRequest = "";
            string jsonResponse = "";
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(_setup.Url);
                using (var request = new HttpRequestMessage())
                {
                    request.Method = httpMethod;
                    if (queryParams?.Count > 0)
                    {
                        urlQuery += "?";
                        urlQuery += String.Join("&", queryParams.Select(x => $"{x.Key}={x.Value}"));
                    }
                    request.RequestUri = new Uri(_setup.Url + urlQuery);
                    request.Headers.Accept.Add(System.Net.Http.Headers.MediaTypeWithQualityHeaderValue.Parse("application/json"));
                    _logger?.LogDebug($"Request METHOD {request.Method} URL {request.RequestUri} AUTHORIZATION {request.Headers.Authorization?.Parameter} ");
                    if (body != null)
                    {
                        var options = new JsonSerializerOptions
                        {
                            WriteIndented = true,
                            Converters = { new JsonStringEnumConverter() }
                        };
                        jsonRequest = JsonSerializer.Serialize(body, options);
                        request.Content = new StringContent(jsonRequest);
                        request.Content.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse("application/json");
                        _logger?.LogDebug($" \r\nCONTENT {jsonRequest}");
                    }

                    try
                    {
                        using (var response = httpClient.Send(request))
                        {
                            var headers = System.Linq.Enumerable.ToDictionary(response.Headers, h_ => h_.Key, h_ => h_.Value);
                            if (response.Content != null && response.Content.Headers != null)
                            {
                                foreach (var item_ in response.Content.Headers)
                                    headers[item_.Key] = item_.Value;
                            }

                            if ((response.StatusCode != System.Net.HttpStatusCode.OK) && (response.StatusCode != System.Net.HttpStatusCode.Created))
                            {
                                jsonResponse = response.Content == null ? null : response.Content.ReadAsStringAsync().Result;

                                throw new ApiException(
                                    message: "The HTTP status code of the response was not expected (" + response.StatusCode + ").",
                                    statusCode: response.StatusCode,
                                    response: jsonResponse,
                                    headers: headers,
                                    innerException: null);
                            }

                            jsonResponse = response.Content.ReadAsStringAsync().Result;
                            if (typeof(T) != typeof(object))
                            {
                                var objectResponse = ReadObjectResponse<T>(response, headers);

                                //if (objectResponse.Object == null)
                                //    throw new ApiException("Response was null which was not expected.", response.StatusCode, objectResponse.Text, headers, null);

                                _logger?.LogDebug($"\r\nResponse STATUS {response.StatusCode} DATA {jsonResponse}");
                                httpClient?.Dispose();
                                return objectResponse.Object;
                            }
                            return null;
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger?.LogWarning(jsonRequest);
                        _logger?.LogWarning(jsonResponse);
                        _logger?.LogError(ex, $"Error by Client. {ex.Message}");
                        httpClient?.Dispose();
                        throw;
                    }
                }
            }
        }

        protected struct ObjectResponseResult<T>
        {
            public ObjectResponseResult(T responseObject, string responseText)
            {
                this.Object = responseObject;
                this.Text = responseText;
            }

            public T Object { get; }

            public string Text { get; }
        }

        protected virtual ObjectResponseResult<T> ReadObjectResponse<T>(System.Net.Http.HttpResponseMessage response,
            System.Collections.Generic.IReadOnlyDictionary<string, System.Collections.Generic.IEnumerable<string>> headers)
        {
            if (response == null || response.Content == null)
                return new ObjectResponseResult<T>(default(T), string.Empty);

            var responseText = response.Content.ReadAsStringAsync().Result;
            try
            {
                var typedBody = JsonSerializer.Deserialize<T>(responseText, options);
                return new ObjectResponseResult<T>(typedBody, responseText);
            }
            catch (Exception exception)
            {
                var message = $"Could not deserialize the response body string as {typeof(T).FullName}. Response text: {responseText}";
                throw new ApiException(message, response.StatusCode, responseText, headers, exception);
            }
        }
    }
}
