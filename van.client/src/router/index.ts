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
                path: 'pictures',
                children: [
                    {
                        path: 'picQR',
                        component: () => import('@/views/picturesView/picQR.vue')
                    },
                    {
                        path: 'picP',
                        component: () => import('@/views/picturesView/picP.vue')
                    },
                    {
                        path: 'picMF',
                        component: () => import('@/views/picturesView/picMF.vue')
                    },
                    {
                        path: 'Ss',
                        children: [
                            {
                                path: 'picSs1',
                                component: () => import('@/views/picturesView/Ss/picSs1.vue')
                            },
                            {
                                path: 'picSs2',
                                component: () => import('@/views/picturesView/Ss/picSs2.vue')
                            },
                            {
                                path: 'picSs3',
                                component: () => import('@/views/picturesView/Ss/picSs3.vue')
                            },
                            {
                                path: 'picSs4',
                                component: () => import('@/views/picturesView/Ss/picSs4.vue')
                            },
                        ]
                    },
                ]
            },
            {
                path: 'videos',
                children: [
                    {
                        path: 'videoQR',
                        component: () => import('@/views/videoView/videoQR.vue')
                    },
                    {
                        path: 'videoP',
                        component: () => import('@/views/videoView/videoP.vue')
                    },
                    {
                        path: 'videoMF',
                        component: () => import('@/views/videoView/videoMF.vue')
                    },
                    {
                        path: 'Ss',
                        children: [
                            {
                                path: 'videoSs1',
                                component: () => import('@/views/videoView/Ss/videoSs1.vue')
                            },
                            {
                                path: 'videoSs2',
                                component: () => import('@/views/videoView/Ss/videoSs2.vue')
                            },
                            {
                                path: 'videoSs3',
                                component: () => import('@/views/videoView/Ss/videoSs3.vue')
                            },
                        ]
                    },
                ]
            },
            {
                path: 'Personal',
                component: () => import('@/views/PersonalView/PersonalView.vue')
            },
        ]
    },
    {
        path: '/LR',
        component: () => import('@/views/LR.vue')
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
