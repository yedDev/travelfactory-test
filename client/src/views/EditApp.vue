<template>
  <div v-if="selected_app" class="card">
    <div class="card-header">
      <b>{{ selected_app.nickname }}</b>
    </div>
    <div class="card-body ">
      <VForm @submit="submit">
        <div class="row">

          <div class="form-group col-sm-3">
            <label class="form-label">Key</label>
            <Field type="text" class="form-control" placeholder="Key" name="key" />
          </div>
          <template v-for="key in  supported_lang " v-bind:key="key">
            <div class="form-group col-sm-3">
              <label class="form-label text-capitalize">{{ key }}</label>
              <Field type="text" class="form-control" :name="`key-value-${key}`" />
            </div>
          </template>

        </div>
        <div class="row mt-5">
          <button type="submit" class="btn btn-success col-sm-3 submit-btn">Submit</button>
        </div>
      </VForm>
    </div>
  </div>
</template>

<script>
import { defineComponent, ref, watch } from "vue";
import { Field, Form as VForm } from "vee-validate";
import { useAppsDataStore } from "../core/stores/appsStores";
import { supported_lang } from '../data/global'
import apiService from '../core/services/apiService'

export default defineComponent({
  name: "edit-app",
  components: {
    VForm,
    Field
  },
  props: {
    appId: String,
  },
  setup(props) {
    const selected_app = ref(null)
    const appsDataStore = useAppsDataStore()

    const setData = () => {
      // display the selected app from the stored data
      const apps = appsDataStore.getAppsData
      if (!apps || !apps.length) return;

      for (let i = 0; i < apps.length; i++) {
        const app = apps[i]
        if (app.id == props.appId)
          selected_app.value = app
      }
    }


    const submit = async (data) => {
      if (!selected_app.value) return;
      try {
        // format the bpdy for the server
        const value_arr = []
        Object.keys(data).forEach(k => {
          if (k.includes('key-value-')) {
            const lang = k.split('key-value-')[1]
            value_arr.push({
              language: lang,
              value: data[k]
            })
          }
        })
        const body = {
          key: data.key,
          valueArray: value_arr
        }

        const res = await apiService.put(`api/app/${selected_app.value.id}`, body)

        // success
        alert(res)

      } catch (error) {
        alert(error)
      }
    }

    watch(props, setData)
    appsDataStore.$subscribe(setData)
    setData()

    return {
      selected_app,
      supported_lang,
      submit
    };
  },
});
</script>
