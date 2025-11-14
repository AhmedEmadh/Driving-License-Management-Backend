using Driving_License_Management_Backend.DTOs.Abstraction;
using Driving_License_Management_BusinessLogicLayer;

namespace Driving_License_Management_Backend.DTOs
{
    public class ApplicationReadDTO:IDTO<clsApplication>
    {
        #region Properties
        public int id { get; set; }
        public int ApplicantPersonID { get; set; }
        public string ApplicantFullName { get; set; }
        public DateTime ApplicationDate { get; set; }
        private int ApplicationTypeID;
        public ApplicationTypeDTO ApplicationType { get; set; }
        public int ApplicationStatusId { get; set; }
        public string ApplicationStatusName { get; set; }
        private int CreatedByUserID { get; set; }
        public UserReadDTO CreatedByUser { get; set; }
        #endregion
        #region Constructors
        public ApplicationReadDTO()
        {
        }
        public ApplicationReadDTO(clsApplication entity)
        {
            SetValuesFromEntity(entity);
        }
        #endregion
        #region Methods

        public void SetValuesFromEntity(clsApplication entity)
        {
            this.id = entity.ApplicationID;
            this.ApplicantPersonID =  entity.ApplicantPersonID;
            this.ApplicantFullName = entity.ApplicantfullName;
            this.ApplicationDate = entity.ApplicationDate;
            this.ApplicationTypeID = entity.ApplicationTypeID;
            this.ApplicationType = new ApplicationTypeDTO(entity.ApplicationTypeInfo);
            this.ApplicationStatusId = (int)entity.ApplicationStatus;
            this.ApplicationStatusName = entity.StatusText;
            this.CreatedByUserID = entity.CreatedByUserID;
            this.CreatedByUser = new UserReadDTO(entity.CreatedByUserInfo);
        }

        public void MapValuesToEntity(clsApplication entity)
        {
            entity.ApplicationID = this.id;
            entity.ApplicantPersonID = this.ApplicantPersonID;
            entity.ApplicantPersonInfo = clsPerson.Find(this.ApplicantPersonID);
            entity.ApplicationDate = this.ApplicationDate;
            entity.ApplicationTypeID = this.ApplicationTypeID;
            entity.ApplicationTypeInfo = clsApplicationType.Find(this.ApplicationTypeID);
            entity.ApplicationStatus = (clsApplication.enApplicationStatus)this.ApplicationStatusId;
            entity.CreatedByUserID = this.CreatedByUserID;
            entity.CreatedByUserInfo = clsUser.FindByUserID(this.CreatedByUserID);
        }
        #endregion
    }
}
