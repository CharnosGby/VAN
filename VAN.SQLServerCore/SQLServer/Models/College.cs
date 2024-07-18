using System.ComponentModel.DataAnnotations;

namespace VAN.SQLServerCore.SQLServer.Models
{
    public class College
    {
        [Key]
        public required int CId { get; set;}
        public required string CName { get; set; }
        public required string CCode { get; set; }
        public required string BeSchoolCode { get; set; }
        public required int Del { get; set; }
    }
}
