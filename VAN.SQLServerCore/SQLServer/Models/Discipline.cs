using System.ComponentModel.DataAnnotations;

namespace VAN.SQLServerCore.SQLServer.Models
{
    public class Discipline
    {
        [Key]
        public required int DId { get; set; }
        public required string DName { get; set; }
        public required string DType { get; set; }
        public required int Del { get; set; }
    }
}
