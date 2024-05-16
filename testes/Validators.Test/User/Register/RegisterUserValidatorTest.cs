using CommonTestUtilities.Requests.Register;
using FluentAssertions;
using MyRecipeBook.Application.UseCases.User.Register;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Exceptions;

namespace Validators.Test.User.Register;

public class RegisterUserValidatorTest
{

    [Fact]
    public void Success()
    {
        //os Tresa AS   Arrange , Action , Assert
        var validator = new RegisterUserValidator();

        var request = RequestRegisterUserJsonBuilder.Build();

        var result = validator.Validate(request);

        result.IsValid.Should().BeTrue();
    }


    [Fact]
    public void Erro_Name_Empty()
    {
        //os Tresa AS   Arrange , Action , Assert
        var validator = new RegisterUserValidator();

        var request = RequestRegisterUserJsonBuilder.Build();
        request.Name = string.Empty;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(c => c.ErrorMessage.Equals(ResourceMessageExceptions.NAME_EMPTY));
    }

    [Fact]
    public void Erro_Email_Empty()
    {
        //os Tresa AS   Arrange , Action , Assert
        var validator = new RegisterUserValidator();

        var request = RequestRegisterUserJsonBuilder.Build();
        request.Email = string.Empty;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(c => c.ErrorMessage.Equals(ResourceMessageExceptions.EMAIL_EMPTY));
    }


    [Fact]
    public void Erro_Email_Invalid()
    {
        //os Tresa AS   Arrange , Action , Assert
        var validator = new RegisterUserValidator();

        var request = RequestRegisterUserJsonBuilder.Build();
        request.Email = "teste";

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(c => c.ErrorMessage.Equals(ResourceMessageExceptions.EMAIL_INVALID));
    }


    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    public void Erro_Password_Invalid(int passwordNumber)
    {
        //os Tresa AS   Arrange , Action , Assert
        var validator = new RegisterUserValidator();

        var request = RequestRegisterUserJsonBuilder.Build(passwordNumber);

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(c => c.ErrorMessage.Equals(ResourceMessageExceptions.PASSWORD_INVALID));
    }



}
