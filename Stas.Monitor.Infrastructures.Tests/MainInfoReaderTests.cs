namespace Stas.Monitor.Infrastructures.Tests;

public class MainInfoReaderTests
{
  /*
   * Mesures:
salon;2023-10-18 12:43:52;C;18.50
salon;2023-10-18 12:45:54;C;5.00
salon;2023-10-18 12:45:55;C;18.00
salon;2023-10-18 14:43:52;C;19.50
chambre;2023-10-18 14:43:55;C;19.50
chambre;2023-10-18 14:45:55;C;19.50
salon;2023-10-18 15:43:52;C;20.00
cuisine;2023-10-19 13:43:52;C;21.50
salon;2023-11-18 13:43:52;C;35.00
salon;2024-10-18 13:43:52;C;18.50
   */

  [Test]
  public void IsFileExistTest()
  {
    using var reader = File.OpenText("Resources/mesures.csv");
    Assert.DoesNotThrow(() => reader.ReadToEnd());
  }

  [Test]
  public void isFileExistTest2()
  {
    using var reader = File.OpenText("Resources/alertes.csv");
    Assert.DoesNotThrow(() => reader.ReadToEnd());
  }

  [Test]
  public void IsFileNOTExistTest()
  {
    var path = "Resources/fileNotExiste.csv";
    Assert.Throws<FileNotFoundException>(() => new MainInfoReader(path));
  }

  [Test]
  public void MainInfo_WhenFileIsNotCsv_ShouldThrowException()
  {
    var path = "Resources/configNotCsv.txt";
    Assert.Throws<FileNotFoundException>(() => new MainInfoReader(path));
  }

  [Test]
  public void GetReadedInfoTest()
  {
    var path = "Resources/mesures.csv";
    var reader = new MainInfoReader(path);
    var readedInfo = reader.GetInfo();
    Assert.That(readedInfo.Count, Is.EqualTo(10));
    Assert.That(readedInfo.Peek().Count, Is.EqualTo(4));
    Assert.That(readedInfo.Peek()[0], Is.EqualTo("salon"));
    Assert.That(readedInfo.Peek()[1], Is.EqualTo("2023-10-18 12:43:52"));
    Assert.That(readedInfo.Peek()[2], Is.EqualTo("C"));
    Assert.That(readedInfo.Peek()[3], Is.EqualTo("18.50"));
  }
  
  [Test]
  public void GetLastNewInfoTest()
  {
    var path = "Resources/mesures.csv";
    var reader = new MainInfoReader(path);
    var lastNewInfo = reader.LastNewInfo();
    Assert.That(lastNewInfo.Count, Is.EqualTo(4));
    Assert.That(lastNewInfo[0], Is.EqualTo("salon"));
    Assert.That(lastNewInfo[1], Is.EqualTo("2023-10-18 12:43:52"));
    Assert.That(lastNewInfo[2], Is.EqualTo("C"));
    Assert.That(lastNewInfo[3], Is.EqualTo("18.50"));
  }
  
  
}
