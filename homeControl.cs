using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MvcCoreCrud.DB_Context;
using MvcCoreCrud.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreCrud.Controllers
{
    public class HomeController : Controller
    {
        
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
			helpContext database = new helpContext();
			
            List<MyModel> mm = new List<MyModel>();
            var res = database.mytable.ToList();
            foreach (var item in res)
            {
                mm.Add(new MyModel
                {
                    Id=item.Id,
                    Name=item.Name,
                    Email=item.Email,
                    City=item.City
                });
            }

            return View(mm);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddEmp(MyModel obj)
        {
            mytable tbl = new mytable();
            tbl.Id = obj.Id;
            tbl.Name = obj.Name;
            tbl.Email = obj.Email;
            tbl.City = obj.City;
			
			if(obj.id==0)
			{
			
			db.Employees.Add(tbl);
            db.SaveChanges();
			}
			else{
				 database.Entry(tbl).State = System.Data.Entity.EntityState.Modified;
                database.SaveChanges();
				
			}
            return RedirectToAction("Index","Home");
        }

        public IActionResult Delete(int Id)
        {
            var delitem = db.Employees.Where(a => a.Id == Id).First();
            db.Employees.Remove(delitem);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
		
	   public IActionResult Delete(int Id)
		{
		   
		  helpContext database = new database();
				mymodel mm = new mymodel();
		   
		   var edit = database.mytable.Where(i=>i.id).First();
		   
		   mm.id=edit.Id;
		    mm.name=edit.Name;
		    mm.email=Email.Id;
		    mm.city=City.Id;
			
			database.SaveChanges();
			
			return view();
		   
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
    }
}