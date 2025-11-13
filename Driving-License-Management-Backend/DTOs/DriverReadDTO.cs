using Driving_License_Management_Backend.DTOs.Abstraction;
using Driving_License_Management_BusinessLogicLayer;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Driving_License_Management_Backend.DTOs
{
    public class DriverReadDTO: IDTO<clsDriver>
    {
        #region Properties
        [Key]
        public int id { get; set; }
        public PersonDTO? PersonalInformation { get; set; }
        public UserReadDTO? CreatedBy { get; set; }
        public DateTime CreationDate { get; set; }
        #endregion
        #region Constructors
        public DriverReadDTO() { }
        public DriverReadDTO(clsDriver driver)
        {
            SetValuesFromEntity(driver);
        }
        #endregion
        #region Methods
        public void SetValuesFromEntity(clsDriver driver)
        {
            this.id = driver.DriverID;
            this.PersonalInformation = new PersonDTO(clsPerson.Find(driver.PersonID));
            this.CreatedBy = new UserReadDTO(clsUser.FindByUserID(driver.CreatedByUserID));
            this.CreationDate = driver.CreatedDate;
        }
        public void MapValuesToEntity(clsDriver driver)
        {
            driver.DriverID = this.id;
            driver.PersonID = this.PersonalInformation.Id;
            driver.PersonInfo = clsPerson.Find(this.PersonalInformation.Id);
            driver.CreatedByUserID = this.CreatedBy.id;
            driver.CreatedByUserInfo = clsUser.FindByUserID(this.CreatedBy.id);
            driver.CreatedDate = this.CreationDate;
        }
        #endregion
    }
}
