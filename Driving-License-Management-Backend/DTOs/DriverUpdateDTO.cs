using Driving_License_Management_Backend.DTOs.Abstraction;
using Driving_License_Management_BusinessLogicLayer;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Driving_License_Management_Backend.DTOs
{
    public class DriverUpdateDTO: IDTO<clsDriver>
    {
        #region Properties
        [Key]
        public int id { get; set; }
        [Required]
        public int PersonId { get; set; }
        [Required]
        public int CreatorUserId { get; set; }
        #endregion
        #region Constructors
        public DriverUpdateDTO() { }
        public DriverUpdateDTO(clsDriver driver)
        {
            SetValuesFromEntity(driver);
        }
        #endregion
        #region Methods
        public void SetValuesFromEntity(clsDriver driver)
        {
            this.id = driver.DriverID;
            this.PersonId = driver.PersonID;
            this.CreatorUserId = driver.CreatedByUserID;
        }
        public void MapValuesToEntity(clsDriver driver)
        {
            driver.DriverID = this.id;
            driver.PersonID = this.PersonId;
            driver.PersonInfo = clsPerson.Find(this.PersonId);
            driver.CreatedByUserID = this.CreatorUserId;
            driver.CreatedByUserInfo = clsUser.FindByUserID(this.CreatorUserId);
        }
        #endregion
    }
}
