import api from "./BaseService";
class CategoryService {
    constructor() {
        this.controller = "Category";
    }

    getAll() {
        return api.get(this.controller + `/GetAll`);
    }
}
export default new CategoryService();