using NSubstitute;

namespace Stas.Monitor.Infrastructures.Tests;

public class IniConfigurationReaderTests
{
  private IniConfigurationReader _iniConfigurationReader;

  [SetUp]
  public void SetUp()
  {
    _iniConfigurationReader = new IniConfigurationReader();
  }

  [Test]
  public void ReadLine_WhenLineIsCommentOrEmpty_ShouldNotAddToSection()
  {
    _iniConfigurationReader.ReadLine("; Comment line");
    _iniConfigurationReader.ReadLine("");

    var sectionMaps = _iniConfigurationReader.GetSectionMaps();

    //Assert.Pass();
    Assert.That(sectionMaps.Count, Is.EqualTo(0));
    Assert.That(sectionMaps.ContainsKey("Section1"), Is.False);
  }

  [Test]
  public void ReadLine_WhenLineIsNewSection_ShouldCreateNewSection()
  {
    _iniConfigurationReader.ReadLine("[Section1]");

    var sectionMaps = _iniConfigurationReader.GetSectionMaps();

    Assert.That(sectionMaps.Count, Is.EqualTo(1));
    Assert.That(sectionMaps.ContainsKey("Section1"), Is.True);
  }

  [Test]
  public void ReadLine_WhenLineIsKeyValuePair_ShouldAddToCurrentSection()
  {
    _iniConfigurationReader.ReadLine("[Section1]");
    _iniConfigurationReader.ReadLine("Key1=Value1");

    var sectionMaps = _iniConfigurationReader.GetSectionMaps();
    var section1 = sectionMaps.Values.First();

    Assert.That(section1.Count, Is.EqualTo(1));
    Assert.That(section1.ContainsKey("Key1"), Is.True);
    Assert.That(section1["Key1"], Is.EqualTo("Value1"));
  }

  [Test]
  public void ReadLine_WhenLineIsInvalidSection_ShouldDoNothing()
  {
    _iniConfigurationReader.ReadLine("[Section1");
    Assert.That(_iniConfigurationReader.GetSectionMaps().Count, Is.EqualTo(0));
  }


  [Test]
  public void Should_Map_A_File_To_A_Configuration_Object()
  {
    using var reader = File.OpenText("Resources/config.ini");
    Assert.DoesNotThrow(() => reader.ReadToEnd());
  }
}