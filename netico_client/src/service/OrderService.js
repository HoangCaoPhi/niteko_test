import api from "./BaseService";
class OrderService {
    constructor() {
        this.controller = "Order";
    }

    getAll() {
        return api.get(this.controller + `/GetAll`);
    }

    save(param) {
        return api.post(this.controller + `/Save`, param);
    }

    search(param) {
        return api.post(this.controller + `/SearchCategory`, param);
    }
}
export default new OrderService();