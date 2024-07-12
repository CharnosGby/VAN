<template>
    <div>
        <el-menu
            :default-active="'Home'"
            background-color="#545c64"
            mode="horizontal"
            text-color="#fff"
            active-text-color="#ffd04b"
            @select="Select"
        >
            <el-menu-item index="Home">首页</el-menu-item>
            <el-sub-menu index="pictures">
                <template #title>图集</template>
                <el-menu-item index="picQR">精品推荐</el-menu-item>
                <el-menu-item index="picP">人气热门</el-menu-item>
                <el-menu-item index="picMF">我的喜欢</el-menu-item>
                <el-sub-menu index="Ss">
                    <template #title>图片专题</template>
                    <el-menu-item index="picSs1">原创自拍</el-menu-item>
                    <el-menu-item index="picSs2">网红大咖</el-menu-item>
                    <el-menu-item index="picSs3">美腿玉足</el-menu-item>
                    <el-menu-item index="picSs4">成人动漫</el-menu-item>
                </el-sub-menu>
            </el-sub-menu>
            <el-sub-menu index="videos">
                <template #title>视频</template>
                <el-menu-item index="videoQR">精品推荐</el-menu-item>
                <el-menu-item index="videoP">人气热门</el-menu-item>
                <el-menu-item index="videoMF">我的喜欢</el-menu-item>
                <el-sub-menu index="Ss">
                    <template #title>视频专题</template>
                    <el-menu-item index="videoSs1">抖音风格</el-menu-item>
                    <el-menu-item index="videoSs2">粉嫩处女</el-menu-item>
                    <el-menu-item index="videoSs3">街拍街射</el-menu-item>
                </el-sub-menu>
            </el-sub-menu>
            <el-menu-item index="Personal">个人中心</el-menu-item>
        </el-menu>
        <div>
            <RouterView />
        </div>
    </div>
</template>

<script setup lang="ts">
import router from '@/router';
import { ref } from 'vue'
const Select = (path: string,indexPath:string[]) => {
    console.log(indexPath.join('/'));
    router.push('/index/'+indexPath.join('/'))
}
</script>

<style scoped>

</style>