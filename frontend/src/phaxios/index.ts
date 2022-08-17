import axios from "axios";
import { API_URL } from "../constants/api";

const instance = axios.create({
    baseURL: API_URL,
});

instance.interceptors.response.use((response) => {
    return response;
}, (error) => {
    return Promise.reject(error.response.data.error);
});


export default instance;