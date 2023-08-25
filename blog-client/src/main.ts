import './assets/main.css';

import { createApp } from 'vue';
import { createPinia } from 'pinia';

import { library } from '@fortawesome/fontawesome-svg-core';
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome';

import VMdEditor from '@kangc/v-md-editor';
import '@kangc/v-md-editor/lib/style/base-editor.css';
import vuepressTheme from '@kangc/v-md-editor/lib/theme/vuepress.js';
import '@kangc/v-md-editor/lib/theme/style/vuepress.css';
import enUS from '@kangc/v-md-editor/lib/lang/en-US';
import 'highlight.js/styles/monokai.css';

import {
  faCheck,
  faTrash,
  faPen,
  faXmark,
  faCloudArrowUp,
  faComment,
  faHeart,
  faHouse,
  faMagnifyingGlass,
  faBookmark,
  faBold,
  faItalic,
  faListOl,
  faPenNib,
  faListUl,
  faLink,
  faImage,
  faHeading,
} from '@fortawesome/free-solid-svg-icons';

library.add(
  faCheck,
  faBookmark,
  faTrash,
  faPen,
  faXmark,
  faMagnifyingGlass,
  faCloudArrowUp,
  faHeart,
  faComment,
  faHouse,
  faPenNib,
  faBold,
  faItalic,
  faListOl,
  faListUl,
  faLink,
  faImage,
  faHeading,
);

import App from './App.vue';
import router from './router';

const app = createApp(App);

app.use(createPinia());
app.use(router);

VMdEditor.use(vuepressTheme);
VMdEditor.lang.use('en-US', enUS);
app.use(VMdEditor);

app.component('font-awesome-icon', FontAwesomeIcon);

app.mount('#root');
