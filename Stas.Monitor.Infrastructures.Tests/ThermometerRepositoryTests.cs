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
    mockedReader.GetReadedConfiguration().Returns(new Dictionary<string, IDictionary<string, string>>()
    {
      {
        "general", new Dictionary<string, string>()
        {
          { "Thermometer 1", "COM1" },
          { "Thermometer 2", "COM2" }
        }
      }
    });
    var repository = new ThermometerRepository(mockedReader);
    var actual = repository.AllThermometers;
    Assert.AreEqual(expected, actual);
  }
  
  
  
  /*
   [Test]
  public void Should_Provide_His_View_With_Thermometer_Name()
  {
    var baahh = new string[] { "Thermometer 1", "Thermometer 2" };
    var mockedView = Substitute.For<IMainView>();
    var mockedRepository = Substitute.For<IThermometerRepository>();
    mockedRepository.AllThermometers.Returns(baahh);
    var presenter = new MainPresenter(mockedView, mockedRepository);

    presenter.Start();
    
    mockedView.Received(1).ThermometerNames = baahh;
  }
  */
  
}