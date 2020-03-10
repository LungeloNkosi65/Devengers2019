using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;



namespace Devengers2019.Models
{
    public class Student
    {
        [Key]
        [JsonProperty(PropertyName = "StudentID")]
        public string StudentID { get; set; }

        [JsonProperty(PropertyName = "StudentNo")]

        [DisplayName("Student Number")]
        [RegularExpression(@"^(\d{8})$", ErrorMessage = "Enter a valid Student Number")]
        public string StudentNo { get; set; }
        [JsonProperty(PropertyName = "Name")]
        [Required(ErrorMessage = "Please enter your first name.")]


        [DisplayName(" Name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "Surname")]
        [Required(ErrorMessage = "Please enter your last name.")]

        [DisplayName(" Surname")]
        public string Surname { get; set; }
        [JsonProperty(PropertyName = "Email")]
        [Required(ErrorMessage = "Please enter your e-mail address")]

        [DisplayName("Email Address")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [JsonProperty(PropertyName = "TelephoneNo")]

        [DisplayName("Telephone Number")]
        [DataType(DataType.PhoneNumber)]
        public string TelephoneNo { get; set; }
        [JsonProperty(PropertyName = "Mobile")]

        [DisplayName("Phone Number")]
        [DataType(DataType.PhoneNumber)]
        public string Mobile { get; set; }

        [JsonProperty(PropertyName = "IsActive")]

        [DisplayName("Is Active")]
        public bool IsActive { get; set; }

        [JsonProperty(PropertyName = "imageUrl")]
        public string ImageUrl { get; set; }

    }
}