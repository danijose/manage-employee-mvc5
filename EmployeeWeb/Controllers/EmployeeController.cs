using EmployeeWeb.Models;
using EmployeeWeb.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace EmployeeWeb.Controllers
{
    public class EmployeeController : Controller
    {

        private EmpDbContext _context;

        public EmployeeController()
        {
            _context = new EmpDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }


        // GET: Employee/List
        public ActionResult List(int? pageIndex, string sortBy)
        {
            int countPerPage = 3;
            if (!pageIndex.HasValue)
                pageIndex = 1;
            if (String.IsNullOrWhiteSpace(sortBy))
                sortBy = "Name";
            // return Content(String.Format("pageIndex = {0}  sortBy = {1}", pageIndex, sortBy));
           
           // var employees = _context.Employees.OrderBy(s => s.Name).ToList();

           var employeeCount = _context.Employees.Count();
           var employees = _context.Employees
                .OrderBy(s => s.Name)
                .Skip((int) ((pageIndex-1) * countPerPage))
                .Take(countPerPage)
                .ToList();
           
           /* switch (sortBy)
            {
                case "Name"   : employees = _context.Employees.OrderBy(s => s.Name).ToList();
                                break;
                case "State"  : employees = _context.Employees.OrderBy(s => s.State).ToList();
                                break;
                case "Country": employees = _context.Employees.OrderBy(s => s.Country).ToList();
                                break;
                default       : employees = _context.Employees.OrderBy(s => s.Name).ToList();
                                break;
            }
            */
            var viewModel = new EmployeeListViewModel
            {
                Employees = employees
            };
            ViewBag.countPerpage = countPerPage;
            ViewBag.pageIndex = pageIndex;
            ViewBag.totalCount = employeeCount;
            //int pageNumber = (pageIndex ?? 1);
            //return View(viewModel.employees.ToPagedList(pageNumber, countPerPage));
            return View(viewModel);

            //return HttpNotFound();
            //return new EmptyResult();  
            //return content("Hello");
            //return RedirectToAction("Index", "Home", new { pageIndex = 1, sortBy = "Name" });
        }

        // GET: Employee/Details
        public ActionResult Details(int id)
        {
            var employee = _context.Employees.SingleOrDefault(c => c.Id == id); //returns a single item.

            if (employee == null)
                return HttpNotFound();
            return View(employee);
        }

        // GET: Employee/New
        public ActionResult New()
        {
            var viewModel = new EmployeeFormViewModel();
            return View("EmployeeForm",viewModel);
        }

        // GET: Employee/Create
        [HttpPost]
       // public ActionResult Create(NewEmployeeViewModel viewModel)
        public ActionResult Save(Employee employee)
        {
            if(employee.Id == 0)
                _context.Employees.Add(employee);
            else
            {
                var employeeInDb = _context.Employees.Single(c => c.Id == employee.Id);

                //Mapper.Map(employee,employeeInDb);

                employeeInDb.Name = employee.Name;
                employeeInDb.Address1= employee.Address1;
                employeeInDb.Address2 = employee.Address2;
                employeeInDb.City = employee.City;
                employeeInDb.State = employee.State;
                employeeInDb.Country = employee.Country;
                employeeInDb.Zipcode = employee.Zipcode;
                employeeInDb.Email = employee.Email;
            }
            _context.SaveChanges();
            return RedirectToAction("List","Employee", new { pageIndex = 1, sortBy = "Name" });
        }

        // GET: Employee/Edit/5
        public ActionResult Edit(int id)
        {
            var employee = _context.Employees.SingleOrDefault(c => c.Id == id);
            if (employee == null)
                return HttpNotFound();
            var viewModel = new EmployeeFormViewModel
            {
                Employee = employee
            };
            return View("EmployeeForm", viewModel);
        }

        // GET: Employee/Delete/5
        public ActionResult Delete(int id)
        {
            var itemToRemove = _context.Employees.SingleOrDefault(c => c.Id == id);

            if (itemToRemove != null)
            {
                _context.Employees.Remove(itemToRemove);
                _context.SaveChanges();
            }

            return RedirectToAction("List", "Employee", new { pageIndex = 1, sortBy = "Name" });
        }

        public ActionResult Index(int? pageIndex, string sortBy)
        {
            if (!pageIndex.HasValue)
                pageIndex = 1;
            if (String.IsNullOrWhiteSpace(sortBy))
                sortBy = "Name";
            return Content(String.Format("pageIndex = {0}  sortBy = {1}", pageIndex, sortBy));
        }
    }
}