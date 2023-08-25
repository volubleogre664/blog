import { ref } from 'vue';
import { defineStore } from 'pinia';
import {
  getFeedArticles,
  getArticle as getArticleApi,
  comment as commentApi,
  favorite as likeArticle,
  getUserArticlesApi,
  bookmark,
  getBookmarks,
  deleteArticleApi,
} from '@/api/api';

export const useArticlesStore = defineStore('articles', () => {
  const articles = ref<any[]>([]);
  const savedArticles = ref<any[]>([]);
  const currentArticle = ref<any | null>(null);
  const articleId = ref<number>(0);
  const userArticles = ref<any[]>([]);

  function setArticles(newArticles: any[]) {
    articles.value = newArticles;
  }

  function addArticle(newArticle: any) {
    articles.value = [newArticle, ...articles.value];
  }

  async function getArticles() {
    const articles = await getFeedArticles();

    setArticles(articles);
  }

  async function getArticle(articleId: number) {
    const article = await getArticleApi(articleId);

    setCurrentArticle(article);
  }

  function setCurrentArticle(newArticle: any) {
    currentArticle.value = newArticle;
  }

  function setArticleId(newArticleId: number) {
    articleId.value = newArticleId;
  }

  function setUserArticles(newArticles: any[]) {
    userArticles.value = newArticles;
  }

  async function addComment(comment: any) {
    const newComment = await commentApi(comment);

    currentArticle.value.comments.push(newComment);
    articles.value.find((article) => article.id === currentArticle.value.id).comments += 1;
  }

  async function bookmarkArticle(articleId: number, authorId: number) {
    if (
      currentArticle.value.bookmarks.find(
        (bookmark: any) =>
          bookmark.bookmarkOwnerId === authorId && bookmark.articleId === articleId,
      )
    ) {
      const bookmarkId = currentArticle.value.bookmarks.find(
        (bookmark: any) => bookmark.bookmarkOwnerId === authorId,
      ).bookmarkId;

      bookmark({
        bookmarkId,
        articleId,
        bookmarkOwnerId: authorId,
      });

      currentArticle.value.bookmarks = currentArticle.value.bookmarks.filter(
        (bookmark: any) => bookmark.bookmarkOwnerId !== authorId,
      );
      articles.value.find((article) => article.id === currentArticle.value.id).bookmarks -= 1;

      return;
    }

    const bookmarked = await bookmark({
      bookmarkId: 0,
      articleId,
      bookmarkOwnerId: authorId,
    });

    currentArticle.value.bookmarks.push(bookmarked);
    articles.value.find((article) => article.id === currentArticle.value.id).bookmarks += 1;
  }

  async function addFavorite(articleId: number, authorId: number) {
    // Check if favorite alredy exist in list
    if (currentArticle.value.likes.find((like: any) => like.authorId === authorId)) {
      const likeId = currentArticle.value.likes.find(
        (like: any) => like.authorId === authorId,
      ).favoriteId;

      likeArticle({
        favoriteId: likeId,
        articleId,
        authorId,
      });

      currentArticle.value.likes = currentArticle.value.likes.filter(
        (like: any) => like.authorId !== authorId,
      );
      articles.value.find((article) => article.id === currentArticle.value.id).likes -= 1;

      return;
    }

    const favorite = await likeArticle({
      favoriteId: 0,
      articleId,
      authorId,
    });

    currentArticle.value.likes.push(favorite);
    articles.value.find((article) => article.id === currentArticle.value.id).likes += 1;
  }

  async function getUserArticles(authorId: number) {
    const articles = await getUserArticlesApi(authorId);

    setUserArticles(articles);
  }

  async function getSavedArticles(authorId: number) {
    const articles = await getBookmarks(authorId);

    savedArticles.value = articles;
  }

  async function deleteArticle() {
    const articleId = currentArticle.value.articleId;

    const data = await deleteArticleApi(articleId);

    if (data) {
      console.log('Yeah deleted');
      setArticles(articles.value.filter((article) => article.articleId !== articleId));

      setCurrentArticle(null);
    }
  }

  return {
    articles,
    currentArticle,
    articleId,
    userArticles,
    savedArticles,
    getSavedArticles,
    getUserArticles,
    addFavorite,
    addComment,
    getArticle,
    setArticleId,
    setCurrentArticle,
    addArticle,
    getArticles,
    setArticles,
    deleteArticle,
    bookmarkArticle,
  };
});
