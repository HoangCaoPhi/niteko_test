const development = 'https://localhost:44385/';
const production = '';

const api = {
    development: development
}
export default api[process.env.NODE_ENV];