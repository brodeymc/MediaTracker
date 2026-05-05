import axios from "axios";

export const setToken = (newToken) => {
    localStorage.setItem("token", newToken)
}

const API = axios.create({
    baseURL: "http://localhost:5068/api"
});

API.interceptors.request.use((config) => {
    const token = localStorage.getItem("token");

    if (token){
        config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
});

export default API;