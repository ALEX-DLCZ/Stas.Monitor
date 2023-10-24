namespace Stas.Monitor.Infrastructures.Tests;

public class CsvInfoReaderTests
{
  private CsvInfoReader _csvInfoReader;


  [SetUp]
  public void Setup()
  {
    _csvInfoReader = new CsvInfoReader();
  }

  [Test]
  public void ReadLine_WhenLineIsEmpty_ShouldNotAddToQueue()
  {
    _csvInfoReader.ReadLine("");

    var info = _csvInfoReader.GetInfo();

    Assert.That(info.Count, Is.EqualTo(0));
  }

  [Test]
  public void ReadLine_WhenLineIsNotEmpty_ShouldAddToQueue()
  {
    _csvInfoReader.ReadLine("thermometre1;2021-10-01 00:00:00;C;20");

    var info = _csvInfoReader.GetInfo();

    Assert.That(info.Count, Is.EqualTo(1));
    Assert.That(info.Peek().Count, Is.EqualTo(4));
    Assert.That(info.Peek()[0], Is.EqualTo("thermometre1"));
    Assert.That(info.Peek()[1], Is.EqualTo("2021-10-01 00:00:00"));
    Assert.That(info.Peek()[2], Is.EqualTo("C"));
    Assert.That(info.Peek()[3], Is.EqualTo("20"));
  }
  
  [Test]
  public void ReadSoloLine_WhenLineIsNotEmpty_ShouldReturnList()
  {
    var line = "thermometre1;2021-10-01 00:00:00;C;20";
    
    var info = _csvInfoReader.GetSoloLine(line);

    Assert.That(info.Count, Is.EqualTo(4));
    Assert.That(info[0], Is.EqualTo("thermometre1"));
    Assert.That(info[1], Is.EqualTo("2021-10-01 00:00:00"));
    Assert.That(info[2], Is.EqualTo("C"));
    Assert.That(info[3], Is.EqualTo("20"));
  }
    
}