using Driving_License_Management_Backend.DTOs.Abstraction;
using Driving_License_Management_BusinessLogicLayer;
using System.ComponentModel.DataAnnotations;
using System.Net.Sockets;

namespace Driving_License_Management_Backend.DTOs
{
    public class LocalDrivingLicenseApplicationUpdateDTO : ApplicationUpdateDTO
    {
        #region Properties
        [Key]
        public int LocalDrivingLicenseApplicationId { get; set; }
        [Required]
        public int LicenseClassId { get; set; }
        #endregion
        #region Constructors
        public LocalDrivingLicenseApplicationUpdateDTO()
        {
            
        }
        public LocalDrivingLicenseApplicationUpdateDTO(clsLocalDrivingLicenseApplication entity)
        {
            SetValuesFromEntity(entity);
        }

        #endregion
        #region Methods
        public void SetValuesFromEntity(clsLocalDrivingLicenseApplication entity)
        {
            LocalDrivingLicenseApplicationId = entity.LocalDrivingLicenseApplicationID;
            ApplicationID = entity.ApplicationID;
            LicenseClassId = entity.LicenseClassID;
            // Base Properties
            base.SetValuesFromEntity(entity);
        }

        public void MapValuesToEntity(clsLocalDrivingLicenseApplication entity)
        {
            entity.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationId;
            entity.ApplicationID = ApplicationID;
            entity.LicenseClassID = LicenseClassId;
            // Base Properties
            base.MapValuesToEntity(entity);
            //Load Object
            entity.LicenseClassInfo = clsLicenseClass.Find(LicenseClassId);
        }
        #endregion
    }
}
