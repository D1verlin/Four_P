import axios from 'axios';

const api = axios.create({
  baseURL: '/api',
  headers: { 'Content-Type': 'application/json' }
});

api.interceptors.request.use(config => {
  if (config.method === 'get') {
    config.params = { ...config.params, _t: Date.now() };
  }
  return config;
});

export const routesApi = {
  getAll: (type) => api.get('/routes', { params: type ? { type } : {} }),
  getById: (id) => api.get(`/routes/${id}`),
  getStops: (id) => api.get(`/routes/${id}/stops`),
};

export const stopsApi = {
  getAll: (search) => api.get('/stops', { params: search ? { search } : {} }),
  getById: (id) => api.get(`/stops/${id}`),
  getArrivals: (id, type) => api.get(`/stops/${id}/arrivals`, { params: type ? { type } : {} }),
};

export const schedulesApi = {
  get: (routeId, period = 'all') => api.get('/schedules', { params: { routeId, period } }),
};

export const notificationsApi = {
  getAll: (type) => api.get('/notifications', { params: type ? { type } : {} }),
};

export const plannerApi = {
  plan: (from, to) => api.get('/planner', { params: { from, to } }),
};

export default api;
