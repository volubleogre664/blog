<style src="@/styles/ArticleComment.css"></style>

<template>
  <div class="comment">
    <div class="comment__header">
      <div class="comment__header__user">
        <UserAvatar :src="comment?.author?.userPicture" />
        <div>
          <h3>{{ `${comment?.author?.firstname} ${comment?.author?.lastname}` }}</h3>
          <span>{{ dayjs(comment.commentDate).format('DD MMM YYYY (HH:mm)') }}</span>
        </div>
      </div>
    </div>
    <div class="comment__body">
      <p>
        {{ comment.commentBody }}
      </p>
    </div>
  </div>
</template>

<script setup lang="ts">
import UserAvatar from './UserAvatar.vue';
import dayjs from 'dayjs';

defineProps({
  comment: {
    type: Object,
    required: true,
  },
});
</script>
