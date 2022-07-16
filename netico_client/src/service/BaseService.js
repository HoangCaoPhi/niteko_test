import axios from "axios";
import apiConfig from "./config/apiConfig";

let api = axios.create( {  withCredentials: true, baseURL: apiConfig });

api.interceptors.response.use(
    response=>response,
    error => {
        // logout
        return Promise.reject(error);
    }
)

export default api;