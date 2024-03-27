import axios from "axios";
import VueAxios from "vue-axios";

class ApiService {

    static vueInstance;
    static init(app) {
        ApiService.vueInstance = app;
        ApiService.vueInstance.use(VueAxios, axios);
        ApiService.vueInstance.axios.defaults.baseURL =
            process.env.VUE_APP_SERVER_HOST;
    }

    static setHeader() {
        // ApiService.vueInstance.axios.defaults.headers.common[
        //     "Authorization"
        // ] = `Token ${JwtService.getToken()}`;
        ApiService.vueInstance.axios.defaults.headers.common["Accept"] =
            "application/json";
    }


    static async query(resource, params) {
        return this.handleResponse(await ApiService.vueInstance.axios.get(resource, params));
    }


    static async get(
        resource,
        slug = ""
    ) {
        try {
            const response = await ApiService.vueInstance.axios.get(`${resource}${slug ? `/${slug}` : ''}`)
            return this.handleResponse(response)
        } catch (error) {
            throw this.handleError(error)
        }
    }


    static async post(resource, params) {
        try {
            const response = await ApiService.vueInstance.axios.post(`${resource}`, params)
            return this.handleResponse(response)
        } catch (error) {
            throw this.handleError(error)
        }
    }


    static async update(
        resource,
        slug,
        params
    ) {
        try {
            const response = await ApiService.vueInstance.axios.put(`${resource}${slug ? `/${slug}` : ''}`, params)
            return this.handleResponse(response)
        } catch (error) {
            throw this.handleError(error)
        }
    }


    static async put(resource, params) {
        try {
            const response = await ApiService.vueInstance.axios.put(`${resource}`, params)
            return this.handleResponse(response)
        } catch (error) {
            throw this.handleError(error)
        }
    }


    static async delete(resource) {
        try {
            const response = await ApiService.vueInstance.axios.delete(resource)
            return this.handleResponse(response)
        } catch (error) {
            throw this.handleError(error)
        }
    }

    static handleResponse(axios_response) {
        return axios_response.data
    }


    static handleError(axios_response) {
        if (axios_response.code == "ERR_NETWORK") {
            console.log(`Network connection lost.`,);
        }
        if (axios_response.response) {
            return axios_response.response.data
        }
        return axios_response
    }
}

export default ApiService;
