using System.ComponentModel.DataAnnotations;

namespace VAN.SQLServerCore.SQLServer.Models
{
    public class Score
    {
        [Key]
        public required int ScoreId { get; set; }
        public required int StudentSno { get; set; }
        public required int DisciplineCode { get; set; }
        public required double Scores { get; set; }
        public required int Del { get; set; }

        public override string? ToString()
        {
            return $"ScoreId: {ScoreId}, StudentSno: {StudentSno}, DisciplineCode: {DisciplineCode}, Scores: {Scores}, Del: {Del}";
        }
    }
}
