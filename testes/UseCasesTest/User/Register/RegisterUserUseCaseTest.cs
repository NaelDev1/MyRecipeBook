using CommonTestUtilities.Requests.Register;
using MyRecipeBook.Application.UseCases.User.Register;
using Xunit;
using FluentAssertions;
using MyRecipeBook.Application.Services.Cryptography;
using CommonTestUtilities.Cryptography;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositores;

namespace UseCasesTest.User.Register;

public class RegisterUserUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var request = RequestRegisterUserJsonBuilder.Build();

        var useCase = UseCaseBuild();

        var result = await useCase.ExecuteAsync(request);

        result.Name.Should().NotBeNull();
        result.Name.Should().Be(request.Name);


    }

    public RegisterUserUseCase UseCaseBuild()
    {
        var encryptor = PasswordEncripterBuilder.Build();

        var autoMapper = AutoMapperBuilder.Build();

        var writeOnlyRepository = IUserWriteOnlyRepositoryBuilder.Build();

        var userReadOnlyRepository = new IUserReadOnlyRepositoryBuilder().Build();

        var unityOfWork = IUnitOfWorkBuilder.Build();


       return  new RegisterUserUseCase(writeOnlyRepository, userReadOnlyRepository, autoMapper, encryptor, unityOfWork);
    }

}
