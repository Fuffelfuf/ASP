using System.Text.Json;
var builder = WebApplication.CreateBuilder();
var app = builder.Build();

app.Map("/library", () => LibraryHandler());

app.Map("/Library/Books", () => BooksHandler());

app.Map("/Library/Profile/{id?}", (string? id) => ProfileHandler(id));

app.Run();

string LibraryHandler()
{
    return "Welcome to the Library!";
}
 
string BooksHandler()
{
    return ReadBooks();
}

string ProfileHandler(string id)
{
    return ReadProfiles(int.Parse(id));
}

string ReadBooks()
{
    string filePath = "Books.json";
    string jsonContent = File.ReadAllText(filePath);
    var books = JsonSerializer.Deserialize<Book[]>(jsonContent);

    if (books != null && books.Length > 0)
    {
        var Books = books.Select(book => $"Book:{book.Title} by {book.Author}, published in {book.Year}");
        return string.Join("\n", Books);
    }
    else
    {
        return "No books found in the configuration file.";
    }
}
Profile user = new Profile
{
    Id = 0,
    Gender = "Male",
    Name = "User",
    Age = 21
};
string ReadProfiles(int id)
{
    if (id != null)
    {
        string filePath = "Profiles.json";
        string jsonContent = File.ReadAllText(filePath);
        var profiles = JsonSerializer.Deserialize<Profile[]>(jsonContent);

        if (profiles != null && profiles.Length > 0)
        {
            var profile = profiles.FirstOrDefault(profile => profile.Id == id);
            if (profile != null)
            {
                return $"Id:{profile.Id} Gender:{profile.Gender} Name:{profile.Name} Age:{profile.Age}";
            }
            else
            {
                return "No profile with such id found in the configuration file.";
            }
        }
        else
        {
            return "No profiles found in the configuration file.";
        }
    }
    else { return $"Id:{user.Id} Gender:{user.Gender} Name:{user.Name} Age:{user.Age}"; }
}
public class Book
{
    public string Title { get; set; }
    public string Author { get; set; }
    public int Year { get; set; }
}

public class Profile
{
    public int Id{ get; set; }
    public string Gender { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
}
