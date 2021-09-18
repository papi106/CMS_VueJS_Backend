<template>
  <Header></Header>
  <Content></Content>
</template>

<script>
import Header from './components/Header.vue';
import Content from './components/Content.vue';

export default {
  name: 'App',
  components: {
    Header,
    Content,
  },
};
</script>

<style>

html,
body {
  margin: 0;
  padding: 0;
  font-family: "Karla", sans-serif;
}

header {
  width: 100%;
  height: 55px;
  background-color: #142850;
}

@media only screen and (max-width: 835px) {
  header {
    height: fit-content;
  }

  .nav {
    justify-content: center;
  }
}

@media only screen and (max-width: 650px) {
  .search-sort {
    flex-direction: column;
  }
}

@media only screen and (max-width: 550px) {
  .filter {
    display: flex;
    flex-direction: column;
    flex-wrap: wrap;
    max-width: 200px;
    align-items:flex-start;
  }

  .filter .form-group {
    margin-bottom: 5%;
  }
}

</style>
