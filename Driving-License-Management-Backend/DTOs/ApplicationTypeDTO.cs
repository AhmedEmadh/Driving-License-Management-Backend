using Driving_License_Management_Backend.DTOs.Abstraction;
using Driving_License_Management_BusinessLogicLayer;
using System.ComponentModel.DataAnnotations;

namespace Driving_License_Management_Backend.DTOs
{
    public class ApplicationTypeDTO : IDTO<clsApplicationType>
    {
        #region Properties
        [Required]
        public int ID { get; set; }
        [Required]
        [MinLength(1)]
        public string Title { get; set; }
        [Required]
        public float Fees { get; set; }
        #endregion
        #region Constructors
        public ApplicationTypeDTO() { }
        public ApplicationTypeDTO(clsApplicationType applicationType)
        {
            SetValuesFromEntity(applicationType);
        }
        #endregion
        #region Methods
        public void MapValuesToEntity(clsApplicationType applicationType)
        {
            applicationType.ID = this.ID;
            applicationType.Title = this.Title;
            applicationType.Fees = this.Fees;
        }

        public void SetValuesFromEntity(clsApplicationType applicationType)
        {
            this.ID = applicationType.ID;
            this.Title = applicationType.Title;
            this.Fees = applicationType.Fees;
        }

        #endregion
    }
}
