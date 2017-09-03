using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void ConfigureWithoutBindMethod()
    {
        var collection = new ServiceCollection();

        var config = new ConfigurationBuilder()
            .AddJsonFile("test.json", optional: false)
            .Build();

        collection.Configure<TestOptions>(config.GetSection("Test"));

        var services = collection.BuildServiceProvider();

        var options = services.GetService<IOptions<TestOptions>>();

        Assert.IsNotNull(options);
    }

    [TestMethod]
    public void ConfigureWithBindMethod()
    {
        var collection = new ServiceCollection();

        var config = new ConfigurationBuilder()
            .AddJsonFile("test.json", optional: false)
            .Build();

        collection.Configure<TestOptions>(o => config.GetSection("Test").Bind(o));

        var services = collection.BuildServiceProvider();

        var options = services.GetService<IOptions<TestOptions>>();

        Assert.IsNotNull(options);
    }

    [TestMethod]
    public void SectionIsAvailable()
    {
        var collection = new ServiceCollection();

        var config = new ConfigurationBuilder()
            .AddJsonFile("test.json", optional: false)
            .Build();

        var section = config.GetSection("Test");
        Assert.IsNotNull(section);
        Assert.AreEqual("yes", section["ItemOne"]);
    }
}

public class TestOptions
{
    public string ItemOne { get; set; }
}
}
