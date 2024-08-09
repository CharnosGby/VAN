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
          <p v-for="item in list" :key="item.key">
            {{ `${item.key}:${item.value}` }}
          </p>
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
  key: string;
  value: string;
}
const list = ref<TransformedData[]>([
  { key: 'a', value: 'b' },
  { key: 'c', value: 'd' },
]);
const get = async () => {
  const res = await API.TestGet();
  const res2 = await API.TestGetUsers();
  return { res, res2 };
};
const init = async () => {
  get().then(({ res, res2 }) => {
    list.value = res.data.data as TransformedData[];
    const res2_data = res2.data as {
      id: number;
      name: string;
      password: string;
    }[];
    res2_data.forEach(({ id, name, password }) => {
      list.value.push({ key: name, value: password });
    });
  });
};
</script>

<style scoped></style>
