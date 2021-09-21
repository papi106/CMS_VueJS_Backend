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
                        <input type="search" class="form-control" placeholder="Caută angajat ..." v-model="filterText"/>
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
                        <tr>
                            <th style="width:25%">Poză</th>
                            <th style="width:15%">Nume</th>
                            <th style="width:15%">Prenume</th>
                            <th style="width:25%">Email</th>
                            <th style="width:15%">Sex</th>
                            <th style="width:20%">Data nașterii</th>
                            <th style="width:15%">Acțiuni</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-for="emp in employees" v-bind:key="emp">
                            <td>{{ emp.ProfilePhoto }}</td>
                            <td>{{ emp.EmployeeLastName }}</td>
                            <td>{{ emp.EmployeeFirstName }}</td>
                            <td>{{ emp.EmployeeEmail }}</td>
                            <td>{{ emp.EmployeeGender }}</td>
                            <td>{{ moment(emp.EmployeeBirthday).format("DD MMM YYYY") }}</td>
                            <td class="action-buttons">
                                <button type="button" data-bs-toggle="modal" data-bs-target="#myModal" @click="editClick(emp)">
                                    <img src="../assets/edit.svg">
                                </button>
                                <button type="button" @click="deleteClick(emp.EmployeeId)">
                                    <img src="../assets/trash.svg">
                                </button>
                            </td>
                        </tr>
                    </tbody>
                </table>            
            </div>
        </div>

        <!--Input form for adding employee-->

        <!-- Button trigger modal -->
        <button type="button" class="btn" style="background-color: #27496D; color:white;" 
            data-bs-toggle="modal" data-bs-target="#myModal" id="modalButton" @click="addClick()">
            Adaugă angajat
        </button>
        
        <!-- Modal -->
        <div class="modal fade" id="myModal">
            <div class="modal-dialog modal-dialog-centered" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="exampleModalLongTitle">{{ modalTitle }}</h5>
                        <button type="button" class="btn-close close-myModal" data-bs-dismiss="modal"></button>
                    </div>

                    <div class="modal-body">
                        <form>
                            <div class="mb-3">
                                <label for="last-name" class="form-label">Nume</label>
                                <input type="text" class="form-control" id="last-name" placeholder="Introdu numele" required 
                                v-model="EmployeeLastName">
                            </div>
                    
                            <div class="mb-3">
                                <label for="first-name" class="form-label">Prenume</label>
                                <input type="text" class="form-control" id="first-name" placeholder="Introdu prenumele" required
                                v-model="EmployeeFirstName">
                            </div>
                    
                            <div class="mb-3">
                                <label for="email-input" class="form-label">Adresă e-mail</label>
                                <input type="email" class="form-control" id="email-input" placeholder="name@example.com" required
                                v-model="EmployeeEmail">
                            </div>
                            
                            <div class="mb-3">
                                <label for="gender-input" class="form-label">Sex</label>
                                <select class="form-select" id="gender-input" required 
                                v-model="EmployeeGender">
                                    <option value="" selected disabled>Selectați sexul</option>
                                    <option value="Masculin">Masculin</option>
                                    <option value="Feminin">Feminin</option>
                                    <option value="Altul">Altul</option>
                                </select>
                            </div>
                
                            <div class="mb-3">
                                <label for="birthdate-input" class="form-label">Data nașterii</label>
                                <input type="date" class="form-control" id="birthdate-input" placeholder="" required 
                                v-model="EmployeeBirthday">
                            </div>
                    
                            <div class="mb-3">
                                <label for="picture" class="form-label">Adaugă poză</label>
                                <input class="form-control" type="file" id="picture" @change="imageUpload">
                                <br>
                                <img width="150px" height="150px" :src="PhotoPath+ProfilePhoto">
                            </div>

                        </form>
                    </div>
                    
                    <div class="modal-footer">
                        <button type="submit" class="btn btn-secondary close-myModal" data-bs-dismiss="modal">Renunță</button>

                        <button type="submit" class="btn" style="background-color:#27496D; color:white;"
                                @click="updateClick()"
                                v-if="EmployeeId !== 0">
                                Actualizează angajat
                        </button>

                        <button type="submit" class="btn" style="background-color:#27496D; color:white;"
                                @click="createClick()"
                                v-if="EmployeeId == 0">
                                Adaugă angajat
                        </button>
                    </div>
                </div>
            </div>
        </div>
    </main>

</template>

<script>

//import axios from "axios";
//import moment from "moment";

// const variables = {
//   API_URL: "http://localhost:31080/api/",
//   PHOTO_URL: "http://localhost:31080/photos/",
// };

</script>


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