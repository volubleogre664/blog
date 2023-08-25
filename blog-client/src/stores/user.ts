import { ref, computed } from 'vue';
import { defineStore } from 'pinia';
import { signIn, signOut } from '@/auth/authPopup';
import { getAccount, updateUserData } from '@/api/api';

import { type User } from '@/types/user';

export const useUserStore = defineStore('user', () => {
  const user = ref<User | null>(null);
  const token = ref<string | null>(null);
  const loading = ref(false);

  const isSignedIn = computed(() => user.value !== null);

  function setUser(newUser: User | null) {
    user.value = newUser;
  }

  async function signInUser() {
    const newUser = await signIn();
    token.value = newUser.token;
    localStorage.setItem('token', newUser.token);
    const userData = await getAccount();
    console.log(userData);
    setUser(userData);
    setLoading(false);
  }

  async function registerUser() {
    const newUser = await signIn();
    token.value = newUser.token;
    localStorage.setItem('token', newUser.token);
    setUser(newUser.user);
    setLoading(false);
  }

  async function updateUser(user: User) {
    const newUser = await updateUserData(user);
    setUser(newUser);
  }

  function signOutUser() {
    signOut();
    setUser(null);
  }

  function setLoading(newLoading: boolean) {
    loading.value = newLoading;
  }

  return {
    user,
    loading,
    isSignedIn,
    token,
    updateUser,
    registerUser,
    setLoading,
    setUser,
    signInUser,
    signOutUser,
  };
});
