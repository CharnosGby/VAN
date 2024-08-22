export interface ITestGet {
  page: number;
  pageSize: number;
}
export interface ITestPost {
  id: number;
  name: string;
}

export interface TransformedData {
  tname: string;
  sex: string;
  tno: string;
  tphone: string;
  cname: string;
  ccode: string;
  sname: string;
  scode: string;
  slevel: string;
}

export interface PageParam {
  page: number;
  pageSize: number;
}

export interface StudentIndexVO {
  Account: string;
  Avg: number;
  Chinese: number;
  English: number;
  Math: number;
  SName: string;
  SSex: string;
  SSno: string;
}

export interface CollagesAndProfessions {
  CollageCode: string;
  CollageName: string;
  NeedAVG: number;
  NeedChineseScore: number;
  NeedEnglishScore: number;
  NeedMathScore: number;
  ProfessionCode: string;
  ProfessionName: string;
  ProfessionType: string;
}

export interface TeachersViewVO {
  BeCollegeCode: string;
  BeCollegeName: string;
  BeSchoolCode: string;
  BeSchoolLevel: string;
  BeSchoolName: string;
  TName: string;
  Tno: string;
  TPhone: string;
  TSex: string;
}

export interface VolunteerInfo {
  CollageCode: string;
  CollageName: string;
  NeedAVG: number;
  NeedChineseScore: number;
  NeedEnglishScore: number;
  NeedMathScore: number;
  ProfessionCode: string;
  ProfessionName: string;
  ProfessionType: string;
  SchoolCode: string;
  SchoolLevel: string;
  SchoolName: string;
  StudentName: string;
  StudentSno: string;
  VolunteerName: string;
}
