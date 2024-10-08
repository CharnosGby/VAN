﻿namespace VAN.WebCore.WebVO
{
    public class ScoreVO
    {
        public required string Sname { get; set; }
        public required string Sno { get; set; }
        public double? Chinese { get; set; }
        public double? Math { get; set; }
        public double? English { get; set; }

        public override string? ToString()
        {
            return $@"Sname: {Sname}
Sno: {Sno}
Chinese: {Chinese}
Math: {Math}
English: {English}
";
        }
    }
}
