using CommonTestUtilities.Requests.Register;
using FluentAssertions;
using MyRecipeBook.Exceptions;
using System.Globalization;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using WebApiTest.InlineData;
using Xunit;

namespace WebApiTest.User.Register;

public class UserControllerTest : IClassFixture<CustomWebApplicationFactory>
{
    HttpClient _client;
    public UserControllerTest(CustomWebApplicationFactory factory)
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


    [Theory]
    [ClassData(typeof(InlineDataCultureMensages))]
    public async void Error_Name_Empty(string culture)
    {
        var request = RequestRegisterUserJsonBuilder.Build();
        request.Name = string.Empty;

        if (_client.DefaultRequestHeaders.Contains("Accept-Language"))
            _client.DefaultRequestHeaders.Remove("Accept-Language");

        _client.DefaultRequestHeaders.Add("Accept-Language", culture);


        var response = await _client.PostAsJsonAsync("User", request);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        using var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData =  await JsonDocument.ParseAsync(responseBody);

        var listaErro = responseData.RootElement.GetProperty("errors").EnumerateArray();

        var expectedMessage = ResourceMessageExceptions.ResourceManager.GetString("NAME_EMPTY", new CultureInfo(culture));

        listaErro.Should().ContainSingle().And.Contain(erro => erro.GetString()!.Equals(expectedMessage));

        


    }






}
