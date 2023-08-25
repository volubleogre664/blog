<style src="@/styles/FeedItem.css"></style>

<template>
  <div class="feedItem">
    <header>
      <div v-if="article.articleCoverImage" class="feedItem__cover">
        <img :src="article.articleCoverImage" alt="feed item cover" />
      </div>
      <div class="feedItem__author">
        <UserAvatar :src="author?.userPicture ?? article.author?.userPicture" />

        <div class="feedItem__authorInfo">
          <h3>
            {{
              `${author?.firstname ?? article.author?.firstname} ${
                author?.lastname ?? article.author?.lastname
              }`
            }}
          </h3>
          <p>{{ dayjs(article?.articlePublishDate).format('DD MMM YYYY (HH:mm)') }}</p>
        </div>
      </div>
    </header>

    <main>
      <h2 @click="openArticle" class="feedItem__title">{{ article.articleTitle }}</h2>
    </main>

    <footer v-if="showActions">
      <div>
        <IconButton icon="fa-solid fa-heart" />
        <span>{{ article.likes }} reactions</span>
      </div>

      <div>
        <IconButton icon="fa-solid fa-comment" />
        <span>{{ article.comments }} comments</span>
      </div>

      <div>
        <IconButton icon="fa-solid fa-bookmark" />
        <span>Save</span>
      </div>
    </footer>
  </div>
</template>

<script setup lang="ts">
import IconButton from './IconButton.vue';
import UserAvatar from './UserAvatar.vue';
import router from '@/router';
import { useArticlesStore } from '@/stores/articles';
import { useUserStore } from '@/stores/user';
import dayjs from 'dayjs';

const articlesStore = useArticlesStore();

const props = defineProps({
  article: {
    type: Object,
    required: true,
  },
  showActions: {
    type: Boolean,
    required: false,
    default: true,
  },
  author: {
    type: Object,
    required: false,
  },
});

const openArticle = () => {
  useUserStore().setLoading(true);
  articlesStore.setArticleId(props.article.articleId);
  router.push(`/article/${props.article.articleId}`);
};
</script>
