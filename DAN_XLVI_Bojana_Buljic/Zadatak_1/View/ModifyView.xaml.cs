using System.Windows;
using Zadatak_1.ViewModel;

namespace Zadatak_1.View
{
    /// <summary>
    /// Interaction logic for ModifyView.xaml
    /// </summary>
    public partial class ModifyView : Window
    {
        public ModifyView()
        {
            InitializeComponent();
            this.DataContext = new ModifyViewModel(this);
        }
    }
}
