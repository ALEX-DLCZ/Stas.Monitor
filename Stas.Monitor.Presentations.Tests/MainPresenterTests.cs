using NSubstitute;
using Stas.Monitor.Domains;

namespace Stas.Monitor.Presentations.Tests;

public class MainPresenterTests
{
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
  
}