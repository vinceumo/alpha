<template>
<div>
  <div v-if="tempCard.isShown">
    <div>
      <label for="authorInput">Author</label>
      <input type="text" name="authorInput" id="authorInput" v-model="tempCard.author">
    </div>
    <div>
      <label for="messageInput">Message</label>
      <input type="text" name="messageInput" id="messageInput" v-model="tempCard.message">
    </div>
    <button v-on:click="submitNewCard">Submit</button>
  </div>
  <ul class="list-columns">
    <li class="column" v-for="(item, index) in board" v-bind:key="index + 'col'">
      <h2>{{item.title}}</h2>
      <button v-on:click="addNewCard(index)">Add a card</button>
      <ul class="list-cards">
        <draggable v-model="item.cards" :options="{group:'cards'}">
          <transition-group>
            <li class="card" v-for="(itemCard, index) in item.cards" v-bind:key="index + 'card'">
              {{itemCard.description }}<br>
              <small>{{itemCard.author}}</small>
            </li>
          </transition-group>
        </draggable>
      </ul>
    </li>
  </ul>
</div>
</template>

<script lang="ts">
import Vue from 'vue'
import { Component, Prop } from 'vue-property-decorator';
import draggable from 'vuedraggable';

@Component({
  components: {
    draggable
  },
})
export default class MainBoard extends Vue {
  public tempCard: any = {
    isShown: false as boolean,
    author: 'Vince' as string,
    message: '' as string,
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

  public addNewCard(colIndex: number) {
    this.tempCard.isShown = true;
    this.tempCard.columnIndex = colIndex;
  };

  public submitNewCard() {
    if(this.tempCard.author !== '' && this.tempCard.message !== ''){
      const colIndex = this.tempCard.columnIndex;
      const newCard = {
        author: this.tempCard.author,
        description: this.tempCard.message
      }
      this.board[colIndex].cards.push(newCard);
      this.tempCard.isShown = false;
      this.tempCard.message = '';
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

  .column {
    flex-grow: 1;
    flex-basis: 0;
    min-width: 300px;
    max-width: 300px;
    padding: spacer(3);
    max-height: 100%;

    h2 {
      text-align: center;
    }
  }
}

.list-cards {
  list-style-type: none;
  margin: 0;
  padding: 0;
  

  > div {
    min-height: 5rem;
    display: block;

    > span {
      min-height: 5rem;
      display: block;
    }
  }

  .card {
    padding: spacer(2);
    margin-bottom: spacer(1);
    border: 1px solid color(dark);
    background-color: color-gray(0);
  }
}
</style>
