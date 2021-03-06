﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Zadatak_1.Model;

namespace Zadatak_1.Services
{
    class Service
    {
        #region Employee and Manager service methods
        /// <summary>
        /// Method for adding new employee or manager in database or editing existing one
        /// </summary>
        /// <param name="employee">employee  to db</param>
        /// <param name="manager">boolean value to check if employee is manager</param>
        public tblEmployee AddEditEmployeeOrManager(tblEmployee employee, bool manager)
        {
            try
            {
                using (EmployeeManagementEntities context = new EmployeeManagementEntities())
                {
                    if (employee.EmployeeId == 0)
                    {
                        tblEmployee newEmployee = new tblEmployee();
                        newEmployee.FirstName = employee.FirstName;
                        newEmployee.LastName = employee.LastName;
                        newEmployee.DateOfBirth = employee.DateOfBirth;
                        newEmployee.JMBG = employee.JMBG;
                        newEmployee.AccountNumber = employee.AccountNumber;
                        newEmployee.Email = employee.Email;
                        newEmployee.Salary = employee.Salary;
                        newEmployee.Position = employee.Position;
                        newEmployee.Username = employee.Username;
                        newEmployee.Password = employee.Password;
                        context.tblEmployees.Add(newEmployee);
                        context.SaveChanges();
                        employee.EmployeeId = newEmployee.EmployeeId;

                        //if employee is manager add him in tblManager
                        if (manager == true)
                        {
                            tblManager newManager = new tblManager();
                            newManager.EmployeeId = employee.EmployeeId;
                            newManager.Sector = employee.Sector;
                            newManager.AccessLevel = employee.AccessLevel;
                            context.tblManagers.Add(newManager);
                            context.SaveChanges();
                        }
                        return employee;
                    }
                    else
                    {
                        tblEmployee employeeToEdit = (from x in context.tblEmployees where x.EmployeeId == employee.EmployeeId select x).FirstOrDefault();
                        employeeToEdit.FirstName = employee.FirstName;
                        employeeToEdit.LastName = employee.LastName;
                        employeeToEdit.DateOfBirth = employee.DateOfBirth;
                        employeeToEdit.JMBG = employee.JMBG;
                        employeeToEdit.AccountNumber = employee.AccountNumber;
                        employeeToEdit.Email = employee.Email;
                        employeeToEdit.Salary = employee.Salary;
                        employeeToEdit.Position = employee.Position;
                        employeeToEdit.Username = employee.Username;
                        employeeToEdit.Password = employee.Password;
                        context.SaveChanges();
                        return employee;
                    }

                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception: " + ex.Message.ToString());
                return null;
            }
        }


        /// <summary>
        /// Method gets all employees from database and returns list of employees
        /// </summary>
        /// <returns>List of employees</returns>
        public List<tblEmployee> GetAllEmployees()
        {
            try
            {
                using (EmployeeManagementEntities context = new EmployeeManagementEntities())
                {
                    List<tblEmployee> employees = new List<tblEmployee>();
                    employees = (from x in context.tblEmployees select x).ToList();
                    return employees;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        /// <summary>
        /// Method for deleting employee from database
        /// </summary>
        /// <param name="employeeID"></param>
        public void DeleteEmployee(tblEmployee employee)
        {
            try
            {
                using (EmployeeManagementEntities context = new EmployeeManagementEntities())
                {
                    if(LogInService.IsManager(employee))
                    {
                        tblManager managerToDelete = (from m in context.tblManagers where m.EmployeeId== employee.EmployeeId select m).First();
                        context.tblManagers.Remove(managerToDelete);
                        context.SaveChanges();
                    }
                    tblEmployee employeeToDelete = (from e in context.tblEmployees where e.EmployeeId == employee.EmployeeId select e).First();
                    context.tblEmployees.Remove(employeeToDelete);                    
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
            }
        }

        /// <summary>
        /// Method gets all managers from database and returns list of managers
        /// </summary>
        /// <returns></returns>
        public List<tblManager> GetAllManagers()
        {
            try
            {
                using (EmployeeManagementEntities context = new EmployeeManagementEntities())
                {
                    List<tblManager> managers = new List<tblManager>();
                    managers = (from x in context.tblManagers select x).ToList();
                    return managers;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }
        #endregion

        #region Reports service methods
        /// <summary>
        /// Method for adding or editing reports into database by Employee
        /// </summary>
        /// <param name="report"></param>
        /// <returns></returns>
        public string AddReport(vwReport report)
        {
            try
            {
                using (EmployeeManagementEntities context = new EmployeeManagementEntities())
                {
                    if(report.ReportId==0)
                    {
                        int noOfreports = (from x in context.tblReports where x.EmployeeId == report.EmployeeId && x.ReportDate == report.ReportDate select x).Count();
                        if (noOfreports < 2)
                        {
                            int hours = 0;
                            if (noOfreports == 1)
                            {
                                hours = (from x in context.tblReports where x.EmployeeId == report.EmployeeId && x.ReportDate == report.ReportDate select x.WorkHours).FirstOrDefault();
                            }

                            if (hours + report.WorkHours <= 12)
                            {
                                tblReport newReport = new tblReport();
                                newReport.EmployeeId = report.EmployeeId;
                                newReport.ReportDate = report.ReportDate;
                                newReport.Project = report.Project;
                                newReport.WorkHours = report.WorkHours;
                                context.tblReports.Add(newReport);
                                context.SaveChanges();
                                return null;
                            }                            
                        }                        
                    }                  
                    
                    else
                    {
                        tblReport reportToEdit = (from x in context.tblReports where x.ReportId == report.ReportId select x).FirstOrDefault();
                        reportToEdit.ReportDate = report.ReportDate;
                        reportToEdit.Project = report.Project;
                        reportToEdit.WorkHours = report.WorkHours;
                        int rep = (from x in context.tblReports where x.EmployeeId == report.EmployeeId && x.ReportDate == report.ReportDate && x.ReportId != report.ReportId select x.WorkHours).FirstOrDefault();
                        if (rep == 0 && reportToEdit.WorkHours <= 12)
                        {
                            context.SaveChanges();
                            return null;
                        }
                        else if (rep + reportToEdit.WorkHours <= 12)
                        {
                            context.SaveChanges();
                        }
                        else
                        {
                            return "Cannot have more than 12 work hours.";
                        }                        
                    }
                }
                return "Cannot have more than 12 work hours.";
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        /// <summary>
        /// Method to get report by Id of employee
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns>single report of employee</returns>
        public vwReport GetReportByEmployeeId(int employeeId)
        {
            try
            {
                using (EmployeeManagementEntities context = new EmployeeManagementEntities())
                {
                    vwReport report = (from x in context.vwReports where x.EmployeeId == employeeId select x).FirstOrDefault();
                    return report;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception " + ex.Message.ToString());
                return null;
            }
        }

        /// <summary>
        /// Method for getting all reports from data base
        /// </summary>
        /// <returns>list of reports</returns>
        public List<vwReport> GetAllReports()
        {
            try
            {
                using (EmployeeManagementEntities context = new EmployeeManagementEntities())
                {
                    List<vwReport> reports = new List<vwReport>();
                    reports = (from x in context.vwReports select x).ToList();
                    return reports;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        /// <summary>
        /// Method for deleting reports from database 
        /// </summary>
        /// <param name="reportID"></param>
        public void DeleteReport(int reportID)
        {
            try
            {
                using (EmployeeManagementEntities context = new EmployeeManagementEntities())
                {
                    tblReport reportToDelete = (from r in context.tblReports where r.ReportId == reportID select r).First();
                    context.tblReports.Remove(reportToDelete);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
            }
        }
        #endregion
    }
}
