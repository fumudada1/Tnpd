using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace TnpdModels
{
    public class SMTPServerSetting : BackendBase
    {
        [Key]
        [Display(Name = "編號")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(200)]
        [Display(Name = "SMTP名稱")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(200)]
        [Display(Name = "SMTP Address")]
        public string Address { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(200)]
        [Display(Name = "Account")]
        public string MailAccount { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(200)]
        [Display(Name = "Password")]
        public string MailPassword { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "Port")]
        public int Port { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "EnableSsl")]
        public bool EnableSsl { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "UseDefaultCredentials")]
        public bool UseDefaultCredentials { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "SmtpDeliveryMethod")]
        public SmtpDeliveryMethod SmtpDeliveryMethod { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "Enable")]
        public bool Enable { get; set; }

       
        [MaxLength(200)]
        [Display(Name = "MailFrom")]
        public string MailFrom { get; set; }
    }
}
