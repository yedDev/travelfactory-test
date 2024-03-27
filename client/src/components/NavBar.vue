<template>
  <div class="card mb-5">
    <div class="card-body d-flex flex-column ">
      <RouterLink class="btn btn-sm btn-success mb-5" :to="`/apps`">Apps</RouterLink>
      <template v-for="app in apps_arr" v-bind:key="app.id">
        <div>
          <RouterLink class="btn btn-sm mb-5" :class="active_id == app.id ? 'btn-outline-secondary' : 'btn-secondary'"
            :to="`/app/${app.id}`">{{ app.nickname
            }}
          </RouterLink>
        </div>
      </template>
      <br>
      <button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#addAppModal">
        Add App
      </button>
    </div>
  </div>
  <AddAppModal></AddAppModal>
</template>

<script>
import { defineComponent, ref, watch } from "vue";
import { RouterLink, useRoute } from 'vue-router'
import { useAppsDataStore } from '../core/stores/appsStores'
import AddAppModal from '../components/AddAppModal.vue'

export default defineComponent({
  name: "nav-ber",
  components: {
    RouterLink,
    AddAppModal
  },
  setup() {
    const appsStore = useAppsDataStore()
    const apps_arr = ref([])
    const route = useRoute()
    const active_id = ref(null)

    const setData = () => {
      apps_arr.value = appsStore.getAppsData
    }

    const activeBtn = () => {
      if (route.path.includes('/app/')) {
        active_id.value = route.path.split('app/')[1]
      }
    }




    appsStore.$subscribe(setData)
    setData()

    watch(route, activeBtn)
    activeBtn()


    return {
      apps_arr,
      active_id
    };
  },
});
</script>

<style scoped>
.card {
  /* position: absolute; */
  width: 200px;
  min-height: 400px;
  left: 30px;
}
</style>
