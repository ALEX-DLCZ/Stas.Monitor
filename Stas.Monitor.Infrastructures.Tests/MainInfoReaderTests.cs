namespace Stas.Monitor.Infrastructures.Tests;

public class MainInfoReaderTests
{
  /*
   * Mesures:
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
    Assert.That(readedInfo.Count, Is.EqualTo(8));
    Assert.That(readedInfo.Peek().Count, Is.EqualTo(4));
    Assert.That(readedInfo.Peek()[0], Is.EqualTo("salon"));
    Assert.That(readedInfo.Peek()[1], Is.EqualTo("2023-10-19 13:43:52"));
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
    Assert.That(lastNewInfo[1], Is.EqualTo("2023-10-19 13:43:52"));
    Assert.That(lastNewInfo[2], Is.EqualTo("C"));
    Assert.That(lastNewInfo[3], Is.EqualTo("18.50"));
  }
}