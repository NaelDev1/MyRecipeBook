using Moq;
using MyRecipeBook.Domain.Repositories;

namespace CommonTestUtilities.Repositores;

public class IUnitOfWorkBuilder
{
    public static IUnitOfWork Build() => new Mock<IUnitOfWork>().Object;

}
