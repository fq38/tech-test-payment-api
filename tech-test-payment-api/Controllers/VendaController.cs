using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using tech_test_payment_api.Data;
using tech_test_payment_api.Models;

namespace tech_test_payment_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VendaController : ControllerBase
    {
        private readonly DataContext _context;

        public VendaController(DataContext context)
        {
            _context = context;
        }


        // GET: lista todos
        [HttpGet("ObterTodos")]
        public List<Vendedor> ObterTodos()
        {
            // Buscar todas as vendas no banco utilizando o EF
            return _context.Vendedores.ToList();
           
        }


        // GET: Buscar o Id no banco utilizando o EF
        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id)
        {
            
            //  Validar o tipo de retorno. Se não encontrar a venda, retornar NotFound,
            // caso contrário retornar OK com a venda encontrada
            var venda = _context.Vendedores.Find(id);

            if (venda == null)
                return NotFound();
            return Ok(venda);
            
        }

        // POST: criar Registrar venda
        [HttpPost]
        public IActionResult Criar(Vendedor venda)
        {
            if (venda.Data == DateTime.MinValue)
                return BadRequest(new { Erro = "A data da venda não pode ser vazia" });

            // Adicionar a venda recebida no EF e salvar as mudanças (save changes)
            _context.Add(venda);
            _context.SaveChanges();
            
            return CreatedAtAction(nameof(ObterPorId), new { id = venda.Id }, venda);
        }


        //UPDATE: Atualizar venda
        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, Vendedor venda)
        {
            var vendaBanco = _context.Vendedores.Find(id);

            if (vendaBanco == null)
                return NotFound();

            if (venda.Data == DateTime.MinValue)
                return BadRequest(new { Erro = "A data da venda não pode ser vazia" });


            //  Atualizar as informações da variável vendaBanco com a venda recebida via parâmetro
            //  Atualizar a variável vendaBanco no EF e salvar as mudanças (save changes)

            //vendaBanco.Nome = venda.Nome;
           // vendaBanco.Email = venda.Email;
            vendaBanco.Status = venda.Status;

            _context.Update(vendaBanco);
            _context.SaveChanges();
            
            return Ok(vendaBanco);
        }


        // Delete: 
        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            var vendaBanco = _context.Vendedores.Find(id);

            if (vendaBanco == null)
                return NotFound();

            //  Remover a venda encontrada através do EF e salvar as mudanças (save changes)
            _context.Vendedores.Remove(vendaBanco);
            _context.SaveChanges(true);
            
            return NoContent();
        }
    }
    
}
