namespace Stas.Monitor.Domains.Tests;

public class InfosTests
{
  [Test]
  public void Should_Return_False_When_IsAlerte_Is_Called()
  {
    var info = new InfoMesure("test", DateTime.Now,"gds", 0);
    Assert.That(info.IsAlerte(), Is.False);
  }
  
  
  [Test]
  public void Should_Return_True_When_IsAlerte_Is_Called()
  {
    var info = new InfoAlerte("test", DateTime.Now, 0, 0);
    Assert.That(info.IsAlerte(), Is.True);
  }
}