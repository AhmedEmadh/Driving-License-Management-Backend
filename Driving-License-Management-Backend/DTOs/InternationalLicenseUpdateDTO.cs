using Driving_License_Management_Backend.DTOs.Abstraction;
using Driving_License_Management_BusinessLogicLayer;
using System.ComponentModel.DataAnnotations;

namespace Driving_License_Management_Backend.DTOs
{
    public class InternationalLicenseUpdateDTO: IDTO<clsInternationalLicense>
    {
        #region Properties
        [Key]
        public int Id { get; set; }
        [Required]
        public int ApplicantId { get; set; }
        [Required]
        public int DriverId { get; set; }
        [Required]
        public int IssuedUsingLocalLicenseID { get; set; }
        //public DateOnly IssueDate { get; set; }
        [Required]
        public DateOnly ExpiryDate { get; set; }
        [Required]
        public bool IsActive { get; set; }
        [Required]
        public int CreatedByUserId { get; set; }

        #endregion
        #region Constructors
        public InternationalLicenseUpdateDTO()
        {
        }
        public InternationalLicenseUpdateDTO(clsInternationalLicense entity)
        {
            SetValuesFromEntity(entity);
        }

        #endregion
        #region Methods

        public void SetValuesFromEntity(clsInternationalLicense entity)
        {
            Id = entity.InternationalLicenseID;
            ApplicantId = entity.ApplicantPersonID;
            DriverId = entity.DriverID;
            IssuedUsingLocalLicenseID = entity.IssuedUsingLocalLicenseID;
            //IssueDate = entity.IssueDate;
            ExpiryDate = DateOnly.FromDateTime(entity.ExpirationDate);
            IsActive = entity.IsActive;
            CreatedByUserId = entity.CreatedByUserID;
        }

        public void MapValuesToEntity(clsInternationalLicense entity)
        {
            entity.InternationalLicenseID = Id;
            entity.ApplicantPersonID = ApplicantId;
            entity.DriverID = DriverId;
            entity.IssuedUsingLocalLicenseID = IssuedUsingLocalLicenseID;
            //entity.IssueDate = IssueDate;
            entity.ExpirationDate = ExpiryDate.ToDateTime(TimeOnly.MinValue);
            entity.IsActive = IsActive;
            entity.CreatedByUserID = CreatedByUserId;
            entity.ApplicantPersonInfo = clsPerson.Find(ApplicantId);
            // Initalize Objects
            entity.DriverInfo = clsDriver.FindByDriverID(DriverId);
        }

        #endregion
    }
}
