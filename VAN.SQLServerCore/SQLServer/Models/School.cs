using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VAN.SQLServerCore.SQLServer.Models
{
    public class School
    {
        [Key]
        public required int SId { get; set;}
        public required string SName { get; set; }
        public required string SCode { get; set; }
        public required int SLevel { get; set; }
        public required int Del { get; set; }
    }
}
