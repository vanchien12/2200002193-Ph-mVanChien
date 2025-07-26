namespace EmployeeManagement.Models.ViewModels
{
    public class EmployeeListViewModel
    {
        public List<Employee> Employees { get; set; } = new List<Employee>();
        public string? SearchString { get; set; }
        public int TotalCount { get; set; }
    }
}