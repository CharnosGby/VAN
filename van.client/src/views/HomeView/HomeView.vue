<template>
    <div>
        HomeView
    </div>
    <el-button type="primary" :icon="Search" @click="init">Search</el-button>
    <p v-for="item in list" :key="item.key">
        {{ `${item.key}:${item.value}` }}
    </p>
</template>

<script setup lang="ts">
import { Search } from '@element-plus/icons-vue'
import { ref } from 'vue'
import axios from 'axios'
interface TransformedData {
    key: string,
    value: string
}
const list = ref<TransformedData[]>([])
const get = async () =>{
    const res = await axios.get('http://localhost:8081/Test/TestGet', { withCredentials: true })
    const res2 = await axios.get('http://localhost:8081/api/User/GetUsers', { withCredentials: true })
    return {res,res2}
}
const init = async () => {
    get().then(({res,res2})=>{
        console.log(res.data)
        list.value = res.data.data as TransformedData[]

        const res2_data = res2.data as { id: number, name: string, password: string }[]
        res2_data.forEach(({id,name,password})=>{
            list.value.push({key: name, value: password})
        })
    })
}
</script>

<style scoped>

</style>