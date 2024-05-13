using MyRecipeBook.Domain.Repositories;

namespace MyRecipeBook.Infrastructure.DataAccess;

public  class UnitOfWork : IUnitOfWork
{
    private readonly MyRecipeBookDbContext _dbContext;
    public UnitOfWork(MyRecipeBookDbContext dbContext)
    {
        _dbContext = dbContext;        
    }
    //fazendo commit depois de tudas operações ralizadas com suces
    public async Task Commit() => await _dbContext.SaveChangesAsync();

}
