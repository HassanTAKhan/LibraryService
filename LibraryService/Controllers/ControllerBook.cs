using System;
namespace LibraryService.Controllers
{
	public class ControllerBook
	{
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime DateOfPublication { get; set; }

        public ControllerBook()
		{
		}
	}
}

