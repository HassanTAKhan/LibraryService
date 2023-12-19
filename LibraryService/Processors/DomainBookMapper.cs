using System;
using LibraryService.Contracts;
using LibraryService.DAL.Contracts;

namespace LibraryService.Processors
{
	public class DomainBookMapper: IDomainBookMapper
	{
        public DomainBook Map(BookData BookData) //mapping function
        {
            return new DomainBook
            {
                Author = BookData.Author,
                DateOfPublication = BookData.DateOfPublication,
                Id = BookData.Id,
                Title = BookData.Title
            };
        }

        public List<DomainBook> Map(List<KeyValuePair<int, BookData>> allDomainbooks)
        {
            List<DomainBook> domainBooks = new List<DomainBook>(); //creating a new DomainBook object

            foreach (var kvp in allDomainbooks)
            {
                // Create a new DomainBook object for each KeyValuePair
                DomainBook domainBook = Map(kvp.Value); // Reuse the existing mapping logic

                domainBooks.Add(domainBook);
            }

            return domainBooks;
        }

        public BookData Map(CreateBookRequest BookData)
        {
            return new BookData
            {
                Author = BookData.Author,
                DateOfPublication = BookData.DateOfPublication,
                Id = MockData.Books.Keys.Max() + 1,
                Title = BookData.Title,
                createdAt = DateTime.Now.ToString(), 
            };
        }
    }
}
//transform one collectiontype to another collectiontype
//linq -> library that lets you manipulate collections in .net

// .Select(kvp => Map(kvp.Value)) => Select is essentially similar to a map function in javascriot
// can be used to get rid of the forEach function


