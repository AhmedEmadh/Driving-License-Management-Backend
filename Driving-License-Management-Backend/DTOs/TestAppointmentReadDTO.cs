using Driving_License_Management_Backend.DTOs.Abstraction;
using Driving_License_Management_BusinessLogicLayer;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Driving_License_Management_Backend.DTOs
{
    public class TestAppointmentReadDTO: IDTO<clsTestAppointment>
    {
        #region Properties
        [Key]
        public int ID { get; set; }
        [JsonIgnore]
        public int TestTypeID { get; set; }
        public TestTypeDTO TestType { get; set; }
        [JsonIgnore]
        public int LocalDrivingLicenseApplicationID { get; set; }
        public LocalDrivingLicenseApplicationReadDTO LocalDrivingLicenseApplication { get; set; }
        public DateTime AppointmentDate { get; set; }
        public bool IsLocked { get; set; }
        [JsonIgnore]
        public int? RetakeTestApplicationID { get; set; }
        public ApplicationReadDTO RetakeTestApplication { get; set; }
        public float PaidFees { get; set; }
        [JsonIgnore]
        public int CreatedByUserID { get; set; }
        public UserReadDTO CreatedByUser { get; set; }
        #endregion
        #region Constructor
        public TestAppointmentReadDTO()
        {
        }
        public TestAppointmentReadDTO(clsTestAppointment entity)
        {
            SetValuesFromEntity(entity);
        }
        #endregion
        #region Methods
        public void MapValuesToEntity(clsTestAppointment entity)
        {
            entity.TestAppointmentID = this.ID;
            entity.TestTypeID = (clsTestType.enTestType)this.TestTypeID;
            entity.LocalDrivingLicenseApplicationID = this.LocalDrivingLicenseApplicationID;
            entity.AppointmentDate = this.AppointmentDate;
            entity.IsLocked = this.IsLocked;
            entity.PaidFees = this.PaidFees;
            entity.CreatedByUserID = this.CreatedByUserID;
            entity.RetakeTestApplicationID = this.RetakeTestApplicationID ?? 0;
            // Fill Objects
            entity.TestTypeInfo = clsTestType.Find(this.TestTypeID);
            entity.LocalDrivingLicenseApplicationInfo = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(this.LocalDrivingLicenseApplicationID);
            if (this.RetakeTestApplicationID.HasValue)
            {
                entity.RetakeTestAppInfo = clsApplication.FindBaseApplication(this.RetakeTestApplicationID.Value);
            }
        }

        public void SetValuesFromEntity(clsTestAppointment entity)
        {
            this.ID = entity.TestAppointmentID;
            this.TestTypeID = (int)entity.TestTypeID;
            this.LocalDrivingLicenseApplicationID = entity.LocalDrivingLicenseApplicationID;
            this.AppointmentDate = entity.AppointmentDate;
            this.IsLocked = entity.IsLocked;
            this.PaidFees = entity.PaidFees;
            this.CreatedByUserID = entity.CreatedByUserID;
            this.RetakeTestApplicationID = entity.RetakeTestApplicationID != 0 ? entity.RetakeTestApplicationID : null;
            // Fill DTO Objects
            this.TestType = new TestTypeDTO(entity.TestTypeInfo);
            this.LocalDrivingLicenseApplication = new LocalDrivingLicenseApplicationReadDTO(entity.LocalDrivingLicenseApplicationInfo);
            if (entity.RetakeTestAppInfo != null)
            {
                this.RetakeTestApplication = new ApplicationReadDTO(entity.RetakeTestAppInfo);
            }
            this.CreatedByUser = new UserReadDTO(clsUser.FindByUserID(this.CreatedByUserID));
        }
        #endregion
    }
}
