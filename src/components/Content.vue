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
                            <th style="width:30%">Email</th>
                            <th style="width:15%">Sex</th>
                            <th style="width:20%">Data nașterii</th>
                            <th style="width:10%">Acțiuni</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-for="emp in employees" v-bind:key="emp">
                            <td>{{ emp.ProfilePhoto }}</td>
                            <td>{{ emp.EmployeeLastName }}</td>
                            <td>{{ emp.EmployeeFirstName }}</td>
                            <td>{{ emp.EmployeeEmail }}</td>
                            <td>{{ emp.EmployeeGender }}</td>
                            <td>{{ getNormalBirthdate(emp.EmployeeBirthday) }}</td>
                            <td>
                                <div class="action-buttons">
                                    <span type="button" data-bs-toggle="modal" data-bs-target="#myModal" @click="editClick(emp)">
                                        <img src="../assets/edit.svg">
                                    </span>

                                    <span type="button" @click="deleteClick(emp.EmployeeId)">
                                        <img src="../assets/trash.svg">
                                    </span>
                                </div>
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

                        <button type="submit" class="btn" style="background-color:#27496D; color:white;" data-bs-dismiss="modal"
                            @click="createClick()">
                            Adaugă angajat
                        </button>

                        <button type="submit" class="btn" style="background-color:#27496D; color:white;" data-bs-dismiss="modal"
                            @click="updateClick()">
                            Actualizează angajat
                        </button>
                    </div>
                </div>
            </div>
        </div>
    </main>

</template>

<script>

import axios from "axios";
import moment from "moment";
import variables from "./../variables";

export default {

    data(){
        return{
            employees:[],
            modalTitle:"",
            EmplpoyeeId:0,
            ProfilePhoto:"profile_pic.png",
            EmployeeLastName:"",
            EmployeeFirstName:"",
            EmployeeEmail:"",
            EmployeeGender:"",
            EmployeeBirthday:"",
            PhotoPath:variables.PHOTO_URL
        }
    },

    methods: {

        getNormalBirthdate(date) {
            return moment(date, "YYYY-MM-DD").format("D MMMM YYYY");
        },

        getAge() {
            var birthdate = new Date(birthdate);
            var diff = new Date(Date.now() - birthdate.getTime());
            var age = diff.getUTCFullYear() - 1970;

            return age >= 16;
        },

        refreshData(){
            axios.get(`${variables.API_URL}Employee`)
                .then(response => {
                    this.employees = response.data;
                })
                .catch(error => {
                this.errorMessage = error.message;
                console.error("There was an error!", error);
            })
        },

        addClick(){
            this.modalTitle="Add Employee",
            this.EmplpoyeeId=0,
            this.ProfilePhoto="profile_pic.png",
            this.EmployeeLastName="",
            this.EmployeeFirstName="",
            this.EmployeeEmail="",
            this.EmployeeGender="",
            this.EmployeeBirthday=""
        },

        createClick(){
            if (this.EmployeeLastName === "") {
                alert ("Introdu numele!");
                return false;
            }

            if (this.EmployeeFirstName === "") {
                alert ("Introdu prenumele!");
                return false;
            }

            if (this.EmployeeEmail === "") {
                alert ("Introdu un email!");
                return false;
            // } else {
            //     if (!regex.test(this.EmployeeEmail)) {
            //         alert('Emailul este invalid! Introdu un email valid.');
            //         return false;
            //     }
            }

            if (this.EmployeeGender === "") {
                alert ("Introdu sexul!");
                return false;
            }

            if (this.EmployeeBirthday === "") {
                alert ("Introdu data nasterii!");
                return false;
            // } else if (!getAge(this.EmployeeBirthday)) {
            //     alert('Trebuie sa ai minim 16 ani.');
            //     return false;
            } 

            axios.post(`${variables.API_URL}Employee`,{
                ProfilePhoto:this.ProfilePhoto,
                EmployeeLastName:this.EmployeeLastName,
                EmployeeFirstName:this.EmployeeFirstName,
                EmployeeEmail:this.EmployeeEmail,
                EmployeeGender:this.EmployeeGender,
                EmployeeBirthday:this.EmployeeBirthday,

            })
            .then(response => {
                this.employees = response.data;
                this.refreshData();
            });
        },

        editClick(emp){
            this.modalTitle="Edit Employee",
            this.EmployeeId=emp.EmployeeId,
            this.ProfilePhoto=emp.ProfilePhoto,
            this.EmployeeLastName=emp.EmployeeLastName,
            this.EmployeeFirstName=emp.EmployeeFirstName,
            this.EmployeeEmail=emp.EmployeeEmail,
            this.EmployeeGender=emp.EmployeeGender,
            this.EmployeeBirthday=emp.EmployeeBirthday
        },

        updateClick() {
            axios.post(`${variables.API_URL}Employee`,{
                ProfilePhoto:this.ProfilePhoto,
                EmployeeLastName:this.EmployeeLastName,
                EmployeeFirstName:this.EmployeeFirstName,
                EmployeeEmail:this.EmployeeEmail,
                EmployeeGender:this.EmployeeGender,
                EmployeeBirthday:this.EmployeeBirthday,
            })
            .then(response => {
                this.employees = response.data;
                this.refreshData();
            });
        },

        deleteClick(id) {
            if(!confirm("Are you sure?")){
                return;
            }
            axios.delete(`${variables.API_URL}Employee/`+id)
            .then(response => {
                this.employees = response.data;
                this.refreshData();
            });
        },
        

        // imageUpload(event){
        //     let formData=new FormData();
        //     formData.append('file',event.target.files[0]);
        //     axios.post(
        //         variables.API_URL+"employee/savefile",
        //         formData)
        //         .then((response)=>{
        //             this.ProfilePhoto=response.data;
        //         }
        //     );
        // },

    },

    mounted() {
        axios.get(`${variables.API_URL}Employee`)
            .then(response => {
                this.employees = response.data;
            })
            .catch(error => {
            this.errorMessage = error.message;
            console.error("There was an error!", error);
        });
    },
};

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

.action-buttons {
    display: flex;
    justify-content:space-around;
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