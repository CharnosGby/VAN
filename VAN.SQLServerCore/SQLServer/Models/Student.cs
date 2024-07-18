using System.ComponentModel.DataAnnotations;

namespace VAN.SQLServerCore.SQLServer.Models
{
    public class Student
    {
        [Key]
        public required int SId { get; set; }
        public required string SName { get; set; }
        public required int Sex { get; set;}
        public required string Sno { get; set; }
        public required int UId { get; set; }
        public required int Del { get; set; }
    }
}
