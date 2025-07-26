namespace EmployeeManagement.Services
{
    public interface IFileService
    {
        Task<string?> SaveFileAsync(IFormFile file, string folder);
        bool DeleteFile(string filePath);
        bool IsValidImageFile(IFormFile file);
    }
}