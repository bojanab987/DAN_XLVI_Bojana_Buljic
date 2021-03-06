﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Zadatak_1.Commands;
using Zadatak_1.Model;
using Zadatak_1.Services;
using Zadatak_1.View;

namespace Zadatak_1.ViewModel
{
    class AdminViewModel:ViewModelBase
    {
        AdminView adminMenu;
        Service service;
        private readonly BackgroundWorker bgWorker = new BackgroundWorker();

        #region Properties
        private tblEmployee employee;
        public tblEmployee Employee
        {
            get { return employee; }
            set
            {
                employee = value;
                OnPropertyChanged("Employee");
            }
        }

        private List<tblEmployee> employeeList;
        public List<tblEmployee> EmployeeList
        {
            get { return employeeList; }
            set
            {
                employeeList = value;
                OnPropertyChanged("EmployeeList");
            }
        }

        private List<tblManager> managerList;
        public List<tblManager> ManagerList
        {
            get { return managerList; }
            set
            {
                managerList = value;
                OnPropertyChanged("ManagerList");
            }
        }

        #endregion

        #region Constructor
        public AdminViewModel(AdminView adminOpen)
        {
            adminMenu = adminOpen;
            Employee = new tblEmployee();
            service = new Service();
            EmployeeList = service.GetAllEmployees().ToList();
            ManagerList = service.GetAllManagers().ToList();
            bgWorker.DoWork += WorkerOnDoWork;

        }
        #endregion

        #region Method for Logging action
        /// <summary>
        /// Writes the message to the log file.
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">DoWorkEventArgs e</param>
        public void WorkerOnDoWork(object sender, DoWorkEventArgs e)
        {
            FileLogger logger = new FileLogger();
            string message = logger.PrintMessage("Added", Employee.FirstName, Employee.LastName);

            logger.LogFile(message);
        }
        #endregion

        #region Commands

        /// <summary>
        /// Add manager command -for adding another manager
        /// </summary>
        private ICommand addManager;
        public ICommand AddManager
        {
            get
            {
                if (addManager == null)
                {
                    addManager = new RelayCommand(param => AddManagerExecute(), param => CanAddManagerExecute());
                }
                return addManager;
            }
        }

        /// <summary>
        /// Method for opening new view for adding new manager
        /// </summary>
        private void AddManagerExecute()
        {
            try
            {
                AdminView newView=new AdminView();
                newView.ShowDialog();
                adminMenu.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// Method for check if command can be executed
        /// </summary>
        /// <returns>true </returns>
        private bool CanAddManagerExecute()
        {
            return true;
        }

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
        /// Method for executing save command...saving added manager
        /// </summary>
        private void SaveExecute()
        {
            try
            {
                tblEmployee emp = service.AddEditEmployeeOrManager(Employee, true);
                if (!bgWorker.IsBusy)
                {
                    // This method will start the execution asynchronously in the background
                    bgWorker.RunWorkerAsync();
                }
                if (emp != null)
                {
                    MessageBox.Show("Manager succesfully created!");                    
                }                
                else
                {
                    MessageBox.Show("Something went wrong! Try again.");
                }                
                EmployeeList = service.GetAllEmployees().ToList();
                ManagerList = service.GetAllManagers().ToList();
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
            if (String.IsNullOrEmpty(Employee.FirstName) || String.IsNullOrEmpty(Employee.LastName) ||
                String.IsNullOrEmpty(Employee.JMBG) || String.IsNullOrEmpty(Employee.AccountNumber) ||
                String.IsNullOrEmpty(Employee.Email) || String.IsNullOrEmpty((Employee.Salary).ToString()) ||
                String.IsNullOrEmpty(Employee.Position) || String.IsNullOrEmpty(Employee.Username) ||
                String.IsNullOrEmpty(Employee.Password))
            {
                return false;
            }
            else
            {
                return true;
            }

        }

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
                adminMenu.Close();
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
        /// Method for closing AdminView
        /// </summary>
        private void CancelExecute()
        {
            try
            {
                adminMenu.Close();

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
