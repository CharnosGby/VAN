<template>
  <el-upload
    class="avatar-uploader"
    :action="`http://localhost:8082/upload/${path}`"
    :show-file-list="false"
    :on-success="handleAvatarSuccess"
    :data="{ FileName: fn }"
    :before-upload="beforeAvatarUpload">
    <img v-if="imageUrl" :src="imageUrl" class="avatar" />
    <el-icon v-else class="avatar-uploader-icon"><Plus /></el-icon>
    <template #tip>
      <div class="el-upload__tip">
         上传头像
      </div>
    </template>
  </el-upload>
</template>

<script lang="ts" setup>
import { ref } from 'vue';
import { ElMessage } from 'element-plus';
import { Plus } from '@element-plus/icons-vue';
import type { UploadProps } from 'element-plus';
import { useUserStore } from '@/stores/useUserStore';
const userStore = useUserStore();

//返回的服务器图片地址
const imageUrl = ref('');
// 文件路径
// const path = ref(`${userStore.StudentInfos?.SName}-${userStore.StudentInfos?.SSno}`);
// 文件名
// const fn = ref(`${userStore.StudentInfos?.SName}-head`)


const path = ref(`File`);
const fn = ref('')
const fnType = ref('')
// 成功响应
type rep = {
  code: number;
  msg: string;
  data: {
    url: string;
    name: string;
  };
};

const handleAvatarSuccess: UploadProps['onSuccess'] = (
  response: rep,
  uploadFile
) => {
  // imageUrl.value = URL.createObjectURL(uploadFile.raw!)
  imageUrl.value = response.data.url;
//   console.log(response);
};

const beforeAvatarUpload: UploadProps['beforeUpload'] = (rawFile) => {
    // 从最后一个点开始截取文件类型
    fnType.value = rawFile.name.substring(rawFile.name.lastIndexOf('.') + 1, rawFile.name.length);
    fn.value = rawFile.name.substring(0, rawFile.name.lastIndexOf('.'));
    const lowFnType = fnType.value.toLocaleLowerCase();
    switch (lowFnType) {
        case 'jpeg':
        case 'jpg':
        case 'png':
        case 'webp':
        case 'avif':
        case 'ico':
            ElMessage({
              dangerouslyUseHTMLString: true,
              type: 'success',
              message: `<strong>图片格式正确,是${lowFnType}</strong>`,
            })
            break;
        case 'txt':
        case 'pdf':
        case 'doc':
        case 'docx':
        case 'ppt':
        case 'pptx':
        case 'xls':
        case 'xlsx':
        ElMessage({
              dangerouslyUseHTMLString: true,
              type: 'success',
              message: `<strong>文件格式正确,是${lowFnType}</strong>`,
            })
            break;
        case 'mp3':
        case 'mp4':
        case 'avi':
        case 'mov':
        ElMessage({
              dangerouslyUseHTMLString: true,
              type: 'success',
              message: `<strong>音视频格式正确,是${lowFnType}</strong>`,
            })
            break;
        default:
            ElMessage.info(lowFnType);
            ElMessage({
              dangerouslyUseHTMLString: true,
              type: 'error',
              message: `
                <div>图片格式必须是jpg,jpeg,png,webp,avif,ico</div>
                <div>文件格式必须是txt,pdf,doc,docx,ppt,pptx,xls,xlsx</div>
                <div>音视频格式必须是mp3,mp4,avi,mov</div>
              `,
            })
            return false;
    }
  if (calcSize(rawFile.size) > 20) {
    ElMessage.error('图片大小不能超过20MB!');
    return false;
  }
  return true;
};

// 计算图片大小
const calcSize = (size: number) => {
    return size / 1024 / 1024
}
</script>

<style scoped>
.avatar-uploader .avatar {
  width: 178px;
  height: 178px;
  display: block;
}
</style>

<style>
.avatar-uploader .el-upload {
  border: 1px dashed var(--el-border-color);
  border-radius: 6px;
  cursor: pointer;
  position: relative;
  overflow: hidden;
  transition: var(--el-transition-duration-fast);
}

.avatar-uploader .el-upload:hover {
  border-color: var(--el-color-primary);
}

.el-icon.avatar-uploader-icon {
  font-size: 28px;
  color: #8c939d;
  width: 178px;
  height: 178px;
  text-align: center;
}
</style>
