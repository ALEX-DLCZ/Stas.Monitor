using NSubstitute;

namespace Stas.Monitor.Domains.Tests;

public class ConfigurationTests
{
  
  [Test]
  public void Should_Throw_KeyNotFoundException_When_Thermometers_Section_Is_Missing()
  {
    var readerMock = Substitute.For<IConfigurationReader>();
    readerMock.GetReadedConfiguration().Returns(new Dictionary<string, IDictionary<string, string>>());

    var config = new Configuration(readerMock);
    try{
    var thermometers = config.Thermometers;
    }
    catch (KeyNotFoundException e)
    {
      Assert.That(e.Message, Is.EqualTo("monitor: missing required section thermometers (general)"));
    }
  }
  
  
  [Test]
  public void Should_Throw_KeyNotFoundException_When_GetPaths_Section_Is_Missing()
  {
    var readerMock = Substitute.For<IConfigurationReader>();
    readerMock.GetReadedConfiguration().Returns(new Dictionary<string, IDictionary<string, string>>());

    var config = new Configuration(readerMock);
    try{
    var paths = config.GetPaths();
    }
    catch (KeyNotFoundException e)
    {
      Assert.That(e.Message, Is.EqualTo("monitor: missing required section paths"));
    }
  }
}