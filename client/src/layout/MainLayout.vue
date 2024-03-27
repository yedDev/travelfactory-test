<template>


  <div class="container-fluid">
    <div class="row">
      <!-- Sidebar -->
      <div class="col-sm-3 sidebar">
        <NavBar></NavBar>
      </div>
      <!-- Body Container -->
      <div class="col-sm-9 d-flex justify-content-start align-items-center">
        <Content></Content>
      </div>
    </div>
  </div>

</template>

<script>
import { defineComponent, } from "vue";
import Content from "./Content.vue";
import NavBar from "../components/NavBar.vue";
import { useAppsDataStore } from "../core/stores/appsStores";

export default defineComponent({
  name: "main-layout",
  components: {
    Content,
    NavBar
  },
  setup() {
    try {
      const appsStore = useAppsDataStore()
      appsStore.fetchData()
    } catch (error) {
      console.log(error);
    }

    return {
    };
  },
});
</script>

<style scoped></style>
