import api from "./BaseService";
class CustomerService {
    constructor() {
        this.controller = "Customer";
    }

    getAll() {
        return api.get(this.controller + `/GetAll`);
    }
}
export default new CustomerService();