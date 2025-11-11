using Driving_License_Management_Backend.DTOs.Abstraction;
using Driving_License_Management_BusinessLogicLayer;
using Microsoft.Identity.Client;
using System;
using System.ComponentModel.DataAnnotations;

namespace Driving_License_Management_Backend.DTOs
{
    public class PersonDTO : IDTO<clsPerson>
    {
        #region Properties
        [Key]
        public int Id { get; set; }
        [Required]
        [MinLength(1)]
        public string NationalNumber { get; set; }
        [Required]
        [MinLength(1)]
        public string FirstName { get; set; }
        [Required]
        [MinLength(1)]
        public string SecoundName { get; set; }
        
        public string? ThirdName { get; set; }
        [Required]
        [MinLength(1)]
        public string LastName { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        [MinLength(1)]
        public string Gender { get; set; }
        [Required]
        [MinLength(1)]
        public string Address { get; set; }
        [Required]
        [MinLength(1)]
        public string PhoneNumber { get; set; }
        [MinLength(1)]
        public string Email { get; set; }
        [Required]
        public int CountryId { get; set; }
        public string? CountryName { get { return clsCountry.Find(CountryId)?.CountryName; } }
        public string? ImageURL { get; set; }
        #endregion
        #region Constructors
        public PersonDTO(){}

        public PersonDTO(clsPerson person)
        {
            SetValuesFromEntity(person);
        }
        #endregion
        #region Methods
        
        public void SetValuesFromEntity(clsPerson person)
        {
            this.Id = person.PersonID;
            this.NationalNumber = person.NationalNo;
            this.FirstName = person.FirstName;
            this.SecoundName = person.SecondName;
            this.ThirdName = person.ThirdName;
            this.LastName = person.LastName;
            this.DateOfBirth = person.DateOfBirth;
            this.Gender = (person.Gender == (short)clsPerson.enGender.Male) ? "Male" : "Female";
            this.Address = person.Address;
            this.PhoneNumber = person.Phone;
            this.Email = person.Email;
            this.CountryId = person.NationalityCountryID;
            this.ImageURL = person.ImagePath;
        }

        public void MapValuesToEntity(clsPerson person)
        {
            person.PersonID = this.Id;
            person.NationalNo = this.NationalNumber;
            person.FirstName = this.FirstName;
            person.SecondName = this.SecoundName;
            person.ThirdName = this.ThirdName;
            person.LastName = this.LastName;
            person.DateOfBirth = this.DateOfBirth;
            person.Gender = (this.Gender == "Male") ? (short)clsPerson.enGender.Male : (short)clsPerson.enGender.Female;
            person.Address = this.Address;
            person.Phone = this.PhoneNumber;
            person.Email = this.Email;
            person.NationalityCountryID = this.CountryId;
            person.ImagePath = this.ImageURL;
        }
        #endregion
    }
}
