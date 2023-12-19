using System;
namespace LibraryService.DAL.Contracts
{
	public class BookData //close to database layer - 
	{
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string createdAt { get; set; }
        public DateTime DateOfPublication { get; set; }

        public BookData()
		{
		}
	}
}

