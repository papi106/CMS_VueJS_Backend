<template>

    <main>
        <div class="container">
            <h1 class="text-center" style="font-weight:bold">Tabelul cu angajați</h1>
        </div>

        <!--Sort + Search bar-->
        <div class="search-sort">

            <!--Search bar-->
            <div class="search-bar">
                <div class="input-group">
                    <div class="form-outline">
                        <input type="search" id="search-employee" onkeyup="searchEmployee()" class="form-control" placeholder="Caută angajat ..."/>
                    </div>
                </div>
            </div>
            
            <!--Sort-->
            <div class="sorter">
                <select class="form-group" id="sortBy">
                    <option value="" selected>Sortează ...</option>
                    <option value="1">Alfabetic - A-Z</option>
                    <option value="2">Alfabetic - Z-A</option>
                    <option value="3">Vârstă - crescător</option>
                    <option value="4">Vârstă - descrescător</option>
                </select>
                <button type="button" class="btn" style="background-color: #27496D; color:white;" id="sortButton">Sortează</button>
            </div>
        </div>

        <!--Filter buttons-->
        <div class="filter">
            <div class="filtru-txt">Filtrează după:</div>

            <select class="form-group">
                <option value="" selected>Sex</option>
                <option value="1">Masculin</option>
                <option value="2">Feimnin</option>
                <option value="3">Altul</option>
            </select>

            <select class="form-group">
                <option value="" selected>Poză</option>
                <option value="1">Are poză</option>
                <option value="2">Nu are poză</option>
            </select>

            <select class="form-group">
                <option value="" selected>Data nașterii</option>
                <option value="1">Perioada</option>
            </select>
        </div>

        <!--Table-->
        <div>
            <div class="table-responsive">
                <table class="table table-bordered border-dark" id="table-employees">
                    <thead style="background-color: #27496D;">
                        <th style="width:25%">Poză</th>
                        <th style="width:15%">Nume</th>
                        <th style="width:15%">Prenume</th>
                        <th style="width:25%">Email</th>
                        <th style="width:15%">Sex</th>
                        <th style="width:20%">Data nașterii</th>
                        <th style="width:15%">Acțiuni</th>
                    </thead>
                    <tbody id="table-employees">
                    </tbody>
                  </table>            
            </div>
        </div>

        <!--Input form for adding employee-->

        <!-- Button trigger modal -->
        <button type="button" class="btn" style="background-color: #27496D; color:white;" id="modalButton" data-bs-toggle="modal" data-bs-target="#myModal">
            Adaugă angajat
        </button>
        
        <!-- Modal -->
        <div class="modal fade" id="myModal">
            <div class="modal-dialog modal-dialog-centered" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="exampleModalLongTitle">Adaugă un angajat</h5>
                        <button type="button" class="btn-close close-myModal" data-bs-dismiss="modal"></button>
                    </div>

                    <div class="modal-body">

                        <form>
                            <div class="mb-3">
                                <label for="last-name" class="form-label">Nume</label>
                                <input type="text" class="form-control" id="last-name" placeholder="Introdu numele" required>
                            </div>
                    
                            <div class="mb-3">
                                <label for="first-name" class="form-label">Prenume</label>
                                <input type="text" class="form-control" id="first-name" placeholder="Introdu prenumele" required>
                            </div>
                    
                            <div class="mb-3">
                                <label for="email-input" class="form-label">Adresă e-mail</label>
                                <input type="email" class="form-control" id="email-input" placeholder="name@example.com" required>
                            </div>
                            
                            <div class="mb-3">
                                <label for="gender-input" class="form-label">Sex</label>
                                <select class="form-select" id="gender-input" required>
                                    <option value="" selected disabled>Selectați sexul</option>
                                    <option value="Masculin">Masculin</option>
                                    <option value="Feminin">Feminin</option>
                                    <option value="Altul">Altul</option>
                                </select>
                            </div>
                
                            <div class="mb-3">
                                <label for="birthdate-input" class="form-label">Data nașterii</label>
                                <input type="date" class="form-control" id="birthdate-input" placeholder="" required>
                            </div>
                    
                            <div class="mb-3">
                                <label for="picture" class="form-label">Adaugă poză</label>
                                <input class="form-control" type="file" id="picture" onchange="showMyImage(this)" accept="image/*">
                                <br>
                                <img id="imgPreview" src="../assets/profile_pic.png" height=150>
                            </div>

                        </form>
                    </div>
                    
                    <div class="modal-footer">
                        <button type="submit" class="btn btn-secondary close-myModal" data-bs-dismiss="modal">Renunță</button>
                        <button type="submit" class="btn" style="background-color:#27496D; color:white;" id="add-employee-button">
                            Adaugă angajat
                        </button>
                    </div>
                </div>
            </div>
        </div>
    </main>

</template>

<style>

.container {
  margin-bottom: 3%;
  margin-top: 3%;
  font-family: "Karla", sans-serif;
  font-size: large;
}

table {
  table-layout: fixed;
  word-wrap: break-word;
  margin: auto;
  width: 80% !important;
  font-family: "Karla", sans-serif;
  text-align: center;
}

table th {
  font-size: 18px;
  color: white;
  vertical-align: middle;
}

td {
  vertical-align: middle;
}

.stergere:hover {
  cursor: pointer;
}

#modalButton {
  float: right;
  margin-right: 45%;
  margin-top: 2%;
  margin-bottom: 5%;
}

.input-group {
  display: flex;
  justify-content: center;
  max-width: 200px;
}

.search-sort {
  display: flex;
  justify-content: space-between;
  margin-right: 10.5%;
  margin-left: 10%;
  margin-bottom: 0.5rem;
}

.sorter,
.search-bar {
  display: flex;
  margin-bottom: 0.5rem;
}

.form-group {
  margin-right: 2%;
  border: 1px solid #ced4da;
  border-radius: 0.25rem;
  padding: 0.25rem 0.75rem;
}

.filter {
  display: flex;
  align-items: center;
  margin-left: 10%;
  margin-bottom: 0.5rem;
}

.filtru-txt {
  margin-right: 2%;
}

.picture {
  height: 80;
  width: 80;
  border-radius: 50%;
}

</style>