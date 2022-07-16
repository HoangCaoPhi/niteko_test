<script>
import DxButton from 'devextreme-vue/button';
import DxTextBox from 'devextreme-vue/text-box';
import AutheService from './../service/AuthenService';

export default {
  components: {
    DxButton,
    DxTextBox
  },
  data() {
    return {
      orders: [],
      isShowPopup: false,
      email: "",
      password: ""
    };
  },
  created() {

  },
  methods: {
    getListOrder() {

    },
    save() {
      var param = {
        Email: this.email,
        Password: this.password
      }
      AutheService.save(param).then(res => {
        console.log(res);
      });
    }
  }
}
</script>

<template>
  <div class="login">
    <div class="label">Login</div>
    <div class="email">
      <DxTextBox placeholder="Enter email..." v-model="email" />
    </div>
    <div class="password">
      <DxTextBox placeholder="Enter password..." v-model="password" mode="password"/>
    </div>

    <div class="save">
      <DxButton text="Search" type="default" @click="save" width="400"/>
    </div>
  </div>
</template>

<style lang="scss">
.login {
  width: 400px;
  border-radius: 8px;
  padding: 48px 48px 40px 48px;
  min-height: 400px;
  background: #fff;
  position: relative;
  box-shadow: 0 12px 20px rgb(0 0 0 / 12%);
  margin: 50%;

  .label {
    font-size: 32px;
    font-weight: 500;
    display: flex;
    justify-content: center;
    align-items: center;
    margin-bottom: 16px;
  }
  .password {
    margin-top: 16px;
  }

  .save {
    margin-top: 16px;
  }
}
</style>
