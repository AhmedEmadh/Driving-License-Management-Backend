using Driving_License_Management_BusinessLogicLayer;
using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;

namespace Driving_License_Management_Backend.DTOs
{
    public class PersonDTO
    {
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
        public DateTime DataOfBirth { get; set; }
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

        public PersonDTO(){}

        public PersonDTO(clsPerson person)
        {
            this.Id = person.PersonID;
            this.NationalNumber = person.NationalNo;
            this.FirstName = person.FirstName;
            this.SecoundName = person.SecondName;
            this.ThirdName = person.ThirdName;
            this.LastName = person.LastName;
            this.DataOfBirth = person.DateOfBirth;
            this.Gender = (person.Gender == (short)clsPerson.enGender.Male)?"Male":"Female";
            this.Address = person.Address;
            this.PhoneNumber = person.Phone;
            this.Email = person.Email;
            this.CountryId = person.NationalityCountryID;
            this.ImageURL = person.ImagePath;


        }
        public static void MapDTOToPersonData(PersonDTO source, clsPerson Destination)
        {

            Destination.NationalNo = source.NationalNumber;
            Destination.FirstName = source.FirstName;
            Destination.SecondName = source.SecoundName;
            Destination.ThirdName = source.ThirdName;
            Destination.LastName = source.LastName;
            Destination.DateOfBirth = source.DataOfBirth;
            Destination.Gender = (source.Gender == "Male") ? (short)clsPerson.enGender.Male : (short)clsPerson.enGender.Female;
            Destination.Address = source.Address;
            Destination.PhoneNumber = source.PhoneNumber;
            Destination.Phone = source.PhoneNumber;
            Destination.Email = source.Email;
            Destination.NationalityCountryID = source.CountryId;
            Destination.ImagePath = source.ImageURL;
        }
    }
}
