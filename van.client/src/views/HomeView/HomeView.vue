<template>
  <div style="height: 100%; width: 100%">
    <div style="background:#fff;">
      <upload></upload>
    </div>
    <div>
      <input v-model="messageInput" placeholder="输入消息" />
      <button @click="sendMessage">Send</button>
      <ul>
        <li v-for="(msg, index) in messages" :key="index">{{ msg }}</li>
      </ul>
    </div>
    <el-card style="max-width: 480px" v-if="StudentIndexInfo != null">
      <template #header>
        <div>学生</div>
      </template>
      <div>
        <div>账号:{{ StudentIndexInfo?.Account }}</div>
        <div>姓名:{{ StudentIndexInfo?.SName }}</div>
        <div>性别:{{ StudentIndexInfo?.SSex }}</div>
        <div>学号:{{ StudentIndexInfo?.SSno }}</div>
        <div>平均分:{{ StudentIndexInfo?.Avg }}</div>
        <div>语文分数:{{ StudentIndexInfo?.Chinese }}</div>
        <div>英语分数:{{ StudentIndexInfo?.English }}</div>
        <div>数学分数:{{ StudentIndexInfo?.Math }}</div>
      </div>
      <template #footer>
        <div>结束</div>
      </template>
    </el-card>

    <el-card style="max-width: 480px" v-if="CollagesAndProfessions.length">
      <template #header>
        <div>学院和专业</div>
        <div style="display: flex; align-items: center">
          <span class="shape-box bg_green"></span>
          <span> 满足条件</span>
        </div>
        <div style="display: flex; align-items: center">
          <span class="shape-box bg_red"></span>
          <span> 不满足条件</span>
        </div>
      </template>
      <div
        class="CollagesAndProfessions-Card"
        :class="{
          bg_green: canChooseProfession(item),
          bg_red: !canChooseProfession(item),
        }"
        v-for="(item, index) in CollagesAndProfessions"
        :key="index">
        <div>学院名称:{{ item.CollageName }}</div>
        <div>学院代码:{{ item.CollageCode }}</div>
        <div>
          专业名称:
          <span
            :class="{
              green: canChooseProfession(item),
              red: !canChooseProfession(item),
            }">
            {{ item.ProfessionName }}
          </span>
        </div>
        <div>专业代码:{{ item.ProfessionCode }}</div>
        <div>专业类别:{{ item.ProfessionType }}</div>
        <div>
          所需平均分:
          <span
            :class="{
              green: StudentIndexInfo!.Avg >= item.NeedAVG,
              red: StudentIndexInfo!.Avg < item.NeedAVG,
            }">
            {{ item.NeedAVG }}
          </span>
        </div>
        <div>
          所需语文分数:
          <span
            :class="{
              green: StudentIndexInfo!.Chinese >= item.NeedChineseScore,
              red: StudentIndexInfo!.Chinese < item.NeedChineseScore,
            }">
            {{ item.NeedChineseScore }}
          </span>
        </div>
        <div>
          所需数学分数:
          <span
            :class="{
              green: StudentIndexInfo!.Math >= item.NeedMathScore,
              red: StudentIndexInfo!.Math < item.NeedMathScore,
            }">
            {{ item.NeedMathScore }}
          </span>
        </div>
        <div>
          所需英语分数:
          <span
            :class="{
              green: StudentIndexInfo!.English >= item.NeedEnglishScore,
              red: StudentIndexInfo!.English < item.NeedEnglishScore,
            }">
            {{ item.NeedEnglishScore }}
          </span>
        </div>
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
import { useUserStore } from '@/stores/useUserStore';
import upload from '@/components/uploadComponent.vue';
import { startConnection,createConnection } from '@/utils/signalrService';
const userStore = useUserStore();
const StudentIndexInfo = ref<Type.StudentIndexVO>();
const CollagesAndProfessions = ref<Type.CollagesAndProfessions[]>([]);


const Sconnection = startConnection('MessageChatHubURL');
const Rconnection = createConnection('MessageChatHubURL');
const messageInput = ref('');
const messages = ref<string[]>([]);
Rconnection.on("APoint", (user, message) => {
    console.log(`User: ${user}, Message: ${message}`);
});

const get = async () => {
  const res = await API.Apifox_GetCollagesAndProfessions();
  return { res };
};

const sendMessage = async () => {
  const conn = await Sconnection;
  const user = 'aaabbbccc'
  if (conn) {
    try {
      await conn.invoke('SendMessage', user, messageInput.value);
      messageInput.value = '';
    } catch (err) {
      console.error('错误: ', err);
    }
  }
};

const init = async () => {
    get().then(({ res }) => {
      CollagesAndProfessions.value = res.data.data as Type.CollagesAndProfessions[];
    });
    StudentIndexInfo.value = userStore.StudentInfos;
    Sconnection.then(conn => {
      conn.on('APoint', (user, message) => {
        messages.value.push(`${user}: ${message}`);
      });
    });
};

onMounted(() => {
  init();
});

const canChooseProfession = (item: Type.CollagesAndProfessions) => {
  const student = StudentIndexInfo.value!;
  return (
    student.Avg >= item.NeedAVG &&
    student.Chinese >= item.NeedChineseScore &&
    student.Math >= item.NeedMathScore &&
    student.English >= item.NeedEnglishScore
  );
};
</script>

<style scoped>
.CollagesAndProfessions-Card {
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
