<style src="@/styles/AppHeader.css"></style>

<template>
  <header class="app__header">
    <div class="app__headerContainer">
      <div>
        <router-link to="/" class="app__headerBrand">
          <span>BLOG</span>
          <span>SPACE</span>
        </router-link>

        <form class="app__headerSearch">
          <input type="text" placeholder="Search..." />
          <button type="submit">
            <font-awesome-icon icon="fa-solid fa-magnifying-glass" />
          </button>
        </form>
      </div>

      <nav class="app__headerNav">
        <ul v-if="!userStore.isSignedIn">
          <li>
            <button @click="handleHeaderLoginClick" class="loginBtn btn">Login</button>
          </li>
          <li>
            <button @click="handleHeaderRegisterClick" class="registerBtn btn">
              Create Account
            </button>
          </li>
        </ul>

        <ul v-if="userStore.isSignedIn">
          <li>
            <button @click="handleHeaderLogoutClick" class="logoutBtn btn">Logout</button>
          </li>
          <li>
            <UserAvatar :src="userStore.user?.userPicture" />
          </li>
        </ul>
      </nav>
    </div>
  </header>
</template>

<script setup lang="ts">
// import { RouterLink, RouterView } from 'vue-router';
import { useUserStore } from '@/stores/user';
import router from '@/router';
import UserAvatar from './UserAvatar.vue';

const userStore = useUserStore();

const handleHeaderLoginClick = () => {
  userStore.setLoading(true);
  userStore.signInUser();
};

const handleHeaderRegisterClick = () => {
  userStore.setLoading(true);
  userStore.registerUser();
  router.push('/signup');
  userStore.signOutUser();
};

const handleHeaderLogoutClick = () => {
  userStore.signOutUser();
};
</script>
