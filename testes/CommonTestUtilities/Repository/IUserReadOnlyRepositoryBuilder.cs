using Moq;
using MyRecipeBook.Domain.Repositories.User;

namespace CommonTestUtilities.Repositores;

public class IUserReadOnlyRepositoryBuilder
{
    private readonly Mock<IUserReadOnlyRepository> _instance;
    public IUserReadOnlyRepositoryBuilder() => _instance = new Mock<IUserReadOnlyRepository>();


    public void ExistActiveUserWithEmail(string email)
    {
        _instance.Setup(repository => repository.ExistActiveUserWithEmail(email)).ReturnsAsync(true);
    }

    public IUserReadOnlyRepository Build() => _instance.Object;
}
