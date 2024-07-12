import { ref, computed } from 'vue'
import { defineStore } from 'pinia'
//使用规范：use+名称+Store
export const useApiStore = defineStore(
  'useApiStore',
  () => {
    const token = ref('')
    return { token }
  },
  {
    // 开启持久化
    persist: true
  }
)
