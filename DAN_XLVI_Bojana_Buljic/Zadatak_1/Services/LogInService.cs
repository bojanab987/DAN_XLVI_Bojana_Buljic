using System;
using System.Linq;
using Zadatak_1.Model;

namespace Zadatak_1.Services
{
    /// <summary>
    /// Service class for getting right person for logg in into app, for log in authentification
    /// </summary>
    class LogInService
    {
        /// <summary>
        /// Method for get right person/employee according to log in credentials match
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static tblEmployee GetEmployeeByCredentials(string username, string password)
        {
            try
            {
                using (EmployeeManagementEntities context = new EmployeeManagementEntities())
                {
                    tblEmployee employee = (from x in context.tblEmployees where x.Username == username && x.Password == password select x).FirstOrDefault();
                    return employee;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception " + ex.Message.ToString());
                return null;
            }
        }

        /// <summary>
        /// Method check if employee is a manager
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public static bool IsManager(tblEmployee employee)
        {
            try
            {
                using (EmployeeManagementEntities context = new EmployeeManagementEntities())
                {
                    tblManager manager = (from x in context.tblManagers where x.EmployeeId== employee.EmployeeId select x).FirstOrDefault();
                    if (manager != null)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception " + ex.Message.ToString());
                return false;
            }
        }

        /// <summary>
        /// Method to get manager by his ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static tblManager GetManagerById(int id)
        {
            try
            {
                using (EmployeeManagementEntities context = new EmployeeManagementEntities())
                {
                    tblManager manager = (from x in context.tblManagers where x.EmployeeId == id select x).FirstOrDefault();
                    return manager;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception " + ex.Message.ToString());
                return null;
            }

        }
    }
}
