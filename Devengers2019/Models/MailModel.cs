using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Devengers2019.Models
{
    public class MailModel
    {
        [Key]
        public int MailId { get; set; }

        [Required]
        [EmailAddress]
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}