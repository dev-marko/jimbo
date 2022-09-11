/* eslint-disable no-param-reassign */
import axios from 'axios';

import { TOKEN } from '~constants/storage';
import { readFromLocalStorage } from '~utils/local-storage';

const fetcher = axios.create();

fetcher.interceptors.response.use(
  (config) => {
    return config;
  },
  (error) => {
    return Promise.reject(error.response.data.error);
  },
);

export default fetcher;
