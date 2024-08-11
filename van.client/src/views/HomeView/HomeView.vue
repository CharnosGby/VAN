<template>
  <div>
    <el-card style="max-width: 480px">
      <template #header>
        <div class="card-header">
          <div>测试</div>
          <el-button type="primary" :icon="Search" @click="init"
            >点击获取结果</el-button
          >
        </div>
      </template>
      <p v-for="o in 4" :key="o" class="text item">{{ 'List item ' + o }}</p>
      <template #footer>
        <el-card style="max-width: 480px">
          <template #header>
            <div>结果</div>
          </template>
          <el-card v-for="(item, index) in list" :key="index">
            <template #header>
              <div>教师姓名:{{ item.tname }}</div>
            </template>
            <div>教师性别:{{ item.sex }}</div>
            <div>教师工号:{{ item.tno }}</div>
            <div>教师电话:{{ item.tphone }}</div>
            <div>所属学院:{{ item.cname }}</div>
            <div>所属学院编号:{{ item.ccode }}</div>
            <div>所属学校:{{ item.sname }}</div>
            <div>所属学校编号:{{ item.scode }}</div>
            <div>所属学校等级:{{ item.slevel }}</div>
          </el-card>
        </el-card>
      </template>
    </el-card>
  </div>
</template>

<script setup lang="ts">
import { Search } from '@element-plus/icons-vue';
import { ref } from 'vue';
import * as API from '@/api/indexApi';
interface TransformedData {
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
const list = ref<TransformedData[]>([]);
const get = async () => {
  var param = <API.ITestGet>{
    page: 1,
    pageSize: 10,
  };
  const res = await API.TestGet(param);
  return { res };
};
const init = async () => {
  get().then(({ res }) => {
    list.value = res.data.data as TransformedData[];
  });
};
</script>

<style scoped></style>
