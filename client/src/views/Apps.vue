<template>
  <div class="row apps-card">
    <template v-for="app in apps" v-bind:key="app.id">
      <AppCard class="col-sm-4 mx-5 mb-5" :appId="String(app.id)"></AppCard>
    </template>
    <div v-if="apps.length == 0">
      <div class="alert alert-primary" role="alert">
        <button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#addAppModal">
          Add New App
        </button>
      </div>

    </div>
  </div>
</template>

<script>
import { defineComponent, ref } from "vue";
import AppCard from "./AppCard.vue";
import { useAppsDataStore } from "../core/stores/appsStores";

export default defineComponent({
  name: "apps-view",
  components: {
    AppCard
  },
  setup() {
    const apps = ref([])
    const appsDataStore = useAppsDataStore()
    appsDataStore.fetchData()
    const setData = async () => {
      // refresh the data
      apps.value = appsDataStore.getAppsData
    }
    setData()
    appsDataStore.$subscribe(setData)

    return {
      apps,
      useAppsDataStore
    };
  },
});
</script>

<style scoped>
.apps-card {
  min-width: 100%;
}
</style>
