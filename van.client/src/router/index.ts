import { createRouter, createWebHistory, type RouteRecordRaw } from 'vue-router'

const routes: Array<RouteRecordRaw> = [
    {
        path: '/index',
        component: () => import('@/views/index.vue'),
        redirect: '/index/Home',
        children: [
            {
                path: 'Home',
                component: () => import('@/views/HomeView/HomeView.vue')
            },
            {
                path: 'Personal',
                children: [
                    {
                        path: 'personalInfos',
                        component: () => import('@/views/Personal/personalInfos.vue')
                    },
                    {
                        path: 'personalScores',
                        component: () => import('@/views/Personal/personalScores.vue')
                    },
                    {
                        path: 'personalOrderTo',
                        component: () => import('@/views/Personal/personalOrderTo.vue')
                    },
                    {
                        path: 'personalResult',
                        component: () => import('@/views/Personal/personalResult.vue')
                    },
                ]
            },
            {
                path: 'View',
                children: [
                    {
                        path: 'ViewCollege',
                        component: () => import('@/views/View/ViewCollege.vue')
                    },
                    {
                        path: 'ViewTeachers',
                        component: () => import('@/views/View/ViewTeachers.vue')
                    },
                    {
                        path: 'ViewLike',
                        component: () => import('@/views/View/ViewLike.vue')
                    },
                ]
            },
            {
                path: 'Exit',
                component: () => import('@/views/LoginVue.vue')
            },
        ]
    },
    {
        path: '/',
        redirect: '/index/Home'
    }
]

const router = createRouter({
    history: createWebHistory(import.meta.env.BASE_URL),
    routes: routes
})

export default router
