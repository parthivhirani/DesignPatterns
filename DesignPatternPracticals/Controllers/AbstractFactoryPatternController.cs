using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using AbstractFactoryPattern.ConcreteFactory;
using AbstractFactoryPattern.AbstractFactory;

namespace DesignPatternPracticals.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ApiVersion("2.1")]
    public class AbstractFactoryPatternController : Controller
    {
        [HttpGet]
        [MapToApiVersion("2.1")]
        public IActionResult GetEmployeeOvertimePay(int id, int hours)
        {
            string conString = "data source=.; database=DPPractical; user id=parthiv; password=Rmha@12345678";

            string dept = "";
            using (var con = new SqlConnection(conString))
            {
                con.Open();
                var da = new SqlDataAdapter($"SELECT d.DepartmentName FROM Employee e JOIN Department d ON d.Id = e.DepartmentId WHERE e.DepartmentId=d.Id AND e.Id={id}; ", con);
                var dt = new DataTable();
                da.Fill(dt);


                if (dt.Rows.Count > 0)
                    dept = dt.Rows[0][0].ToString();

                IEmployeeFactory employee;
                if(dept.ToUpper().Equals("IT") || dept.ToUpper().Equals("HR") || dept.ToUpper().Equals("ADMIN"))
                {
                    employee = new IndoorFactory();
                    var indoorEmployee = employee.GetEmployeeObject(dept);
                    var overtimeTotal = indoorEmployee.OvertimePayment(hours);
                    return Ok(new {indoorEmployee.DepartmentType, indoorEmployee.Department, overtimeTotal });
                }
                else if(dept.ToUpper().Equals("SALES") || dept.ToUpper().Equals("ON-SITE"))
                {
                    employee = new OutdoorFactory();
                    var outdoorEmployee = employee.GetEmployeeObject(dept);
                    var overtimeTotal = outdoorEmployee.OvertimePayment(hours);
                    return Ok(new { outdoorEmployee.DepartmentType, outdoorEmployee.Department, overtimeTotal });
                }
                else
                {
                    return BadRequest(new { error = "Enter valid employee id" });
                }
            }
        }
    }
}
