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
    class ModifyViewModel:ViewModelBase
    {
        ModifyView modify;
        Service service;

        #region Constructor
        public ModifyViewModel(ModifyView modifyOpen)
        {
            modify = modifyOpen;
            service = new Service();
            EmployeeList = service.GetAllEmployees().ToList();
        }
        #endregion

        #region Properties
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

        private tblEmployee employee;
        public tblEmployee Employee
        {
            get
            {
                return employee;
            }
            set
            {
                employee = value;
                OnPropertyChanged("Employee");
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

        private bool isDeletedEmployee;
        public bool IsDeletedEmployee
        {
            get
            {
                return isDeletedEmployee;
            }
            set
            {
                isDeletedEmployee = value;
            }
        }
        #endregion

        #region Commands
        /// <summary>
        /// Add command
        /// </summary>
        private ICommand addEmployee;
        public ICommand AddEmployee
        {
            get
            {
                if (addEmployee == null)
                {
                    addEmployee = new RelayCommand(param => AddNewEmployeeExecute(), param => CanAddNewEmployeeExecute());

                }
                return addEmployee;
            }
        }

        /// <summary>
        /// Method for executing add Command, opens view for adding new employee
        /// </summary>
        private void AddNewEmployeeExecute()
        {
            try
            {
                AddEditEmployeeView addEmployee = new AddEditEmployeeView();
                addEmployee.ShowDialog();
                if ((addEmployee.DataContext as AddEditEmployeeViewModel).IsUpdatedEmployee == true)
                {
                    EmployeeList = service.GetAllEmployees().ToList();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// Checks if its possible to add the new employee
        /// </summary>
        /// <returns>true</returns>
        private bool CanAddNewEmployeeExecute()
        {
            return true;
        }

        

        /// <summary>
        /// Delete command
        /// </summary>
        private ICommand deleteEmployee;
        public ICommand DeleteEmployee
        {
            get
            {
                if (deleteEmployee == null)
                {
                    deleteEmployee = new RelayCommand(param => DeleteEmployeeExecute(), param => CanDeleteEmployeeExecute());

                }
                return deleteEmployee;
            }
        }

        /// <summary>
        /// Method for execution of deleting employee command
        /// </summary>
        private void DeleteEmployeeExecute()
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete record about employee?", "Delete employee", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    if(Employee!=null)
                    {
                        int employeeId = Employee.EmployeeId;
                        service.DeleteEmployee(Employee);
                        //Logg actions missing
                        isDeletedEmployee = true;
                        if (IsDeletedEmployee == true)
                        {
                            EmployeeList = service.GetAllEmployees().ToList();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                return;
            }
        }

        /// <summary>
        /// Method confirm delete command execution is possible
        /// </summary>
        /// <returns>true</returns>
        private bool CanDeleteEmployeeExecute()
        {
            if (Employee == null)
                return false;
            else
                return true;
        }

        /// <summary>
        /// Edit Employee Command
        /// </summary>
        private ICommand editEmployee;
        public ICommand EditEmployee
        {
            get
            {
                if (editEmployee == null)
                {
                    editEmployee = new RelayCommand(param => EditEmployeeExecute(), param => CanEditEmployeeExecute());
                }
                return editEmployee;
            }
        }

        /// <summary>
        /// Method to execute edit employee command..opens window for editing employee records
        /// </summary>
        private void EditEmployeeExecute()
        {
            try
            {
                if (Employee != null)
                {
                    AddEditEmployeeView editEmployee = new AddEditEmployeeView(Employee);
                    editEmployee.ShowDialog();
                    if ((editEmployee.DataContext as AddEditEmployeeViewModel).IsUpdatedEmployee == true)
                    {
                        EmployeeList = service.GetAllEmployees().ToList();

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// Method to confirm edit command execution is possible
        /// </summary>
        /// <returns>true</returns>
        private bool CanEditEmployeeExecute()
        {
            if (Employee == null)
            {
                return false;
            }
            else
                return true;
        }
        #endregion
    }
}
