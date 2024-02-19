using Xunit;
using Moq;
using System;
using System.Collections.Generic;
using LibraryService.DAL;
using LibraryService.DAL.Contracts;
using LibraryService.Processors;
using LibraryService.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LibraryServiceTests
{
    public class LibraryProcessorTests
    {
        [Fact]
        public void GetAll_GivenBookExists_ShouldReturnListOfDomainBooks()
        {
            var mockedBookRepository = new Mock<IBookRepository>();
            var mockedDomainBookMapper = new Mock<IDomainBookMapper>();

            var bookDataList = new List<KeyValuePair<int, BookData>>
            {
                new KeyValuePair<int, BookData>(1, new BookData
                {
                    Title = "Harry Potter and The Half Blood Prince",
                    Author = "J.K. Rowling",
                    DateOfPublication = new DateTime(2005, 7, 16),
                    createdAt = "created_at_datetime",
                    Id = 1
                }),
                new KeyValuePair<int, BookData>(2, new BookData
                {
                    Title = "To Kill a Mockingbird",
                    Author = "Harper Lee",
                    DateOfPublication = new DateTime(1960, 7, 11),
                    createdAt = "created_at_datetime",
                    Id = 2
                }),
                new KeyValuePair<int, BookData>(3, new BookData
                {
                    Title = "1984",
                    Author = "George Orwell",
                    DateOfPublication = new DateTime(1949, 6, 8),
                    createdAt = "created_at_datetime",
                    Id = 3
                }),
            };

            mockedBookRepository.Setup(x => x.GetAllBooks()).Returns(bookDataList);

            var libraryProcessor = new LibraryProcessor(mockedBookRepository.Object, mockedDomainBookMapper.Object);

            var domainBooks = libraryProcessor.GetAllBooks();

            Assert.NotNull(domainBooks);
            Assert.Equal(3, domainBooks.Count());
        }

        //[Fact]
        //public void GetBookById_GivenBookExists_shouldReturnBook()
        //{
        //    // Arrange
        //    var mockedBookRepository = new Mock<IBookRepository>();
        //    var mockedDomainBookMapper = new Mock<IDomainBookMapper>();

        //    var libraryProcessor = new LibraryProcessor(mockedBookRepository.Object, mockedDomainBookMapper.Object);

        //    // Setup mockedBookRepository to return a specific BookData object when GetById is called with "1"
        //    mockedBookRepository.Setup(x => x.GetById("1")).Returns(new BookData
        //    {
        //        Title = "Harry Potter and The Half Blood Prince",
        //        Author = "jk rowling",
        //        DateOfPublication = new DateTime(2005, 7, 16),
        //        createdAt = "created_at_datetime",
        //        Id = 1
        //    });

        //    // Act
        //    var result = libraryProcessor.GetById("1");

        //    // Assert
        //    Assert.Equal("jk rowling", result.Author);
        //}

        [Fact]
        public void DeleteById_GivenBookExists_ShouldDeleteBook()
        {
            var mockedBookRepository = new Mock<IBookRepository>();
            var mockedDomainBookMapper = new Mock<IDomainBookMapper>();

            var bookDataList = new List<KeyValuePair<int, BookData>>
            {
                new KeyValuePair<int, BookData>(1, new BookData
                {
                    Title = "Harry Potter and The Half Blood Prince",
                    Author = "J.K. Rowling",
                    DateOfPublication = new DateTime(2005, 7, 16),
                    createdAt = "created_at_datetime",
                    Id = 1
                }),
                new KeyValuePair<int, BookData>(2, new BookData
                {
                    Title = "To Kill a Mockingbird",
                    Author = "Harper Lee",
                    DateOfPublication = new DateTime(1960, 7, 11),
                    createdAt = "created_at_datetime",
                    Id = 2
                }),
            };

            mockedBookRepository.Setup(x => x.DeleteBookById("1")).Returns(true);

            var libraryProcessor = new LibraryProcessor(mockedBookRepository.Object, mockedDomainBookMapper.Object);

            var result = libraryProcessor.DeleteBookById("1");

            Assert.True(result);
        }

        [Fact]
        public void AddBook_GivenBookIsAdded_ShouldReturnAddedBook()
        {
            // Arrange
            var mockedBookRepository = new Mock<IBookRepository>();
            var mockedDomainBookMapper = new Mock<IDomainBookMapper>();

            var newBook = new LibraryService.Contracts.CreateBookRequest
            {
                Title = "To Kill a Mockingbird",
                Author = "Harper Lee",
                DateOfPublication = new DateTime(2019, 1, 6),
            };

            var addedBookData = new BookData
            {
                Title = "To Kill a Mockingbird",
                Author = "Harper Lee",
                DateOfPublication = new DateTime(2019, 1, 6),
                Id = 2
            };

            mockedBookRepository.Setup(x => x.AddBook(It.IsAny<BookData>())).Returns(addedBookData);

            var libraryProcessor = new LibraryProcessor(mockedBookRepository.Object, mockedDomainBookMapper.Object);

            var result = libraryProcessor.AddBook(newBook);

            Assert.NotNull(result); 
            Assert.Equal(addedBookData, result);
        }
    }

}