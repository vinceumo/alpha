<template>
    <div id="view1">
        <h3>{{ message }}</h3>
    </div>
</template>

<script lang="ts">
import Vue from 'vue';
import { Component, Prop } from 'vue-property-decorator';

@Component
export default class View1Component extends Vue {
  @Prop({ default: 'This is View 1' })
  public message: string;
}
</script>