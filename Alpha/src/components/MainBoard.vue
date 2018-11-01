<template>
<div>
  <div v-if="tempCard.isShown">
    <div>
      <label for="authorInput">Author</label>
      <input type="text" name="authorInput" id="authorInput" v-model="tempCard.author">
    </div>
    <div>
      <label for="descriptionInput">Description</label>
      <input type="text" name="descriptionInput" id="descriptionInput" v-model="tempCard.description">
    </div>
    <button v-on:click="submitNewCard">Submit</button>
  </div>
  <ul class="list-columns">
    <li class="column" v-for="(item, index) in board" v-bind:key="index + 'col'">
      <h2>{{item.title}}</h2>
      <button v-on:click="addNewCard(index)">Add a card</button>
      <draggable v-model="item.cards" :options="{group:'cards'}">
          <transition-group tag="ul" class="list-cards">
            <li class="card" v-for="(itemCard, index) in item.cards" v-bind:key="index + 'card'">
              {{itemCard.description }}<br>
              <small>{{itemCard.author}}</small>
            </li>
          </transition-group>
        </draggable>
    </li>
  </ul>
</div>
</template>

<script lang="ts">
import Vue from 'vue'
import { Component, Prop } from 'vue-property-decorator';
import draggable from 'vuedraggable';
import * as signalR from '@aspnet/signalr';

@Component({
  components: {
    draggable
  },
})
export default class MainBoard extends Vue {
  public tempCard: any = {
    isShown: false as boolean,
    author: 'Vince' as string,
    description: '' as string,
    columnIndex: 0 as number,
  };
  public board: any[] = [
    {
      title: 'Glad',
      cards: []
    },
    {
      title: 'Mad',
      cards: []
    },
    {
      title: 'Sad',
      cards: []
    },
  ];
  public connection: any = null;

  public created() {
        this.connection = new signalR.HubConnectionBuilder()
        .withUrl('../boardHub')
        .build();
    }

    public mounted(){
        this.connection.start();

        this.connection.on('ReceiveMessage', (colIndex: number, author: string, description: string) => {
          const newCard = {
            author,
            description
          }
            this.board[colIndex].cards.push(newCard);
        });
    }

  public addNewCard(colIndex: number) {
    this.tempCard.isShown = true;
    this.tempCard.columnIndex = colIndex;
  };

  public submitNewCard() {
    if(this.tempCard.author !== '' && this.tempCard.description !== ''){
      const colIndex = this.tempCard.columnIndex;
      const newCard = {
        author: this.tempCard.author,
        description: this.tempCard.description
      }
      this.connection.invoke('SendMessage', this.tempCard.columnIndex, this.tempCard.author, this.tempCard.description).catch((err: any) => console.error(err.toSting()));
      this.tempCard.isShown = false;
      this.tempCard.description = '';
    }
  }
};
</script>


<style lang="scss">
.list-columns {
  list-style-type: none;
  margin: 0;
  padding: 0;
  display: flex;
  overflow-y: scroll;

  .column {
    flex-grow: 1;
    flex-basis: 0;
    min-width: 300px;
    max-width: 300px;
    padding: spacer(3);
    max-height: 100%;
  }
}

.list-cards {
  list-style-type: none;
  margin: 0;
  padding: 0;
  min-height: 5rem;
  display: block;

  .card {
    padding: spacer(2);
    margin-bottom: spacer(1);
    border: 1px solid color(dark);
    background-color: color-gray(0);

    &.sortable-chosen {
      &.sortable-ghost {
        opacity: 0.1;
      }
    }
    &.sortable-drag {
      border-color: color(focus);
      opacity: 1;
      transform: rotate(5deg);
    }
  }
}
</style>
