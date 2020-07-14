using System.Windows;
using Zadatak_1.Model;
using Zadatak_1.ViewModel;

namespace Zadatak_1.View
{
    /// <summary>
    /// Interaction logic for EmployeeView.xaml
    /// </summary>
    public partial class EmployeeView : Window
    {
        public EmployeeView(int id)
        {
            InitializeComponent();
            this.DataContext = new EmployeeViewModel(this, id);
        }

        public EmployeeView(vwReport report)
        {
            InitializeComponent();
            this.DataContext = new EmployeeViewModel(this, report);
        }
    }
}
