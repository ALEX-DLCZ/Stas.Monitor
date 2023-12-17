using Stas.Monitor.Domains;

namespace Stas.Monitor.Presentations.Tests;

public class MeasurePresenterModelTest
{

    [Test]
    public void MeasurePresenterModelTest_WhenMeasureRecordIsTemperature_ThenColorIsOrange()
    {
        // Arrange
        var measureRecord = new MeasureRecord("name", "temperature", DateTime.Now, new Measure(0, 0, "0.00"));
        var measurePresenterModel = new MeasurePresenterModel(measureRecord);

        // Act
        var color = measurePresenterModel.Color;

        // Assert
        Assert.That(color, Is.EqualTo("0xFFA500"));
    }

    [Test]
    public void MeasurePresenterModelTest_WhenMeasureRecordIsHumidity_ThenColorIsBlue()
    {
        // Arrange
        var measureRecord = new MeasureRecord("name", "humidity", DateTime.Now, new Measure(0, 0, "0.00"));
        var measurePresenterModel = new MeasurePresenterModel(measureRecord);

        // Act
        var color = measurePresenterModel.Color;

        // Assert
        Assert.That(color, Is.EqualTo("0x0000FF"));
    }

    [Test]
    public void MeasurePresenterModelTest_WhenMeasureRecordIsOther_ThenColorIsCyan()
    {
        // Arrange
        var measureRecord = new MeasureRecord("name", "other", DateTime.Now, new Measure(0, 0, "0.00"));
        var measurePresenterModel = new MeasurePresenterModel(measureRecord);

        // Act
        var color = measurePresenterModel.Color;

        // Assert
        Assert.That(color, Is.EqualTo("0x00FFFF"));
    }

    [Test]
    public void MeasurePresenterModelTest_WhenMeasureRecordIsTemperature_ThenTypeIsTemperature()
    {
        // Arrange
        var measureRecord = new MeasureRecord("name", "temperature", DateTime.Now, new Measure(0, 0, "0.00"));
        var measurePresenterModel = new MeasurePresenterModel(measureRecord);

        // Act
        var type = measurePresenterModel.Type;

        // Assert
        Assert.That(type, Is.EqualTo("temperature"));
    }

    [Test]
    public void Should_Return_0_When_Difference_Is_0()
    {
        // Arrange
        var measureRecord = new MeasureRecord("name", "temperature", DateTime.Now, new Measure(0, 0, "0.00"));
        var measurePresenterModel = new MeasurePresenterModel(measureRecord);

        // Act
        var difference = measurePresenterModel.Difference;

        // Assert
        Assert.That(difference, Is.EqualTo("0"));
    }

    [Test]
    public void Should_Return_Difference_Formated_When_Difference_Is_Not_0()
    {
        // Arrange
        var measureRecord = new MeasureRecord("name", "temperature", DateTime.Now, new Measure(0, 0.5, "0%"));
        var measurePresenterModel = new MeasurePresenterModel(measureRecord);

        // Act
        var difference = measurePresenterModel.Difference;

        // Assert
        Assert.That(difference, Is.EqualTo("50%"));
    }

    [Test]
    public void Should_Return_Date_Formated()
    {
        // Arrange
        var measureRecord = new MeasureRecord("name", "temperature", new DateTime(2021, 01, 01, 12, 0, 0), new Measure(0, 0, "0.00"));
        var measurePresenterModel = new MeasurePresenterModel(measureRecord);

        // Act
        var date = measurePresenterModel.Date;

        // Assert
        Assert.That(date, Is.EqualTo("01/01/2021 12:00:00"));
    }

    [Test]
    public void Should_Return_Value_Formated()
    {
        // Arrange
        var measureRecord = new MeasureRecord("name", "temperature", DateTime.Now, new Measure(0.5, 0, "00.00°"));
        var measurePresenterModel = new MeasurePresenterModel(measureRecord);

        // Act
        var value = measurePresenterModel.Value;

        // Assert
        Assert.That(value, Is.EqualTo("00,50°"));
    }

    [Test]
    public void Should_Return_Value_Formated_When_Value_Is_0()
    {
        // Arrange
        var measureRecord = new MeasureRecord("name", "temperature", DateTime.Now, new Measure(0, 0, "00.00°"));
        var measurePresenterModel = new MeasurePresenterModel(measureRecord);

        // Act
        var value = measurePresenterModel.Value;

        // Assert
        Assert.That(value, Is.EqualTo("00,00°"));
    }

}
