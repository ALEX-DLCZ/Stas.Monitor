﻿using Stas.Monitor.Domains;

namespace Stas.Monitor.Presentations;

public class MainPresenter
{
    private readonly IMainView _view;
    private readonly IThermometerRepository _repository;
    private FilterEventArgs _args;

    public MainPresenter(IMainView view, IThermometerRepository repository)
    {
        _view = view;
        _repository = repository;
    }

    public void Start()
    {
        _view.FilterChanged += OnQueryChanged;

        _view.ThermometersNames = _repository.AllThermometers;

        _view.Types = _repository
            .NewRequest()
            .SelectDistinct("type");
    }

    private void OnQueryChanged(object? sender, FilterEventArgs args)
    {
        _args = args;
        var type = new HashSet<string>(args.Types);

        var request = _repository
            .NewRequest();

        _view.Result = request.Where("thermometerName", val => $"LIKE '%{val}%'", args.ThermometerTarget)
            .Where("type", val => $"IN ('{val}')", string.Join("','", args.Types))
            .Where("datetime", val => $">= (SELECT MAX(datetime) FROM Mesures) - INTERVAL {val} SECOND", args.TimeSelected)
            .Select(mesure => new MeasurePresenterModel(mesure)).ToList();
    }

    public void Update()
    {
        var request = _repository
            .NewRequest();

        _view.UpdateResult = request.Where("thermometerName", val => $"LIKE '%{val}%'", _args.ThermometerTarget)
            .Where("type", val => $"IN ('{val}')", string.Join("','", _args.Types))
            .WhereUpdate()
            .Select(mesure => new MeasurePresenterModel(mesure)).ToList();
    }
}
