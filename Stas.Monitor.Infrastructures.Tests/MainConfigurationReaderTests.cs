namespace Stas.Monitor.Infrastructures.Tests;

public class MainConfigurationReaderTests
{
  [Test]
  public void IsFileExistTest()
  {
    using var reader = File.OpenText("Resources/config.ini");
    Assert.DoesNotThrow(() => reader.ReadToEnd());
  }

  [Test]
  public void IsFileExistTest2()
  {
    using var reader = File.OpenText("Resources/configNotIni.txt");
    Assert.DoesNotThrow(() => reader.ReadToEnd());
  }

  [Test]
  public void IsFileNOTExistTest()
  {
    var path = "Resources/configNotExiste.ini";
    Assert.Throws<FileNotFoundException>(() => new MainConfigurationReader(path));
  }

  [Test]
  public void MainConfiguration_WhenFileIsNotIni_ShouldThrowException()
  {
    var path = "Resources/configNotIni.txt";
    Assert.Throws<FileNotFoundException>(() => new MainConfigurationReader(path));
  }

  [Test]
  public void GetReadedConfigurationTest()
  {
    var path = "Resources\\config.ini";
    var reader = new MainConfigurationReader(path);
    var readedConfiguration = reader.GetReadedConfiguration();
    Assert.That(readedConfiguration.Count, Is.EqualTo(2));
    Assert.That(readedConfiguration.ContainsKey("general"), Is.True);
    Assert.That(readedConfiguration.ContainsKey("paths"), Is.True);
    Assert.That(readedConfiguration["general"].Count, Is.EqualTo(3));
    Assert.That(readedConfiguration["paths"].Count, Is.EqualTo(2));
    Assert.That(readedConfiguration["general"].ContainsKey("thermometre1"), Is.True);
    Assert.That(readedConfiguration["paths"].ContainsKey("mesures"), Is.True);
    Assert.That(readedConfiguration["paths"]["mesures"],
      Is.EqualTo("\\INIFile\\CSVfile\\mesures.csv"));
  }
}