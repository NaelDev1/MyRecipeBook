using AutoMapper;
using MyRecipeBook.Application.Services.AutoMapper;

namespace CommonTestUtilities.Mapper;

public static class AutoMapperBuilder
{

    public static IMapper Build()
    {
        return new AutoMapper.MapperConfiguration(options =>
        {
            options.AddProfile<AutoMapping>();
        }).CreateMapper();
    }

}
