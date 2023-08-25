<style src="@/styles/RegisterView.css"></style>

<template>
  <main class="register">
    <h1>Here are your Account details</h1>

    <section class="register__body">
      <form>
        <div class="form__picture">
          <img :src="src || user.userPicture || placeholderImage" alt="user profile" />
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
            :value="userStore.user?.bio"
            @input="onInput"
            placeholder="I work in Payroll"
          ></textarea>
        </div>

        <button @click="updateUser" class="btn">Update Details</button>
      </form>

      <section class="user__articles">
        <h2>Your Articles</h2>

        <div v-if="articlesStore.userArticles.length" class="user__articlesList">
          <div v-for="article in articlesStore.userArticles" :key="article.articleId">
            <FeedItem
              :showActions="false"
              :author="(userStore?.user as Object)"
              :article="article"
            />
          </div>
        </div>
      </section>
    </section>
  </main>
</template>

<script setup lang="ts">
import { ref, type VNodeRef } from 'vue';

import { type User } from '@/types/user';
import { useUserStore } from '@/stores/user';
import { useArticlesStore } from '@/stores/articles';
import { uploadToCloud } from '@/utils/uploadFiles';
import router from '@/router';

import FormInput from '@/components/FormInput.vue';
import FeedItem from '@/components/FeedItem.vue';

const userStore = useUserStore();
const src = ref<string | null>(userStore.user?.userPicture || null);
const articlesStore = useArticlesStore();

if (!userStore.isSignedIn) {
  router.push('/');
}

const placeholderImage =
  'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQrIPFzd_Y8UgFpD7VvKXAISw3kUNCpfm2sFK-SRGw&s';

const inputRef = ref<VNodeRef | null>(null);
const user = ref<User>({
  userId: userStore.user?.userId || 0,
  firstname: userStore.user?.firstname || '',
  lastname: userStore.user?.lastname || '',
  email: userStore.user?.email || '',
  bio: userStore.user?.bio || '',
  userPicture: userStore.user?.userPicture || '',
} as User);

articlesStore.getUserArticles(userStore.user?.userId || 0);

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

const onInput = (e: Event) => {
  const target = e.target as HTMLInputElement;
  user.value[target.name] = target.value as string;
};

const updateUser = async (e: Event) => {
  userStore.setLoading(true);
  e.preventDefault();

  user.value.identityAccountId = userStore.user?.identityAccountId || '';
  user.value.userPicture = src.value ?? userStore.user?.userPicture ?? null;
  user.value.email = userStore.user?.email || '';
  user.value.firstname = userStore.user?.firstname || '';
  user.value.lastname = userStore.user?.lastname || '';
  await userStore.updateUser(user.value);
  userStore.setLoading(false);
};
</script>
