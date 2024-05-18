using Moq;
using MyRecipeBook.Domain.Repositories.User;

namespace CommonTestUtilities.Repositores;

public class IUserWriteOnlyRepositoryBuilder
{
    public static IUserWriteOnlyRepository Build() => new Mock<IUserWriteOnlyRepository>().Object;

}
