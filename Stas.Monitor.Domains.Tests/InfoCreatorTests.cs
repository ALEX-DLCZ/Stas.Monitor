using System.Collections;
using NSubstitute;

namespace Stas.Monitor.Domains.Tests;

public class InfoCreatorTests
{
  /*
salon;2023-10-19 13:43:52;C;18.50
salon;2023-10-19 13:43:50;C;35.00
cuisine;2023-10-19 13:43:47;C;21.50
salon;2023-10-19 13:43:35;C;20.00
chambre;2023-10-19 13:43:26;C;19.50
chambre;2023-10-19 13:43:14;C;19.50
salon;2023-10-19 13:43:05;C;19.50
salon;2023-10-19 13:42:55;C;18.00
salon;2023-10-18 13:35:54;C;5.00
salon;2023-10-18 12:21:52;C;18.50

salon;2023-10-19 13:43:52;18.50;6.50
salon;2023-10-19 13:43:50;35.00;10.00
cuisine;2023-10-19 13:43:47;21.50;6.50
salon;2023-10-19 13:43:35;20.00;00.00
chambre;2023-10-19 13:43:26;19.50;01.73
chambre;2023-10-19 13:43:14;19.50;6.50
salon;2023-10-19 13:43:05;19.50;00.00
salon;2023-10-19 13:42:55;18.00;02.50
salon;2023-10-18 13:35:54;5.00;00.00
salon;2023-10-18 12:21:52;18.50;6.50

   */

  private Queue<List<string>> _mesures = new Queue<List<string>>(new List<List<string>>
  {
    new List<string> { "salon", "2023-10-19 13:43:52", "C", "18.50" },
    new List<string> { "salon", "2023-10-19 13:43:50", "C", "35.00" },
    new List<string> { "cuisine", "2023-10-19 13:43:47", "C", "21.50" },
    new List<string> { "salon", "2023-10-19 13:43:35", "C", "20.00" },
    new List<string> { "chambre", "2023-10-19 13:43:26", "C", "19.50" },
    new List<string> { "chambre", "2023-10-19 13:43:14", "C", "19.50" },
    new List<string> { "salon", "2023-10-19 13:43:05", "C", "19.50" },
    new List<string> { "salon", "2023-10-19 13:42:55", "C", "18.00" },
    new List<string> { "salon", "2023-10-18 13:35:54", "C", "5.00" },
    new List<string> { "salon", "2023-10-18 12:21:52", "C", "18.50" },
  });

  private Queue<List<string>> _alertes = new Queue<List<string>>(new List<List<string>>
  {
    new List<string> {"salon", "2023-10-19 13:43:52", "18.50", "6.50"},
    new List<string> {"salon", "2023-10-19 13:43:50", "35.00", "10.00"},
    new List<string> {"cuisine", "2023-10-19 13:43:47", "21.50", "6.50"},
    new List<string> {"salon", "2023-10-19 13:43:35", "20.00", "00.00"},
    new List<string> {"chambre", "2023-10-19 13:43:26", "19.50", "01.73"},
    new List<string> {"chambre", "2023-10-19 13:43:14", "19.50", "6.50"},
    new List<string> {"salon", "2023-10-19 13:43:05", "19.50", "00.00"},
    new List<string> {"salon", "2023-10-19 13:42:55", "18.00", "02.50"},
    new List<string> {"salon", "2023-10-18 13:35:54", "5.00", "00.00"},
    new List<string> {"salon", "2023-10-18 12:21:52", "18.50", "6.50"},
    
  });
  
  
  [Test]
  public void GetInfos__WhenIInfoReaderReturnCorrectInformations_ShouldReturnListOfIInfos()
  {
    var readerMesure = Substitute.For<IInfoReader>();
    var readerAlerte = Substitute.For<IInfoReader>();
    
    
    readerMesure.GetInfo().Returns(_mesures);
    readerAlerte.GetInfo().Returns(_alertes);
    
    var creator = new InfoCreator(readerMesure, readerAlerte);
    var infos = creator.GetInfos();
    Assert.That(infos.Count, Is.EqualTo(20));
    //vérifie que _readerMesure.GetInfo() et _readerAlerte.GetInfo() ont été appelés une fois
    readerMesure.Received(1).GetInfo();
    readerAlerte.Received(1).GetInfo();
    //vérifie que _readerMesure.GetInfo() et _readerAlerte.GetInfo() ne possèdent plus d'éléments
    Assert.That(readerMesure.GetInfo().Count, Is.EqualTo(0));
    Assert.That(readerAlerte.GetInfo().Count, Is.EqualTo(0));
  }
  
  
  [Test]
  public void UpdateInfoMesure_WhenIInfoReaderReturnCorrectInformations_ShouldReturnInfoMesure()
  {
    var readerMesure = Substitute.For<IInfoReader>();
    var readerAlerte = Substitute.For<IInfoReader>();
    
    readerMesure.LastNewInfo().Returns(new List<string> {"salon", "2023-10-19 13:43:52", "C", "18.50"});
    
    var creator = new InfoCreator(readerMesure, readerAlerte);
    var info = creator.UpdateInfoMesure();
    var shouldReturn = new InfoMesure("salon", DateTime.Parse("2023-10-19 13:43:52"), "C", 18.50);
    Assert.That(info.GetInfo()[0], Is.EqualTo("salon"));
    Assert.That(info.GetInfo()[1], Is.EqualTo("19/10/2023 13:43:52"));
    Assert.That(info.GetInfo()[2], Is.EqualTo("C"));
    Assert.That(info.GetInfo()[3], Is.EqualTo("18,5"));
    Assert.IsInstanceOf<InfoMesure>(info);
  }
  
  [Test]
  public void UpdateInfoAlerte_WhenIInfoReaderReturnCorrectInformations_ShouldReturnInfoAlerte()
  {
    var readerMesure = Substitute.For<IInfoReader>();
    var readerAlerte = Substitute.For<IInfoReader>();
    
    readerAlerte.LastNewInfo().Returns(new List<string> {"salon", "2023-10-19 13:43:52", "18.50", "6.50"});
    
    var creator = new InfoCreator(readerMesure, readerAlerte);
    var info = creator.UpdateInfoAlerte();
    var shouldReturn = new InfoAlerte("salon", DateTime.Parse("2023-10-19 13:43:52"), 18.50, 6.50);
    Assert.That(info.GetInfo()[0], Is.EqualTo("salon"));
    Assert.That(info.GetInfo()[1], Is.EqualTo("19/10/2023 13:43:52"));
    Assert.That(info.GetInfo()[2], Is.EqualTo("18,5"));
    Assert.That(info.GetInfo()[3], Is.EqualTo("6,5"));
    Assert.IsInstanceOf<InfoAlerte>(info);
  }
}