<template>
  <div>
    <p>{{msg}}</p>
  </div>
</template>

<script lang="ts">
import Vue from 'vue'
import { Component, Prop } from 'vue-property-decorator';

@Component
export default class HelloWorld extends Vue {
  @Prop(String) public msg!: string
}
</script>
