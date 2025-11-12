using Driving_License_Management_Backend.DTOs.Abstraction;
using Driving_License_Management_BusinessLogicLayer;

namespace Driving_License_Management_Backend.DTOs
{
    public class UserReadDTO : IDTO<clsUser>
    {
        #region Properties
        public int id { get; set; }
        public string UserName { get; set; }
        public bool IsActive { get; set; }
        public PersonDTO PersonalInformation { get; set; }
        #endregion
        #region Constructors
        public UserReadDTO()
        {
            PersonalInformation = new PersonDTO();
        }
        public UserReadDTO(clsUser entity)
        {
            SetValuesFromEntity(entity);
        }
        #endregion
        #region Methods
        public void MapValuesToEntity(clsUser entity)
        {
            entity.UserID = this.id;
            entity.UserName = this.UserName;
            entity.IsActive = this.IsActive;
            entity.PersonID = this.PersonalInformation.Id;
            entity.PersonInfo = clsPerson.Find(this.PersonalInformation.Id);
        }

        public void SetValuesFromEntity(clsUser entity)
        {
            this.id = entity.UserID;
            this.UserName = entity.UserName;
            this.IsActive = entity.IsActive;
            this.PersonalInformation = new PersonDTO(entity.PersonInfo);
        }
        #endregion
    }
}
