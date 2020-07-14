using System.Windows;
using Zadatak_1.ViewModel;

namespace Zadatak_1.View
{
    /// <summary>
    /// Interaction logic for ReadOnlyView.xaml
    /// </summary>
    public partial class ReadOnlyView : Window
    {
        public ReadOnlyView()
        {
            InitializeComponent();
            this.DataContext = new ReadOnlyViewModel(this);
        }
    }
}
