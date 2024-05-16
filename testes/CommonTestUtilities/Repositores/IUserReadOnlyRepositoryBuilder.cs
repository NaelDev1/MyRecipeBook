using Moq;
using MyRecipeBook.Domain.Repositories.User;

namespace CommonTestUtilities.Repositores;

public class IUserReadOnlyRepositoryBuilder
{
    private readonly IUserReadOnlyRepository _instance;
    public IUserReadOnlyRepositoryBuilder() => _instance = new Mock<IUserReadOnlyRepository>().Object;


    public IUserReadOnlyRepository Build() => _instance;
}
