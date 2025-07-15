using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Web_E_Commerce.DTOs.Seller.Requests;
using Web_E_Commerce.Tests.Shared;

namespace Web_E_Commerce.Tests.Controller
{
    public class SellerRequestTests
    {
        private readonly HttpClient _client;

        public SellerRequestTests(CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task SendSellerRequest_ShouldReturnSuccess()
        {
            // Arrange
            var loginResponse = await _client.PostAsJsonAsync("/api/v1/auth/login", new
            {
                UserName = "testuser",
                Password = "testpassword"
            });

            var token = await loginResponse.Content.ReadFromJsonAsync<AuthResponse>();
            if (token?.Data?.AccessToken == null)
                throw new Exception("Không thể lấy access token");

            _client.DefaultRequestHeaders.Authorization = new("Bearer", token.Data.AccessToken);

            // Act
            var response = await _client.PostAsJsonAsync("/api/v1/seller-requests", new SellerRequestDto
            {
                ShopName = "Test Shop",
                Note = "Tôi muốn bán hàng"
            });

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
            var content = await response.Content.ReadAsStringAsync();
            Assert.Contains("submitted successfully", content);
        }
    }

    public class AuthResponse
    {
        public string Message { get; set; } = string.Empty;
        public AuthData Data { get; set; } = new AuthData();
    }

    public class AuthData
    {
        public string AccessToken { get; set; } = string.Empty;
    }
}