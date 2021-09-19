using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Backend_CMS.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace Backend_CMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;

        public EmployeeController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

        //Get method for getting information from DB
        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
             select EmployeeId, EmployeeFirstName, EmployeeLastName, EmployeeEmail, EmployeeGender, EmployeeBirthday, ProfilePhoto from
             dbo.Employee
             ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(table);
        }

        //Post method to post the employees into DB
        [HttpPost]
        public JsonResult Post(Employee emp)
        {
            string query = @"
                            insert into dbo.Employee
                            (EmployeeFirstName, EmployeeLastName, EmployeeEmail, EmployeeGender, EmployeeBirthday, ProfilePhoto)
                            values (@EmployeeFirstName, @EmployeeLastName, @EmployeeEmail, @EmployeeGender, @EmployeeBirthday, @ProfilePhoto)
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@EmployeeFirstName", emp.EmployeeFirstName);
                    myCommand.Parameters.AddWithValue("@EmployeeLastName", emp.EmployeeLastName);
                    myCommand.Parameters.AddWithValue("@EmployeeEmail", emp.EmployeeEmail);
                    myCommand.Parameters.AddWithValue("@EmployeeGender", emp.EmployeeGender);
                    myCommand.Parameters.AddWithValue("@EmployeeBirthday", emp.EmployeeBirthday);
                    myCommand.Parameters.AddWithValue("@ProfilePhoto", emp.ProfilePhoto);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Added successfully");
        }

        //Put method to edit the employees in DB
        [HttpPut]
        public JsonResult Put(Employee emp)
        {
            string query = @"
                            update dbo.Employee
                            set EmployeeFirstName=@EmployeeFirstName,
                            EmployeeLastName=@EmployeeLastName,
                            EmployeeEmail=@EmployeeEmail,
                            EmployeeGender=@EmployeeGender,
                            EmployeeBirthday=@EmployeeBirthday,
                            ProfilePhoto=@ProfilePhoto
                            where EmployeeId=@EmployeeId
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@EmployeeId", emp.EmployeeId);
                    myCommand.Parameters.AddWithValue("@EmployeeFirstName", emp.EmployeeFirstName);
                    myCommand.Parameters.AddWithValue("@EmployeeLastName", emp.EmployeeLastName);
                    myCommand.Parameters.AddWithValue("@EmployeeEmail", emp.EmployeeEmail);
                    myCommand.Parameters.AddWithValue("@EmployeeGender", emp.EmployeeGender);
                    myCommand.Parameters.AddWithValue("@EmployeeBirthday", emp.EmployeeBirthday);
                    myCommand.Parameters.AddWithValue("@ProfilePhoto", emp.ProfilePhoto);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Updated successfully");
        }

        //Delete method for deleting employees from DB
        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"
                            delete from dbo.Employee
                            where EmployeeId=@EmployeeId
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@EmployeeId", id);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Deleted Successfully");
        }

        //Save photo from folder
        [Route("SaveFile")]
        [HttpPost]
        public JsonResult SaveFile()
        {
            try
            {
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                string filename = postedFile.FileName;
                var physicalPath = _env.ContentRootPath + "/Photos/" + filename;

                using (var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }
                return new JsonResult(filename);
            }
            catch (Exception)
            {

                return new JsonResult("anonymous.png");
            }
        }
    }
}
