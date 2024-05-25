using System.Collections;

namespace WebApiTest.InlineData;

public class InlineDataCultureMensages : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[] { "pt-BR" };
        yield return new object[] { "en" };

    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
