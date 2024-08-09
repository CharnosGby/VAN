import axios from 'axios'
export const TestGet = async () => {
    const res = await axios.get('http://localhost:8081/Test/TestGet', { withCredentials: true })
    return res
}

interface ITest {
    id: number,
    name: string,
}
export const TestPost = async (param: ITest) => {
    const res = await axios.post('http://localhost:8081/Test/TestPost', param, { withCredentials: true })
    return res
}

export const TestGetUsers = async () => {
    const res = await axios.get('http://localhost:8081/api/User/GetUsers', { withCredentials: true })
    return res
}