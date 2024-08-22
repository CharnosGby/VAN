<template>
  <div
    v-if="VolunteerInfos.length"
    style="display: flex; justify-content: space-around; align-items: center">
    <el-card
      style="max-width: 480px"
      v-for="(item, index) in VolunteerInfos"
      :key="index">
      <template #header>
        <div>
          {{ `${item.StudentName}-${item.StudentSno}: ${item.VolunteerName}` }}
        </div>
      </template>
      <div>
        <div>专业名称:{{ item.ProfessionName }}</div>
        <div>专业代码:{{ item.ProfessionCode }}</div>
        <div>专业类别:{{ item.ProfessionType }}</div>
        <div>
          所需分数:{{
            `语文:${item.NeedChineseScore},数学:${item.NeedMathScore},英语:${item.NeedEnglishScore},平均分:${item.NeedAVG}`
          }}
        </div>
        <div>学院名称:{{ item.CollageName }}</div>
        <div>学院代码:{{ item.CollageCode }}</div>
        <div>学校名称:{{ item.SchoolName }}</div>
        <div>学校代码:{{ item.SchoolCode }}</div>
        <div>学校等级:{{ item.SchoolLevel }}</div>
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
const VolunteerInfos = ref<Type.VolunteerInfo[]>([]);
const get = async () => {
  const res = await API.Apifox_GetVolunteerBySno();
  return { res };
};

const init = async () => {
  get().then(({ res }) => {
    VolunteerInfos.value = res.data.data as Type.VolunteerInfo[];
  });
};

onMounted(() => {
  init();
});
</script>

<style scoped></style>
