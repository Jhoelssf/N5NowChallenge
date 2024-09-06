using System.ComponentModel.DataAnnotations;

namespace N5Domain.DTOs
{
    public class AddPermissionTypeDto
    {
        [Required(ErrorMessage = "Description is required.")]
        [MaxLength(200, ErrorMessage = "Description cannot be longer than 200 characters.")]
        private string? _description;
        public string Description
        {
            get => _description ?? string.Empty;
            set => _description = value;
        }
    }
}