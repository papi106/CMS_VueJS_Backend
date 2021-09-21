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
            <input
              type="search"
              v-model="EmployeeNameFilter"
              v-on:keyup="searchEmployee()"
              class="form-control"
              placeholder="Caută angajat ..."
            />
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
        <button
          type="button"
          class="btn"
          style="background-color: #27496D; color:white;"
          id="sortButton"
        >
          Sortează
        </button>
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
              <th style="width:15%">Poză</th>
              <th style="width:10%">Nume</th>
              <th style="width:10%">Prenume</th>
              <th style="width:30%">Email</th>
              <th style="width:10%">Sex</th>
              <th style="width:15%">Data nașterii</th>
              <th style="width:10%">Acțiuni</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="emp in employees" v-bind:key="emp.EmplpoyeeId">
              <td>
                <img
                  v-if="emp.ProfilePhoto"
                  :src="PhotoPath + emp.ProfilePhoto"
                  class="picture"
                />
              </td>
              <td>{{ emp.EmployeeLastName }}</td>
              <td>{{ emp.EmployeeFirstName }}</td>
              <td>{{ emp.EmployeeEmail }}</td>
              <td>{{ emp.EmployeeGender }}</td>
              <td>{{ getNormalBirthdate(emp.EmployeeBirthday) }}</td>
              <td>
                <div class="action-buttons">
                  <span
                    type="button"
                    data-bs-toggle="modal"
                    data-bs-target="#myModal"
                    @click="editClick(emp)"
                  >
                    <img src="../assets/edit.svg" />
                  </span>

                  <span type="button" @click="deleteClick(emp.EmployeeId)">
                    <img src="../assets/trash.svg" />
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
    <button
      type="button"
      class="btn"
      style="background-color: #27496D; color:white;"
      data-bs-toggle="modal"
      data-bs-target="#myModal"
      id="modalButton"
      @click="addClick()"
    >
      Adaugă angajat
    </button>

    <!-- Modal -->
    <div class="modal fade" id="myModal">
      <div class="modal-dialog modal-dialog-centered" role="document">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title" id="exampleModalLongTitle">
              {{ modalTitle }}
            </h5>
            <button
              type="button"
              class="btn-close close-myModal"
              data-bs-dismiss="modal"
            ></button>
          </div>

          <div class="modal-body">
            <form>
              <div
                v-if="validationErrors.length !== 0"
                class="alert alert-danger"
                role="alert"
              >
                <ul class="mb-0">
                  <li v-for="err in validationErrors" :key="err">{{ err }}</li>
                </ul>
              </div>
              <div class="mb-3">
                <label for="last-name" class="form-label">Nume</label>
                <input
                  type="text"
                  class="form-control"
                  id="last-name"
                  placeholder="Introdu numele"
                  required
                  v-model="EmployeeLastName"
                />
              </div>

              <div class="mb-3">
                <label for="first-name" class="form-label">Prenume</label>
                <input
                  type="text"
                  class="form-control"
                  id="first-name"
                  placeholder="Introdu prenumele"
                  required
                  v-model="EmployeeFirstName"
                />
              </div>

              <div class="mb-3">
                <label for="email-input" class="form-label"
                  >Adresă e-mail</label
                >
                <input
                  type="email"
                  class="form-control"
                  id="email-input"
                  placeholder="name@example.com"
                  required
                  v-model="EmployeeEmail"
                />
              </div>

              <div class="mb-3">
                <label for="gender-input" class="form-label">Sex</label>
                <select
                  class="form-select"
                  id="gender-input"
                  required
                  v-model="EmployeeGender"
                >
                  <option value="" selected disabled>Selectați sexul</option>
                  <option value="Masculin">Masculin</option>
                  <option value="Feminin">Feminin</option>
                  <option value="Altul">Altul</option>
                </select>
              </div>

              <div class="mb-3">
                <label for="birthdate-input" class="form-label"
                  >Data nașterii</label
                >
                <input
                  type="date"
                  class="form-control"
                  id="birthdate-input"
                  placeholder=""
                  required
                  v-model="EmployeeBirthday"
                />
              </div>

              <div class="mb-3">
                <label for="picture" class="form-label">Adaugă poză</label>
                <input
                  class="form-control"
                  type="file"
                  id="picture"
                  @change="saveImageUpload"
                />
                <br />
                <img class="picture-preview" :src="PhotoPath + ProfilePhoto" />
              </div>
            </form>
          </div>

          <div class="modal-footer">
            <button
              type="submit"
              class="btn btn-secondary close-myModal"
              data-bs-dismiss="modal"
            >
              Renunță
            </button>

            <button
              type="submit"
              class="btn"
              style="background-color:#27496D; color:white;"
              @click="createClick()"
              v-show="hideBtn"
            >
              Adaugă angajat
            </button>

            <button
              type="submit"
              class="btn"
              style="background-color:#27496D; color:white;"
              data-bs-dismiss="modal"
              @click="updateClick()"
              v-show="!hideBtn"
            >
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
  data() {
    return {
      hideBtn: true,
      regex: /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/,
      employees: [],
      modalTitle: "",
      EmployeeNameFilter: "",
      EmplpoyeeId: 0,
      ProfilePhoto: "profile_pic.png",
      EmployeeLastName: "",
      EmployeeFirstName: "",
      EmployeeEmail: "",
      EmployeeGender: "",
      EmployeeBirthday: "",
      PhotoPath: variables.PHOTO_URL,
      validationErrors: [],
    };
  },

  methods: {
    refreshData() {
      axios
        .get(`${variables.API_URL}Employee`)
        .then((response) => {
          this.employees = response.data;
        })
        .catch((error) => {
          this.errorMessage = error.message;
          console.error("There was an error!", error);
        });
    },

    addClick() {
      this.hideBtn = true;
      this.modalTitle = "Add Employee";
      this.EmplpoyeeId = 0;
      this.ProfilePhoto = "profile_pic.png";
      this.EmployeeLastName = "";
      this.EmployeeFirstName = "";
      this.EmployeeEmail = "";
      this.EmployeeGender = "";
      this.EmployeeBirthday = "";
    },

    createClick() {
      if (this.validateForm()) {
        axios
          .post(`${variables.API_URL}Employee`, {
            ProfilePhoto: this.ProfilePhoto,
            EmployeeLastName: this.EmployeeLastName,
            EmployeeFirstName: this.EmployeeFirstName,
            EmployeeEmail: this.EmployeeEmail,
            EmployeeGender: this.EmployeeGender,
            EmployeeBirthday: this.EmployeeBirthday,
          })
          .then((response) => {
            this.employees = response.data;
            console.log(response.data);
            this.refreshData();
          });
      }
    },

    editClick(emp) {
      this.hideBtn = false;
      this.modalTitle = "Edit Employee";
      this.EmployeeId = emp.EmployeeId;
      this.ProfilePhoto = emp.ProfilePhoto;
      this.EmployeeLastName = emp.EmployeeLastName;
      this.EmployeeFirstName = emp.EmployeeFirstName;
      this.EmployeeEmail = emp.EmployeeEmail;
      this.EmployeeGender = emp.EmployeeGender;
      this.EmployeeBirthday = emp.EmployeeBirthday;
    },

    updateClick() {
      axios
        .put(`${variables.API_URL}Employee`, {
          EmployeeId: this.EmployeeId,
          ProfilePhoto: this.ProfilePhoto,
          EmployeeLastName: this.EmployeeLastName,
          EmployeeFirstName: this.EmployeeFirstName,
          EmployeeEmail: this.EmployeeEmail,
          EmployeeGender: this.EmployeeGender,
          EmployeeBirthday: this.EmployeeBirthday,
        })
        .then((response) => {
          this.refreshData();
          this.employees = response.data;
          console.log(response.data);
        });
    },

    deleteClick(id) {
      if (!confirm("Esti sigur ca vrei sa stergi ?")) {
        return;
      }
      axios.delete(`${variables.API_URL}Employee/` + id).then((response) => {
        this.employees = response.data;
        console.log(response.data);
        this.refreshData();
      });
    },

    imageUpload(fileInput) {
      var imageFile = fileInput.files[0];
      var img = document.getElementById("ProfilePhoto");
      var imageType = /assets.*/;

      if (imageFile.type.match(imageType)) {
        img.file = imageFile;

        var reader = new FileReader();
        reader.onload = (function(img) {
          return function(e) {
            img.src = e.target.result;
          };
        })(img);
        reader.readAsDataURL(imageFile);
      }
    },

    saveImageUpload(event) {
      let formData = new FormData();
      formData.append("file", event.target.files[0]);

      axios
        .post(`${variables.API_URL}Employee/SaveFile`, formData)
        .then((response) => {
          this.ProfilePhoto = response.data;
        });
    },

    validateForm() {
      this.validationErrors = [];
      var errors = [];

      if (this.EmployeeLastName === "") {
        errors.push("Introdu numele de familie.");
      }

      if (this.EmployeeFirstName === "") {
        errors.push("Introdu prenumele.");
      }

      if (this.EmployeeEmail === "") {
        errors.push("Introdu un email.");
      } else {
        if (!this.regex.test(this.EmployeeEmail)) {
          errors.push("Email invalid. Introdu un email valid.");
        }
      }

      if (this.EmployeeGender === "") {
        errors.push("Alege sexul.");
      }

      if (this.EmployeeBirthday === "") {
        errors.push("Introdu data nasterii.");
      } else if (this.getAge() < 16) {
        errors.push("Trebuie sa ai peste 16 ani.");
      }
      if (errors.length === 0) return true;

      this.validationErrors = errors;
      return false;
    },

    getNormalBirthdate(date) {
      return moment(date, "YYYY-MM-DD").format("D MMMM YYYY");
    },

    getAge() {
      var birthdate = new Date(birthdate);
      var diff = new Date(Date.now() - birthdate.getTime());
      var age = diff.getUTCFullYear() - 1970;

      return age;
    },

    // searchEmployee() {
    //     var EmployeeNameFilter = this.EmployeeNameFilter;
    // },
  },

  mounted() {
    axios
      .get(`${variables.API_URL}Employee`)
      .then((response) => {
        this.employees = response.data;
      })
      .catch((error) => {
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
  justify-content: space-around;
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
  margin-right: 1rem;
}

.picture {
  height: 80px;
  width: 80px;
  border-radius: 50%;
}

.picture-preview {
  height: 100px;
}
</style>
