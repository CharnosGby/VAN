<template>
  <div>
    <el-card style="max-width: 480px" v-if="StudentInfos">
      <template #header>
        <div>学生</div>
      </template>
      <div>
        <div>姓名:{{ StudentInfos?.SName }}</div>
        <div>语文分数:{{ StudentInfos?.Chinese }}</div>
        <div>英语分数:{{ StudentInfos?.English }}</div>
        <div>数学分数:{{ StudentInfos?.Math }}</div>
      </div>
      <template #footer>
        <div>结束</div>
      </template>
    </el-card>
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted } from 'vue';
import * as Type from '@/api/indexApiType';
import { useUserStore } from '@/stores/useUserStore';
const userStore = useUserStore();
const StudentInfos = ref<Type.StudentIndexVO>();
const init = async () => {
    StudentInfos.value = userStore.StudentInfos;
};

onMounted(() => {
  init();
});
</script>

<style scoped></style>
