using BookAPI.Model;
using BookAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository _bookrepository;

        public BookController(IBookRepository bookRepository)
        {
            _bookrepository = bookRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Book>> GetBooks()
        {
            return await _bookrepository.Get();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBooks(int id)
        {
            var book = await _bookrepository.Get(id);
            if (book == null)
            {
                return NotFound();
            }
            return book;
        }

        [HttpPost]
        public async Task<ActionResult<Book>> PostBook([FromBody] Book book)
        {
            var newBook = await _bookrepository.Create(book);
            return CreatedAtAction(nameof(GetBooks), new { id = newBook.Id }, newBook);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBook(int id)
        {
            var bookToDelete = await _bookrepository.Get(id);
            if (bookToDelete == null)
            {
                return NotFound(); // O livro não foi encontrado.
            }

            await _bookrepository.Delete(bookToDelete.Id);
            return NoContent(); // Deletado com sucesso.
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutBooks(int id, [FromBody] Book book)
        {
            if (id != book.Id)
            {
                return BadRequest("ID na URL não corresponde ao ID do livro."); // IDs inconsistentes.
            }

            await _bookrepository.Update(book);
            return NoContent(); // Atualizado com sucesso.
        }
    }
}
