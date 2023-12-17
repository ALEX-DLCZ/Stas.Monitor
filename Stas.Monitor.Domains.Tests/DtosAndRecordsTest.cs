namespace Stas.Monitor.Domains.Tests;

public class DtosAndRecordsTest
{

    [Test]
    public void MeasureRecordTest()
    {
        var dateTime = DateTime.Now;
        var measureRecord = new MeasureRecord("test", "test", dateTime, new Measure(1, 1, "test"));
        Assert.Multiple(() =>
        {
            Assert.That(measureRecord.Name, Is.EqualTo("test"));
            Assert.That(measureRecord.Type, Is.EqualTo("test"));
            Assert.That(measureRecord.Date, Is.EqualTo(dateTime));
            Assert.That(measureRecord.Measure.Value, Is.EqualTo(1));
            Assert.That(measureRecord.Measure.Difference, Is.EqualTo(1));
            Assert.That(measureRecord.Measure.Format, Is.EqualTo("test"));
        });
    }

    [Test]
    public void ThermometerTest()
    {
        var thermometer = new Thermometer("test");
        Assert.That(thermometer.Name, Is.EqualTo("test"));
    }
}
