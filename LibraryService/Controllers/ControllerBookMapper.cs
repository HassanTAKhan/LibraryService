using System;
using LibraryService.Controllers;
using LibraryService.DAL.Contracts;

namespace LibraryService.Processors
{
    public class ControllerBookMapper : IControllerBookMapper
    {
        public ControllerBook Map(DomainBook BookData)
        {
            return new ControllerBook
            {
                Author = BookData.Author,
                DateOfPublication = BookData.DateOfPublication,
                Id = BookData.Id,
                Title = BookData.Title
            };
        }
    }
}

