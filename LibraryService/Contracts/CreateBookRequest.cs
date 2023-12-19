using System;
namespace LibraryService.Contracts
{
	public class CreateBookRequest
	{
        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime DateOfPublication { get; set; }
        
        public CreateBookRequest()
		{

		}
	}
}
