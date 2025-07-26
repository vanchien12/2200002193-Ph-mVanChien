using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmployeeManagement.Data;
using EmployeeManagement.Models;
using EmployeeManagement.Models.ViewModels;
using EmployeeManagement.Services;
using EmployeeManagement.Utilities;

namespace EmployeeManagement.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IFileService _fileService;

        public EmployeeController(ApplicationDbContext context, IFileService fileService)
        {
            _context = context;
            _fileService = fileService;
        }

        // GET: Employee
        public async Task<IActionResult> Index(string searchString)
        {
            var employees = _context.Employees.Include(e => e.Department).AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                // Fuzzy search implementation
                employees = employees.Where(e =>
                    EF.Functions.Like(e.EmployeeName, $"%{searchString}%") ||
                    EF.Functions.Like(e.Phone ?? "", $"%{searchString}%") ||
                    EF.Functions.Like(e.Email ?? "", $"%{searchString}%"));
            }

            ViewData["CurrentFilter"] = searchString;
            return View(await employees.OrderBy(e => e.EmployeeName).ToListAsync());
        }

        // GET: Employee/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .Include(e => e.Department)
                .FirstOrDefaultAsync(m => m.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employee/Create
        public async Task<IActionResult> Create()
        {
            var viewModel = new EmployeeCreateViewModel
            {
                Departments = await _context.Departments.ToListAsync(),
                DateOfBirth = DateTime.Now.AddYears(-25)
            };
            return View(viewModel);
        }

        // POST: Employee/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var employee = new Employee
                {
                    EmployeeName = viewModel.EmployeeName,
                    Gender = viewModel.Gender,
                    DateOfBirth = viewModel.DateOfBirth,
                    Email = viewModel.Email,
                    Phone = viewModel.Phone,
                    Salary = viewModel.Salary,
                    DepartmentId = viewModel.DepartmentId
                };

                // Handle file upload
                if (viewModel.PhotoFile != null)
                {
                    if (_fileService.IsValidImageFile(viewModel.PhotoFile))
                    {
                        employee.PhotoImagePath = await _fileService.SaveFileAsync(viewModel.PhotoFile, "photos");
                    }
                    else
                    {
                        ModelState.AddModelError("PhotoFile", "Please upload a valid image file (jpg, jpeg, png) with maximum size of 2MB.");
                        viewModel.Departments = await _context.Departments.ToListAsync();
                        return View(viewModel);
                    }
                }

                _context.Add(employee);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Employee created successfully!";
                return RedirectToAction(nameof(Index));
            }

            viewModel.Departments = await _context.Departments.ToListAsync();
            return View(viewModel);
        }

        // GET: Employee/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            var viewModel = new EmployeeEditViewModel
            {
                EmployeeId = employee.EmployeeId,
                EmployeeName = employee.EmployeeName,
                Gender = employee.Gender,
                DateOfBirth = employee.DateOfBirth,
                Email = employee.Email,
                Phone = employee.Phone,
                Salary = employee.Salary,
                DepartmentId = employee.DepartmentId,
                CurrentPhotoPath = employee.PhotoImagePath,
                Departments = await _context.Departments.ToListAsync()
            };

            return View(viewModel);
        }

        // POST: Employee/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EmployeeEditViewModel viewModel)
        {
            if (id != viewModel.EmployeeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var employee = await _context.Employees.FindAsync(id);
                    if (employee == null)
                    {
                        return NotFound();
                    }

                    employee.EmployeeName = viewModel.EmployeeName;
                    employee.Gender = viewModel.Gender;
                    employee.DateOfBirth = viewModel.DateOfBirth;
                    employee.Email = viewModel.Email;
                    employee.Phone = viewModel.Phone;
                    employee.Salary = viewModel.Salary;
                    employee.DepartmentId = viewModel.DepartmentId;

                    // Handle file upload
                    if (viewModel.PhotoFile != null)
                    {
                        if (_fileService.IsValidImageFile(viewModel.PhotoFile))
                        {
                            // Delete old photo if exists
                            if (!string.IsNullOrEmpty(employee.PhotoImagePath))
                            {
                                _fileService.DeleteFile(employee.PhotoImagePath);
                            }

                            employee.PhotoImagePath = await _fileService.SaveFileAsync(viewModel.PhotoFile, "photos");
                        }
                        else
                        {
                            ModelState.AddModelError("PhotoFile", "Please upload a valid image file (jpg, jpeg, png) with maximum size of 2MB.");
                            viewModel.Departments = await _context.Departments.ToListAsync();
                            return View(viewModel);
                        }
                    }

                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Employee updated successfully!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(viewModel.EmployeeId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            viewModel.Departments = await _context.Departments.ToListAsync();
            return View(viewModel);
        }

        // GET: Employee/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .Include(e => e.Department)
                .FirstOrDefaultAsync(m => m.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee != null)
            {
                // Delete photo file if exists
                if (!string.IsNullOrEmpty(employee.PhotoImagePath))
                {
                    _fileService.DeleteFile(employee.PhotoImagePath);
                }

                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Employee deleted successfully!";
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Employee/Statistics
        public async Task<IActionResult> Statistics()
        {
            var statistics = await _context.Departments
                .Select(d => new DepartmentStatisticsViewModel
                {
                    DepartmentId = d.DepartmentId,
                    DepartmentName = d.DepartmentName,
                    NumberOfEmployees = d.Employees.Count(),
                    TotalSalary = d.Employees.Sum(e => e.Salary),
                    TotalFemale = d.Employees.Count(e => e.Gender == true)
                })
                .OrderBy(s => s.DepartmentName)
                .ToListAsync();

            return View(statistics);
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.EmployeeId == id);
        }
    }
}