using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Falquan.KeyRinger.Models
{
    public class Service
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        [NotMapped]
        public string Password { get; set; }
        public byte[] EncryptedPassword { get; set; }
        [DataType(DataType.Url)]
        public string UrlString { get; set; }

        [NotMapped]
        public Uri Uri
        {
            get { return new Uri(this.UrlString); }
        }

        public virtual User User { get; set; }
    }
}