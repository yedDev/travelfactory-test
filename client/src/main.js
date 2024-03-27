import { createApp } from 'vue'
import App from './App.vue'
import { createPinia } from "pinia";
import apiService from "./core/services/apiService";

import router from "./core/router.js";

const app = createApp(App);
app.use(router);
apiService.init(app)

app.use(createPinia());

app.mount("#app");
