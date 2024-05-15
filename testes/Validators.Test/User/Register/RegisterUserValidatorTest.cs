using CommonTestUtilities.Requests.Register;
using MyRecipeBook.Application.UseCases.User.Register;
using MyRecipeBook.Communication.Requests;

namespace Validators.Test.User.Register;

public class RegisterUserValidatorTest
{

    [Fact]
    public void Assert_Register()
    {
        //os Tresa AS   Arrange , Action , Assert
        var validator = new RegisterUserValidator();

        var request = RequestRegisterUserJsonBuilder.Builder();

        var result = validator.Validate(request);

        Assert.True(result.IsValid);
    }

}
