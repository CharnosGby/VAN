using System.ComponentModel.DataAnnotations;

namespace VAN.SQLServerCore.SQLServer.Models
{
    public class Volunteer
    {
        [Key]
        public required int VId { get; set; }
        public required string Vname { get; set; }
        public required int StudentSno { get; set; }
        public required int SchoolCode { get; set; }
        public required string Pids { get; set; }
        public required int Del { get; set; }
    }
}
