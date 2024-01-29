using AutoMapper;
using Demo.BLL.Interfaces;
using Demo.DAL;
using Demo.DAL.Models;
using Demo.PL.Helpers;
using Demo.PL.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace Demo.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        //private readonly IEmployeeRepository _employeeRepository;

        //private readonly IDepartmentRepository _departmentRepo;

        public EmployeeController(
            IUnitOfWork unitOfWork,

            IMapper mapper /*IEmployeeRepository employeeRepository*/ /*,IDepartmentRepository departmentRepo*/ )

        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            // _employeeRepository = employeeRepository;
            //_departmentRepo = departmentRepo;
        }

        //Employee/Index
        public async Task<IActionResult> Index(string SearchInp)
        {
            var employees = Enumerable.Empty<Employee>();

            if (string.IsNullOrEmpty(SearchInp))
                employees = await _unitOfWork.EmployeeRepository.GetAll();

            else
                employees = _unitOfWork.EmployeeRepository.SearchByName(SearchInp.ToLower());

            var mappedEmp = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees);

            return View(mappedEmp);
        }
        // /Employee/Create

        // [HttpGet]
        public IActionResult Create()
        {
            //ViewData["Departments"]=_departmentRepo.GetAll();
            //ViewBag.Departments=_departmentRepo.GetAll();   

            return View();
        }

        //I Explain Mapping at Create[HttpPost] 

        [HttpPost]
        public async  Task<IActionResult> Create(EmployeeViewModel employeeVM)
        {
            if (ModelState.IsValid) //Server side Validation 
            {
                //Make UploadFile and return FileName then put it ImageName

                employeeVM.ImageName= await DocumentSettings.UploadFile(employeeVM.Image, "images");

                #region Explain Why Make Mapping Here

                ///Add Repository receive Employee not EmployeeViewMode =>Must convert(Map) from EmployeeViewMode to Employee(Mapping) 
                ///All this at Actions that work[HttpPost],
                ///Any EmployeeViewModel want to (Create,Update,delete) Must to convert(Map) it to Model(Employee)
                ///because (Create,Update,delete) Repository receive Model(Employee) 

                #endregion

                #region Manual mapping  
                ///var mappedEmp = new Employee()
                ///{
                ///    Name = employeeVM.Name,
                ///    Age = employeeVM.Age,
                ///    Address = employeeVM.Address,
                ///    Salary = employeeVM.Salary,
                ///    Email = employeeVM.Email,
                ///    PhoneNumber = employeeVM.PhoneNumber,
                ///    IsActive = employeeVM.IsActive,
                ///    HireDate = employeeVM.HireDate
                ///}; 
                ///There way easier from Previous => Overrloading to casting operator(Manual mapping)
                /// Employee mappedEmp = (Employee)employeeVM;

                #endregion

                #region Explain How  Mapping Happen
                ///We not use Manual Mapping , we use package named automapper
                ///=> install package automapper then At Constructor ask From CLR to inject object from class that immplement interface IMapper
                ///then allow dependency at startup, then use _mapper.Map< > , then we must know CLR How convert from EmployeeViewModel to Employee
                ///Through make profile 
                #endregion

                var mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);

                _unitOfWork.EmployeeRepository.Add(mappedEmp);

                //Delete
                //Update

                // _dbContext.SaveChanges();//=> PL doesn't has Access On DBContext,
                // Because PL deal with BLL and BLL deal with DAL that contains on DBcontext

                 var Count = await _unitOfWork.Complete();

                if (Count > 0)
                    return RedirectToAction(nameof(Index));
            }
            return View(employeeVM);
        }

        // /Employee/Details/1

        //I Explain Mapping at Details[HttpGet] 
        [HttpGet]
        public async Task<IActionResult> Details(int? id, string ViewName = "Details")
        {
            #region Explain How  Mapping Happen

            ///At [HttpGet] happen opposite in Mapping,
            ///It comes from the database Model I want to display it as ViewModel

            #endregion

            //Defensive Code
            if (id is null)
                return BadRequest(); //400

            var employee = await _unitOfWork.EmployeeRepository.Get(id.Value);

            var mappedEmp = _mapper.Map<Employee, EmployeeViewModel>(employee);

            if (employee is null)
                return NotFound(); //404

            //Will Execute it if pass at all Validations 
            return View(ViewName, mappedEmp);
        }


        // /Employee/Edit/1
        // /Employee/Edit
        public async Task<IActionResult> Edit(int? id)
        {

            ///if (id is null)
            ///    return BadRequest(); //400
            ///var Employee = _EmployeeRepository.Get(id.Value);
            ///if (Employee is null)
            ///    return NotFound(); //404
            ///Will Execute it if pass at all Validations 
            ///return View(Employee);
            ///

            //  ViewBag.Departments = _departmentRepo.GetAll();


            return await Details(id, "Edit");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int id, EmployeeViewModel employeeVM)
        {
            if (id != employeeVM.Id)
                return BadRequest();

            if (ModelState.IsValid) //Server side Validation 
            {
                try
                {
                    var mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);

                    _unitOfWork.EmployeeRepository.Update(mappedEmp);
                   await _unitOfWork.Complete();
                    return RedirectToAction(nameof(Index));

                }
                catch (Exception ex)
                {
                    // 1.  Log Exception
                    // 2.  Friendly Message

                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(employeeVM);
        }

        // /Employee/Delete/1
        // /Employee/Delete/
        public async Task<IActionResult> Delete(int? id)
        {
            return  await Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] int id, EmployeeViewModel employeeVM)
        {
            if (id != employeeVM.Id)
                return BadRequest();
            try
            {
                var mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);

                _unitOfWork.EmployeeRepository.Delete(mappedEmp);
              var count=  await _unitOfWork.Complete();
                if (count > 0)
                    DocumentSettings.DeleteFile(employeeVM.ImageName, "images");
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                // 1.  Log Exception
                // 2.  Friendly Message

                ModelState.AddModelError(string.Empty, ex.Message);

                return View(employeeVM);

            }
        }
    }
}
