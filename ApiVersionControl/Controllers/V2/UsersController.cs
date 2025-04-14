using ApiVersionControl.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace ApiVersionControl.Controllers.V2
{
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private const string ApiTestURL = "https://dummyapi.io/data/v1/user?limit=30";
        private const string AppId = "60d0fe4f5311236168a109ca";
        private readonly HttpClient _httpClient;

        public UsersController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [MapToApiVersion("2.0")] // estara disponible unicamente para esta version
        [HttpGet(Name = "GetUsersData")]
        public async Task<IActionResult> GetUsersDataAsync()
        {
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("app-id", AppId);
            var response = await _httpClient.GetStreamAsync(ApiTestURL);
            var userData = await JsonSerializer.DeserializeAsync<UserResponseData>(response);
            var users = userData?.data;
            return Ok(users);
        }
    }
}
