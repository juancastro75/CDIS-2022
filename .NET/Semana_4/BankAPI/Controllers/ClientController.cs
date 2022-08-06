using Microsoft.AspNetCore.Mvc;
using BankAPI.Services;
using BankAPI.Data.BankModels;

namespace BankAPI.Controllers;

[ApiController]
[Route("api/[controller]")]

public class ClientController : ControllerBase
{
    private readonly ClientService clientService;

    public ClientController(ClientService client)
    {
        clientService = client;
    }

    [HttpGet]
    public IEnumerable<Client> Get()
    {
        return clientService.GetAll();
    }

    [HttpGet("{id}")]
    public ActionResult<Client> GetById(int id)
    {
        var client = clientService.GetById(id);

        if(client is null)
            return NotFound();

        return client;
    }

    [HttpPost]
    public IActionResult Create(Client client)
    {
        var newClient = clientService.Create(client); 

        return CreatedAtAction(nameof(GetById), new {id = client.Id}, client);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, Client client)
    {
        if (id != client.Id)
            return BadRequest();
        
        var clientToUpdate = clientService.GetById(id);
        if(clientToUpdate is not null)
        {
            clientService.Update(id, client);
            return NoContent();
        }
        else
        {
            return NotFound();
        }
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var clientToDelete = clientService.GetById(id);
        if(clientToDelete is not null)
        {
            clientService.Delete(id);
            return NoContent();
        }
        else
        {
            return NotFound();
        }
    }

}