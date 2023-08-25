<style src="@/styles/HomeView.css"></style>

<template>
  <main class="home">
    <aside>
      <SidebarNav />
    </aside>

    <main>
      <h1>You saved articles</h1>

      <FeedItem
        :v-if="articlesStore.savedArticles.length"
        v-for="article in articlesStore.savedArticles"
        :key="article.id"
        :article="article"
        :show-actions="false"
      />
      <!-- <FeedItem /> -->
    </main>
  </main>
</template>

<script setup lang="ts">
import FeedItem from '@/components/FeedItem.vue';
import SidebarNav from '@/components/SidebarNav.vue';
import { useArticlesStore } from '@/stores/articles';
import { useUserStore } from '@/stores/user';
import router from '@/router';

const userStore = useUserStore();
const articlesStore = useArticlesStore();

articlesStore.getSavedArticles(userStore.user?.userId as number);

if (!userStore.isSignedIn) {
  router.push('/login');
}
</script>
