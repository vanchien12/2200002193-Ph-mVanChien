using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Models.ViewModels
{
    public class EmployeeCreateViewModel
    {
        [Required]
        [Display(Name = "Employee Name")]
        [StringLength(100)]
        public string EmployeeName { get; set; } = string.Empty;

        [Display(Name = "Gender")]
        public bool Gender { get; set; }

        [Required]
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Email")]
        [EmailAddress]
        [StringLength(100)]
        public string? Email { get; set; }

        [Display(Name = "Phone")]
        [StringLength(20)]
        public string? Phone { get; set; }

        [Display(Name = "Photo")]
        public IFormFile? PhotoFile { get; set; }

        [Required]
        [Display(Name = "Salary")]
        [Range(0, double.MaxValue, ErrorMessage = "Salary must be greater than 0")]
        public decimal Salary { get; set; }

        [Required]
        [Display(Name = "Department")]
        public int DepartmentId { get; set; }

        public List<Department> Departments { get; set; } = new List<Department>();
    }
}