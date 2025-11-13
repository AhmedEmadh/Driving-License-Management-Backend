using Driving_License_Management_Backend.DTOs.Abstraction;
using Driving_License_Management_BusinessLogicLayer;
using System.ComponentModel.DataAnnotations;

namespace Driving_License_Management_Backend.DTOs
{
    public class UserUpdateDTO : IDTO<clsUser>
    {
        #region Properties
        [Key]
        public int id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        public bool IsActive { get; set; } = true;
        [Required]
        public int PersonID { get; set; }
        #endregion
        #region Constructors
        public UserUpdateDTO() { }
        public UserUpdateDTO(clsUser entity)
        {
            SetValuesFromEntity(entity);
        }
        #endregion
        #region Methods
        public void MapValuesToEntity(clsUser entity)
        {
            entity.UserID = this.id;
            entity.PersonID = this.PersonID;
            entity.PersonInfo = clsPerson.Find(this.PersonID);
            entity.UserName = this.UserName;
            entity.Password = this.Password;
            entity.IsActive = this.IsActive;
        }

        public void SetValuesFromEntity(clsUser entity)
        {
            this.id = entity.UserID;
            this.PersonID = entity.PersonID;
            this.UserName = entity.UserName;
            this.Password = entity.Password;
            this.IsActive = entity.IsActive;
        }
        #endregion
    }
}
