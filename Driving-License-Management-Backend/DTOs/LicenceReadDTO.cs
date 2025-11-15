using Driving_License_Management_Backend.DTOs.Abstraction;
using Driving_License_Management_BusinessLogicLayer;
using Microsoft.Identity.Client;
using System.Text.Json.Serialization;

namespace Driving_License_Management_Backend.DTOs
{
    public class LicenceReadDTO : IDTO<clsLicense>
    {
        #region Properties
        public int Id { get; set; }
        public int ApplicationId { get; set; }
        [JsonIgnore]
        public int DriverId { get; set; }
        public DriverReadDTO Driver { get; set; }
        [JsonIgnore]
        public int LicenseClassId { get; set; }
        public LicenseClassDTO LicenseClass { get; set; }
        public DateOnly IssueDate { get; set; }
        public DateOnly ExpiryDate { get; set; }
        public string Notes { get; set; }
        public float PaidFees { get; set; }
        bool IsActive { get; set; }
        public short IssueReason { get; set; }
        [JsonIgnore]
        public int CreatedById { get; set; }
        public UserReadDTO CreatedBy { get; set; }
        #endregion
        #region Constructors
        public LicenceReadDTO()
        {
        }
        public LicenceReadDTO(clsLicense entity)
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
            Driver = new DriverReadDTO(entity.DriverInfo);
            LicenseClassId = entity.LicenseClass;
            LicenseClass = new LicenseClassDTO(entity.licenseClassInfo);
            IssueDate = DateOnly.FromDateTime(entity.IssueDate);
            ExpiryDate = DateOnly.FromDateTime(entity.ExpirationDate);
            Notes = entity.Notes;
            PaidFees = entity.PaidFees;
            IsActive = entity.IsActive;
            IssueReason = (short)entity.IssueReason;
            CreatedById = entity.CreatedByUserID;
            // CreatedBy property mapping can be added here if needed

        }

        public void MapValuesToEntity(clsLicense entity)
        {
            entity.LicenseID = Id;
            entity.ApplicationID = ApplicationId;
            entity.DriverID = DriverId;
            entity.LicenseClass = LicenseClassId;
            //entity.IssueDate = IssueDate.ToDateTime(new TimeOnly(0, 0));
            //entity.ExpirationDate = ExpiryDate.ToDateTime(new TimeOnly(0, 0));
            entity.Notes = Notes;
            entity.PaidFees = PaidFees;
            entity.IsActive = IsActive;
            entity.IssueReason = (clsLicense.enIssueReason)IssueReason;
            entity.CreatedByUserID = CreatedById;
        }
        #endregion
    }
}
