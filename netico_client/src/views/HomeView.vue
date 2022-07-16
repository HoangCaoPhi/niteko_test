<script>
import DxButton from 'devextreme-vue/button';
import DxTextBox from 'devextreme-vue/text-box';
import DxDataGrid from 'devextreme-vue/data-grid';
import { customers } from './data.js';
import PopupNewOrder from '../components/PopupNewOrder.vue';
import OrderService from './../service/OrderService';

export default {
  components: {
    DxButton,
    DxTextBox,
    DxDataGrid,
    PopupNewOrder
  },
  data() {
    return {
      orders: [],
      isShowPopup: false,
      columns: ['ProductName', 'CategoryName', 'CustomerName', 'OrderDate', 'Amount'],
      searchText: ""
    };
  },
  created() {
    this.getListOrder();
  },
  methods: {
    getListOrder() {
      OrderService.getAll().then(res => {
        this.orders = res.data.Data;
      });
    },
    showPopupCreateNewOrder() {
      this.isShowPopup = true;
    },
    closePopup() {
      this.isShowPopup = false;
    },
    reloadGrid() {
      this.getListOrder();
    },
    search() {
      if (this.searchText == "") {
        this.reloadGrid();
        return;
      }
      var param = {
        CategoryName: this.searchText
      }
      OrderService.search(param).then(res => {
        this.orders = res.data.Data;
      });
    }
  }
}
</script>

<template>
  <div class="home">
    <!-- Phần header -->
    <div class="header">

      <div class="left">
        <div class="input-search">
          <DxTextBox placeholder="Enter category name..." v-model="searchText" />
        </div>
        <div class="button-search">
          <DxButton text="Search" type="default" icon="search" @click="search" />
        </div>
      </div>

      <div class="right">
        <div class="new-item">
          <DxButton text="Create new order" type="default" icon="add" @click="showPopupCreateNewOrder()" />
        </div>
      </div>

    </div>

    <!-- Phần Grid -->
    <div class="grid">
      <DxDataGrid :data-source="orders" key-expr="ID" :columns="columns" :show-borders="true" />
    </div>

    <!-- Popup tạo đơn hàng -->
    <PopupNewOrder :isShowPopup="isShowPopup" @closePopup="closePopup" @reloadGrid="reloadGrid"></PopupNewOrder>
  </div>
</template>

<style lang="scss">
.home {
  grid-area: a;
  align-self: center;
  justify-self: center;

  .header {
    display: flex;
    margin-bottom: 16px;

    .left {
      display: flex;

      .label {
        display: flex;
        justify-content: center;
        align-items: center;

        font-weight: 500;
      }

      .input-search {}

      .button-search {
        margin-left: 8px;
      }
    }

    .right {
      margin-left: auto;
    }

  }

  .grid {}
}
</style>
