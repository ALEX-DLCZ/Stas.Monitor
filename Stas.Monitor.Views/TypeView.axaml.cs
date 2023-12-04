using System;
using Avalonia.Controls;
using Avalonia.Media;
using Stas.Monitor.Presentations;

namespace Stas.Monitor.Views;

public partial class TypeView : UserControl
{

    public TypeView(ISievedType sievedType, SolidColorBrush color)
    {
        InitializeComponent();
        TypeName.Text = sievedType.GetTypeName();

        foreach ( var info in sievedType.GetInfos() )
        {
            InfoViewItems.Items.Add(new InfoView(info, color));
        }

    }
}

