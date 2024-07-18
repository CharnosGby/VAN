using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VAN.SQLServerCore.SQLServer.Models
{
    public class Teacher
    {
        public required int TId { get; set; }
        public required string TName { get; set; }
        public required int Sex { get; set; }
        public required string Tno { get; set; }
        public required string TPhone { get; set; }
        public required int BeCollegeId { get; set; }
        public required int Uid { get; set; }
        public required int Del { get; set; }
    }
}
