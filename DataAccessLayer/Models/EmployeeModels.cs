using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class EmployeeModels
    {
        [Key]
        public int id { get; set; }
        public string name{ get; set; }
        public string email { get; set; }
        public string adress { get; set; }
        public string phone { get; set; }
    }
}
