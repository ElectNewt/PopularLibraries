using FluentAssertions;

namespace PopularLibraries.Tests;

public class TestFluentAssertions
{
    [Fact]
    public void TestCode1()
    {
        string value1 = "Este es el valor de mi cadena de texto";
        bool booleanValue = true;

        Assert.Equal("Este es el valor de mi cadena de texto", value1);
        Assert.True(booleanValue);
    }

    [Fact]
    public void TestCode1b()
    {
        string value1 = "Este es el valor de mi cadena de texto";
        bool booleanValue = true;

        value1.Should().NotBeNull();
        value1.Should().Be("Este es el valor de mi cadena de texto");

        booleanValue.Should().BeTrue();
    }

    [Fact]
    public void TestCode2()
    {
        string value1 = "Este es el valor de mi cadena de texto";

        value1.Should().BeEquivalentTo("este es el valor de mi cadena de texto");
    }


    [Fact]
    public void TestDates()
    {
        DateOnly dt = new DateOnly(2000,01,01);
        dt.Should().BeBefore(DateOnly.FromDateTime(DateTime.UtcNow));
    }
    
    private record ExampleObj(string Name, int Age);

    [Fact]
    public void TestCode3()
    {
        List<ExampleObj> list = new List<ExampleObj>()
        {
            new ExampleObj("Name1", 24),
            new ExampleObj("Name2", 45),
            new ExampleObj("Name3", 30),
        };

        Assert.True(list.Any());
        ExampleObj firstElement = list.First();
        Assert.Equal("Name1", firstElement.Name);
        Assert.Equal(24, firstElement.Age);
    }

    [Fact]
    public void TestCode3b()
    {
        List<ExampleObj> list = new List<ExampleObj>()
        {
            new ExampleObj("Name1", 24),
            new ExampleObj("Name2", 45),
            new ExampleObj("Name3", 30),
        };

        list.Should().NotBeNull()
            .And.Contain(a => a.Name == "Name1")
            .Which.Age.Should().Be(24);
    }

    [Fact]
    public void TestCodeException()
    {
        Assert.Throws<NotImplementedException>(() => ThisMethodReturnsException());
    }
    
    [Fact]
    public void TestCodeExceptionFA()
    {
        Action action = () => ThisMethodReturnsException();
        action.Should().Throw<NotImplementedException>();
    }

    private int ThisMethodReturnsException()
    {
        throw new NotImplementedException();
    }
    
}