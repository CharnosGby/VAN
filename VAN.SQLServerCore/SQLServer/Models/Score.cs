using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VAN.SQLServerCore.SQLServer.Models
{
    public class Score
    {
        [Key]
        public required int ScoreId { get; set; }
        public required int StudentId { get; set; }
        public required int DisciplineId { get; set; }
        public required double Scores { get; set; }
        public required int Del { get; set; }

        public override string? ToString()
        {
            return $"ScoreId: {ScoreId}, StudentId: {StudentId}, DisciplineId: {DisciplineId}, Scores: {Scores}, Del: {Del}";
        }
    }
}
