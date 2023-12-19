using System;
using System.Net;
using LibraryService.DAL;
using System.Collections.Generic; // Import the namespace for List
using LibraryService.DAL.Contracts;
using Newtonsoft.Json;
using System.Diagnostics;
using LibraryService.Contracts;

namespace LibraryService.Processors
{
    public interface ILibraryProcessor
    {
        DomainBook GetById(string bookId);
        IEnumerable<DomainBook> GetAllBooks();
        Boolean DeleteBookById(string bookId);
        BookData AddBook(CreateBookRequest bookData);
    }

    public class LibraryProcessor : ILibraryProcessor
    {
        private readonly IBookRepository _bookRepository;
        private readonly IDomainBookMapper _domainBookMapper;
        
        public LibraryProcessor(
             IBookRepository bookRepository,
             IDomainBookMapper domainBookMapper
            )
        {
            _bookRepository = bookRepository;
            _domainBookMapper = domainBookMapper;
        }

        public DomainBook GetById(string bookId) // can only be async when there is a db connected 
        {
            var domainBook = _bookRepository.GetById(bookId);

            if(domainBook != null)
            {
                return _domainBookMapper.Map(domainBook);
            }

            return null;
        }

        public IEnumerable<DomainBook> GetAllBooks()
        {
            var allDomainbooks = _bookRepository.GetAllBooks();
            return allDomainbooks.Select(kvp => _domainBookMapper.Map(kvp.Value));
        }

        public Boolean DeleteBookById(string bookId)
        {
            _bookRepository.DeleteBookById(bookId);
            return true;
        }
        public BookData AddBook(CreateBookRequest createBookRequest)
        {
            var domainBook = _domainBookMapper.Map(createBookRequest);
            return _bookRepository.AddBook(domainBook);
        }
    }
}

// for the next session => look at what response do we want to return in each scenario
// from the postman requests (when it can find book and when it cant)
// i.e when the book successfully created when it is not


// results of the above:
// When we can find a book we should return the whole book (if searching by ID)
// if we cannot find a book we should return an object with an error message and name attributes (for the GET byId request and also the PUT for updating )
// the status code that should be returned in this case is a 204 (resource not found) - same for whenever the request is processed but the resource has not been found
// If there is an error that is caught then a 500 should be returned






// check mapping data between different layers 
// DAL => layer where you fetch data => where you get the data from
// @ trianline we fetch data from a DB or by calling another microservice
// domain or business logic layer is where you manipulate data where a human has formatted it (in this case a processor)
// in the domain you would want to transform the raw data from the DB to another object that is more readable

// domain driven development
//3 diff layers
// application layer =>  controller (no business logic)
// domain layer / business => processor (all business logic here)
// DAL => where DB is connected OR requests to other microservices are made (raw data here)

