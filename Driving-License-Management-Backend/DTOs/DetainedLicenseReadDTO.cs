using Driving_License_Management_Backend.DTOs.Abstraction;
using Driving_License_Management_BusinessLogicLayer;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Driving_License_Management_Backend.DTOs
{
    public class DetainedLicenseReadDTO : IDTO<clsDetainedLicense>
    {
        #region Properties
        [Key]
        public int Id { get; set; }
        [JsonIgnore]
        public int LicenseId { get; set; }
        LicenseReadDTO License { get; set; }
        [JsonIgnore]
        public int CreatedByUserId { get; set; }
        public UserReadDTO CreatedBy { get; set; }
        [JsonIgnore]
        public int ReleasedByUserId { get; set; }
        public UserReadDTO? ReleasedBy { get; set; }
        [JsonIgnore]
        public int ReleaseApplicationId { get; set; }
        ApplicationReadDTO? ReleaseApplication { get; set; }
        //public DateOnly DetainDate { get; set; }
        public float FineFees { get; set; }
        public bool IsReleased { get; set; }

        public DateOnly? ReleaseDate { get; set; }
        #endregion
        #region Constructors
        public DetainedLicenseReadDTO()
        {
        }
        public DetainedLicenseReadDTO(clsDetainedLicense entity)
        {
            SetValuesFromEntity(entity);
        }

        #endregion
        #region Methods
        public void SetValuesFromEntity(clsDetainedLicense entity)
        {
            Id = entity.DetainID;
            LicenseId = entity.LicenseID;
            License = new LicenseReadDTO(entity.LicenseInfo);
            CreatedByUserId = entity.CreatedByUserID;
            CreatedBy = new UserReadDTO(entity.CreatedByUserInfo);
            ReleasedByUserId = entity.ReleasedByUserID;
            if (entity.ReleasedByUserInfo != null)
            {
                ReleasedBy = new UserReadDTO(entity.ReleasedByUserInfo);
            }
            ReleaseApplicationId = entity.ReleaseApplicationID;
            if (entity.ReleaseApplicationInfo != null)
            {
                ReleaseApplication = new ApplicationReadDTO(entity.ReleaseApplicationInfo);
            }
            //DetainDate = DateOnly.FromDateTime(entity.DetainDate);
            FineFees = entity.FineFees;
            IsReleased = entity.IsReleased;
            if (entity.ReleaseDate != DateTime.MinValue)
            {
                ReleaseDate = DateOnly.FromDateTime(entity.ReleaseDate);
            }
        }

        public void MapValuesToEntity(clsDetainedLicense entity)
        {
            entity.DetainID = Id;
            entity.LicenseID = LicenseId;
            entity.LicenseInfo = clsLicense.Find(LicenseId);
            entity.CreatedByUserID = CreatedByUserId;
            entity.CreatedByUserInfo = clsUser.FindByUserID(CreatedByUserId);
            entity.ReleasedByUserID = ReleasedByUserId;
            entity.ReleasedByUserInfo = clsUser.FindByUserID(ReleasedByUserId);
            entity.ReleaseApplicationID = ReleaseApplicationId;
            entity.ReleaseApplicationInfo = clsApplication.FindBaseApplication(ReleaseApplicationId);
            //entity.DetainDate = DetainDate.ToDateTime(new TimeOnly(0, 0));
            entity.FineFees = FineFees;
            entity.IsReleased = IsReleased;
        }
        #endregion
    }
}
