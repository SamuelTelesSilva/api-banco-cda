using Banco.Models;
using Banco.Persistence;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Banco.Controllers
{
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {
   
        private readonly ClienteDbContext _clienteDbContext;
        public ClienteController(ClienteDbContext clienteDbContext)
        {
            _clienteDbContext = clienteDbContext;
        }

        
        [HttpGet]
        public IActionResult Get() 
        {
            var clientes = _clienteDbContext.Clientes.ToList();
            return Ok(clientes);
        }

        
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var cliente = _clienteDbContext.Clientes.SingleOrDefault(p => p.Id == id);
            
            if (cliente == null)
            {
                return NotFound();
            }
            return Ok(cliente);
         
        }

        //post para o BD em memoria
        [HttpPost]
        public IActionResult Post([FromBody] ClienteInputModel clienteInputModel)
        {
            if (clienteInputModel == null)
            {
                return BadRequest();
            }

            var cliente = new Cliente(clienteInputModel.Nome, clienteInputModel.Cpf);

            _clienteDbContext.Clientes.Add(cliente);
            _clienteDbContext.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = cliente.Id }, cliente);
        }

        [HttpPut("{id}")]
        public IActionResult Put ([FromBody] ClienteInputModel clienteInputModel, int id)
        {
            if(clienteInputModel == null)
            {
                return BadRequest();
            }

            var cliente = _clienteDbContext.Clientes.SingleOrDefault(p => p.Id == id);

            if (cliente == null)
            {
                return NotFound();
            }

            cliente.Nome = clienteInputModel.Nome;
            cliente.Cpf = clienteInputModel.Cpf;

            _clienteDbContext.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete (int id)
        {
            var cliente = _clienteDbContext.Clientes.SingleOrDefault(p => p.Id == id);

            if (cliente == null)
            {
                return NotFound();
            }

            _clienteDbContext.Clientes.Remove(cliente);

            _clienteDbContext.SaveChanges();

            return NoContent();
        }
        
    }
}
