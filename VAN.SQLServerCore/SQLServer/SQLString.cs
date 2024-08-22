namespace VAN.SQLServerCore.SQLServer
{
    public static class SQLString
    {
        public static string GetTeachers(int page, int pageSize) {
            string sql = $@"
				SELECT
					teacher.tname AS [教师], 
					CASE 
						WHEN teacher.sex = 0 THEN '女'
						WHEN teacher.sex = 1 THEN '男'
						ELSE '其他'
					END AS [性别], 
					teacher.tno AS [教师职工号], 
					teacher.tphone AS [电话], 
					college.cname AS [学院], 
					college.ccode AS [学院编号], 
					school.sname AS [学校], 
					school.scode AS [学校编码], 
					CASE 
						WHEN school.slevel = 1 THEN '专科'
						WHEN school.slevel = 2 THEN '二本'
						WHEN school.slevel = 3 THEN '一本'
						WHEN school.slevel = 4 THEN '211'
						WHEN school.slevel = 5 THEN '985'
						ELSE '其他'
					END AS [学校等级]
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
					stu.sname AS [学生], 
					stu.sno AS [学号], 
					MAX(CASE WHEN di.dname = '语文' THEN sc.scores ELSE NULL END) AS [语文], 
					MAX(CASE WHEN di.dname = '数学' THEN sc.scores ELSE NULL END) AS [数学], 
					MAX(CASE WHEN di.dname = '英语' THEN sc.scores ELSE NULL END) AS [英语], 
					ROUND(AVG(sc.scores), 1) AS [平均分]
				FROM
					[work].score AS sc
					LEFT JOIN
					[work].student AS stu
					ON 
						sc.studentsno = stu.sno
					LEFT JOIN
					[work].discipline AS di
					ON 
						sc.disciplineid = di.did
				WHERE
					stu.sno = {sno}
				GROUP BY
					stu.sname, 
					stu.sno
			";
			return sql;
        }
    }
}
