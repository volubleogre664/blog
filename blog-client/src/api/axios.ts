import axios from 'axios';

const instance = axios.create({
  baseURL: 'https://localhost:7037/api/',
  headers: {
    'Content-Type': 'application/json',
    Authorization: `Bearer ${localStorage.getItem('token') ?? ''}`,
  },
});

export default instance;
