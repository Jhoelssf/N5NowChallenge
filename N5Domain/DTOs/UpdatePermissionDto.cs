using System.ComponentModel.DataAnnotations;

namespace N5Domain.DTOs
{
    public class UpdatePermissionDto
    {
        [Required(ErrorMessage = "Employee forename is required.")]
        [MaxLength(50, ErrorMessage = "Employee forename cannot be longer than 50 characters.")]
        private string? _employeeForename;
        public string EmployeeForename
        {
            get => _employeeForename ?? throw new ArgumentNullException(nameof(EmployeeForename));
            set => _employeeForename = value;
        }

        [Required(ErrorMessage = "Employee surname is required.")]
        [MaxLength(50, ErrorMessage = "Employee surname cannot be longer than 50 characters.")]
        private string? _employeeSurname;
        public string EmployeeSurname
        {
            get => _employeeSurname ?? throw new ArgumentNullException(nameof(EmployeeSurname));
            set => _employeeSurname = value;
        }

        [Required(ErrorMessage = "PermissionId is required.")]
        [MaxLength(50, ErrorMessage = "PermissionId is required.")]
        private int? _permissionType;
        public int PermissionTypeId
        {
            get => _permissionType ?? throw new ArgumentNullException(nameof(PermissionTypeId));
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