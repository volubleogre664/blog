import type { User } from '@/types/user';
import axios from './axios';

const endpoints = {
  createAccount: '/user/',
  getAccount: '/user/',
  getArticles: '/article/',
  getArticle: '/article/',
  createArticle: '/article/',
  updateArticle: '/article/',
  comment: '/comment/',
  favorite: '/favorite/',
  bookmark: '/bookmark/',
  getBookmarks: '/bookmark/',
};

async function deleteArticleApi(articleId: number): Promise<boolean> {
  const response = await axios.delete(`${endpoints.updateArticle}${articleId}`);

  if (response.status === 200) {
    return true;
  } else {
    return false;
  }
}

async function updateUserData(user: User): Promise<User | null> {
  const response = await axios.put(endpoints.createAccount, user);

  if (response.status === 200) {
    return response.data;
  } else {
    return null;
  }
}

async function createAccount(newUser: User): Promise<User | null> {
  const response = await axios.post(endpoints.createAccount, newUser);

  if (response.status === 200) {
    return response.data;
  } else {
    return null;
  }
}

async function getAccount(): Promise<User | null> {
  const response = await axios.get(`${endpoints.getAccount}`);

  if (response.status === 200) {
    return response.data;
  } else {
    return null;
  }
}

async function getFeedArticles(): Promise<any[] | []> {
  const response = await axios.get(`${endpoints.getArticles}`);

  if (response.status === 200) {
    console.log(response.data);
    return response.data;
  } else {
    return [];
  }
}

async function getUserArticlesApi(authorId: number): Promise<any[]> {
  const response = await axios.get(`${endpoints.getArticles}getUserArticles/${authorId}`);

  if (response.status === 200) {
    return response.data;
  } else {
    return [];
  }
}

async function getArticle(slug: number): Promise<any> {
  const response = await axios.get(`${endpoints.getArticle}${slug}`);

  if (response.status === 200) {
    return response.data;
  } else {
    return null;
  }
}

async function createArticle(article: any): Promise<any> {
  const response = await axios.post(endpoints.createArticle, article);

  if (response.status === 200) {
    return response.data;
  } else {
    return null;
  }
}

async function comment(comment: any): Promise<any> {
  const response = await axios.post(endpoints.comment, comment);

  if (response.status === 200) {
    return response.data;
  } else {
    return null;
  }
}

async function favorite(favorite: any): Promise<any> {
  const response = await axios.post(endpoints.favorite, favorite);

  if (response.status === 200) {
    return response.data;
  } else {
    return null;
  }
}

async function bookmark(bookmark: any): Promise<any> {
  const response = await axios.post(endpoints.bookmark, bookmark);

  if (response.status === 200) {
    return response.data;
  } else {
    return null;
  }
}

async function getBookmarks(userId: number): Promise<any> {
  const response = await axios.get(`${endpoints.getBookmarks}${userId || ''}`);

  if (response.status === 200) {
    return response.data;
  } else {
    return null;
  }
}

export {
  createAccount,
  getArticle,
  createArticle,
  comment,
  favorite,
  bookmark,
  getFeedArticles,
  getBookmarks,
  getAccount,
  getUserArticlesApi,
  updateUserData,
  deleteArticleApi,
};
