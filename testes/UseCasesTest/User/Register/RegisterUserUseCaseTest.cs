using CommonTestUtilities.Requests.Register;
using MyRecipeBook.Application.UseCases.User.Register;
using Xunit;
using FluentAssertions;
using MyRecipeBook.Application.Services.Cryptography;
using CommonTestUtilities.Cryptography;
using CommonTestUtilities.Mapper;

namespace UseCasesTest.User.Register;

public class RegisterUserUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        //var request = RequestRegisterUserJsonBuilder.Build();

        //var encryptor = PasswordEncripterBuilder.Build();

        //var autoMapper = AutoMapperBuilder.Build();

        //var useCase = new RegisterUserUseCase(encryptor, autoMapper);

        var result = await useCase.ExecuteAsync(request);

        result.Name.Should().NotBeNull();
        result.Name.Should().Be(request.Name);


    }

}
