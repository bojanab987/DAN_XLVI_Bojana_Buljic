using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zadatak_1.Model;
using Zadatak_1.Services;
using Zadatak_1.View;

namespace Zadatak_1.ViewModel
{
    class EmployeeViewModel:ViewModelBase
    {
        EmployeeView employeeView;
        Service service;

        public EmployeeViewModel(EmployeeView viewOpen, int id)
        {
            employeeView = viewOpen;
            service = new Service();
            Report.EmployeeId = id;
        }

        private tblReport report;
        public tblReport Report
        {
            get { return report; }
            set
            {
                report = value;
                OnPropertyChanged("Report");
            }
        }
    }
}
