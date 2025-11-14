using Driving_License_Management_Backend.DTOs.Abstraction;
using Driving_License_Management_BusinessLogicLayer;
using System.ComponentModel.DataAnnotations;

namespace Driving_License_Management_Backend.DTOs
{
    public class TestTypeDTO : IDTO<clsTestType>
    {
        #region Properties
        [Key]
        public int id { get; set; }
        [Required]
        public string Title {get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public float Fees { get; set; }
        #endregion
        #region Constructors
        public TestTypeDTO()
        {

        }
        public TestTypeDTO(clsTestType entity)
        {
            SetValuesFromEntity(entity);
        }
        #endregion
        #region Methods
        public void MapValuesToEntity(clsTestType entity)
        {
            entity.ID = this.id;
            entity.Title = this.Title;
            entity.Description = this.Description;
            entity.Fees = this.Fees;
        }

        public void SetValuesFromEntity(clsTestType entity)
        {
            this.id = entity.ID;
            this.Title = entity.Title;
            this.Description = entity.Description;
            this.Fees = entity.Fees;
        }
        #endregion
    }
}
