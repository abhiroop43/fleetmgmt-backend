using System.ComponentModel.DataAnnotations;

namespace FleetMgmt.IdentityServer.Models.AccountViewModels
{
    public class ExternalLoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
