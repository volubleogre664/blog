<style src="@/styles/CreateArticleView.css"></style>

<template>
  <section class="editor">
    <header class="editor__header">
      <div class="editor__header__container">
        <h2></h2>
        <div class="editor__header__actions">
          <button class="loginBtn btn" @click="publishArticle">Publish</button>
        </div>
      </div>
    </header>

    <main class="editor__body">
      <header>
        <button class="btn" @click="addCoverImage">Add a cover image</button>
        <input
          @change="onFileChange"
          id="add__cover__image"
          type="file"
          :ref="coverImgRef"
          style="display: none"
        />
        <img v-if="src" :src="src" alt="cover image" />
      </header>

      <h1 contenteditable role="textbox" class="editor__body__title"></h1>

      <v-md-editor v-model="text" height="400px"></v-md-editor>
    </main>
  </section>
</template>

<script setup lang="ts">
import { ref, type VNodeRef } from 'vue';
import { useUserStore } from '@/stores/user';
import { useArticlesStore } from '@/stores/articles';
import { createArticle } from '@/api/api';
import { uploadToCloud } from '@/utils/uploadFiles';
import router from '@/router';

const userStore = useUserStore();
const articlesStore = useArticlesStore();

if (!userStore.isSignedIn) router.push('/login');

const coverImgRef = ref<VNodeRef | null>(null);
const text = ref('');
const src = ref('');

const addCoverImage = () => {
  const fileInput = document.querySelector('#add__cover__image') as HTMLInputElement;
  fileInput.click();
};

const onFileChange = (e: Event) => {
  const file = (e.target as HTMLInputElement).files?.[0];

  const fileReader = new FileReader();

  // read file as data url here

  fileReader.onload = async (readerEvent) => {
    const result = readerEvent.target?.result as string;

    const getImageUrl = await uploadToCloud(result, file?.name as string);
    console.log(getImageUrl);
    src.value = getImageUrl;
  };

  fileReader.readAsDataURL(file as Blob);

  console.log(file);
};

const publishArticle = async () => {
  const title = document.querySelector('.editor__body__title')?.innerHTML;

  const article = {
    articleTitle: title,
    articleBody: text.value,
    isPublished: true,
    articlePublishDate: new Date(),
    authorId: userStore.user?.userId,
    articleCoverImage: src.value || null,
    articleId: 0,
  };

  userStore.setLoading(true);
  const newArticle = await createArticle(article);
  articlesStore.addArticle(newArticle);
  userStore.setLoading(false);
  router.push('/');
};
</script>
