<style src="@/styles/ArticleView.css"></style>

<template>
  <section class="article">
    <aside>
      <div class="article__actions">
        <span>
          <IconButton
            :click="likeArticle"
            :active="userStore.isSignedIn && articlesStore.currentArticle?.likes?.find((_:any) => _.authorId == userStore.user?.userId) !== undefined"
            icon="fa-solid fa-heart"
          />
          <span>{{ articlesStore.currentArticle?.likes?.length }}</span>
        </span>

        <span>
          <IconButton :click="showComments" icon="fa-solid fa-comment" />
          <span>{{ articlesStore.currentArticle?.comments?.length }}</span>
        </span>

        <span>
          <IconButton
            :click="bookmarkArticle"
            :active="userStore.isSignedIn && articlesStore.currentArticle?.bookmarks?.find((_:any) => _.bookmarkOwnerId == userStore.user?.userId) !== undefined"
            icon="fa-solid fa-bookmark"
          />
          <span>{{ articlesStore.currentArticle?.bookmarks?.length }}</span>
        </span>
      </div>
    </aside>

    <main class="article__body">
      <header>
        <img
          v-if="articlesStore.currentArticle?.articleCoverImage"
          :src="articlesStore.currentArticle?.articleCoverImage || ''"
          alt="article cover"
        />
        <h1>
          {{ articlesStore.currentArticle?.articleTitle || 'Article Title' }}
        </h1>
      </header>

      <main>
        <p>
          <Markdown :source="articlesStore.currentArticle?.articleBody || 'Article Description'" />
          <!-- {{ articlesStore.currentArticle?.articleBody || 'Article Content' }} -->
        </p>
      </main>

      <hr />

      <footer id="top-comments">
        <h2>Top Comments ({{ articlesStore.currentArticle?.comments?.length || 0 }})</h2>

        <div v-if="userStore.isSignedIn" class="article__addComment">
          <UserAvatar :src="userStore.user?.userPicture" />
          <form @submit="sendComment">
            <textarea
              required
              :value="commentText"
              @input="onInput"
              placeholder="Leave your input..."
            />
            <button class="btn" type="submit">Submit</button>
          </form>
        </div>

        <div class="article__comments">
          <ArticleComment
            v-for="comment in articlesStore.currentArticle?.comments"
            :key="comment.commentId"
            :comment="comment"
          />
          <!-- <ArticleComment /> -->
        </div>
      </footer>
    </main>

    <aside>
      <div class="article__author">
        <h3>Author Details</h3>
        <div class="article__authorInfo">
          <UserAvatar :src="author?.userPicture" />
          <div>
            <h3>{{ `${author?.firstname} ${author?.lastname}` }}</h3>
            <p>
              {{
                dayjs(articlesStore.currentArticle?.articlePublishDate).format(
                  'DD MMM YYYY (HH:mm)',
                )
              }}
            </p>
          </div>
        </div>

        <p>{{ author?.bio }}</p>
      </div>

      <br />
      <button
        @click="deleteArticle"
        v-if="userStore.isSignedIn && userStore.user?.userId === authorId"
        class="btn"
      >
        Delete Article
      </button>
    </aside>
  </section>
</template>

<script setup lang="ts">
import { ref } from 'vue';
import IconButton from '@/components/IconButton.vue';
import UserAvatar from '@/components/UserAvatar.vue';
import ArticleComment from '@/components/ArticleComment.vue';
import Markdown from 'vue3-markdown-it';
import dayjs from 'dayjs';
import router from '@/router';

import { useArticlesStore } from '@/stores/articles';
import { useUserStore } from '@/stores/user';

const commentText = ref('');
const authorId = ref(0);

const articlesStore = useArticlesStore();
const userStore = useUserStore();

const author = articlesStore.articles.find((_) => _.articleId == articlesStore.articleId)?.author;

const onInput = (e: Event) => {
  const target = e.target as HTMLInputElement;
  commentText.value = target.value;
};

(async () => {
  await articlesStore.getArticle(articlesStore.articleId);
  console.log(articlesStore.currentArticle);
  authorId.value = articlesStore.currentArticle.authorId;
  userStore.setLoading(false);
})();

const showComments = () => {
  const comments = document.querySelector('#top-comments');
  comments?.scrollIntoView({ behavior: 'smooth' });
};

const deleteArticle = async () => {
  if (!userStore.isSignedIn) return;

  userStore.setLoading(true);

  await articlesStore.deleteArticle();

  userStore.setLoading(false);
  router.push('/');
};

const sendComment = async (e: Event) => {
  e.preventDefault();
  if (!userStore.isSignedIn) return;

  await articlesStore.addComment({
    commentBody: commentText.value,
    commentAuthorId: userStore.user?.userId,
    articleId: articlesStore.currentArticle.articleId,
    commentDate: new Date(),
  });

  commentText.value = '';
};

const bookmarkArticle = async () => {
  if (!userStore.isSignedIn) return;

  await articlesStore.bookmarkArticle(
    articlesStore.currentArticle.articleId,
    userStore.user?.userId as number,
  );
};

const likeArticle = async () => {
  if (!userStore.isSignedIn) return;

  await articlesStore.addFavorite(
    articlesStore.currentArticle.articleId,
    userStore.user?.userId as number,
  );
};
</script>
