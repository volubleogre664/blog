import { createRouter, createWebHistory } from 'vue-router';
import HomeView from '@/views/HomeView.vue';
import ArticleView from '@/views/ArticleView.vue';

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: HomeView,
    },
    {
      path: '/signup',
      name: 'signup',
      component: () => import('@/views/RegisterView.vue'),
    },
    {
      path: '/article/:slug',
      name: 'article',
      component: ArticleView,
    },
    {
      path: '/editor',
      name: 'editor',
      component: () => import('@/views/CreateArticleView.vue'),
    },
    {
      path: '/profile',
      name: 'profile',
      component: () => import('@/views/UserProfileView.vue'),
    },
    {
      path: '/bookmark',
      name: 'bookmarks',
      component: () => import('@/views/BookmarksView.vue'),
    },
  ],
});

export default router;
