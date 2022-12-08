using System.Windows;

namespace TerentievCourseWork.Views.Controls;

public partial class GenreButtonView
{
    public GenreButtonView()
    {
        InitializeComponent();
    }

    public static readonly DependencyProperty IsSelectedProperty = DependencyProperty.Register(
        nameof(IsSelected),
        typeof(bool),
        typeof(GenreButtonView),
        new PropertyMetadata(default(bool)));

    public bool IsSelected
    {
        get { return (bool)GetValue(IsSelectedProperty); }
        set { SetValue(IsSelectedProperty, value); }
    }
}