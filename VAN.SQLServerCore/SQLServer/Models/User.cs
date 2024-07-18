using System.ComponentModel.DataAnnotations;

namespace VAN.SQLServerCore.SQLServer.Models
{
    public class User
    {
        [Key]
        public required long Id { get; set; }
        public required string Name { get; set; }
        public required string Password { get; set; }
        public required int Permission { get; set; }
        public required string UserAccount { get; set; }
        public required int Del { get; set; }
    }
}