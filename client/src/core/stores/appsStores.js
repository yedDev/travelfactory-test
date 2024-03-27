import { defineStore } from "pinia";
import appsService from "../services/appsService";
import apiService from "../services/apiService";

export const useAppsDataStore = defineStore("appsData", {
    state: () => ({
        apps_data: appsService.getAppsData(),
    }),
    actions: {
        delete() {
            appsService.destroyData()
            this.apps_data = null
        },
        setData(apps_data) {
            if (!apps_data) return;
            const data = appsService.saveAppsData(apps_data)
            this.apps_data = data
        },
        async fetchData() {
            console.log('fetch data');
            try {
                try {
                    const apps_arr = await apiService.get('api/app')
                    console.log(apps_arr);
                    this.setData(apps_arr)
                } catch (error) {
                    console.error(error);
                }

            } catch (error) {
                console.log(error);
            }
        },
    },
    getters: {
        getAppsData: (state) => {
            if (!state.apps_data) return []
            return state.apps_data.apps_arr
        },

    },
});