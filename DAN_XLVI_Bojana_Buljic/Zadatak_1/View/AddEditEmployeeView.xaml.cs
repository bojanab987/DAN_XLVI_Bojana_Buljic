using System.Windows;
using Zadatak_1.Model;
using Zadatak_1.ViewModel;

namespace Zadatak_1.View
{
    /// <summary>
    /// Interaction logic for AddEditEmployeeView.xaml
    /// </summary>
    public partial class AddEditEmployeeView : Window
    {
        public AddEditEmployeeView()
        {
            InitializeComponent();
            this.DataContext = new AddEditEmployeeViewModel(this);
        }

        public AddEditEmployeeView(tblEmployee employee)
        {
            InitializeComponent();
            this.DataContext = new AddEditEmployeeViewModel(this,employee);
        }
    }
}
