namespace VAN.SQLServerCore.SQLServer
{
    public static class SQLString
    {
        public static string GetTeachers(int page, int pageSize) {
            string sql = $@"
				SELECT
					teacher.tname AS Tname, 
					CASE 
						WHEN teacher.sex = 0 THEN '女'
						WHEN teacher.sex = 1 THEN '男'
						ELSE '其他'
					END AS Sex, 
					teacher.tno AS Tno, 
					teacher.tphone AS Tphone, 
					college.cname AS Cname, 
					college.ccode AS Ccode, 
					school.sname AS Sname, 
					school.scode AS Scode, 
					CASE 
						WHEN school.slevel = 1 THEN '专科'
						WHEN school.slevel = 2 THEN '二本'
						WHEN school.slevel = 3 THEN '一本'
						WHEN school.slevel = 4 THEN '211'
						WHEN school.slevel = 5 THEN '985'
						ELSE '其他'
					END AS Slevel
				FROM
					[work].teacher
					LEFT JOIN
					[work].college
					ON 
						teacher.becollegeid = college.cid
					LEFT JOIN
					[work].school
					ON 
						college.beschoolcode = school.sid
				ORDER BY
					teacher.tno ASC
				OFFSET {(page - 1) * pageSize} ROWS
				FETCH NEXT {pageSize} ROWS ONLY;
			";
			return sql;
        }

		public static string GetStudentScore(string sno) {
            string sql = $@"
				SELECT
					stu.sname AS Sname, 
					stu.sno AS Sno,
					MAX(CASE WHEN di.dname = '语文' THEN sc.scores ELSE NULL END) AS Chinese,
					MAX(CASE WHEN di.dname = '数学' THEN sc.scores ELSE NULL END) AS Math,
					MAX(CASE WHEN di.dname = '英语' THEN sc.scores ELSE NULL END) AS English
				FROM
					[work].score AS sc
					LEFT JOIN [work].student AS stu ON sc.studentid = stu.sid
					LEFT JOIN [work].discipline AS di ON sc.disciplineid = di.did
				WHERE
					stu.sno = {sno}
				GROUP BY
					stu.sname, stu.sno;
			";
			return sql;
        }
    }
}
