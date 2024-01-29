using AutoMapper;
using Demo.BLL.Interfaces;
using Demo.BLL.Repositories;
using Demo.DAL;
using Demo.DAL.Contexts;
using Demo.DAL.Models;
using Demo.PL.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo.PL.Controllers
{
    //Inheritance:DepartmentController is a Controller 
    //Aggregation:DepartmentController has a DepartmentRepository
    public class DepartmentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
      //private  readonly IDepartmentRepository _departmentRepository;

        
        public DepartmentController(
                  IUnitOfWork unitOfWork,
            IMapper mapper/* IDepartmentRepository departmentRepository*/) 

        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
           // _departmentRepository = departmentRepository;
        }


        //Department/Index
        public async Task<IActionResult> Index() 
        {
            //1.ViewData is a Dictionary Object(introduced in ASP.Net Framework 3.5)
            //=>It helps us to transfer data from controller to view x`

            //ViewData["Message"] = "Hello View Data";

            //2. ViewBag is a Dynamic property (introduced in ASP.Net Framework 4  based on dynamic feature)
            //=>It helps us to transfer data from controller to view 

            //ViewBag.Message = "Hello View Bag";
      
            var departments =await _unitOfWork.DepartmentRepository.GetAll();
            var mappedDep = _mapper.Map<IEnumerable<Department>, IEnumerable<DepartmentViewModel>>(departments);

            return View(mappedDep);
        }
       // /Department/Create

       // [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

       [HttpPost]
        public async  Task<IActionResult> Create(DepartmentViewModel DepartmentVM)
        {

        if(ModelState.IsValid) //Server side Validation 
            {
                var mappedDep = _mapper.Map<DepartmentViewModel, Department>(DepartmentVM);
                _unitOfWork.DepartmentRepository.Add(mappedDep);

             await   _unitOfWork.Complete();

                TempData["Message"] = "Department is created Successfully";
                return RedirectToAction(nameof(Index));  //Indexهروح ارجعه فى صفحه ال  Department عندما تضيف  
            }
        return View(DepartmentVM); // if model state not valide it will return it  at the same page  with the same wrong data
        }

        // /Department/Details/1

        [HttpGet]
        public async Task<IActionResult> Details(int? id, string ViewName = "Details")
        {
         //Defensive Code

            if (id is null)
                return BadRequest(); //400

            var department =await  _unitOfWork.DepartmentRepository.Get(id.Value);
            var mappedDep = _mapper.Map<Department, DepartmentViewModel>(department);


            if (department is  null)
                return NotFound(); //404

            //Will Execute it if pass at all Validations 
            return View(ViewName, mappedDep);
        }


        // /Department/Edit/1
        // /Department/Edit
        public async Task<IActionResult> Edit(int? id )
        {
        
            ///if (id is null)
            ///    return BadRequest(); //400
            ///var department = _departmentRepository.Get(id.Value);
            ///if (department is null)
            ///    return NotFound(); //404
            ///Will Execute it if pass at all Validations 
            ///return View(department);
            ///

            return await Details(id,"Edit");
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int id, DepartmentViewModel departmentVM)
        {
            if(id !=departmentVM.Id)
                return BadRequest();

            if (ModelState.IsValid) //Server side Validation 
            {
                try
                {
                    var mappedDep = _mapper.Map<DepartmentViewModel, Department>(departmentVM);

                    _unitOfWork.DepartmentRepository.Update(mappedDep);
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
            return View(departmentVM);
        }

        // /Department/Delete/1
        // /Department/Delete/
        public async Task<IActionResult> Delete(int? id)
        {
            return await Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] int id,DepartmentViewModel departmentVM)
        {
            if (id != departmentVM.Id)
                return BadRequest();
            try
            {
                var mappedDep = _mapper.Map<DepartmentViewModel, Department>(departmentVM);

                _unitOfWork.DepartmentRepository.Delete(mappedDep);
              await _unitOfWork.Complete();
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                // 1.  Log Exception
                // 2.  Friendly Message

                ModelState.AddModelError(string.Empty, ex.Message);

                return View(departmentVM);

            }
        }
    }
}
