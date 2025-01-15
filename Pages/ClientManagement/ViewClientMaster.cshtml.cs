using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TURN.Models;
using TURN.Models;
using System.Collections.Generic;

namespace TURN.Pages.ClientManagement
{
    public class ViewClientMasterModel : PageModel
    {
        private readonly ClientService _clientService;

        public ViewClientMasterModel(ClientService clientService)
        {
            _clientService = clientService;
        }

        public List<ClientModel> Clients { get; private set; }

        public void OnGet()
        {
            // Fetch client data
            Clients = _clientService.GetClients();
        }
    }
}




