using NSubstitute;

namespace Stas.Monitor.Domains.Tests;

public class ConfigurationTests
{
    [Test]
    public void Should_Throw_KeyNotFoundException_When_Thermometers_Section_Is_Missing()
    {
        var readerMock = Substitute.For<IConfigurationReader>();
        readerMock.GetReadedConfiguration().Returns(new Dictionary<string, IDictionary<string, string>>());

        try
        {
            var config = new Configuration(readerMock);
        }
        catch (KeyNotFoundException e)
        {
            Assert.That(e.Message, Is.EqualTo("monitor: missing required section thermometers (general)"));
        }
    }

    [Test]
    public void Should_Throw_KeyNotFoundException_When_GetBd_Section_Is_Missing()
    {
        var readerMock = Substitute.For<IConfigurationReader>();
        var dico = new Dictionary<string, IDictionary<string, string>>();
        dico.Add("general", new Dictionary<string, string>());

        readerMock.GetReadedConfiguration().Returns(dico);

        try
        {
            var config = new Configuration(readerMock);
        }
        catch (KeyNotFoundException e)
        {
            Assert.That(e.Message, Is.EqualTo("monitor: missing required section paths"));
        }
    }

    [Test]
    public void Should_Return_Section_GoodWay()
    {
        var readerMock = Substitute.For<IConfigurationReader>();
        var dico = new Dictionary<string, IDictionary<string, string>>();
        dico.Add("general", new Dictionary<string, string>());
        dico["general"].Add("test", "test");

        dico.Add("BD", new Dictionary<string, string>());
        dico["BD"].Add("test", "test");

        readerMock.GetReadedConfiguration().Returns(dico);

        var config = new Configuration(readerMock);
        var general = config.GetGeneral();
        Assert.That(general, Is.EqualTo(dico["general"]));

        var bd = config.GetBb();
        Assert.That(bd, Is.EqualTo(dico["BD"]));
    }
}
