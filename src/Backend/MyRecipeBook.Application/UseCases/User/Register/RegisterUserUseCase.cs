using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Communication.Responses;
using MyRecipeBook.Exceptions.ExceptionsBase;

namespace MyRecipeBook.Application.UseCases.User.Register;

public class RegisterUserUseCase
{
    public ResponseRegisteredUserJson Execute(RequestRegisterUserJson request) 
    {
        // validar a request
        Validate(request);

        // mapear a request em uma entidade


        // criptografia da senha

        // salvar no banco de dados


        return new ResponseRegisteredUserJson { Name = request.Name};
    }

    private void Validate(RequestRegisterUserJson request)
    {
        var validator = new RegisterUserValidator();
        var result = validator.Validate(request);

        if(result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage);

            throw new ErrorOnValidationException(errorMessages.ToList());

        }

    }
}
