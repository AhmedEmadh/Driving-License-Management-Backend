using Driving_License_Management_BusinessLogicLayer;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Driving_License_Management_Backend.DTOs
{
    public class LicenseClassDTO
    {
        #region Properties
        [Key]
        public int ID { get; set; }
        [Required,MinLength(1),MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(500)]
        public string? Description { get; set; }
        [Required]
        public byte MinimumAllowedAge { get; set; }
        [Required]
        [Range(1, byte.MaxValue, ErrorMessage = "MinimumAllowedAge must be greater than 0.")]
        public byte DefaultValidityLengthInYears { get; set; }
        [Required]
        public float ClassFees { get; set; }
        #endregion
        #region Constructors
        public LicenseClassDTO() { }
        public LicenseClassDTO(clsLicenseClass licenseClass)
        {
            SetValueTo(licenseClass);

        }
        #endregion
        #region Methods
        public void MapToEntity(clsLicenseClass licenseClass)
        {
            licenseClass.LicenseClassID = this.ID;
            licenseClass.ClassName = this.Name;
            licenseClass.ClassDescription = this.Description;
            licenseClass.MinimumAllowedAge = this.MinimumAllowedAge;
            licenseClass.DefaultValidityLength = this.DefaultValidityLengthInYears;
            licenseClass.ClassFees = this.ClassFees;

        }
        public void SetValueTo(clsLicenseClass licenseClass)
        {
            this.ID = licenseClass.LicenseClassID;
            this.Name = licenseClass.ClassName;
            this.Description = licenseClass.ClassDescription;
            this.MinimumAllowedAge = licenseClass.MinimumAllowedAge;
            this.DefaultValidityLengthInYears = licenseClass.DefaultValidityLength;
            this.ClassFees = licenseClass.ClassFees;
        }
        #endregion
    }
}
