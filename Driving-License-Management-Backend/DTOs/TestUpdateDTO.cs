using Driving_License_Management_Backend.DTOs.Abstraction;
using Driving_License_Management_BusinessLogicLayer;
using System.ComponentModel.DataAnnotations;

namespace Driving_License_Management_Backend.DTOs
{
    public class TestUpdateDTO : IDTO<clsTest>
    {
        #region Properties
        [Key]
        public int Id { get; set; }
        [Required]
        public int TestAppointmentID { get; set; }
        [Required]
        public bool TestResult { get; set; }
        [Required]
        public string Notes { get; set; }
        [Required]
        public int CreatedByUserID { get; set; }
        #endregion
        #region Constructors
        public TestUpdateDTO() { }
        public TestUpdateDTO(clsTest entity) => SetValuesFromEntity(entity);

        #endregion
        #region Methods
        public void SetValuesFromEntity(clsTest entity)
        {
            this.Id = entity.TestID;
            this.TestAppointmentID = entity.TestAppointmentID;
            this.TestResult = entity.TestResult;
            this.Notes = entity.Notes;
            this.CreatedByUserID = entity.CreatedByUserID;
        }

        public void MapValuesToEntity(clsTest entity)
        {
            entity.TestID = Id;
            entity.TestAppointmentID = TestAppointmentID;
            entity.TestResult = TestResult;
            entity.Notes = Notes;
            entity.CreatedByUserID = CreatedByUserID;
            // Initalize Objects
            entity.TestAppointmentInfo = clsTestAppointment.Find(TestAppointmentID);

        }
        #endregion
    }
}
