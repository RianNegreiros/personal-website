using Backend.Application.Helpers;
using Xunit;

namespace Backend.Tests.Application.Helpers;

public class SlugHelperTests
{
    [Theory]
    [InlineData("Hello, World!", "hello-world")]
    [InlineData("This is a test.", "this-is-a-test")]
    [InlineData("Slûg Exåmplé", "slug-example")]
    [InlineData("", "")]
    [InlineData("----", "")]
    [InlineData(" Testing  Spaces  ", "testing-spaces")]
    [InlineData("ALL CAPS", "all-caps")]
    [InlineData("Mixed CaSe", "mixed-case")]
    [InlineData("123_456", "123-456")]
    [InlineData("Special #Chars!", "special-chars")]
    public void Slugify_ShouldGenerateCorrectSlugs(string input, string expected)
    {
        // Act
        string actual = SlugHelper.Slugify(input);

        // Assert
        Assert.Equal(expected, actual);
    }
}
