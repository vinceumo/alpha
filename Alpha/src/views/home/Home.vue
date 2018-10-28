<template>
    <div class="form-group">
        <HelloWorld msg="Welcome to Your Vue.js + TypeScript App"/>
        <form>
            <div>
                <label for="userName">Your Name</label>
                <input type="text" name="Name" id="userName" v-model="userName">
            </div>
            <div>
                <label for="userMessage">Message</label>
                <input type="text" name="Message" id="userMessage" v-model="userMessage">
            </div>
            <button v-on:click="submitCard" type="button">Submit</button>
        </form>
        <ul v-for="(item, index) in cards" v-bind:key="index + 'itemCard'">
            <li><b>Name: </b>{{item.user}}</li>
            <li><b>Message: </b>{{item.message}}</li>
        </ul>
        <div>
            <h3>temps card</h3>
            <p>
                <b>User Name</b> {{userName}}
            </p>
            <p>
                <b>User Message</b> {{userMessage}}
            </p>
        </div>
    </div>
</template>

<script lang="ts">
import Vue from 'vue';
import { Component, Prop, Watch } from 'vue-property-decorator';
import { client } from '@/shared';
import { Hello } from '@/dtos';
import * as signalR from '@aspnet/signalr';

import HelloWorld from '@/components/HelloWorld.vue';

@Component({
  components: {
    HelloWorld,
  },
})
export default class HomeComponent extends Vue {
    public userName: string = '';
    public userMessage: string = '';
    public connection: any = null;
    public cards: any[] = [];

    public created() {
        this.connection = new signalR.HubConnectionBuilder()
        .withUrl('../boardHub')
        .build();
    }

    public mounted(){
        this.connection.start();

        this.connection.on('ReceiveMessage', (user: string, message: string) => {
            this.cards.push({user, message});
        });
    }

    public submitCard() {
        if(this.userName && this.userMessage) {
            this.connection.invoke('SendMessage', this.userName, this.userMessage).catch((err: any) => console.error(err.toSting()));

            this.userName = '';
            this.userMessage = '';
        }
    }
}
</script>