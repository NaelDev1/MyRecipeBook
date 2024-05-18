using CommonTestUtilities.Requests.Register;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using MyRecipeBook.Communication.Requests;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Xunit;

namespace WebApiTest.User.Register;

public class UserControllerTest : IClassFixture<WebApplicationFactory<Program>>
{
    HttpClient _client;
    public UserControllerTest(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }


    [Fact]
    public async void Success()
    {
        var request = RequestRegisterUserJsonBuilder.Build();

        var response = await _client.PostAsJsonAsync("User", request);

        response.StatusCode.Should().Be(HttpStatusCode.Created);

        using var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        responseData.RootElement.GetProperty("name").GetString().Should().NotBeNull().And.Be(request.Name);
    }



}
