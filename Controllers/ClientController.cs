

using Microsoft.AspNetCore.Mvc;
using TURN.Models;

[ApiController]
[Route("api/[controller]")]
public class ClientController : ControllerBase
{
    private readonly ClientService _clientService;

    public ClientController(ClientService clientService)
    {
        _clientService = clientService;
    }

    // Endpoint to load all clients
    [HttpGet("Load")]
    public IActionResult LoadClients()
    {
        var clients = _clientService.GetClients();
        return Ok(clients); // Return the data as JSON
    }

    // Endpoint to save a new client
    [HttpPost("Save")]
    public IActionResult SaveClient([FromBody] ClientModel client)
    {
        _clientService.InsertClient(client.FirstName, client.MiddleName, client.LastName, client.Suffix,
            client.DateOfBirth.Value, client.Gender, client.ContactNumber, client.Email);
        return Ok("Client saved successfully.");
    }

    // Endpoint to update an existing client
    [HttpPut("Update")]
    public IActionResult UpdateClient([FromBody] ClientModel client)
    {
        if (client.ClientID == null)
        {
            return BadRequest("ClientID is required for update.");
        }

        _clientService.UpdateClient(client.ClientID.Value, client.FirstName, client.MiddleName, client.LastName,
            client.Suffix, client.DateOfBirth, client.Gender, client.ContactNumber, client.Email);
        return Ok("Client updated successfully.");
    }

    // Endpoint to delete a client
    [HttpDelete("Delete/{id}")]
    public IActionResult DeleteClient(int id)
    {
        _clientService.DeleteClient(id);
        return Ok("Client deleted successfully.");
    }
}

