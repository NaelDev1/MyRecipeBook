using Bogus;
using MyRecipeBook.Communication.Requests;

namespace CommonTestUtilities.Requests.Register;

public static  class RequestRegisterUserJsonBuilder
{

    public static RequestRegisterUserJson Build(int passwordNumber = 10)
    {
        return new Faker<RequestRegisterUserJson>()
            .RuleFor(user => user.Name, (f) => f.Person.FirstName)
            .RuleFor(user => user.Email, (f, u) => f.Internet.Email(u.Name))
            .RuleFor(user => user.Password, (f) => f.Internet.Password(passwordNumber));
    }

}
