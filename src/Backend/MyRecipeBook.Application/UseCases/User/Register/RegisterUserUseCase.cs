using AutoMapper;
using MyRecipeBook.Application.Services.AutoMapper;
using MyRecipeBook.Application.Services.Cryptography;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Communication.Responses;
using MyRecipeBook.Domain.Repositories;
using MyRecipeBook.Domain.Repositories.User;
using MyRecipeBook.Exceptions;
using MyRecipeBook.Exceptions.ExceptionsBase;

namespace MyRecipeBook.Application.UseCases.User.Register;

public class RegisterUserUseCase: IRegisterUserUseCase
{
    private readonly IUserWriteOnlyRepository _writeOnlyRepository;
    private readonly IUserReadOnlyRepository _readOnlyRepository;
    private readonly IMapper _mapper;
    private readonly PasswordEncripter _passwordEncripter;
    private readonly IUnitOfWork _unityOfWork;

    public RegisterUserUseCase(
        IUserWriteOnlyRepository writeOnlyRepository,
        IUserReadOnlyRepository readOnlyRepository,
        IMapper mapper,
        PasswordEncripter passwordEncripter,
        IUnitOfWork unityOfWork)
    {
        _writeOnlyRepository = writeOnlyRepository;
        _readOnlyRepository = readOnlyRepository;
        _mapper = mapper;
        _passwordEncripter = passwordEncripter;
        _unityOfWork = unityOfWork;
    }

    public async Task<ResponseRegisteredUserJson> ExecuteAsync(RequestRegisterUserJson request) 
    {
        await ValidateAsync(request);

        var user = _mapper.Map<Domain.Entities.User>(request);
        
        user.Password = _passwordEncripter.Encrypt(request.Password);

        await _writeOnlyRepository.Add(user);
        await _unityOfWork.Commit();
        return new ResponseRegisteredUserJson { Name = request.Name};
    }

    private async Task ValidateAsync(RequestRegisterUserJson request)
    {
        var validator = new RegisterUserValidator();
        var result = await validator.ValidateAsync(request);

        bool existEmail =  await _readOnlyRepository.ExistActiveUserWithEmail(request.Email);
        if (existEmail)
            result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, ResourceMessageExceptions.EMAIL_ALREADY_EXISTS));


        if(!result.IsValid)
        {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage);

            throw new ErrorOnValidationException(errorMessages.ToList());

        }

    }
}
