<template>
    <div class="form-group">
        <MainBoard/>
    </div>
</template>

<script lang="ts">
import Vue from 'vue';
import { Component, Prop, Watch } from 'vue-property-decorator';
import { client } from '@/shared';
import { Hello } from '@/dtos';

import HelloWorld from '@/components/HelloWorld.vue';
import MainBoard from '@/components/MainBoard.vue';

@Component({
  components: {
    HelloWorld,
    MainBoard
  },
})
export default class HomeComponent extends Vue {
}
</script>