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
    class AddEditEmployeeViewModel:ViewModelBase
    {
        AddEditEmployeeView addEmployee;
        Service service;

        #region Properties
        private tblEmployee newEmployee;
        public tblEmployee NewEmployee
        {
            get
            {
                return newEmployee;
            }
            set
            {
                newEmployee = value;
                OnPropertyChanged("NewEmployee");
            }
        }

        private bool isUpdatedEmployee;
        public bool IsUpdatedEmployee
        {
            get
            {
                return isUpdatedEmployee;
            }
            set
            {
                isUpdatedEmployee = value;
            }
        }
        #endregion

        #region Constructors
        public AddEditEmployeeViewModel(AddEditEmployeeView openWindow)
        {
            addEmployee = openWindow;
            NewEmployee = new tblEmployee();
            service = new Service();
        }

        public AddEditEmployeeViewModel(AddEditEmployeeView open, tblEmployee editEmployee)
        {
            addEmployee = open;
            NewEmployee = editEmployee;
            service = new Service();
        }
        #endregion        

        #region Commands
        /// <summary>
        /// Save Command
        /// </summary>
        private ICommand save;
        public ICommand Save
        {
            get
            {
                if (save == null)
                {
                    save = new RelayCommand(param => SaveExecute(), param => CanSaveExecute());
                }
                return save;
            }
        }

        /// <summary>
        /// Method for executing save command...saving added employee
        /// </summary>
        private void SaveExecute()
        {
            try
            {
                service.AddEditEmployeeOrManager(NewEmployee, false);
                IsUpdatedEmployee = true;
                addEmployee.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// Method to check conditions if save is possible
        /// </summary>
        /// <returns>true or false</returns>
        private bool CanSaveExecute()
        {
            if (String.IsNullOrEmpty(NewEmployee.FirstName) || String.IsNullOrEmpty(NewEmployee.LastName) ||
                String.IsNullOrEmpty(NewEmployee.JMBG) || String.IsNullOrEmpty(NewEmployee.AccountNumber) ||
                String.IsNullOrEmpty(NewEmployee.Email) || String.IsNullOrEmpty((NewEmployee.Salary).ToString()) ||
                String.IsNullOrEmpty(NewEmployee.Position) || String.IsNullOrEmpty(NewEmployee.Username) ||
                String.IsNullOrEmpty(NewEmployee.Password))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Clancel command - closes the view
        /// </summary>
        private ICommand close;
        public ICommand Close
        {
            get
            {
                if (close == null)
                {
                    close = new RelayCommand(param => CloseExecute(), param => CanCloseExecute());
                }
                return close;
            }
        }

        /// <summary>
        /// Method for closing AddEdit View
        /// </summary>
        private void CloseExecute()
        {
            try
            {
                addEmployee.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// Method for check if close is possible to be Executed
        /// </summary>
        /// <returns>true </returns>
        private bool CanCloseExecute()
        {
            return true;
        }
        #endregion
    }
}
