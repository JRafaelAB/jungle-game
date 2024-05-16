using Domain.Utils;
using Xunit;

namespace UnitTests.Utils;

public class ValidationUtilsTest
{
    [Theory]
    [InlineData(10)]
    [InlineData("10")]
    [InlineData(10.0)]
    [InlineData('c')]
    public void Test_ValidateNullArgument_Valid_Object(object? obj)
    {
        obj.ValidateNullArgument(nameof(obj));
    }
        
    [Fact]
    public void Test_ValidateNullArgument_Null_Object()
    {
        object? obj = null;
        Assert.Throws<ArgumentException>(() => obj.ValidateNullArgument(nameof(obj)));
    }
        
        
    [Theory]
    [InlineData("Hello World")]
    [InlineData("ahuhuhashashsauksaushushaksahksa")]
    [InlineData("c")]
    public void Test_ValidateStringArgumentNotEmpty_Valid_String(string argument)
    {
        argument.ValidateStringArgumentNotNullOrEmpty(nameof(argument));
    }
        
    [Fact]
    public void Test_ValidateStringArgumentNotEmpty_Empty_String()
    {
        string argument = string.Empty;
        Assert.Throws<ArgumentException>(() => argument.ValidateStringArgumentNotNullOrEmpty(nameof(argument)));
    }
        
}
