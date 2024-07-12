import { useApiStore } from '@/stores'
import { ElMessage } from 'element-plus'
import router from '@/router'
import axios from 'axios'
// 创建axios实例
const instance = axios.create({
  timeout: 10000,
  headers: {
    'Content-Type': 'application/json',
  },
})
// 设置请求拦截器
instance.interceptors.request.use(
  (config) => {
    // 使用apiStore中的token
    const apiStore = useApiStore()
    if (apiStore.token) {
      config.headers.Authorization = apiStore.token
    }
    return config
  },
  (err) => Promise.reject(err)
)

// 设置响应拦截器
instance.interceptors.response.use(
  (res) => {
    // 如果响应的数据code为200，则返回
    if (res.data.code === 200) {
      return res
    } else {
      // 否则弹出错误提示
      ElMessage.error({
        showClose: true,
        message: res.data.message || '出现未知错误',
        grouping: true
      })
      // 返回Promise.reject
      return Promise.reject(res.data)
    }
  },
  (err) => {
    // 弹出错误提示
    ElMessage.error({
      showClose: true,
      message: err.response.data.message || '出现未知错误',
      grouping: true,
      onClose: () => {
        // 跳转到登录页
        router.push({ name: 'LoginVue' })
      }
    })
    // 返回Promise.reject
    Promise.reject(err)
  }
)
export default instance
