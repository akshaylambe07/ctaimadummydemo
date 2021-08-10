using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using DemoApp.DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DemoApp.Controllers
{
    [Route("api/[controller]")]
    public class EmployeeController : Controller
    {
        private const string BaseUrl = "https://ctaimadummy-api.azurewebsites.net/api/";
        private const string ApiKey = "Zwt8fgP4WPe4aremBWW90l4VIMhDzbXF";
       
        [HttpGet("[action]")]
        public object GetEmployees()
        {
            List<EmployeeDto> employees = new List<EmployeeDto>();
            List<CompanyDto> companies = new List<CompanyDto>();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("ApiKey", ApiKey);
                var response = httpClient.GetAsync(BaseUrl + "employees");
                response.Wait();
                var apiResponse = response.Result.Content.ReadAsStringAsync();
                apiResponse.Wait();
                employees = JsonConvert.DeserializeObject<List<EmployeeDto>>(apiResponse.Result);

            }

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("ApiKey", ApiKey);
                var response = httpClient.GetAsync(BaseUrl + "companies");
                response.Wait();
                var apiResponse = response.Result.Content.ReadAsStringAsync();
                apiResponse.Wait();
                companies = JsonConvert.DeserializeObject<List<CompanyDto>>(apiResponse.Result);

            }
            var result = from e in employees
                         join c in companies on e.CompanyId equals c.Id
                         select new
                         {
                             EmpoyeeId = e.Id,
                             e.CompanyId,
                             EmployeeName = e.Name + e.Surname,
                             e.Email,
                             e.DNI,
                             CompanyName = c.Name,
                             CompanyEmail = c.Email,
                             c.Description,
                             c.NIF
                         };

            return result;
        }
    }
}
