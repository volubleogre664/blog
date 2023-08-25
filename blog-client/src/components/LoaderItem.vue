<style scoped>
.loader__container {
  position: fixed;
  top: 0;
  left: 0;
  z-index: 10000;
  width: 100vw;
  height: 100vh;
  display: grid;
  place-items: center;
  background-color: rgba(44, 62, 80, 0.7);
}

.loader {
  display: inline-block;
  position: relative;
  width: 80px;
  height: 80px;
}
.loader div {
  display: inline-block;
  position: absolute;
  left: 8px;
  width: 16px;
  background: var(--color-primary);
  animation: loader 1.2s cubic-bezier(0, 0.5, 0.5, 1) infinite;
}
.loader div:nth-child(1) {
  left: 8px;
  animation-delay: -0.24s;
}
.loader div:nth-child(2) {
  left: 32px;
  animation-delay: -0.12s;
}
.loader div:nth-child(3) {
  left: 56px;
  animation-delay: 0;
}
@keyframes loader {
  0% {
    top: 8px;
    height: 64px;
  }
  50%,
  100% {
    top: 24px;
    height: 32px;
  }
}
</style>

<template>
  <div class="loader__container">
    <div class="loader">
      <div></div>
      <div></div>
      <div></div>
    </div>
  </div>
</template>

<script setup lang="ts"></script>
