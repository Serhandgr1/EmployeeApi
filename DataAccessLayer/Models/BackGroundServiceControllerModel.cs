using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class BackGroundServiceControllerModel
    {
        [Key]
        public int Id { get; set; }
        public bool IsWork { get; set; }

    }
}
