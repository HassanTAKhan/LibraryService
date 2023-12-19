using System.Diagnostics;
using System.Xml;
using LibraryService.DAL.Contracts;
using LibraryService.Processors;
using Microsoft.AspNetCore.Mvc;
using PricingService.Host.Contracts;
using Newtonsoft.Json;
using LibraryService.Contracts;

namespace LibraryService.Controllers;


[ApiController]
[Route("books")]
public class LibraryController: ControllerBase
{
    private readonly ILogger<LibraryController> _logger;
    private readonly ILibraryProcessor _libraryProcessor;
    private readonly IControllerBookMapper _controllerBookMapper;


    public LibraryController(
        ILogger<LibraryController> logger,
        ILibraryProcessor libraryProcessor
        )
	{
        _logger = logger;
        _libraryProcessor = libraryProcessor;
    }
    [HttpGet("{id}")]
    public IActionResult Convert([FromRoute] int id)
    {
        _logger.LogInformation("Book Id", id);

        var processedBook = _libraryProcessor.GetById(id.ToString());

        if(processedBook != null)
        {
            var controllerBook = _controllerBookMapper.Map(processedBook);
            return Ok(controllerBook);
        }
        else
        {
            return NotFound();
        }
    }
    [HttpGet]
    public IActionResult getAll()
    {
        var allBooks = _libraryProcessor.GetAllBooks();
        //// return a status code such as .OK()
        return Ok(allBooks);
    }
    [HttpDelete("{id}")]
    public IActionResult DeleteBookById([FromRoute] int id)
    {
        Debug.WriteLine("test", JsonConvert.SerializeObject(id, Newtonsoft.Json.Formatting.Indented));

        var deleted = _libraryProcessor.DeleteBookById(id.ToString());
        if(deleted == true)
        {
            return Ok("Book deleted");
        } else
        {
            return NotFound("Book not found");
        }

    }
    [HttpPost]
    public IActionResult AddBook([FromBody] CreateBookRequest bookData)
    {
        // Your logic to add the book
        BookData addedBook =  _libraryProcessor.AddBook(bookData);
        
        if (addedBook != null)
        {
            return Ok(addedBook); // Successfully added, return the added book
        }

        return Ok(new BookData()); // Successfully added, return the added book
    }
}


//take a look at restful API's in .net
