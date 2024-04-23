using BookApp.Context;
using BookApp.DTOs.BookDto;
using BookApp.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly AppDbContext _context;
        public BooksController(AppDbContext context) 
        {
            _context = context;
        }

        [HttpPost("")]
        public async Task<IActionResult> Create(BookPostDto bookPostDto) 
        {
            var newBook = new Book()
            {
                Name = bookPostDto.Name,
                SalePrice = bookPostDto.SalePrice,
                CostPrice = bookPostDto.CostPrice,
                ImageUrl = bookPostDto.ImageUrl,
            };
            await _context.Books.AddAsync(newBook);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllAsync(BookGetDto bookGetDto)  
            => Ok(await _context.Books.ToListAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(BookGetDto bookGetDto)
        {
            var newBook = new Book()
            {
                Id = bookGetDto.Id,
            };
            await _context.Books.FindAsync(newBook);
            return Ok();
        }

        [HttpPut("{action}/{id}")]
        public async Task<IActionResult> Update (int id ,BookPutDto bookPutDto)
        {

            try
            {
                var book = await _context.Books.FindAsync(id);

                if (book == null)
                {
                    return NotFound();
                }

                book.Name = bookPutDto.Name;
                book.SalePrice = bookPutDto.SalePrice;
                book.CostPrice = bookPutDto.CostPrice;
                book.IsDeleted = bookPutDto.IsDeleted;
                book.UpdatedDate = DateTime.Now;

                _context.Books.Update(book);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(404, $"Error: {ex.Message}");
            }
        }

        [HttpDelete("")]
        public async Task<IActionResult> Delete(BookDeleteDto bookDeleteDto) 
        {
            var newBook = new Book()
            {
                Id = bookDeleteDto.Id,
            };
            _context.Books.Remove(newBook);
            await _context.SaveChangesAsync();
            return Ok();
        }

    }
}
