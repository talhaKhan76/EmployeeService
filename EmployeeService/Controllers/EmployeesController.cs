using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EmployeeDataAccess;


namespace EmployeeService.Controllers
{
    public class EmployeesController : ApiController
    {
        public HttpResponseMessage Get(string gender="all")
        {
            using (CMSEntities entity = new CMSEntities())
            {
                switch (gender)
                {
                    case "all":
                        return Request.CreateResponse(HttpStatusCode.OK,entity.Employees.ToList());
                    case "M":
                        return Request.CreateResponse(HttpStatusCode.OK, entity.Employees.Where(e => e.gender == "M").ToList());
                    case "F":
                        return Request.CreateResponse(HttpStatusCode.OK, entity.Employees.Where(e => e.gender == "F").ToList());
                    default:
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Your Searching Parameter is Invalid");
                }
            }
        }

        public HttpResponseMessage  Get(int id)
        {
            using (CMSEntities entity = new CMSEntities())
            {
                var record = entity.Employees.FirstOrDefault(e => e.emp_id == id);
                if(record != null){
                    return Request.CreateResponse(HttpStatusCode.OK, record);
                }
                else{
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound,"You request ID= "+id.ToString() + " data has not Found ");
                }
            }
        }


        public HttpResponseMessage Post([FromBody] Employee employee)
        {
            try { 
            using (CMSEntities entity = new CMSEntities())
            {
                entity.Employees.Add(employee);
                entity.SaveChanges();
                var msg = Request.CreateResponse(HttpStatusCode.Created, employee);
                msg.Headers.Location = new Uri(Request.RequestUri+ employee.emp_id.ToString());
                return msg;
            }
                }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,ex);
            }
        }

        public HttpResponseMessage Delete(int id)
        {
            try
            {
                using (CMSEntities entity = new CMSEntities())
                {
                    var record = entity.Employees.FirstOrDefault(e => e.emp_id == id);
                    if (record != null)
                    {
                        entity.Employees.Remove(record);
                        entity.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Your Desire ID = " + id.ToString() + " Not Found");
                    }

                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,ex);
            }
        }
        


        public HttpResponseMessage Put(int id, [FromBody] Employee emply)
        {
            try
            {
                using (CMSEntities entity = new CMSEntities())
                {
                    var record = entity.Employees.FirstOrDefault(e => e.emp_id == id);
                    if (record != null)
                    {
                        record.firstName = emply.firstName;
                        record.lastName = emply.lastName;
                        record.username = emply.username;
                        record.contactNo = emply.contactNo;
                        record.address = emply.address;
                        record.email = emply.email;
                        record.gender = emply.gender;
                        record.country = emply.country;
                        record.city = emply.city;
                        record.password = emply.password;
                        record.role = emply.role;
                        record.date = emply.date;

                        entity.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK,record);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not Update Employee Data, ID= " + id.ToString() + "Not Found");
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }


    }
}
