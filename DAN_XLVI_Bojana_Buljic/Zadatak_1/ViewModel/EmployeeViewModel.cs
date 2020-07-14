using System;
using System.Windows;
using System.Windows.Input;
using Zadatak_1.Commands;
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
            Report = new vwReport();
            Report.EmployeeId = id;
        }

        public EmployeeViewModel(EmployeeView viewOpen, vwReport report)
        {
            employeeView = viewOpen;
            Report = report;            
        }

        private vwReport report;
        public vwReport Report
        {
            get { return report; }
            set
            {
                report = value;
                OnPropertyChanged("Report");
            }
        }

        private bool isUpdatedReport;
        public bool IsUpdatedReport
        {
            get
            {
                return isUpdatedReport;
            }
            set
            {
                isUpdatedReport = value;
            }
        }       

        #region Commands
        /// <summary>
        /// Save command
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
        /// Method for executing save command, saving report
        /// </summary>
        private void SaveExecute()
        {
            try
            {
                string rep = service.AddReport(Report);
                if (rep != null)
                {
                    MessageBox.Show(rep);
                }
                else
                {
                    MessageBox.Show("Report has been succesfully saved.");
                    isUpdatedReport = true;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// Confirm save is possible to execute
        /// </summary>
        /// <returns></returns>
        private bool CanSaveExecute()
        {
            if (String.IsNullOrEmpty(Report.Project) || String.IsNullOrEmpty((Report.WorkHours).ToString())) 
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// LogOut command
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
        /// Method to perform log Out action
        /// </summary>
        private void LogOutExecute()
        {
            try
            {
                employeeView.Close();
                LogInView newLogIn = new LogInView();                
                newLogIn.ShowDialog();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// Method confirms logOut can be executed
        /// </summary>
        /// <returns></returns>
        private bool CanLogOutExecute()
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
        /// Method for closing Employee View
        /// </summary>
        private void CancelExecute()
        {
            try
            {
                employeeView.Close();
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
