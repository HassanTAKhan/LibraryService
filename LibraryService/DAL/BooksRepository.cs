using System;
using System.Diagnostics;
using System.Net;
using LibraryService.Controllers;
using LibraryService.DAL;
using LibraryService.DAL.Contracts;
using LibraryService.Processors;
using Microsoft.Extensions.Logging;
using static System.Reflection.Metadata.BlobBuilder;
namespace LibraryService.DAL //investigate different collection types in .net ---> Lists/Dictionary/Hash tables
{
    public interface IBookRepository
    {
        BookData GetById(string bookId);
        List<KeyValuePair<int, BookData>> GetAllBooks();
        bool DeleteBookById(string bookId);
        BookData AddBook(BookData bookdata);
    }



    public class BookRepository : IBookRepository
    {
        public List<KeyValuePair<int, BookData>> GetAllBooks()
        {
            return MockData.Books.ToList();
        }

        public BookData GetById(string bookId)
        {
            if (MockData.Books.TryGetValue(Int32.Parse(bookId), out var bookValue))
            {
                return bookValue;

            }
            return null;
        }
        public bool DeleteBookById(string bookId)
        {
            Debug.WriteLine("test");

            if (MockData.Books.TryGetValue(int.Parse(bookId), out var book))
            {
                MockData.Books.Remove(int.Parse(bookId));
                Debug.WriteLine($"Removed Book: {book}");
                return true;
            }

            return false;
        }
        public BookData AddBook(BookData bookData)
        {
            try
            {
                // Find the next available key for the new book
                int newKey = MockData.Books.Keys.Max() + 1;

                // Add the book with the new key
                MockData.Books.Add(newKey, bookData);

                Debug.WriteLine($"Added Book: {bookData}");

                return bookData; // Book added successfully
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                Debug.WriteLine($"Error adding book: {ex.Message}");
                return new BookData(); // Book not added successfully
            }
        }
    }
}


public static class MockData // static class allows you to access values (as long as they are public) without instantiating the class.
{
    public static Dictionary<int, BookData> Books = new Dictionary<int, BookData> // use dictionary -> <what youre searching by, what you expect to return>
    {
        { 1, new BookData { Id = 1, Title = "The Catcher in the Rye", Author = "J.D. Salinger", DateOfPublication = new DateTime(1951, 7, 16) } },
        { 2, new BookData { Id = 2, Title = "To Kill a Mockingbird", Author = "Harper Lee", DateOfPublication = new DateTime(1960, 7, 11) } },
        { 3, new BookData { Id = 3, Title = "1984", Author = "George Orwell", DateOfPublication = new DateTime(1949, 6, 8) } },
        { 4, new BookData { Id = 4, Title = "The Great Gatsby", Author = "F. Scott Fitzgerald", DateOfPublication = new DateTime(1925, 4, 10) } },
        { 5, new BookData { Id = 5, Title = "Pride and Prejudice", Author = "Jane Austen", DateOfPublication = new DateTime(1813, 1, 28) } }
    };
}