using Driving_License_Management_Backend.DTOs.Abstraction;
using Driving_License_Management_BusinessLogicLayer;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Driving_License_Management_Backend.DTOs
{
    public class InternationalLicenseReadDTO : IDTO<clsInternationalLicense>
    {
        #region Properties
        [Key]
        public int Id { get; set; }
        [JsonIgnore]
        public int ApplicantId { get; set; }
        public PersonDTO? ApplicantPersonInfo { get; set; }
        [Required]
        [JsonIgnore]
        public int DriverId { get; set; }
        public DriverReadDTO? DriverInfo { get; set; }
        [JsonIgnore]
        public int IssuedUsingLicenseID { get; set; }
        public LicenseReadDTO? IssuedUsingLicenseInfo { get; set; }
        //public DateOnly IssueDate { get; set; }
        [Required]
        public DateOnly ExpiryDate { get; set; }
        [Required]
        public bool IsActive { get; set; }
        [JsonIgnore]
        public int CreatedByUserId { get; set; }
        public UserReadDTO? CreatedByUserInfo { get; set; }
        #endregion
        #region Constructors
        public InternationalLicenseReadDTO()
        {
        }
        public InternationalLicenseReadDTO(clsInternationalLicense entity)
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
            IssuedUsingLicenseID = entity.IssuedUsingLocalLicenseID;
            //IssueDate = entity.IssueDate;
            ExpiryDate = DateOnly.FromDateTime(entity.ExpirationDate);
            IsActive = entity.IsActive;
            CreatedByUserId = entity.CreatedByUserID;
            // Initalize Objects
            ApplicantPersonInfo = new PersonDTO(entity.ApplicantPersonInfo);
            DriverInfo = new DriverReadDTO(entity.DriverInfo);
            IssuedUsingLicenseInfo = new LicenseReadDTO(clsLicense.Find(IssuedUsingLicenseID));
            CreatedByUserInfo = new UserReadDTO(entity.CreatedByUserInfo);
        }

        public void MapValuesToEntity(clsInternationalLicense entity)
        {
            entity.InternationalLicenseID = Id;
            entity.ApplicantPersonID = ApplicantId;
            entity.DriverID = DriverId;
            entity.IssuedUsingLocalLicenseID = IssuedUsingLicenseID;
            //entity.IssueDate = IssueDate;
            entity.ExpirationDate = ExpiryDate.ToDateTime(TimeOnly.MinValue);
            entity.IsActive = IsActive;
            entity.CreatedByUserID = CreatedByUserId;
            entity.ApplicantPersonInfo = clsPerson.Find(ApplicantId);
            // Initalize Objects
            entity.DriverInfo = clsDriver.FindByDriverID(DriverId);
            //entity.IssuedUsingLicenseInfo = clsLicense.Find(IssuedUsingLicenseID);
            entity.CreatedByUserInfo = clsUser.FindByPersonID(CreatedByUserId);
        }

        #endregion
    }
}
