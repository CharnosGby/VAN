import axios from 'axios'
import * as Type from './indexApiType'
const BASEURL = 'http://localhost:8081'
const Apifox_TestBaseURL = 'http://127.0.0.1:4523/m1/3902542-3537059-default'

export const TestGet = async (param: Type.ITestGet) => {
    const res = await axios.get(`${BASEURL}/Test/TestGet`, { params: param, withCredentials: true })
    return res
}


export const TestPost = async (param: Type.ITestPost) => {
    const res = await axios.post(`${BASEURL}/Test/TestPost`, param, { withCredentials: true })
    return res
}

export const TestGetUsers = async () => {
    const res = await axios.get(`${BASEURL}/api/User/GetUsers`, { withCredentials: true })
    return res
}

export const Apifox_GetUserInfo_student = async () => {
    const res = await axios.get(`${Apifox_TestBaseURL}/Test/GetUserInfo-student`, { withCredentials: true })
    return res
}
export const Apifox_GetCollagesAndProfessions = async () => {
    const res = await axios.get(`${Apifox_TestBaseURL}/Test/GetCollagesAndProfessions`, { withCredentials: true })
    return res
}
export const Apifox_GetTeachers = async () => {
    const res = await axios.get(`${Apifox_TestBaseURL}/Test/GetTeachers`, { withCredentials: true })
    return res
}
export const Apifox_GetVolunteerBySno = async () => {
    const res = await axios.get(`${Apifox_TestBaseURL}/Test/GetVolunteerBySno`, { withCredentials: true })
    return res
}