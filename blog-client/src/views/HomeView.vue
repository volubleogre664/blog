<style src="@/styles/HomeView.css"></style>

<template>
  <main class="home">
    <aside>
      <div v-if="!userStore.isSignedIn" class="signup__block">
        <h2>Join the community</h2>
        <p>Sign in or create an account in seconds and start giving to the community</p>
        <button class="signupBtn btn">Create Account</button>
      </div>

      <SidebarNav />
    </aside>

    <main>
      <FeedItem
        :v-if="articlesStore.articles.length"
        v-for="article in articlesStore.articles"
        :key="article.id"
        :article="article"
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

const userStore = useUserStore();
const articlesStore = useArticlesStore();
</script>
