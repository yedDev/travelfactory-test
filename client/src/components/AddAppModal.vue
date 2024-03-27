<template>
  <div class="modal" tabindex="-1" role="dialog" id="addAppModal">
    <div class="modal-dialog" role="document">
      <div class="modal-content">

        <VForm @submit="submit">
          <div class="modal-header">
            <h5 class="modal-title">Add New App</h5>

          </div>
          <div class="modal-body">
            <div class="form-group col-sm-12">
              <label class="form-label text-capitalize">Nickname</label>
              <Field type="text" class="form-control" name="nickname" />
            </div>

          </div>
          <div class="modal-footer">
            <button type="submit" class="btn btn-primary">Add</button>
            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
          </div>

        </VForm>
      </div>
    </div>
  </div>
</template>

<script>
import { defineComponent } from "vue";
import { Field, Form as VForm } from "vee-validate";
import apiService from "../core/services/apiService";
import { useAppsDataStore } from "../core/stores/appsStores";

export default defineComponent({
  name: "add-app-modal",
  components: {
    Field,
    VForm
  },
  setup() {
    const submit = async (params) => {
      try {
        const body = {
          Nickname: params.nickname
        }
        if (body.length > 45 || body.length == 0) return alert('Enter a valid nickname')
        await apiService.post('api/app', body)
        await useAppsDataStore().fetchData()
      } catch (error) {
        console.error(error)
      }
    }
    return {
      submit
    };
  },
});
</script>

<style scoped></style>
