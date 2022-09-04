import axios from 'axios';

const fetcher = axios.create();

fetcher.interceptors.response.use((response) => {
  return response;
}, (error) => {
  return Promise.reject(error.response.data.error);
});

export default fetcher;
