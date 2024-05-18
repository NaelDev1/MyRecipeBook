using CommonTestUtilities.Cryptography;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositores;
using CommonTestUtilities.Requests.Register;
using FluentAssertions;
using MyRecipeBook.Application.UseCases.User.Register;
using MyRecipeBook.Exceptions;
using MyRecipeBook.Exceptions.ExceptionsBase;
using Xunit;

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


    [Fact]
    public async Task Error_Email_Already_Exist()
    {
        var request = RequestRegisterUserJsonBuilder.Build();

        var useCase = UseCaseBuild(request.Email);

        Func<Task> act = () => useCase.ExecuteAsync(request);

        (await act.Should().ThrowAsync<ErrorOnValidationException>())
            .Where(c => c.ErrorMessages.Count == 1 && c.ErrorMessages.Contains(ResourceMessageExceptions.EMAIL_ALREADY_EXISTS));
    }


    public RegisterUserUseCase UseCaseBuild(string? email = null)
    {
        var encryptor = PasswordEncripterBuilder.Build();

        var autoMapper = AutoMapperBuilder.Build();

        var writeOnlyRepository = IUserWriteOnlyRepositoryBuilder.Build();

        var userReadOnlyBuilder = new IUserReadOnlyRepositoryBuilder();

        if (!string.IsNullOrEmpty(email))
            userReadOnlyBuilder.ExistActiveUserWithEmail(email);

        var unityOfWork = IUnitOfWorkBuilder.Build();


       return  new RegisterUserUseCase(writeOnlyRepository, userReadOnlyBuilder.Build(), autoMapper, encryptor, unityOfWork);
    }

}
