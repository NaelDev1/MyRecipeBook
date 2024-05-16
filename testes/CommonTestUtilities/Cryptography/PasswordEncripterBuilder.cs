using MyRecipeBook.Application.Services.Cryptography;

namespace CommonTestUtilities.Cryptography;

public static class PasswordEncripterBuilder
{
    public static PasswordEncripter Build() => new PasswordEncripter("abcd");

}
