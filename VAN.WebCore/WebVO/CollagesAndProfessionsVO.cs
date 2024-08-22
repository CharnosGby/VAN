namespace VAN.WebCore.WebVO
{
    internal class CollagesAndProfessionsVO
    {
        public required string CollageCode { get; set; }
        public required string CollageName { get; set; }
        public required double NeedAvg { get; set; }
        public required double NeedChineseScore { get; set; }
        public required double NeedEnglishScore { get; set; }
        public required double NeedMathScore { get; set; }
        public required string ProfessionCode { get; set; }
        public required string ProfessionName { get; set; }
        public required string ProfessionType { get; set; }
    }
}
