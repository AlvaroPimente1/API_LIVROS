using BookAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace BookAPI.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly BookContext _context;

        public BookRepository(BookContext context)
        {
            _context = context;
        }

        //Criar
        public async Task<Book> Create(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return book;
        }

        //Deletar
        public async Task Delete(int id)
        {
            var bookToDelete = await _context.Books.FindAsync(id);
            if(bookToDelete == null)
            {
                throw new ArgumentException($"Não existe livro com o id{id}", nameof(id));
            }
            _context.Books.Remove(bookToDelete);
            await _context.SaveChangesAsync();
        }

        //Consultar
        public async Task<IEnumerable<Book>> Get()
        {
            return await _context.Books.ToListAsync();
        }

        //Consultar por ID
        public async Task<Book> Get(int id)
        {
            var bookToShow = await _context.Books.FindAsync(id);
            if (bookToShow == null)
            {
                throw new ArgumentException($"Não existe livro com o id{id}", nameof(id));
            }
            return bookToShow;
        }

        //Atualizar
        public async Task Update(Book book)
        {
            _context.Entry(book).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
