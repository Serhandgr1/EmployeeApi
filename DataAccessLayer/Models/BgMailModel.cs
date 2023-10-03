using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class BgMailModel
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Konu { get; set; }
        public string Icerik { get; set; }
    }
}
