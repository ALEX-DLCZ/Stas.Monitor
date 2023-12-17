using NSubstitute;
using Stas.Monitor.Domains;

namespace Stas.Monitor.Presentations.Tests;

public class MainPresenterTest
{
    [Test]
    public void MainPresenterTest_WhenStart_ThenFilterChangedIsCalled()
    {
        // Arrange
        var view = Substitute.For<IMainView>();
        var repository = Substitute.For<IThermometerRepository>();
        var presenter = new MainPresenter(view, repository);

        // Act
        presenter.Start();

        // Assert
        view.Received(1).FilterChanged += Arg.Any<EventHandler<FilterEventArgs>>();
    }

    [Test]
    public void MainPresenterTest_WhenStart_ThenThermometersNamesIsCalled()
    {
        // Arrange
        var view = Substitute.For<IMainView>();
        var repository = Substitute.For<IThermometerRepository>();
        var presenter = new MainPresenter(view, repository);

        // Act
        presenter.Start();

        // Assert
        view.Received(1).ThermometersNames = repository.AllThermometers;
        repository.ReceivedCalls();
    }

    [Test]
    public void MainPresenterTest_WhenStart_ThenTypesIsCalled()
    {
        // Arrange
        var view = Substitute.For<IMainView>();
        var repository = Substitute.For<IThermometerRepository>();
        var presenter = new MainPresenter(view, repository);

        // Act
        presenter.Start();

        // Assert
        view.Received(1).Types = repository
            .NewRequest()
            .SelectDistinct("type");
        repository.Received(2).NewRequest();
    }

    [Test]
    public void MainPresenterTest_WhenOnQueryChanged_ThenWhereIsCalled()
    {
        // Arrange
        var view = Substitute.For<IMainView>();
        var repository = Substitute.For<IThermometerRepository>();
        var request = Substitute.For<IRequest>();
        repository.NewRequest().Returns(request);
        request.WhereUpdate().Returns(request);
        request.Where(Arg.Any<string>(), Arg.Any<Func<string, string>>(), Arg.Any<string>()).Returns(request);
        var presenter = new MainPresenter(view, repository);
        var args = new FilterEventArgs(new List<string>() { "temperature" }, "name", 60);


        // Act
        presenter.Start();
        presenter.OnQueryChanged(null, args);

        // Assert
        repository.Received(2).NewRequest();
        request.Received(2).Where(Arg.Any<string>(), Arg.Any<Func<string, string>>(), Arg.Any<string>());
        view.Received(1).Result = Arg.Any<List<MeasurePresenterModel>>();
        request.Received(0).WhereUpdate();
    }

    [Test]
    public void MainPresenterTest_WhenUpdate_ThenWhereUpdateIsCalled()
    {
        // Arrange
        var view = Substitute.For<IMainView>();
        var repository = Substitute.For<IThermometerRepository>();
        var request = Substitute.For<IRequest>();
        repository.NewRequest().Returns(request);
        request.WhereUpdate().Returns(request);
        request.Where(Arg.Any<string>(), Arg.Any<Func<string, string>>(), Arg.Any<string>()).Returns(request);
        var presenter = new MainPresenter(view, repository);
        var args = new FilterEventArgs(new List<string>() { "temperature" }, "name", 60);

        // Act
        presenter.Start();
        presenter.OnQueryChanged(null, args);
        presenter.Update();

        // Assert
        repository.Received(3).NewRequest();
        request.Received(4).Where(Arg.Any<string>(), Arg.Any<Func<string, string>>(), Arg.Any<string>());
        view.Received(1).Result = Arg.Any<List<MeasurePresenterModel>>();
        request.Received(1).WhereUpdate();
    }
}
