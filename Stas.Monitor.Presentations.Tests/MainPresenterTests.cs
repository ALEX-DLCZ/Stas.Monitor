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

  [Test]
  public void Should_Provide_His_View_With_SelectedThermometer_Infos()
  {
    var Info1 = new InfoAlerte("Thermometer 1", DateTime.Now, 1, 1);
    var Info2 = new InfoMesure("Thermometer 1", DateTime.Now, "Mesure", 1);
    var Info3 = new InfoMesure("Thermometer 1", DateTime.Now, "Mesure", 1);
    var baaahh = new LinkedList<IInfo>();
    baaahh.AddLast(Info1);
    baaahh.AddLast(Info2);
    baaahh.AddLast(Info3);
    var mockedView = Substitute.For<IMainView>();
    var mockedRepository = Substitute.For<IThermometerRepository>();
    mockedRepository.AllInfos(1).Returns(baaahh);
    var presenter = new MainPresenter(mockedView, mockedRepository);
    
    presenter.ThermometerSelected(1);
    
    mockedView.Received(1).InfosThermometer = Arg.Any<LinkedList<string[]>>();
    
  }
}