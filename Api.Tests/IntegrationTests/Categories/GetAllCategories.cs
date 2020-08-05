using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http.Json;
using System.Collections.Generic;
using Api.Models;
using System.Linq;
using System.Text.Json;
using System.Text;

namespace Api.Tests.IntegrationTests.Categories
{
    [TestClass]
    public class GetAllCategories
    {
        private CustomWebApplicationFactory<Startup> factory;
        const string BaseURL = "https://localhost";
        private HttpClient client { get; set; }

        [TestInitialize]
        public void Initialize()
        {
            factory = new CustomWebApplicationFactory<Startup>(); 
            client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                BaseAddress = new Uri(BaseURL)
            });
        }

        [TestMethod]
        public async Task GetAllEndpointReturnsSuccess()
        {
            var response = await client.GetAsync("/api/categories");
            var statusCode = response.StatusCode;
            Assert.AreEqual(HttpStatusCode.OK, statusCode);
        }

        [TestMethod]
        public async Task ReturnsListOfSixCategories()
        {
            var category = new CategoryDto() { Title = "New Title", Description = "New Description" };
            var requestdata = JsonSerializer.Serialize(category);
            HttpContent content = new StringContent(requestdata, Encoding.UTF8, "application/json");
            var responsePost = await client.PostAsync("/api/categories", content);


            var response = await client.GetAsync("/api/categories");
            //var contentGet = await response.Content.ReadFromJsonAsync<object>();
            var categories = await response.Content.ReadFromJsonAsync<List<CategoryDto>>();
            
            Assert.IsNotNull(categories);

        }

        [TestMethod]
        public async Task CanCreateCategory()
        {
            var category = new CategoryDto() { Title = "New Title", Description = "New Description"};
            var requestdata = JsonSerializer.Serialize(category);
            HttpContent content = new StringContent(requestdata, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/api/categories", content);
            var responseContent = await response.Content.ReadFromJsonAsync<object>();

            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

            var newresponse = await client.GetAsync("/api/categories");
            var newresponseContent = await newresponse.Content.ReadFromJsonAsync<object>();
            Assert.AreEqual(HttpStatusCode.OK, newresponse.StatusCode);

        } 



    }
}
