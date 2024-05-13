using MyRecipeBook.Application.Services.AutoMapper;
using MyRecipeBook.Application.Services.Cryptography;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Communication.Responses;
using MyRecipeBook.Domain.Repositories.User;
using MyRecipeBook.Exceptions.ExceptionsBase;

namespace MyRecipeBook.Application.UseCases.User.Register;

public class RegisterUserUseCase
{
    private readonly IUserWriteOnlyRepository _writeOnlyRepository;
    private readonly IUserReadOnlyRepository _readOnlyRepository;



    public async Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request) 
    {
        // validar a request
        Validate(request);

        // mapear a request em uma entidade
        var autoMapper = new AutoMapper.MapperConfiguration(options =>
        {
            options.AddProfile(new AutoMapping());
        }).CreateMapper();

        var user = autoMapper.Map<Domain.Entities.User>(request);

        // criptografia da senha
        var criptografia = new PasswordEncripter();
        user.Password = criptografia.Encrypt(request.Password);
      

      
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
