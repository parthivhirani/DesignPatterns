using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRSPattern.Models
{
    public class EmployeeQueryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Salary { get; set; }
        public int DepartmentId { get; set; }
        public string EmailId { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? JoiningDate { get; set; }
        public string? Status { get; set; }
    }
}
