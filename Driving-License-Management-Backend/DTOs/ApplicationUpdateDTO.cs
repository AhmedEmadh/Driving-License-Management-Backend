using Driving_License_Management_Backend.DTOs.Abstraction;
using Driving_License_Management_BusinessLogicLayer;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Driving_License_Management_Backend.DTOs
{
    public class ApplicationUpdateDTO : IDTO<clsApplication>
    {
        #region Properties
        [Key]
        public int ApplicationID { get; set; }
        [Required]
        public int ApplicantPersonID { get; set; }
        [Required]
        public int ApplicationTypeID { get; set; }
        [Required]
        public short ApplicationStatusId { get; set; }
        private DateTime ApplicationDate = DateTime.Now;
        private DateTime LastStatusDate = DateTime.Now;
        [Required]
        public float PaidFees { get; set; }
        [Required]
        public int CreatorUserID { get; set; }
        #endregion
        #region Constructors
        public ApplicationUpdateDTO()
        {
        }
        public ApplicationUpdateDTO(clsApplication entity)
        {
            SetValuesFromEntity(entity);
        }
        #endregion
        #region Methods
        public void MapValuesToEntity(clsApplication entity)
        {
            entity.ApplicationID = ApplicationID;
            entity.ApplicantPersonID = ApplicantPersonID;
            entity.ApplicationTypeID = ApplicationTypeID;
            entity.ApplicationStatus = (clsApplication.enApplicationStatus)ApplicationStatusId;
            entity.PaidFees = PaidFees;
            entity.LastStatusDate = LastStatusDate;
            entity.ApplicationDate = ApplicationDate;
            entity.CreatedByUserID = CreatorUserID;
        }

        public void SetValuesFromEntity(clsApplication entity)
        {
            ApplicationID = entity.ApplicationID;
            ApplicantPersonID = entity.ApplicantPersonID;
            ApplicationTypeID = entity.ApplicationTypeID;
            ApplicationStatusId = (short)entity.ApplicationStatus;
            PaidFees = entity.PaidFees;
            LastStatusDate = entity.LastStatusDate;
            ApplicationDate = entity.ApplicationDate;
            CreatorUserID = entity.CreatedByUserID;
        }
        #endregion
    }
}
