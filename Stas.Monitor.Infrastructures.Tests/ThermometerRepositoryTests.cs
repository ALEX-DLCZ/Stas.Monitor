using NSubstitute;
using Stas.Monitor.Domains;

namespace Stas.Monitor.Infrastructures.Tests;

public class ThermometerRepositoryTests
{
  [SetUp]
  public void Setup()
  {
  }

  [Test]
  public void AllThermometers_WhenConfigFileIsCorrect_ShouldReturnStringOfThermometersName()
  {
    var expected = new string[] { "COM1", "COM2" };
    var mockedReader = Substitute.For<IConfigurationReader>();
    mockedReader.GetReadedConfiguration().Returns(
      new Dictionary<string, IDictionary<string, string>>()
      {
        {
          "general",
          new Dictionary<string, string>()
          {
            { "Thermometer 1", "COM1" }, { "Thermometer 2", "COM2" }
          }
        },
        {
          "paths",
          new Dictionary<string, string>()
          {
            { "mesure", "Resources/mesures.csv" }, { "alerte", "Resources/alertes.csv" }
          }
        }
      });
    
    var repository = new ThermometerRepository(mockedReader);
    var actual = repository.AllThermometers;
    Assert.That(actual, Is.EqualTo(expected));
  }
  [Test]
  
  public void AllThermometers_WhenConfigFileIsCorrect_ShouldReturnLinkedListOfIInfos()
  {
    var mockedReader = Substitute.For<IConfigurationReader>();
    mockedReader.GetReadedConfiguration().Returns(
      new Dictionary<string, IDictionary<string, string>>()
      {
        {
          "general",
          new Dictionary<string, string>()
          {
            { "Thermometer 1", "chambre" }, { "Thermometer 2", "salon" }
          }
        },
        {
          "paths",
          new Dictionary<string, string>()
          {
            { "mesure", "Resources/mesures.csv" }, { "alerte", "Resources/alertes.csv" }
          }
        }
      });
    
    var repository = new ThermometerRepository(mockedReader);
    var actual = repository.AllInfos(0);
    Assert.That(actual.Count, Is.EqualTo(4));
    Assert.That(actual.First.Value.GetInfoForView(), Is.EqualTo(new string[] { "19,5", "19/10/2023 13:43:14" }));


  }
}