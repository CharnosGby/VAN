namespace VAN.WebCore.WebVO
{
    public class TeacherVO
    {
        public required string Tname { get; set; }
        public required string Sex { get; set; }
        public required string Tno { get; set; }
        public required string Tphone { get; set; }
        public required string Cname { get; set; }
        public required string Ccode { get; set; }
        public required string Sname { get; set; }
        public required string Scode { get; set; }
        public required string Slevel { get; set; }

        public override string? ToString()
        {
            return $@"Tname：{Tname}
Sex：{Sex}
Tno：{Tno}
Tphone：{Tphone}
Cname：{Cname}
Ccode：{Ccode}
Sname：{Sname}
Scode：{Scode}
Slevel：{Slevel}
";
        }
    }
}
