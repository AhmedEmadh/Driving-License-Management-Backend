using Driving_License_Management_Backend.DTOs.Abstraction;
using Driving_License_Management_BusinessLogicLayer;
using System.ComponentModel.DataAnnotations;

namespace Driving_License_Management_Backend.DTOs
{
    public class LicenceUpdateDTO: IDTO<clsLicense>
    {
        #region Properties
        [Key]
        public int Id { get; set; }
        [Required]
        public int ApplicationId { get; set; }
        [Required]
        public int DriverId { get; set; }
        [Required]
        public int LicenseClassId { get; set; }
        //public DateOnly IssueDate { get; set; }
        //public DateOnly ExpiryDate { get; set; }
        [Required]
        public string Notes { get; set; }
        [Required]
        public float PaidFees { get; set; }
        [Required]
        public bool IsActive { get; set; }
        [Required]
        public short IssueReason { get; set; }
        [Required]
        public int CreatedById { get; set; }
        #endregion
        #region Constructors
        public LicenceUpdateDTO() { }
        public LicenceUpdateDTO(clsLicense entity)
        {
            SetValuesFromEntity(entity);
        }

        #endregion
        #region Methods
        public void SetValuesFromEntity(clsLicense entity)
        {
            Id = entity.LicenseID;
            ApplicationId = entity.ApplicationID;
            DriverId = entity.DriverID;
            LicenseClassId = entity.LicenseClass;
            //IssueDate = DateOnly.FromDateTime(entity.IssueDate);
            //ExpiryDate = DateOnly.FromDateTime(entity.ExpirationDate);
            Notes = entity.Notes;
            PaidFees = entity.PaidFees;
            IsActive = entity.IsActive;
            IssueReason = (short)entity.IssueReason;
            CreatedById = entity.CreatedByUserID;
        }

        public void MapValuesToEntity(clsLicense entity)
        {
            entity.LicenseID = Id;
            entity.ApplicationID = ApplicationId;
            entity.DriverID = DriverId;
            entity.DriverInfo = clsDriver.FindByDriverID(DriverId);
            entity.LicenseClass = LicenseClassId;
            entity.licenseClassInfo = clsLicenseClass.Find(LicenseClassId);
            //entity.IssueDate = IssueDate.ToDateTime(new TimeOnly(0, 0));
            //entity.ExpirationDate = ExpiryDate.ToDateTime(new TimeOnly(0, 0));
            entity.Notes = Notes;
            entity.PaidFees = PaidFees;
            entity.IsActive = IsActive;
            entity.IssueReason = (clsLicense.enIssueReason)IssueReason;
            entity.CreatedByUserID = CreatedById;
            entity.CreatedByUserInfo = clsUser.FindByUserID(CreatedById);
        }
        #endregion
    }
}
