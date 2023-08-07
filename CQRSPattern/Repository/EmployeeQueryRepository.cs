﻿using CQRSPattern.Interfaces;
using CQRSPattern.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRSPattern.Repository
{
    public class EmployeeQueryRepository : IEmployeeQueryRepository
    {
        string conString = "data source=.; database=DPPractical; user id=parthiv; password=Rmha@12345678";
        public List<EmployeeQueryModel> GetAll()
        {
            using (var con = new SqlConnection(conString))
            {
                con.Open();
                var da = new SqlDataAdapter("SELECT * FROM Employee;", con);
                
                var dt = new DataTable();
                da.Fill(dt);

                var empList = new List<EmployeeQueryModel>();
                foreach (DataRow emp in dt.Rows)
                {
                    var employee = new EmployeeQueryModel()
                    {
                        Id = (int)emp[0],
                        Name = (string)emp[1],
                        Salary = (double)(decimal)emp[2],
                        DepartmentId = (int)emp[3],
                        EmailId = (string)emp[4],
                        JoiningDate = (DateTime)emp[5],
                        Status = (string)emp[6]
                    };
                    empList.Add(employee);
                }
                return empList;
            }
        }

        public EmployeeQueryModel GetById(int id)
        {
            using (var con = new SqlConnection(conString))
            {
                con.Open();
                var da = new SqlDataAdapter($"SELECT * FROM Employee WHERE Id={id};", con);

                var dt = new DataTable();
                da.Fill(dt);
                if(dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    var employee = new EmployeeQueryModel()
                    {
                        Id = (int)dr[0],
                        Name = (string)dr[1],
                        Salary = (double)(decimal)dr[2],
                        DepartmentId = (int)dr[3],
                        EmailId = (string)dr[4],
                        JoiningDate = (DateTime)dr[5],
                        Status = (string)dr[6]
                    };
                    return employee;
                }
                else
                {
                    return new EmployeeQueryModel();
                }
            }
        }
    }
}
