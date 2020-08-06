using Api.Handlers.Auth;
using Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Api.Tests.IntegrationTests
{
    [TestClass]
    public class AuthTests
    {
        private CustomWebApplicationFactory<Startup> factory;
        const string BaseURL = "https://localhost";
        private HttpClient client { get; set; }
        private HttpClient clientUser { get; set; }
        const string UserEmail = "user@test.com";
        const string AdminEmail = "user@test.com";
        const string Password = "Pa$$w0rd";


        [TestInitialize]
        public async Task Initialize()
        {
            factory = new CustomWebApplicationFactory<Startup>();
            client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                BaseAddress = new Uri(BaseURL)
            });

            var loginRequest = new Login.Request() { Email = UserEmail, Password = Password };
            HttpContent content = new StringContent(JsonSerializer.Serialize(loginRequest), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/api/auth/login", content);
            var data = await response.Content.ReadFromJsonAsync<AuthData>();
            
            clientUser = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                BaseAddress = new Uri(BaseURL)
            });
            
            clientUser.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", data.Token);
        }

        [TestMethod]
        public async Task AnonimousAccessToUserIsNotAllowed()
        {
            var response = await client.GetAsync("api/auth/user");
            var statusCode = response.StatusCode;
            Assert.AreEqual(HttpStatusCode.Unauthorized, statusCode);
        }

        [TestMethod]
        public async Task CanGetToken()
        {
            var loginRequest = new Login.Request() { Email = UserEmail, Password = Password };
            HttpContent content = new StringContent(JsonSerializer.Serialize(loginRequest), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/api/auth/login", content);
            var data = await response.Content.ReadFromJsonAsync<AuthData>();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsNotNull(data);
            Assert.IsTrue(!string.IsNullOrEmpty(data.Token));
        }

        [TestMethod]
        public async Task CanRegister()
        {
            var loginRequest = new Register.Request() { Email = "user1@test.com", Password = Password, ConfirmPassword = Password };
            HttpContent content = new StringContent(JsonSerializer.Serialize(loginRequest), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/api/auth/register", content);
            var data = await response.Content.ReadFromJsonAsync<AuthData>();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsNotNull(data);
            Assert.IsTrue(!string.IsNullOrEmpty(data.Token));
        }

        [TestMethod]
        public async Task AuthorizedAccessToUserIsAllowed()
        {
            var response = await clientUser.GetAsync("api/auth/user");
            var statusCode = response.StatusCode;
            Assert.AreEqual(HttpStatusCode.OK, statusCode);
        }

    }
}
