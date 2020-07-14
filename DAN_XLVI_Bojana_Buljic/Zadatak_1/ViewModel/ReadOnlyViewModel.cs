using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Zadatak_1.Commands;
using Zadatak_1.Model;
using Zadatak_1.Services;
using Zadatak_1.View;

namespace Zadatak_1.ViewModel
{
    class ReadOnlyViewModel:ViewModelBase
    {
        ReadOnlyView readOnly;
        Service service;

        #region Property
        private List<tblEmployee> employeeList;
        public List<tblEmployee> EmployeeList
        {
            get
            {
                return employeeList;
            }
            set
            {
                employeeList = value;
                OnPropertyChanged("EmployeesList");
            }
        }        

        private Visibility employeesView = Visibility.Visible;
        public Visibility EmployeesView
        {
            get
            {
                return employeesView;
            }
            set
            {
                employeesView = value;
                OnPropertyChanged("EmployeesView");
            }
        }
        #endregion

        #region constructor
        public ReadOnlyViewModel(ReadOnlyView readOnlyOpen)
        {
            readOnly = readOnlyOpen;
            service = new Service();
            EmployeeList = service.GetAllEmployees().ToList();
        }
        #endregion

        #region Commands
        /// <summary>
        /// LogOut Command
        /// </summary>
        private ICommand logOut;
        public ICommand LogOut
        {
            get
            {
                if (logOut == null)
                {
                    logOut = new RelayCommand(param => LogOutExecute(), param => CanLogOutExecute());
                }
                return logOut;
            }
        }

        /// <summary>
        /// Method for logging out admin from app
        /// </summary>  
        private void LogOutExecute()
        {
            try
            {
                readOnly.Close();
                LogInView newLogin = new LogInView();
                newLogin.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        
        /// <summary>
        /// Method check if logout is possible to be Executed
        /// </summary>
        private bool CanLogOutExecute()
        {
            return true;
        }
        #endregion
    }
}
