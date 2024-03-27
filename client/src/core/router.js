import {
    createRouter,
    createWebHashHistory,
} from "vue-router";

const routes = [
    {
        path: "/",
        redirect: "/apps",
        component: () => import("../layout/MainLayout.vue"),
        meta: {
        },
        children: [
            {
                path: "apps",
                name: "apps-view",
                component: () => import("../views/Apps.vue"),
                meta: {
                    pageTitle: "Apps",
                },
            },
            {
                path: "/app/:appId",
                name: "edit-app",
                props: true,
                component: () => import("../views/EditApp.vue"),
                meta: {
                    pageTitle: "Edit App",
                },
            }
        ],
    },
    {
        path: "/:pathMatch(.*)*",
        redirect: "/404",
    },
];

const router = createRouter({
    history: createWebHashHistory(),
    routes,
});

router.beforeEach(async (to, from, next) => {
    // current page view title
    document.title = `${to.meta.pageTitle}`;

    next();

    // Scroll page to top on every route change
    window.scrollTo({
        top: 0,
        left: 0,
        behavior: "smooth",
    });
});

export default router;
