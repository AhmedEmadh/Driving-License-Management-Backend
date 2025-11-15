using Driving_License_Management_Backend.DTOs.Abstraction;
using Driving_License_Management_BusinessLogicLayer;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Driving_License_Management_Backend.DTOs
{
    public class DetainedLicenseUpdateDTO: IDTO<clsDetainedLicense>
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int LicenseId { get; set; }
        [Required]
        public int CreatedByUserId { get; set; }
        public int ReleasedByUserId { get; set; }
        public int ReleaseApplicationId { get; set; }
        //public DateOnly DetainDate { get; set; }
        [Required]
        public float FineFees { get; set; }
        public bool IsReleased { get; set; }
        
        public DateOnly ReleaseDate { get; set; }

        public void MapValuesToEntity(clsDetainedLicense entity)
        {
            entity.DetainID = Id;
            entity.LicenseID = LicenseId;
            //entity.DetainDate = DetainDate.ToDateTime(new TimeOnly(0, 0));
            entity.FineFees = FineFees;
            entity.CreatedByUserID = CreatedByUserId;
            entity.IsReleased = IsReleased;
            //entity.ReleaseDate = ReleaseDate.ToDateTime(new TimeOnly(0, 0));
            entity.ReleasedByUserID = ReleasedByUserId;
            entity.ReleaseApplicationID = ReleaseApplicationId;
        }

        public void SetValuesFromEntity(clsDetainedLicense entity)
        {
            Id = entity.DetainID;
            LicenseId = entity.LicenseID;
            //DetainDate = DateOnly.FromDateTime(entity.DetainDate);
            FineFees = entity.FineFees;
            CreatedByUserId = entity.CreatedByUserID;
            IsReleased = entity.IsReleased;
            ReleaseDate = DateOnly.FromDateTime(entity.ReleaseDate);
            ReleasedByUserId = entity.ReleasedByUserID;
            ReleaseApplicationId = entity.ReleaseApplicationID;
        }
    }
}
