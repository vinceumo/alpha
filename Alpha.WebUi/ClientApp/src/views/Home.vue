<template>
    <div class="home">
        <MainBoard />
        <img alt="Vue logo" src="../assets/logo.png" />
        <HelloWorld msg="Welcome to Your Vue.js App" />
    </div>
</template>

<script>
// @ is an alias to /src
    import HelloWorld from "@/components/HelloWorld.vue";
    import MainBoard from '@/components/MainBoard.vue';

export default {
  name: "home",
  components: {
      HelloWorld,
      MainBoard
  }
};
</script>
