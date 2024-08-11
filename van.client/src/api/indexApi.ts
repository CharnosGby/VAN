import axios from 'axios'
const BASEURL = 'http://localhost:8081'
export interface ITestGet {
    page:number,
    pageSize:number
}
export const TestGet = async (param: ITestGet) => {
    const res = await axios.get(`${BASEURL}/Test/TestGet`, { params: param, withCredentials: true })
    return res
}

export interface ITestPost {
    id: number,
    name: string,
}
export const TestPost = async (param: ITestPost) => {
    const res = await axios.post(`${BASEURL}/Test/TestPost`, param, { withCredentials: true })
    return res
}

export const TestGetUsers = async () => {
    const res = await axios.get(`${BASEURL}/api/User/GetUsers`, { withCredentials: true })
    return res
}