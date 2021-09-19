<template>

    <header>
        <div class="nav">
            <div class="navlogo"><a class="logo" href="#">Content Management System</a></div>
            <div class="navitems">
                <nav>
                    <ul class="nav-ul">
                        <li><a href="#">Acasă</a></li>
                        <li><a href="#">Autentificare</a></li>
                    </ul>
                </nav>
            </div>
        </div>
    </header>

</template>

<style>

.nav {
  display: flex;
  justify-content: space-between;
}

.navlogo {
  padding: 0.3rem;
  margin-left: 2%;
  margin-top: 4px;
  display: flex;
  color: whitesmoke;
  font-size: 26px;
  font-family: "Karla", sans-serif;
}

.navitems {
  display: flex;
  margin-right: 2%;
}

.logo,
.logo:hover {
  color: whitesmoke;
  text-decoration: none;
}

.nav-ul {
  width: auto;
  float: right;
  margin-top: 5px;
}

li {
  display: inline-block;
  padding: 10px 15px;
}

li a {
  padding: 5px;
  text-align: center;
  color: whitesmoke;
  text-decoration: none;
  font-size: 18px;
  font-family: "Karla", sans-serif;
}

li a:hover {
  background-color: whitesmoke;
  color: black;
  border-radius: 4px;
  text-decoration: none;
}
</style>