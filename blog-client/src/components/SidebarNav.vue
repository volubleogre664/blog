<style src="@/styles/Navigation.css"></style>

<template>
  <nav class="navigation">
    <h2>Menu</h2>

    <ul>
      <li>
        <router-link to="/">
          <span class="icon">
            <font-awesome-icon icon="fa-solid fa-house" />
          </span>
          <span>Home</span>
        </router-link>
      </li>
      <li v-if="userStore.isSignedIn">
        <router-link to="/bookmark">
          <span class="icon">
            <font-awesome-icon icon="fa-solid fa-bookmark" />
          </span>
          <span>Bookmarks</span>
        </router-link>
      </li>

      <li v-if="userStore.isSignedIn">
        <router-link to="/editor">
          <span class="icon">
            <font-awesome-icon icon="fa-solid fa-pen-nib" />
          </span>
          <span>Create Post</span>
        </router-link>
      </li>
    </ul>
  </nav>
</template>

<script setup lang="ts">
import { useUserStore } from '@/stores/user';

const userStore = useUserStore();
</script>
