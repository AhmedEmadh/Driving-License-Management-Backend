using Driving_License_Management_Backend.DTOs.Abstraction;
using Driving_License_Management_BusinessLogicLayer;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Driving_License_Management_Backend.DTOs
{
    public class LocalDrivingLicenseApplicationReadDTO : ApplicationReadDTO
    {
        #region Properties
        [Key]
        public int LocalDrivingLicenseApplicationId { get; set; }
        [JsonIgnore]
        public int CreatedUsingApplicationId { get; set; }
        public ApplicationReadDTO ApplicationInfo { get; set; }
        [JsonIgnore]
        public int LicenseClassId { get; set; }
        public LicenseClassDTO LicenseClassInfo { get; set; }
        #endregion
        #region Constructors
        public LocalDrivingLicenseApplicationReadDTO()
        {

        }
        public LocalDrivingLicenseApplicationReadDTO(clsLocalDrivingLicenseApplication entity)
        {
            SetValuesFromEntity(entity);
        }

        #endregion
        #region Methods
        public void SetValuesFromEntity(clsLocalDrivingLicenseApplication entity)
        {
            LocalDrivingLicenseApplicationId = entity.LocalDrivingLicenseApplicationID;
            CreatedUsingApplicationId = entity.LocalDrivingLicenseApplicationID;
            ApplicationId = entity.ApplicationID;
            LicenseClassId = entity.LicenseClassID;
            // Base Values
            base.SetValuesFromEntity(entity);
            // Initalize Objects
            ApplicationInfo = new ApplicationReadDTO(clsApplication.FindBaseApplication(entity.ApplicationID));
            LicenseClassInfo = new LicenseClassDTO(entity.LicenseClassInfo);
        }

        public void MapValuesToEntity(clsLocalDrivingLicenseApplication entity)
        {
            entity.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationId;
            entity.ApplicationID = ApplicationId;
            entity.LicenseClassID = LicenseClassId;
            // Base Values
            base.MapValuesToEntity(entity);
            //Load Object
            entity.LicenseClassInfo = clsLicenseClass.Find(LicenseClassId);
        }
        #endregion
    }
}
