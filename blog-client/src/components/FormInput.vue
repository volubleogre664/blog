<style scoped>
.input {
  width: 100%;
  display: flex;
  flex-direction: column;
}

.input label {
  font-size: 1.2rem;
  font-weight: 600;
  margin-bottom: 0.2rem;
  color: var(--color-primary);
}

.input input {
  padding: 0.5rem;
  border-radius: 0.2rem;
  background-color: transparent;
  border: 1px solid var(--color-primary);
  font-size: 1.1rem;
  outline: none;
  color: white;
}
</style>

<template>
  <div class="input">
    <label :for="id">{{ label }}</label>
    <input
      @input="(e) => onInput(e)"
      :name="id"
      :id="id"
      :value="value"
      :disabled="disabled"
      type="email"
      :required="required"
      :placeholder="placeholder"
    />
  </div>
</template>

<script setup lang="ts">
defineProps({
  label: {
    type: String,
    required: true,
  },
  id: {
    type: String,
    required: true,
  },
  placeholder: {
    type: String,
    required: true,
  },
  value: {
    type: String,
    required: true,
  },
  onInput: {
    type: Function,
    required: true,
  },
  required: {
    type: Boolean,
    required: true,
    default: false,
  },
  disabled: {
    type: Boolean,
    required: false,
    default: false,
  },
});
</script>
