using DATATABLEMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace DATATABLEMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        //List<Employee> employees = new List<Employee>()
        //    {
        //        new Employee() {Name = "Rubab",Position=".Net Developer", Office = "Creative Garage",Age =2 , StartDateString = "10-March-2022", Salary =50000  },
        //        new Employee() {Name = "Masam",Position=".Net Developer", Office = "Creative Garage",Age = 4, StartDateString = "10-March-2022", Salary = 50000 },
        //        new Employee() {Name = "Hassan",Position=".Net Developer", Office = "Creative Garage",Age = 5, StartDateString = "10-March-2022", Salary = 60000 },
        //        new Employee() {Name = "Saqlain",Position=".Net Developer", Office = "Creative Garage",Age = 6, StartDateString = "10-March-2022", Salary = 70000 },
        //        new Employee() {Name = "Masoma",Position=".Net Developer", Office = "Creative Garage",Age =7 , StartDateString = "10-March-2022", Salary = 80000 },
        //        new Employee() {Name = "MAryam",Position=".Net Developer", Office = "Creative Garage",Age = 8, StartDateString = "10-March-2022", Salary = 90000 }
        //    };
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
       
        public IActionResult Index()
        {

            return View();
        }
      
        public ActionResult GetData(JqueryDatatableParam param)
        {
            List<Employee> employees = new List<Employee>()
                {
                    new Employee() {Name = "Rubab",Position=".Net Developer", Office = "Creative Garage",Age =2 , StartDateString = "10-March-2022", Salary =50000  },
                    new Employee() {Name = "Masam",Position=".Net Developer", Office = "Creative Garage",Age = 4, StartDateString = "10-March-2022", Salary = 50000 },
                    new Employee() {Name = "Hassan",Position=".Net Developer", Office = "Creative Garage",Age = 5, StartDateString = "10-March-2022", Salary = 60000 },
                    new Employee() {Name = "Saqlain",Position=".Net Developer", Office = "Creative Garage",Age = 6, StartDateString = "10-March-2022", Salary = 70000 },
                    new Employee() {Name = "Masoma",Position=".Net Developer", Office = "Creative Garage",Age =7 , StartDateString = "10-March-2022", Salary = 80000 },
                    new Employee() {Name = "MAryam",Position=".Net Developer", Office = "Creative Garage",Age = 8, StartDateString = "10-March-2022", Salary = 90000 }
                };

            employees.ToList().ForEach(x => x.StartDateString = x.StartDate.ToString("dd'/'MM'/'yyyy"));

            if (!string.IsNullOrEmpty(param.sSearch))
            {
                employees = employees.Where(x => x.Name.ToLower().Contains(param.sSearch.ToLower())
                                              || x.Position.ToLower().Contains(param.sSearch.ToLower())
                                              || x.Location.ToLower().Contains(param.sSearch.ToLower())
                                              || x.Salary.ToString().Contains(param.sSearch.ToLower())
                                              || x.Age.ToString().Contains(param.sSearch.ToLower())
                                              || x.StartDate.ToString("dd'/'MM'/'yyyy").ToLower().Contains(param.sSearch.ToLower())).ToList();
            }

            var sortColumnIndex = Convert.ToInt32(Request.Form["iSortCol_0"]);
            var sortDirection = Request.QueryString["sSortDir_0"];

            if (sortColumnIndex == 3)
            {
                employees = sortDirection == "asc" ? employees.OrderBy(c => c.Age) : employees.OrderByDescending(c => c.Age);
            }
            else if (sortColumnIndex == 4)
            {
                employees = sortDirection == "asc" ? employees.OrderBy(c => c.StartDate) : employees.OrderByDescending(c => c.StartDate);
            }
            else if (sortColumnIndex == 5)
            {
                employees = sortDirection == "asc" ? employees.OrderBy(c => c.Salary) : employees.OrderByDescending(c => c.Salary);
            }
            else
            {
                Func<Employee, string> orderingFunction = e => sortColumnIndex == 0 ? e.Name :
                                                               sortColumnIndex == 1 ? e.Position :
                                                               e.Location;

                employees = sortDirection == "asc" ? employees.OrderBy(orderingFunction) : employees.OrderByDescending(orderingFunction);
            }

            var displayResult = employees.Skip(param.iDisplayStart)
                .Take(param.iDisplayLength).ToList();
            var totalRecords = employees.Count();

           
            return Json(new {
                param.sEcho,
                iTotalRecords = totalRecords,
                iTotalDisplayRecords = totalRecords,
                aaData = displayResult
            });
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        //    public JsonResult CustomServerSideSearchAction(DataTableAjaxPostModel model)
        //    {
        //        // action inside a standard controller
        //        int filteredResultsCount;
        //        int totalResultsCount;
        //        var res = YourCustomSearchFunc(model, out filteredResultsCount, out totalResultsCount);

        //        var result = new List<YourCustomSearchClass>(res.Count);
        //        foreach (var s in res)
        //        {
        //            // simple remapping adding extra info to found dataset
        //            result.Add(new YourCustomSearchClass
        //            {
        //                EmployerId = User.ClaimsUserId(),
        //                Id = s.Id,
        //                Pin = s.Pin,
        //                Firstname = s.Firstname,
        //                Lastname = s.Lastname,
        //                RegistrationStatusId = DoSomethingToGetIt(s.Id),
        //                Address3 = s.Address3,
        //                Address4 = s.Address4
        //            });
        //        };

        //        return Json(new
        //        {
        //            // this is what datatables wants sending back
        //            draw = model.draw,
        //            recordsTotal = totalResultsCount,
        //            recordsFiltered = filteredResultsCount,
        //            data = result
        //        });
        //    }
        //    public IList<YourCustomSearchClass> YourCustomSearchFunc(DataTableAjaxPostModel model, out int filteredResultsCount, out int totalResultsCount)
        //    {
        //        var searchBy = (model.search != null) ? model.search.value : null;
        //        var take = model.length;
        //        var skip = model.start;

        //        string sortBy = "";
        //        bool sortDir = true;

        //        if (model.order != null)
        //        {
        //            // in this example we just default sort on the 1st column
        //            sortBy = model.columns[model.order[0].column].data;
        //            sortDir = model.order[0].dir.ToLower() == "asc";
        //        }

        //        // search the dbase taking into consideration table sorting and paging
        //        var result = GetDataFromDbase(searchBy, take, skip, sortBy, sortDir, out filteredResultsCount, out totalResultsCount);
        //        if (result == null)
        //        {
        //            // empty collection...
        //            return new List<YourCustomSearchClass>();
        //        }
        //        return result;
        //    }
        //}
    }
}
