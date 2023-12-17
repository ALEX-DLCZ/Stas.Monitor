namespace Stas.Monitor.Infrastructures.Tests;


public class IniConfigurationReaderTests
{
    //Resources/config.ini =
    // [general]
    // thermometer1 = Living Room 1
    // thermometer2 = salon
    // thermometer3 = chambre
    // [BD]
    // IpServer= 192.168.132.200
    // PortServer= 13306
    // User= Q210007
    // Pws= 0007

    [Test]
    public void Should_Throw_Exception_When_File_Not_Found()
    {
        var iniConfigurationReader = new IniConfigurationReader();

        Assert.Throws<FileNotFoundException>(() => iniConfigurationReader.ExecuteConfigurationStrategy("Resources/NotExistconfig.ini"));
    }

    [Test]
    public void Should_Read_A_Section()
    {
        var iniConfigurationReader = new IniConfigurationReader();

        var sectionMaps = iniConfigurationReader.ExecuteConfigurationStrategy("Resources/config.ini");

        Assert.That(sectionMaps, Has.Count.EqualTo(2));
        Assert.Multiple(() =>
        {
            Assert.That(sectionMaps.ContainsKey("general"), Is.True);
            Assert.That(sectionMaps.ContainsKey("BD"), Is.True);
        });
    }

    [Test]
    public void Should_Read_A_Section_With_Keys_And_Values()
    {
        var iniConfigurationReader = new IniConfigurationReader();

        var sectionMaps = iniConfigurationReader.ExecuteConfigurationStrategy("Resources/config.ini");

        Assert.That(sectionMaps, Has.Count.EqualTo(2));
        Assert.That(sectionMaps["general"], Has.Count.EqualTo(3));
        Assert.Multiple(() =>
        {
            Assert.That(sectionMaps["general"].ContainsKey("thermometer1"), Is.True);
            Assert.That(sectionMaps["general"].ContainsKey("thermometer2"), Is.True);
            Assert.That(sectionMaps["general"].ContainsKey("thermometer3"), Is.True);
            Assert.That(sectionMaps["BD"], Has.Count.EqualTo(4));
        });
        Assert.Multiple(() =>
        {
            Assert.That(sectionMaps["BD"].ContainsKey("IpServer"), Is.True);
            Assert.That(sectionMaps["BD"].ContainsKey("PortServer"), Is.True);
            Assert.That(sectionMaps["BD"].ContainsKey("User"), Is.True);
            Assert.That(sectionMaps["BD"].ContainsKey("Pws"), Is.True);
        });
    }

}
