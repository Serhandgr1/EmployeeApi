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
       public string JobName { get; set; }
        public string JobArgs { get; set; }
        public int TryCount { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime NextTryTime { get; set; }
        public DateTime LastTryTime { get; set; }
        public bool IsAbandoned { get; set; }
        public bool Priority { get; set; }
        public string ExtraProperties { get; set; }
        public string ConcurrencyStamp { get; set; }

    }
}
