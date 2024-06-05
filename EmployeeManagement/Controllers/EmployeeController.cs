using AutoMapper;
using EmployeeManagement.Core.ApplicationInterface;
using EmployeeManagement.Core.Entity;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
	
	public class EmployeeController : Controller
	{
		private readonly IEmployeeService _employeeService;
		private readonly IMapper _mapper;
		public EmployeeController(IEmployeeService employeeService, IMapper mapper)
		{
			_employeeService = employeeService;
			_mapper = mapper;
		}

		// GET: EmployeeController
		public async Task<IActionResult> Index()

		{
			var allEmployee = await _employeeService.GetAllEmployess();
			var employee = _mapper.Map<List<EmployeeViewModel>>(allEmployee);
			return View(employee);
		}

		// GET: EmployeeController/Details/5
		public async Task<IActionResult> CreateEmployee()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> CreateEmployee(EmployeeViewModel employeeViewModel)
		{
			if (ModelState.IsValid)
			{
				var employee = _mapper.Map<Employee>(employeeViewModel);
				var isSucess = await _employeeService.AddEmployee(employee);
				if (isSucess)
				{
                    TempData["SuccessMessage"] = "Employee Added Sucessfully";
                    return RedirectToAction(nameof(Index));
				}
			}

			return View(employeeViewModel);
		}

		public async Task<IActionResult> EditEmployee(int id)
		{
			var employee = await _employeeService.GetEmployeeById(id);
			if (employee != null)
			{
				var employeeModel = _mapper.Map<EmployeeViewModel>(employee);
				return View(employeeModel);
			}
			return RedirectToAction(nameof(Index));
		}

		// POST: EmployeeController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> EditEmployee(int id, EmployeeViewModel employeeViewModel)
		{
			if (ModelState.IsValid)
			{
				var employee = _mapper.Map<Employee>(employeeViewModel);
				var isSucess = await _employeeService.UpdateEmployee(employee);
				if (isSucess)
				{
                    TempData["SuccessMessage"] = "Employee Updated Sucessfully";
                    return RedirectToAction(nameof(Index));
				}
			}
			return View(employeeViewModel);
		}

		[HttpDelete]
        [Route("Employee/DeleteEmployee/{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
		{
			var isSucess = await _employeeService.DeleteEmployee(id);
            if (isSucess)
            {
				TempData["SuccessMessage"] = "Employee Deleted Sucessfully";
                return Ok();
            }
            return BadRequest();
		}
	}
}
