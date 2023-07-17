using System.Net.Http;
using System.Threading.Tasks;
using System;
using System.Net.Http.Json;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Azure.Core;
using System.Text;

namespace SilkFlo.Web.Services
{
    public class SaasFullfilmentManager
    {
        private readonly HttpClient httpClient;
        private readonly IConfiguration _configuration;

        public SaasFullfilmentManager(IConfiguration configuration)
        {
            httpClient = new HttpClient();
            _configuration = configuration;
        }

        public async Task<Models.ResolveSubscriptionResponse> ResolveSubscription(string marketplaceToken)
        {
            // API endpoint URL
            string apiUrl = "https://marketplaceapi.microsoft.com/api/saas/subscriptions/resolve?api-version=2018-08-31";
            return await CallResolveApi(apiUrl, marketplaceToken);
            //// Request headers
            //httpClient.DefaultRequestHeaders.Add("content-type", "application/json");
            //httpClient.DefaultRequestHeaders.Add("x-ms-requestid", Guid.NewGuid().ToString());
            //httpClient.DefaultRequestHeaders.Add("x-ms-correlationid", Guid.NewGuid().ToString());
            //// Replace "<access_token>" with your actual access token
            //httpClient.DefaultRequestHeaders.Add("authorization", $"Bearer {accessToken}");
            //httpClient.DefaultRequestHeaders.Add("x-ms-marketplace-token", marketplaceToken);

        }

        public async Task<Models.ResolveSubscriptionResponse> CallResolveApi(string apiUrl, string marketplaceToken)
        {
            string accessToken = await GetAccessToken();

            var request = new HttpRequestMessage(HttpMethod.Post, apiUrl);

            // Set the request headers
            //request.Headers.Add("content-type", "application/json");
            request.Headers.Add("x-ms-requestid", Guid.NewGuid().ToString());
            request.Headers.Add("x-ms-correlationid", Guid.NewGuid().ToString());
            request.Headers.Add("authorization", $"Bearer {accessToken}");
            request.Headers.Add("x-ms-marketplace-token", marketplaceToken);

            // Make the API call
            HttpResponseMessage response = await httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                // Parse the response JSON into the ResolveSubscriptionResponse object
                string resolveResponse = await response.Content.ReadAsStringAsync();
                Models.ResolveSubscriptionResponse resolveSubscriptionResponse = null;

                if (!String.IsNullOrEmpty(resolveResponse))
                {
                    resolveSubscriptionResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.ResolveSubscriptionResponse>(resolveResponse);
                }

                return resolveSubscriptionResponse;
            }
            else
            {
                // Handle different response status codes here
                // You can customize the error handling based on your requirements
                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    // Handle 400 Bad Request error
                    throw new InvalidOperationException("Your token is malformed, invalid, or expired. Please contact admin support.");
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
                {
                    // Handle 403 Forbidden error
                    throw new InvalidOperationException("Your token is malformed, invalid, or expired. Please contact admin support.");
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                {
                    // Handle 500 Internal Server Error
                    throw new InvalidOperationException("There's an error occurred. Please contact admin support.");
                }

                // Throw an exception or return an appropriate response based on your error handling approach
                throw new Exception("Failed to resolve subscription. Status code: " + response.StatusCode);
            }
        }


        public async Task ActivateSubscription(string subscriptionId, string planId, int quantity)
        {
            // API endpoint URL
            string apiUrl = $"https://marketplaceapi.microsoft.com/api/saas/subscriptions/{subscriptionId}/activate?api-version=2018-08-31";

            string accessToken = await GetAccessToken();

            //// Request headers
            //httpClient.DefaultRequestHeaders.Add("content-type", "application/json");
            //httpClient.DefaultRequestHeaders.Add("x-ms-requestid", Guid.NewGuid().ToString());
            //httpClient.DefaultRequestHeaders.Add("x-ms-correlationid", Guid.NewGuid().ToString());
            //// Replace "<access_token>" with your actual access token
            //httpClient.DefaultRequestHeaders.Add("authorization", "Bearer <access_token>");

            var request = new HttpRequestMessage(HttpMethod.Post, apiUrl);

            // Set the request headers
            //request.Headers.Add("content-type", "application/json");
            request.Headers.Add("x-ms-requestid", Guid.NewGuid().ToString());
            request.Headers.Add("x-ms-correlationid", Guid.NewGuid().ToString());
            request.Headers.Add("authorization", $"Bearer {accessToken}");

            // Request payload
            var requestPayload = new Models.ActivateSubscriptionRequest
            {
                planId = planId,
                quantity = quantity
            };

            // Serialize the request payload to JSON
            var jsonPayload = Newtonsoft.Json.JsonConvert.SerializeObject(requestPayload);

            // Set the request content
            request.Content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");


            // Make the API call
            HttpResponseMessage response = await httpClient.SendAsync(request);

            // Make the API call
            //HttpResponseMessage response = await httpClient.PostAsJsonAsync(apiUrl, requestPayload);

            if (response.IsSuccessStatusCode)
            {
                // Subscription activation was successful
            }
            else
            {
                // Handle different response status codes here
                // You can customize the error handling based on your requirements
                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    // Handle 400 Bad Request error
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
                {
                    // Handle 403 Forbidden error
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    // Handle 404 Not Found error
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                {
                    // Handle 500 Internal Server Error
                }

                // Throw an exception or return an appropriate response based on your error handling approach
                throw new Exception("Failed to activate subscription. Status code: " + response.StatusCode);
            }
        }

        private async Task<string> GetAccessToken()
        {
            // Retrieve the Azure AD app settings from configuration
            var azureAdOptions = _configuration.GetSection("AzureAd").Get<SilkFlo.Web.Models.AzureAdOptions>();

            // Set the request URL
            string requestUrl = $"https://login.microsoftonline.com/{azureAdOptions.TenantId}/oauth2/token";
            // Replace "{{tenantId}}" with the actual value

            var request = new HttpRequestMessage(HttpMethod.Post, requestUrl);

            // Set the request headers
            //request.Headers.Add("Content-Type", "application/x-www-form-urlencoded");


            // Prepare the request body
            var requestBody = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("grant_type", "client_credentials"),
                new KeyValuePair<string, string>("client_id", azureAdOptions.ClientId),
                new KeyValuePair<string, string>("client_secret", azureAdOptions.ClientSecret),
                new KeyValuePair<string, string>("resource", "20e940b3-4c77-4b0b-9a53-9e16a1b010a7")
            });

            request.Content = requestBody;
            // Set the request headers
            //httpClient.DefaultRequestHeaders.Add("content-type", "application/x-www-form-urlencoded");

            // Send the POST request
            HttpResponseMessage response = await httpClient.SendAsync(request);

            // Read the response content as a string
            string responseContent = await response.Content.ReadAsStringAsync();

            // Handle the response
            if (response.IsSuccessStatusCode)
            {
                // Assuming you have the response content in the 'responseContent' variable
                var tokenResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.TokenResponse>(responseContent);
                return tokenResponse.access_token;
            }

            return "";
        }
    }
}
