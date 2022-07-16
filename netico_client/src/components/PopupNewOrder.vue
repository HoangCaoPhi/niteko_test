<script>
import { DxPopup } from 'devextreme-vue/popup';
import DxTextBox from 'devextreme-vue/text-box';
import DxSelectBox from 'devextreme-vue/select-box';
import DxDateBox from 'devextreme-vue/date-box';
import DxButton from 'devextreme-vue/button';
import CategoryService from '../service/CategoryService';
import ProductService from '../service/ProductService';
import CustomerService from '../service/CustomerService';
import OrderService from '../service/OrderService';

export default {
    components: {
        DxPopup,
        DxTextBox,
        DxSelectBox,
        DxDateBox,
        DxButton
    },
    props: {
        isShowPopup: {
            type: Boolean,
            default: false
        }
    },

    data() {
        return {
            listCategory: [],
            listProduct: [],
            listCustomer: [],
            orderName: "",
            amount: "",
            valueCategory: {},
            valueProduct: {},
            valueCustomer: {},
            orderDate: undefined
        };
    },
    created() {
        this.getListCategory();
        this.getListProduct();
        this.getListCustomer();
    },
    methods: {
        /**
         * Lấy danh sách chuyên mục
         */
        getListCategory() {
            CategoryService.getAll().then(response => {
                if (response && response.data.Subcode === 200) {
                    this.listCategory = response.data.Data;
                }
            });
        },
        getListProduct() {
            ProductService.getAll().then(response => {
                if (response && response.data.Subcode === 200) {
                    this.listProduct = response.data.Data;
                }
            });
        },
        getListCustomer() {
            CustomerService.getAll().then(response => {
                if (response && response.data.Subcode === 200) {
                    this.listCustomer = response.data.Data;
                }
            });
        },
        closePopup() {
            this.$emit("closePopup", true);
        },
        changeCategory(e) {
            this.valueCategory = e.itemData;
        },
        changeProduct(e) {
            this.valueProduct = e.itemData;
        },
        changeCustomer(e) {
            this.valueCustomer = e.itemData;
        },
        save() {
            var itemSave = {
                CustomerID: this.valueCustomer.CustomerID,
                ProductID: this.valueProduct.ProductID,
                Amount: this.amount,
                ProductName: this.valueProduct.ProductName,
                CategoryName: this.valueCategory.Name,
                CustomerName: this.valueCustomer.Name,
                OrderDate: this.orderDate
            };

            OrderService.save(itemSave).then(res => {
                if (res.data.Subcode == 200) {
                    this.closePopup();
                    this.$emit("reloadGrid");
                    alert("Create order new success!");
                }
            });
        }
    },
};
</script>

<template>
    <div class="popup-new-order">
        {{ value }}
        <DxPopup v-model:visible="isShowPopup" :drag-enabled="false" :hide-on-outside-click="true"
            :show-close-button="true" :show-title="true" :width="'auto'" :height="'auto'" title="Create new order"
            @hidden="closePopup" class="popup">

            <!-- Nội dung popup -->
            <div class="content">
                <div class="item-popup-order">
                    <label class="label">Order Name</label>
                    <DxTextBox width="228" v-model="orderName"></DxTextBox>
                </div>
                <div class="item-popup-order">
                    <label class="label">Category</label>
                    <DxSelectBox width="228" :data-source="listCategory" display-expr="Name" value-expr="CategoryID"
                        @itemClick="changeCategory($event)" />
                </div>
                <div class="item-popup-order">
                    <label class="label">Product</label>
                    <DxSelectBox width="228" :data-source="listProduct" display-expr="ProductName"
                        value-expr="ProductID" @itemClick="changeProduct($event)" />
                </div>
                <div class="item-popup-order">
                    <label class="label">Customer</label>
                    <DxSelectBox width="228" :data-source="listCustomer" display-expr="Name" value-expr="CustomerID"
                        @itemClick="changeCustomer($event)" />
                </div>
                <div class="item-popup-order">
                    <label class="label">Order Date</label>
                    <DxDateBox :value="now" type="date" v-model="orderDate" />
                </div>
                <div class="item-popup-order">
                    <label class="label">Amount</label>
                    <DxTextBox width="228" v-model="amount"></DxTextBox>
                </div>
            </div>

            <!-- Footer -->
            <div class="footer">
                <div class="cancel">
                    <DxButton text="Cancel" type="danger" @click="closePopup" />
                </div>
                <div class="save">
                    <DxButton text="Save" type="default" @click="save" />
                </div>
            </div>
        </DxPopup>
    </div>
</template>

<style lang="scss">
.content {
    .item-popup-order {
        display: flex;

        margin-top: 16px;

        .label {
            display: flex;
            align-items: center;

            margin-right: 8px;
            font-weight: 500;
            width: 90px;
        }
    }
}

.footer {
    display: flex;
    margin-top: 16px;

    justify-content: flex-end;

    .cancel {
        margin-right: 8px;
    }

    .save {}
}
</style>