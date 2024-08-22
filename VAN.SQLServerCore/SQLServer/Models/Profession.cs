using System.ComponentModel.DataAnnotations;

namespace VAN.SQLServerCore.SQLServer.Models
{
    public class Profession
    {
        [Key]
        public required int PId { get; set; }
        public required string PName { get; set; }
        public required string PCode { get; set; }
        public required int BeCollegeCode { get; set; }
        public required string NeedScore { get; set; }
        public required string PType { get; set; }
        public required int Del { get; set; }
    }
}
