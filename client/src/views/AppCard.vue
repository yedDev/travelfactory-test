<template>
  <div v-if="selected_app" class="card">
    <div class="card-header">
      <b> {{ selected_app.nickname }}</b>
    </div>
    <div class="card-body col-lg-12 col-md-8 col-sm-6">
      <b>Last Updated:</b> <br> {{ new Date(selected_app.lastUpdated).toUTCString() }}
    </div>
    <div class="card-footer">
      <RouterLink :to="`/app/${selected_app.id}`" class="btn btn-success mx-2">Deploy</RouterLink>
      <button @click="downloanCsv" class="btn btn-success mx-2">Downloan .csv</button>
    </div>
  </div>
</template>

<script>
import { defineComponent, ref, watch } from "vue";
import { RouterLink } from 'vue-router'
import { useAppsDataStore } from "../core/stores/appsStores";
import { supported_lang } from '../data/global'
import apiService from "@/core/services/apiService";
import JSONToCSVConverter from "@/core/code/JSONToCSVConverter"

export default defineComponent({
  name: "app-card",
  components: {
    RouterLink
  },
  props: {
    appId: String,
  },
  setup(props) {
    const selected_app = ref(null)
    const appsDataStore = useAppsDataStore()

    const setData = () => {
      const apps = appsDataStore.getAppsData
      if (!apps || !apps.length) return;

      for (let i = 0; i < apps.length; i++) {
        const app = apps[i]
        if (app.id == props.appId)
          selected_app.value = app
      }
    }


    const downloanCsv = async () => {
      if (!selected_app.value) return;
      try {
        const res = await apiService.get(`api/app/${selected_app.value.id}`)
        if (!res.jsonData) alert('No data yet')
        const json = JSON.parse(res.jsonData)
        const converter = new JSONToCSVConverter()
        converter.convertToCSV(json)
        converter.downloadCSV('name.csv')
      } catch (error) {
        console.error(error);
      }
    }



    watch(props, setData)
    appsDataStore.$subscribe(setData)
    setData()

    return {
      selected_app,
      supported_lang,
      downloanCsv,

    };
  },
});
</script>
@/core/code/JSONToCSVConverter