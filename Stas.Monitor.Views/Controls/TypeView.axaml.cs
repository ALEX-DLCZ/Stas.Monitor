using Avalonia.Controls;

namespace Stas.Monitor.Views.Controls;

public partial class TypeView : UserControl
{
    public TypeView()
    {
        InitializeComponent();
    }

    public string ViewTypeName
    {
        set => TypeName.Text = value.ToUpper();
    }

    public void AddInfoView(InfoView infoView) => InfoViewItems.Items.Insert(0, infoView);

    public void Reset()
    {
        TypeName.Text = string.Empty;

        InfoViewItems.Items.Clear();
    }
}
