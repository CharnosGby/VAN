<template>
  <div>
    <el-card style="max-width: 480px" v-if="Teachers.length">
      <template #header>
        <div>
          <div>教师</div>
        </div>
      </template>
      <div
        class="TeachersView-Card"
        v-for="(item, index) in Teachers"
        :key="index">
        <div>教师姓名:{{ item.TName }}</div>
        <div>教师代码:{{ item.Tno }}</div>
        <div>教师代码:{{ item.TSex }}</div>
        <div>教师代码:{{ item.TPhone }}</div>
        <div>学院名称:{{ item.BeCollegeName }}</div>
        <div>学院代码:{{ item.BeCollegeCode }}</div>
        <div>学校名称:{{ item.BeSchoolName }}</div>
        <div>学校代码:{{ item.BeSchoolCode }}</div>
        <div>学校等级:{{ item.BeSchoolLevel }}</div>
      </div>
      <template #footer>
        <div>结束</div>
      </template>
    </el-card>
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted } from 'vue';
import * as API from '@/api/indexApi';
import * as Type from '@/api/indexApiType';
const Teachers = ref<Type.TeachersViewVO[]>([]);

const get = async () => {
  const res = await API.Apifox_GetTeachers();
  return { res };
};

const init = async () => {
  get().then(({ res }) => {
    Teachers.value = res.data.data as Type.TeachersViewVO[];
  });
};

onMounted(() => {
  init();
});
</script>

<style scoped>
.TeachersView-Card {
  margin: 10px 0px;
  border: 1px solid #000;
  padding: 10px;
  border-radius: 20px;
  box-shadow: 3px -3px 5px 0px #0000007d;
}
.red {
  color: red;
}
.green {
  color: green;
}
.bg_green {
  background-color: #00ff5714;
}
.bg_red {
  background-color: #ff000014;
}
.shape-box {
  display: flex;
  border: 1px solid #000;
  border-radius: 5px;
  margin-right: 10px;
  height: 20px;
  width: 40px;
}
</style>
