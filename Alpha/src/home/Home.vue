<template>
    <div class="form-group">
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
        <div>
            <h3>temps card</h3>
            <p>
                <b>User Name</b> {{userName}}
            </p>
            <p>
                <b>User Message</b> {{userMessage}}
            </p>
        </div>
        <input class="form-control" type="text" placeholder="Your name" v-model="txtName" />
        <h3 class="result">{{ result }}</h3>
    </div>
</template>

<script lang="ts">
import Vue from 'vue';
import { Component, Prop, Watch } from 'vue-property-decorator';
import { client } from '../shared';
import { Hello } from '../dtos';
import * as signalR from '@aspnet/signalr';

@Component
export default class HomeComponent extends Vue {
    @Prop()
    public name: string;
    public txtName: string = this.name;
    public result: string = '';
    public userName: string = '';
    public userMessage: string = '';
    public connection: any = new signalR.HubConnectionBuilder().withUrl('../boardHub').build();


    public activated() {
        this.nameChanged(this.name);
    }

    @Watch('txtName')
    public onNameChanged(value: string, oldValue: string) {
        this.nameChanged(value);
    }

    public async nameChanged(name: string) {
        if (name) {
            const request = new Hello();
            request.name = name;
            const r = await client.get(request);
            this.result = r.result;
        } else {
            this.result = '';
        }
    }

    public submitCard() {
        if(this.userName && this.userMessage) {
            this.connection.invoke('SendMessage', this.userName, this.userMessage).catch(err => console.error(err.toSting()));
        }
    }
}
</script>

<style lang="scss">
@import '../app.scss';

.result {
    margin: 10px;
    color: darken($navbar-background, 10%);
}
</style>