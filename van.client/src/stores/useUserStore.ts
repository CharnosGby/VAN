import { ref } from 'vue'
import { defineStore } from 'pinia'
import * as Type from '@/api/indexApiType';
//使用规范：use+名称+Store
export const useUserStore = defineStore(
  'useUserStore',
  () => {
    const StudentInfos = ref<Type.StudentIndexVO>()
    const setStudentInfos = (data: Type.StudentIndexVO) => {
      StudentInfos.value = data
    }
    return { StudentInfos,setStudentInfos }
  },
  {
    // 开启持久化
    persist: true
  }
)
