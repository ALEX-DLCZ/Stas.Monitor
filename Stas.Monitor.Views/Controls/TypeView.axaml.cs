using System;
using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Media;
using Stas.Monitor.Presentations;

namespace Stas.Monitor.Views.Controls;

public partial class TypeView : UserControl
{
    private readonly IList<InfoView> _queryResult = new List<InfoView>();


    public TypeView()
    {
        InitializeComponent();
    }

    public string ViewTypeName
    {
        set => TypeName.Text = value.ToUpper();
    }

    public void AddInfoView(InfoView infoView)
    {
        _queryResult.Add(infoView);
        InfoViewItems.Items.Add(infoView);
    }

    public void Reset()
    {
        TypeName.Text = string.Empty;

        InfoViewItems.Items.Clear();
        _queryResult.Clear();
    }
}
