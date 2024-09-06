namespace N5Application.DTO;

public class PermissionDto
{
    public int Id { get; set; }
    public string EmployeeForename { get; set; }
    public string EmployeeSurname { get; set; }
    public DateTime PermissionDate { get; set; }
    public int PermissionTypeId { get; set; }
}
public class AddPermissionDto
{
    public string EmployeeForename { get; set; }
    public string EmployeeSurname { get; set; }
    public DateTime PermissionDate { get; set; }
    public int PermissionTypeId { get; set; }
}
public class UpdatePermissionDto
{
    public string EmployeeForename { get; set; }
    public string EmployeeSurname { get; set; }
    public DateTime PermissionDate { get; set; }
    public int PermissionTypeId { get; set; }
}

