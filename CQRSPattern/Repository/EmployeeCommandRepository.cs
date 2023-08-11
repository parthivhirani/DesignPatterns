﻿using CQRSPattern.Interfaces;
using CQRSPattern.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CQRSPattern.DataConnection;

namespace CQRSPattern.Repository
{
    public class EmployeeCommandRepository : IEmployeeCommandRepository
    {
        private readonly string conString = DBCS.ConnectionString();
        public bool CreateEmployee(EmployeeCommandModel employeeCommand)
        {
            if (employeeCommand != null)
            {
                using (var con = new SqlConnection(conString))
                {
                    con.Open();
                    var cmd = new SqlCommand("INSERT INTO Employee VALUES (@name, @salary, @departmentid, @emailid, @joiningdate, @status);", con);
                    SqlParameter name = new SqlParameter("@name", SqlDbType.VarChar, 100);
                    SqlParameter salary = new SqlParameter("@salary", SqlDbType.Money);
                    SqlParameter departmentid = new SqlParameter("@departmentid", SqlDbType.Int, 100);
                    SqlParameter emailid = new SqlParameter("@emailid", SqlDbType.VarChar, 50);
                    SqlParameter joiningdate = new SqlParameter("@joiningdate", SqlDbType.Date);
                    SqlParameter status = new SqlParameter("@status", SqlDbType.VarChar, 50);

                    name.Value = employeeCommand.Name;
                    salary.Value = employeeCommand.Salary;
                    departmentid.Value = employeeCommand.DepartmentId;
                    emailid.Value = employeeCommand.EmailId;
                    joiningdate.Value = employeeCommand.JoiningDate;
                    status.Value = employeeCommand.Status;

                    cmd.Parameters.Add(name);
                    cmd.Parameters.Add(salary);
                    cmd.Parameters.Add(departmentid);
                    cmd.Parameters.Add(emailid);
                    cmd.Parameters.Add(joiningdate);
                    cmd.Parameters.Add(status);

                    var response = cmd.ExecuteNonQuery();
                    if (response >= 1)
                        return true;
                    return false;
                }
            }
            return false;
        }

        public bool DeleteEmployee(int id)
        {
            using (var con = new SqlConnection(conString))
            {
                con.Open();
                var cmd = new SqlCommand("DELETE FROM Employee WHERE Id=@id;", con);
                SqlParameter empid = new SqlParameter("@id", SqlDbType.Int);
                empid.Value = id;
                cmd.Parameters.Add(empid);
                var deleteResult = cmd.ExecuteNonQuery();
                if (deleteResult == 1)
                    return true;
                return false;
            }
        }

        public bool EditEmployee(int id, EmployeeCommandModel employeeCommand)
        {
            using (var con = new SqlConnection(conString))
            {
                con.Open();
                var cmd = new SqlCommand("UPDATE Employee SET Name=@name, Salary=@salary, DepartmentId=@departmentId, EmailId=@emailId, JoiningDate=@joiningDate, Status=@status WHERE Id=@Id;", con);

                SqlParameter name = new SqlParameter("@name", SqlDbType.VarChar, 100);
                SqlParameter salary = new SqlParameter("@salary", SqlDbType.Money);
                SqlParameter departmentid = new SqlParameter("@departmentId", SqlDbType.Int, 100);
                SqlParameter emailid = new SqlParameter("@emailId", SqlDbType.VarChar, 50);
                SqlParameter joiningdate = new SqlParameter("@joiningDate", SqlDbType.Date);
                SqlParameter status = new SqlParameter("@status", SqlDbType.VarChar, 50);
                SqlParameter Id = new SqlParameter("Id", SqlDbType.Int);

                name.Value = employeeCommand.Name;
                salary.Value = employeeCommand.Salary;
                departmentid.Value = employeeCommand.DepartmentId;
                emailid.Value = employeeCommand.EmailId;
                joiningdate.Value = employeeCommand.JoiningDate;
                status.Value = employeeCommand.Status;
                Id.Value = id;

                cmd.Parameters.Add(name);
                cmd.Parameters.Add(salary);
                cmd.Parameters.Add(departmentid);
                cmd.Parameters.Add(emailid);
                cmd.Parameters.Add(joiningdate);
                cmd.Parameters.Add(status);
                cmd.Parameters.Add(Id);

                var editResult = cmd.ExecuteNonQuery();
                if (editResult == 1)
                    return true;
                return false;
            }
        }
    }
}
