using System;
using System.Windows;
using System.Windows.Input;
using Zadatak_1.Commands;
using Zadatak_1.Model;
using Zadatak_1.View;

namespace Zadatak_1.ViewModel
{
    class LogInViewModel:ViewModelBase
    {
        LogInView loginView;        

        #region Property
        private Employee loggedEmployee;
        public Employee LoggedEmployee
        {
            get { return loggedEmployee; }
            set
            {
                loggedEmployee = value;
                OnPropertyChanged("LoggedEmployee");
            }
        }
        #endregion

        #region Constructor
        public LogInViewModel(LogInView loginOpen)
        {
            loginView = loginOpen;
            loggedEmployee = new Employee();
        }
        #endregion Constructor

        #region Commands
        /// <summary>
        /// LogIn Command
        /// </summary>
        private ICommand login;
        public ICommand Login
        {
            get
            {
                if (login == null)
                {
                    login = new RelayCommand(param => LoginExecute(), param => CanLoginExecute());
                }
                return login;
            }
        }
        /// <summary>
        /// Method for deciding which View will open according to logged in Employee credentials
        /// </summary>
        private void LoginExecute()
        {
            try
            {
                switch (loggedEmployee.Role)
                {
                    case "Admin":
                        AdminView adminMenu = new AdminView();
                        adminMenu.ShowDialog();
                        break;
                    case "Employee":
                        EmployeeView employeeView = new EmployeeView(loggedEmployee.ID);
                        employeeView.ShowDialog();
                        break;
                    case "Manager":
                        tblManager manager = Services.LogInService.GetManagerById(loggedEmployee.ID);
                        if (manager.AccessLevel == "Modify")
                        {
                            ModifyView modify = new ModifyView();
                            modify.ShowDialog();
                        }
                        else
                        {
                            ReadOnlyView readOnly = new ReadOnlyView();
                            readOnly.ShowDialog();
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// Method to confirm logIn command execution
        /// </summary>
        /// <returns></returns>
        private bool CanLoginExecute()
        {
            return true;
        }

        /// <summary>
        /// Clancel command - closes the view
        /// </summary>
        private ICommand cancel;
        public ICommand Cancel
        {
            get
            {
                if (cancel == null)
                {
                    cancel = new RelayCommand(param => CancelExecute(), param => CanCancelExecute());
                }
                return cancel;
            }
        }

        /// <summary>
        /// Method for closing AddIDcard View
        /// </summary>
        private void CancelExecute()
        {
            try
            {
                loginView.Close();
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
        private bool CanCancelExecute()
        {
            return true;
        }
        #endregion
    }
}
