import 'es6-shim';
import './styles/global.scss';

import Vue from 'vue';
import App from './App.vue';

import router from './router';

const app = new Vue({
  el: '#app',
  render: h => h(App),
  router
});
