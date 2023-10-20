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
  public void MainConfiguration_WhenFileIsNotIni_ShouldThrowException()
  {
    var path = "Resources/configNotIni.txt";
    Assert.Throws<Exception>(() => new MainConfigurationReader(path));
  }
  
  [Test]
  public void GetReadedConfigurationTest()
  {
    var path = "Resources\\config.ini";
    var reader = new MainConfigurationReader(path);
    var readedConfiguration = reader.GetReadedConfiguration();
    Assert.That(readedConfiguration.Count, Is.EqualTo(3));
    Assert.That(readedConfiguration.ContainsKey("cuisine"), Is.True);
    Assert.That(readedConfiguration.ContainsKey("salle de bain"), Is.True);
    Assert.That(readedConfiguration.ContainsKey("salon"), Is.True);
    Assert.That(readedConfiguration["cuisine"].Count, Is.EqualTo(2));
    Assert.That(readedConfiguration["salle de bain"].Count, Is.EqualTo(2));
    Assert.That(readedConfiguration["salon"].Count, Is.EqualTo(2));
    Assert.That(readedConfiguration["cuisine"].ContainsKey("path1"), Is.True);
    Assert.That(readedConfiguration["cuisine"].ContainsKey("path2"), Is.True);
    Assert.That(readedConfiguration["salle de bain"]["path1"], Is.EqualTo("\\INIFile\\cs file\\mesures\\salle de bain.csv"));
  }
  
}