namespace EmployeeManagement.Models.ViewModels
{
    public class DepartmentStatisticsViewModel
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; } = string.Empty;
        public int NumberOfEmployees { get; set; }
        public decimal TotalSalary { get; set; }
        public int TotalFemale { get; set; }
    }
}