using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VAN.WebCore.WebVO
{
    public class TeacherVO
    {
        public required string Tname;
        public required string Sex;
        public required string Tno;
        public required string Tphone;
        public required string Cname;
        public required string Ccode;
        public required string Sname;
        public required string Scode;
        public required string Slevel;

        public override string? ToString()
        {
            return $"Tname：{Tname}，Sex：{Sex}，Tno：{Tno}，Tphone：{Tphone}，Cname：{Cname}，Ccode：{Ccode}, Sname：{Sname}，Scode：{Scode}，Slevel：{Slevel}";
        }
    }
}
