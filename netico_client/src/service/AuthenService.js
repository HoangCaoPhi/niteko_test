import api from "./BaseService";
class AutheService {
    constructor() {
        this.controller = "Authenticate";
    }

    save(param) {
        return api.post(this.controller + `/Login`,param);
    }
}
export default new AutheService();