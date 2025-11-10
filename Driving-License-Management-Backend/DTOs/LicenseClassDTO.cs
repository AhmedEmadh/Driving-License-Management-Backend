using Driving_License_Management_BusinessLogicLayer;

namespace Driving_License_Management_Backend.DTOs
{
    public class LicenseClassDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte MinimumAllowedAge { get; set; }
        public byte DefaultValidityLengthInYears { get; set; }
        public float ClassFees { get; set; }

        public LicenseClassDTO() { }
        public LicenseClassDTO(clsLicenseClass licenseClass)
        {
            this.ID = licenseClass.LicenseClassID;
            this.Name = licenseClass.ClassName;
            this.Description = licenseClass.ClassDescription;
            this.MinimumAllowedAge = licenseClass.MinimumAllowedAge;
            this.DefaultValidityLengthInYears = licenseClass.DefaultValidityLength;
            this.ClassFees = licenseClass.ClassFees;

        }
        public void MapToEntity(clsLicenseClass licenseClass)
        {
            licenseClass.LicenseClassID = this.ID;
            licenseClass.ClassName = this.Name;
            licenseClass.ClassDescription = this.Description;
            licenseClass.MinimumAllowedAge = this.MinimumAllowedAge;
            licenseClass.DefaultValidityLength = this.DefaultValidityLengthInYears;
            licenseClass.ClassFees = this.ClassFees;

        }
    }
}
