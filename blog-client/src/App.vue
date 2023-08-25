<template>
  <main class="app">
    <LoaderItem v-if="userStore.loading" />
    <AppHeader />

    <RouterView />
  </main>
</template>

<script setup lang="ts">
import { RouterLink, RouterView } from 'vue-router';
import { useUserStore } from '@/stores/user';
import { useArticlesStore } from '@/stores/articles';
import { firebaseConfig } from './utils/firebase';
import { initializeApp } from 'firebase/app';

import AppHeader from '@/components/AppHeader.vue';
import LoaderItem from '@/components/LoaderItem.vue';
import { onMounted } from 'vue';

const userStore = useUserStore();
const articlesStore = useArticlesStore();

onMounted(() => {
  initializeApp(firebaseConfig);
  articlesStore.getArticles();
});
</script>
