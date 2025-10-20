using Driving_License_Management_BusinessLogicLayer;
using System.ComponentModel.DataAnnotations;

namespace Driving_License_Management_Backend.DTOs
{
    public class ApplicationTypeDTO
    {
        [Required]
        public int ID { get; set; }
        [Required]
        [MinLength(1)]
        public string Title { get; set; }
        [Required]
        public float Fees { get; set; }
        public ApplicationTypeDTO() { }
        public ApplicationTypeDTO(clsApplicationType applicationType)
        {
            this.ID = applicationType.ID;
            this.Title = applicationType.Title;
            this.Fees = applicationType.Fees;
        }
        public void MapToEntity(clsApplicationType applicationType)
        {
            applicationType.ID = this.ID;
            applicationType.Title = this.Title;
            applicationType.Fees = this.Fees;
        }
    }
}
