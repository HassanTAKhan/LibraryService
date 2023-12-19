using System;
using LibraryService.Contracts;
using LibraryService.DAL.Contracts;

namespace LibraryService.Processors
{
	public interface IDomainBookMapper
	{
		DomainBook Map(BookData BookData);
        List<DomainBook> Map(List<KeyValuePair<int, BookData>> allDomainbooks);
        BookData Map(CreateBookRequest BookData);
    }
}

