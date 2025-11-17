using Driving_License_Management_Backend.DTOs.Abstraction;
using Driving_License_Management_BusinessLogicLayer;
using System.Text.Json.Serialization;

namespace Driving_License_Management_Backend.DTOs
{
    public class TestReadDTO: IDTO<clsTest>
    {
        #region Properties
        public int Id { get; set; }
        [JsonIgnore]
        public int TestAppointmentID { get; set; }
        public TestAppointmentReadDTO TestAppointmentInfo { get; set; }
        public bool TestResult { get; set; }
        public string Notes { get; set; }
        [JsonIgnore]
        public int CreatedByUserID { get; set; }
        public UserReadDTO CreatedByUserInfo { get; set; }
        #endregion
        #region Constructors
        public TestReadDTO() { }
        public TestReadDTO(clsTest entity)
        {
            SetValuesFromEntity(entity);
        }

        #endregion
        #region Methods
        public void SetValuesFromEntity(clsTest entity)
        {
            this.Id = entity.TestID;
            this.TestAppointmentID = entity.TestAppointmentID;
            this.TestResult = entity.TestResult;
            this.Notes = entity.Notes;
            this.CreatedByUserID = entity.CreatedByUserID;
            // Fill DTO Objects
            this.TestAppointmentInfo = new TestAppointmentReadDTO(entity.TestAppointmentInfo);
            this.CreatedByUserInfo = new UserReadDTO(clsUser.FindByUserID(this.CreatedByUserID));

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
