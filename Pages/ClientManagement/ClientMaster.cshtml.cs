using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TURN.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace TURN.Pages.ClientManagement
{
    public class ClientMasterModel : PageModel
    {
        private readonly ClientService _clientService;

        // Constructor to inject ClientService
        public ClientMasterModel(ClientService clientService)
        {
            _clientService = clientService;
        }

        [BindProperty]
        public int? ClientID { get; set; } // Add ClientID for Edit functionality

        [BindProperty]
        [Required(ErrorMessage = "*")]
        public string FirstName { get; set; }

        [BindProperty]
        public string MiddleName { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "*")]
        public string LastName { get; set; }

        [BindProperty]
        public string Suffix { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "*")]
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; } // Nullable to handle invalid dates

        [BindProperty]
        [Required(ErrorMessage = "*")]
        public string Gender { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "*")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "*")]
        public string ContactNumber { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "*")]
        [EmailAddress(ErrorMessage = "*")]
        public string Email { get; set; }

        public void OnGet(string ClientID, string FirstName, string MiddleName, string LastName, string Suffix,
                   string DateOfBirth, string Gender, string ContactNumber, string Email)
        {
            this.ClientID = int.TryParse(ClientID, out var id) ? id : (int?)null;
            this.FirstName = FirstName;
            this.MiddleName = MiddleName;
            this.LastName = LastName;
            this.Suffix = Suffix;
            this.DateOfBirth = DateTime.TryParse(DateOfBirth, out var dob) ? dob : (DateTime?)null;
            this.Gender = Gender;
            this.ContactNumber = ContactNumber;
            this.Email = Email;
        }


       

        // Add Update action
 

        // Add Delete action

    }
}
