<style scoped>
.avatar {
  cursor: pointer;
  height: 50px;
  aspect-ratio: 1;
  overflow: hidden;
  border-radius: 50%;
  box-shadow: rgba(50, 50, 93, 0.25) 0px 30px 60px -12px, rgba(0, 0, 0, 0.3) 0px 18px 36px -18px;
}

.avatar img {
  width: 50px;
  height: 50px;
  object-fit: cover;
}
</style>

<template>
  <div @click="openProfile" class="avatar">
    <img :src="src || placeholder" alt="user avatar" />
  </div>
</template>

<script setup lang="ts">
import router from '@/router';

const placeholder =
  'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQrIPFzd_Y8UgFpD7VvKXAISw3kUNCpfm2sFK-SRGw&s';

defineProps({
  src: {
    type: String,
    required: false,
  },
});

const openProfile = () => {
  router.push({ name: 'profile' });
};
</script>
