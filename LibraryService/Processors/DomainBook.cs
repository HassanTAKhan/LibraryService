using System;
namespace LibraryService.Processors
{
	public class DomainBook
	{
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime DateOfPublication { get; set; }

        public DomainBook()
		{
		}
    }
}

