<template>
  <ul class="list-columns">
    <li class="column" v-for="(n, i) in 10" v-bind:key="i + 'col'">
      <h2>Column {{ n }}</h2>
      <button>Add a card</button>
      <ul class="list-cards">
        <li class="card" v-for="(n, i) in 10" v-bind:key="i + 'card'">Card {{ n }}</li>
      </ul>
    </li>
  </ul>
</template>

<script lang="ts">
import Vue from 'vue'
import { Component, Prop } from 'vue-property-decorator';

@Component
export default class MainBoard extends Vue {
  @Prop(String) public msg!: string
}
</script>


<style lang="scss">
.list-columns {
  list-style-type: none;
  margin: 0;
  padding: 0;
  display: flex;

  .column {
    flex-grow: 1;
    flex-basis: 0;
    padding: spacer(3);
    max-height: 100%;
  }
}

.list-cards {
  list-style-type: none;
  margin: 0;
  padding: 0;

  .card {
    padding: spacer(2);
    margin-bottom: spacer(1);
    border: 1px solid color(dark);
    width: 300px;
  }
}
</style>
