using LibraryService.Controllers;
using LibraryService.DAL.Contracts;
using LibraryService.Processors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace LibraryServiceTests;

public class LibraryControllerTests
{
    [Fact]
    public void GetAll_GivenBookExists_ShouldReturnOkResponse()
    {
        //controller unit tests usually test status codes and different response types (500/404)
        var mockedLogger = new Mock<ILogger<LibraryController>>();
        var mockedLibraryProcessor = new Mock<ILibraryProcessor>();

        var domainBookList = new List<DomainBook>
        {
            new DomainBook
            {
                Title = "Harry Potter and The Half Blood Prince",
                Author = "J.K. Rowling",
                DateOfPublication = new DateTime(2005, 7, 16),
                Id = 1
            },
            new DomainBook
            {
                Title = "To Kill a Mockingbird",
                Author = "Harper Lee",
                DateOfPublication = new DateTime(1960, 7, 11),
                Id = 2
            },
        };
 
        mockedLibraryProcessor.Setup(x => x.GetAllBooks()).Returns(domainBookList);

        var controller = new LibraryController(mockedLogger.Object, mockedLibraryProcessor.Object);

        OkObjectResult result = (OkObjectResult)controller.getAll();

        var booksList = (List<DomainBook>?)result.Value;

        mockedLibraryProcessor.Verify(x => x.GetAllBooks(), Times.Once);

        Assert.Equal(2, booksList?.Count);
        Assert.Equal(domainBookList[0].Author, booksList?.First().Author);
        Assert.Equal(domainBookList[1].Author, booksList?.Skip(1).First().Author);
    }

    [Fact]
    public void GetBookById_GivenBookExists_shouldReturnBook()
    {
        var mockedLogger = new Mock<ILogger<LibraryController>>();
        var mockedLibraryProcessor = new Mock<ILibraryProcessor>();

        var domainBook = new DomainBook
        {
            Title = "Harry Potter and The Half Blood Prince",
            Author = "jk rowling",
            DateOfPublication = new DateTime(2005, 7, 16), // Example date
            Id = 1
        };

        mockedLibraryProcessor.Setup(x => x.GetById("1")).Returns(domainBook);

        var controller = new LibraryController(mockedLogger.Object, mockedLibraryProcessor.Object);

        IActionResult actionResult = controller.Convert(1);
        var result = actionResult as OkObjectResult;

        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);

        var book = result.Value as DomainBook;
        Assert.NotNull(book);
        Assert.Equal(domainBook.Author, book.Author);
    }


    [Fact]
    public void GetBookById_GivenBookDoesNotExist_shouldReturnNotFound()
    {
        var mockedLogger = new Mock<ILogger<LibraryController>>();
        var mockedLibraryProcessor = new Mock<ILibraryProcessor>();

        mockedLibraryProcessor.Setup(x => x.GetById("2")).Returns<DomainBook>(null);

        var controller = new LibraryController(mockedLogger.Object, mockedLibraryProcessor.Object);

        IActionResult actionResult = controller.Convert(2);
        var result = actionResult as NotFoundResult;

        Assert.NotNull(result);
        Assert.Equal(404, result.StatusCode);
    }

    [Fact]
    public void AddBook_GivenBookIsAdded_ShouldReturnAddedBook()
    {
        // Arrange
        var mockedLogger = new Mock<ILogger<LibraryController>>();
        var mockedLibraryProcessor = new Mock<ILibraryProcessor>();

        var newBook = new LibraryService.Contracts.CreateBookRequest
        {
            Title = "To Kill a Mockingbird",
            Author = "Harper Lee",
            DateOfPublication = new DateTime(2019, 1, 6),
        };

        var addedBookData = new BookData
        {
            Title = newBook.Title,
            Author = newBook.Author,
            DateOfPublication = newBook.DateOfPublication,
            Id = 2
        };

        mockedLibraryProcessor.Setup(x => x.AddBook(newBook)).Returns(addedBookData);

        var controller = new LibraryController(mockedLogger.Object, mockedLibraryProcessor.Object);

        IActionResult actionResult = controller.AddBook(newBook);
        var result = actionResult as OkObjectResult;

        var returnedBook = result?.Value as BookData;
        Assert.Equal(200, result?.StatusCode);

        Assert.Equal(addedBookData.Id, returnedBook?.Id);
        Assert.Equal(newBook.Author, returnedBook?.Author);
        Assert.Equal(newBook.Title, returnedBook?.Title);
    }

    [Fact]
    public void DeleteBookById_GivenIdProvided_ShouldDeleteBook()
    {
        var mockedLogger = new Mock<ILogger<LibraryController>>();
        var mockedLibraryProcessor = new Mock<ILibraryProcessor>();

        var domainBookList = new List<DomainBook>
        {
            new DomainBook
            {
                Title = "Harry Potter and The Half Blood Prince",
                Author = "J.K. Rowling",
                DateOfPublication = new DateTime(2005, 7, 16),
                Id = 1
            },
            new DomainBook
            {
                Title = "To Kill a Mockingbird",
                Author = "Harper Lee",
                DateOfPublication = new DateTime(1960, 7, 11),
                Id = 2
            },
        };

        string responseMessage = "Book deleted";

        mockedLibraryProcessor.Setup(x => x.DeleteBookById("1")).Returns(true);

        var controller = new LibraryController(mockedLogger.Object, mockedLibraryProcessor.Object);

        IActionResult actionResult = controller.DeleteBookById(1);
        var result = actionResult as OkObjectResult;

        var res = result?.Value as string;

        Assert.Equal(200, result?.StatusCode);
        Assert.Equal(responseMessage, res);
    }

}
