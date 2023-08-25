<style scoped>
.icon-btn {
  background-color: transparent;
  border: none;
  padding: 8px;
  outline: none;
  cursor: pointer;
  height: 40px;
  aspect-ratio: 1;
  font-size: 1.5rem;
  display: grid;
  place-items: center;
  color: var(--color-primary);
  transition: all 0.2s ease-in-out;
  border-radius: 50%;
}

.icon-btn:hover {
  background-color: var(--color-primary);
  color: white;
}

.icon-btn--active {
  background-color: var(--color-primary);
  color: white;
}
</style>

<template>
  <button
    @click="() => click()"
    :class="{
      'icon-btn': true,
      'icon-btn--active': active,
    }"
  >
    <font-awesome-icon :icon="icon" />
  </button>
</template>

<script setup lang="ts">
defineProps({
  icon: {
    type: String,
    required: true,
  },
  active: {
    type: Boolean,
    reqiured: false,
    default: false,
  },
  click: {
    type: Function,
    required: false,
    default: () => {},
  },
});
</script>
