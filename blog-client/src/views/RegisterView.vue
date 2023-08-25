<style src="@/styles/RegisterView.css"></style>

<template>
  <div v-if="!userStore.isSignedIn" class="register">
    <h1>Loading your account details...</h1>
  </div>

  <main v-if="userStore.isSignedIn" class="register">
    <h1>Finish Setting up your account</h1>

    <section class="register__body">
      <form @submit="registerUser">
        <div class="form__picture">
          <img :src="src || user.userProfile || placeholderImage" alt="user profile" />
          <button @click="addProfileImage" class="btn">Upload Image</button>

          <input
            @change="onFileChange"
            style="display: none"
            id="add__profile__image"
            type="file"
            :ref="inputRef"
          />
        </div>

        <div class="form__header">
          <FormInput
            id="firstname"
            label="Firstname"
            placeholder="Enter firstname"
            disabled
            :required="true"
            :onInput="onInput"
            :value="userStore.user?.firstname || ''"
          />

          <FormInput
            id="lastname"
            label="Lastname"
            placeholder="Enter lastname"
            disabled
            :required="true"
            :onInput="onInput"
            :value="userStore.user?.lastname || ''"
          />
        </div>

        <div>
          <FormInput
            id="email"
            label="Email"
            placeholder="Enter email"
            disabled
            :required="true"
            :onInput="onInput"
            :value="userStore.user?.email || ''"
          />
        </div>

        <div class="form__bio">
          <label for="bio">About you</label>
          <textarea
            name="bio"
            id="bio"
            :value="user.bio"
            @input="onInput"
            placeholder="I work in Payroll"
          ></textarea>
        </div>

        <button type="submit" class="btn">Submit Details</button>
      </form>
    </section>
  </main>
</template>

<script setup lang="ts">
import { ref, type VNodeRef } from 'vue';

import { type User } from '@/types/user';
import { useUserStore } from '@/stores/user';
import { createAccount } from '@/api/api';
import { uploadToCloud } from '@/utils/uploadFiles';
import router from '@/router';

import FormInput from '@/components/FormInput.vue';

const src = ref<string>('');
const userStore = useUserStore();
const placeholderImage =
  'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQrIPFzd_Y8UgFpD7VvKXAISw3kUNCpfm2sFK-SRGw&s';

const inputRef = ref<VNodeRef | null>(null);
const user = ref<User>({
  userId: 0,
  firstname: userStore.user?.firstname || '',
  lastname: userStore.user?.lastname || '',
  email: userStore.user?.email || '',
  bio: '',
  userPicture: '',
} as User);

const onInput = (e: Event) => {
  const target = e.target as HTMLInputElement;
  user.value[target.name] = target.value as string;
};

const addProfileImage = (e: Event) => {
  e.preventDefault();
  const fileInput = document.querySelector('#add__profile__image') as HTMLInputElement;
  fileInput.click();
};

const onFileChange = (e: Event) => {
  const file = (e.target as HTMLInputElement).files?.[0];

  const fileReader = new FileReader();

  // read file as data url here

  fileReader.onload = async (readerEvent) => {
    const result = readerEvent.target?.result as string;

    userStore.setLoading(true);
    const getImageUrl = await uploadToCloud(result, file?.name as string);
    userStore.setLoading(false);
    src.value = getImageUrl;
  };

  fileReader.readAsDataURL(file as Blob);
};

const registerUser = async (e: Event) => {
  e.preventDefault();

  // userStore.setLoading(true);
  user.value.identityAccountId = userStore.user?.identityAccountId || '';
  user.value.userPicture = src.value || null;
  user.value.email = userStore.user?.email || '';
  user.value.firstname = userStore.user?.firstname || '';
  user.value.lastname = userStore.user?.lastname || '';
  const userData = await createAccount(user.value);

  if (userData) {
    userStore.setUser(userData);
  }

  userStore.setLoading(false);
  router.push('/');
};
</script>
