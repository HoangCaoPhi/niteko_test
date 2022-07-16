import api from "./BaseService";
class ProductService {
    constructor() {
        this.controller = "Product";
    }

    getAll() {
        return api.get(this.controller + `/GetAll`);
    }
}
export default new ProductService();