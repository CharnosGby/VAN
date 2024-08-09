<template>
    <div class="nav">
        <el-menu
            :default-active="'Home'"
            background-color="#545c64"
            class="nav-menu"
            mode="horizontal"
            text-color="#fff"
            :ellipsis="false"
            :router="true"
            menu-trigger="click"
            active-text-color="#4bffff"
        >
            <el-menu-item index="/index/Home">
                <span>预科学生分流系统</span>
            </el-menu-item>
            <div style="flex: 1;"></div>
            <el-sub-menu index="Personal">
                <template #title>个人中心</template>
                <el-menu-item index="/index/Personal/personalInfos">个人资料</el-menu-item>
                <el-menu-item index="/index/Personal/personalScores">个人成绩</el-menu-item>
                <el-menu-item index="/index/Personal/personalOrderTo">填报志愿</el-menu-item>
                <el-menu-item index="/index/Personal/personalResult">填报结果</el-menu-item>
            </el-sub-menu>
            <el-sub-menu index="View">
                <template #title>查看</template>
                <el-menu-item index="/index/View/ViewCollege">各学院信息</el-menu-item>
                <el-menu-item index="/index/View/ViewTeachers">各教师信息</el-menu-item>
                <el-menu-item index="/index/View/ViewLike">收藏</el-menu-item>
            </el-sub-menu>
            <el-menu-item index="/index/Exit">退出登录</el-menu-item>
        </el-menu>
    </div>
    <div class="content">
        <RouterView />
    </div>
</template>

<script setup lang="ts">
</script>

<style scoped>
.content{
    padding: 10px;
}
</style>