using System.ComponentModel.DataAnnotations;

namespace VAN.SQLServerCore.SQLServer.Models
{
    public class UserModel
    {
        [Key]
        public required long Id { get; set; }
        public required string Name { get; set; }
        public required string Password { get; set; }
    }
}