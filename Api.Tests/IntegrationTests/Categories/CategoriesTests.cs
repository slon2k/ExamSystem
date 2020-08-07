using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http.Json;
using System.Collections.Generic;
using Api.Models;
using Api.Tests.Utils;

namespace Api.Tests.IntegrationTests.Categories
{
    [TestClass]
    public class CategoriesTests
    {
        private CustomWebApplicationFactory<Startup> factory;
        const string BaseURL = "https://localhost";
        private HttpClient client { get; set; }
        private List<CategoryDto> categories { get; set; }

        [TestInitialize]
        public async Task Initialize()
        {
            factory = new CustomWebApplicationFactory<Startup>(); 
            client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                BaseAddress = new Uri(BaseURL)
            });
            var response = await client.GetAsync("/api/categories");
            categories = await response.Content.ReadFromJsonAsync<List<CategoryDto>>();
        }

        [TestMethod]
        public async Task GetAllEndpointReturnsSuccess()
        {
            var response = await client.GetAsync("/api/categories");
            var statusCode = response.StatusCode;
            Assert.AreEqual(HttpStatusCode.OK, statusCode);
        }

        [TestMethod]
        public async Task ReturnsListOfCategories()
        {
            var response = await client.GetAsync("/api/categories");
            var categories = await response.Content.ReadFromJsonAsync<List<CategoryDto>>();
            
            Assert.IsNotNull(categories);
            Assert.AreEqual("Title 1", categories[0]?.Title);
            Assert.AreEqual("Description 1", categories[0]?.Description);
        }

        [TestMethod]
        public async Task ReturnsSingleCategory()
        {
            var id = categories[0].Id ?? null;
            var response = await client.GetAsync(@$"/api/categories/{id}");
            var category = await response.Content.ReadFromJsonAsync<CategoryDto>();
            var statusCode = response.StatusCode;
            
            Assert.AreEqual(HttpStatusCode.OK, statusCode);
            Assert.IsNotNull(category);
            Assert.AreEqual("Title 1", category?.Title);
            Assert.AreEqual("Description 1", category?.Description);
        }

        [TestMethod]
        public async Task CanCreateCategory()
        {
            var category = new CategoryDto() { Title = "New Title", Description = "New Description" };
            var response = await client.PostAsync("/api/categories", HttpClientUtils.CreateContent(category));
            var createdCategory = await response.Content.ReadFromJsonAsync<CategoryDto>();

            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
            Assert.AreEqual("New Title", createdCategory.Title);
            Assert.AreEqual("New Description", createdCategory.Description);
        } 

        [TestMethod]
        public async Task CanUpdateCategory()
        {
            var count = categories.Count;
            var id = categories[count - 1].Id ?? null;
            var category = new CategoryDto() { Title = "Updated Title", Description = "Updated Description" };
            
            var response = await client.PutAsync(@$"/api/categories/{id}", HttpClientUtils.CreateContent(category));
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);

            response = await client.GetAsync(@$"/api/categories/{id}");
            var updatedCategory = await response.Content.ReadFromJsonAsync<CategoryDto>();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual("Updated Title", updatedCategory.Title);
            Assert.AreEqual("Updated Description", updatedCategory.Description);
        }
        
        [TestMethod]
        public async Task CanDeleteCategory()
        {
            var count = categories.Count;
            Guid? id = categories[count -1].Id ?? null;

            var response = await client.DeleteAsync(@$"/api/categories/{id}");           
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
            
            response = await client.GetAsync(@$"/api/categories/{id}");
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
