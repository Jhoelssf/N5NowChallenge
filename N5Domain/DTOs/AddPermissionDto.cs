using System.ComponentModel.DataAnnotations;

namespace N5Domain.DTOs
{
    public class AddPermissionDto
    {
        [Required(ErrorMessage = "Employee forename is required.")]
        [MaxLength(50, ErrorMessage = "Employee forename cannot be longer than 50 characters.")]
        private string? _employeeForename;
        public string EmployeeForename
        {
            get => _employeeForename ?? string.Empty;
            set => _employeeForename = value;
        }

        [Required(ErrorMessage = "Employee surname is required.")]
        [MaxLength(50, ErrorMessage = "Employee surname cannot be longer than 50 characters.")]
        private string? _employeeSurname;
        public string EmployeeSurname
        {
            get => _employeeSurname ?? string.Empty;
            set => _employeeSurname = value;
        }

        [Required(ErrorMessage = "PermissionId is required.")]
        [MaxLength(50, ErrorMessage = "PermissionId is required.")]
        private int? _permissionType;
        public int PermissionTypeId
        {
            get => _permissionType ?? 0;
            set => _permissionType = value;
        }
        [Required(ErrorMessage = "PermissionDate is required.")]
        [MaxLength(50, ErrorMessage = "PermissionDate is required.")]
        private DateTime? _permissionDate;
        public DateTime PermissionDate
        {
            get => _permissionDate ?? new DateTime();
            set => _permissionDate = value;
        }
    }
}