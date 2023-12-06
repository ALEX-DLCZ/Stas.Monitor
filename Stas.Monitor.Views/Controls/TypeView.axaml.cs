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
        // ResultPanel.Children.Add(infoView);
        InfoViewItems.Items.Add(infoView);
    }

    // public MeasurePresenterModel ViewModel
    // {
    //     set
    //     {
    //         NumBlock.Text = value.Num;
    //         NameBlock.Text = value.Name;
    //         LabelsPanel.Children.Clear();
    //         foreach (var text in value.Labels)
    //         {
    //             LabelsPanel.Children.Add(new TextBlock
    //             {
    //                 Text = text,
    //                 FontSize = 10
    //             });
    //         }
    //     }
    // }

    public void Reset()
    {
        TypeName.Text = string.Empty;
        // NumBlock.Text = string.Empty;
        // NameBlock.Text = string.Empty;
        // LabelsPanel.Children.Clear();

        InfoViewItems.Items.Clear();
        _queryResult.Clear();

    }




    // public TypeView(ISievedType sievedType, SolidColorBrush color)
    // {
    //     InitializeComponent();
    //     TypeName.Text = sievedType.GetTypeName();
    //
    //     foreach ( var info in sievedType.GetInfos() )
    //     {
    //         InfoViewItems.Items.Add(new InfoView(info, color));
    //     }
    //
    // }

}

