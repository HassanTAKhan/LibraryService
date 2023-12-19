using System;
using LibraryService.Controllers;
using LibraryService.DAL.Contracts;

namespace LibraryService.Processors
{
    public interface IControllerBookMapper
    {
        ControllerBook Map(DomainBook BookData);
    }
}

