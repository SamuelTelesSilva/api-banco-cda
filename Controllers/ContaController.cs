using Banco.Persistence;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Banco.Models;
using System;

namespace Banco.Controllers
{
    [Route("api/[Controller]")]
    public class ContaController : ControllerBase
    {
        private readonly ContaDbContext _contaDbContext;
      
        public ContaController(ContaDbContext contaDbContext)
        {
            _contaDbContext = contaDbContext;
        }

       
        [HttpGet]
        public IActionResult Get()
        {
            var contas = _contaDbContext.Contas.ToList();
            return Ok(contas);
        }

   
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var conta = _contaDbContext.Contas.SingleOrDefault(p => p.Id == id);

            if (conta == null)
            {
                return NotFound();
            }
            return Ok(conta);
        }

        [HttpGet("{id}")]
        public IActionResult GetPorData(int id)
        {
            var conta = _contaDbContext.Contas.SingleOrDefault(p => p.Id == id);

            if (conta == null)
            {
                return NotFound();
            }
            return Ok(conta);
        }

        [HttpPost("postar")]
        public IActionResult Post([FromBody] ContaInputModel contaInputModel)
        {
            if (contaInputModel == null)
            {
                return BadRequest();
            }

            var deposito = new Conta(contaInputModel.Saldo, contaInputModel.NumeroConta, contaInputModel.ValorDeposito, contaInputModel.SacarValor);

            _contaDbContext.Contas.Add(deposito);
            _contaDbContext.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = deposito.Id }, deposito);
        }


        [HttpPost("{id}/depositar")]
        public IActionResult Depositar([FromBody] ContaInputModel contaInputModel, int id)
        {
            if (contaInputModel == null)
            {
                return BadRequest();
            }

            var deposito = _contaDbContext.Contas.SingleOrDefault(p => p.Id == id);

            if (deposito == null)
            {
                return NotFound();
            }

            /*
            DateTime dataExtrato = DateTime.Today;
            dataExtrato.ToString("d");*/

            
            deposito.Saldo += contaInputModel.ValorDeposito;
            _contaDbContext.SaveChanges();

            return NoContent();
        }

        [HttpPost("{id}/sacar")]
        public IActionResult Sacar([FromBody] ContaInputModel contaInputModel, int id)
        {
            if (contaInputModel == null)
            {
                return BadRequest("Causas do Erro: Saldo insuficiente ou o valor não informado!");
            }

            var sacarV = _contaDbContext.Contas.SingleOrDefault(p => p.Id == id);

            if (sacarV == null)
            {
                return NotFound();
            }

            sacarV.Saldo -= contaInputModel.SacarValor;
            _contaDbContext.SaveChanges();

            return NoContent();
        }




    }
}
