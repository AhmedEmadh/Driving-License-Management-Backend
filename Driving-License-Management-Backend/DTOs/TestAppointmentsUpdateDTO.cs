using Driving_License_Management_Backend.DTOs.Abstraction;
using Driving_License_Management_BusinessLogicLayer;
using System.ComponentModel.DataAnnotations;

namespace Driving_License_Management_Backend.DTOs
{
    public class TestAppointmentsUpdateDTO: IDTO<clsTestAppointment>
    {
        #region Properties
        [Key]
        public int ID { get; set; }
        [Required]
        public int TestTypeID { get; set; }
        [Required]
        public int LocalDrivingLicenseApplicationID { get; set; }
        [Required]
        public DateTime AppointmentDate { get; set; }
        [Required]
        public bool IsLocked { get; set; }
        public int? RetakeTestApplicationID { get; set; }
        [Required]
        public float PaidFees { get; set; }
        [Required]
        public int CreatedByUserID { get; set; }
        #endregion
        #region Constructor
        public TestAppointmentsUpdateDTO()
        {
        }
        public TestAppointmentsUpdateDTO(clsTestAppointment entity)
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
        }
        #endregion
    }
}
