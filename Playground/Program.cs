// See https://aka.ms/new-console-template for more information
using LibraryService.DAL;

Console.WriteLine("Hello, World!");

var newList = new List<LibraryService.DAL.Book>();

for (var i = 0; i < 90000000; i++)
{
    newList.Add(new LibraryService.DAL.Book { Id = i, Title = $"book number {i}", Author = $"Author number {i}" });
}

Console.WriteLine("start " + DateTime.UtcNow);

Console.WriteLine(newList.Count);

var found = newList.Find(x => x.Id == 500034);

Console.WriteLine("end " + DateTime.UtcNow);

Console.ReadLine();