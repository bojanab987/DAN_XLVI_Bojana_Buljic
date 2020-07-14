using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadatak_1.Model
{
    /// <summary>
    /// Model class having credentials for logging in
    /// </summary>
    class Employee
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role
        {
            get
            {
                if (Username == "WPFadmin" && Password == "WPFadmin")
                {
                    return "Admin";
                }
                else if (Services.LogInService.GetEmployeeByCredentials(Username, Password) == null)
                {
                    return "Access denied";
                }
                else
                {
                    tblEmployee employee = Services.LogInService.GetEmployeeByCredentials(Username, Password);
                    if (Services.LogInService.IsManager(employee))
                    {
                        return "Manager";
                    }
                    else
                    {
                        return "Employee";
                    }
                }
            }
        }

        public int ID
        {
            get
            {
                if (Role == "Manager" || Role == "Employee")
                {
                    return Services.LogInService.GetEmployeeByCredentials(Username, Password).EmployeeId;
                }
                else if (Role == "Admin")
                {
                    return -1;
                }
                else
                {
                    return 0;
                }

            }
        }
    }
}
