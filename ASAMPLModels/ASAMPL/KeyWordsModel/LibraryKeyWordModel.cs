using BLL.ASAMPL.AbstractModel;
using BLL.ASAMPL.DataContainerModel;
using System.Diagnostics;
using System.Text;

public class LibraryKeyWordModel : AsamplKeyWordModel
{
    public List<LibraryDataContainer> Libraries { get; set; }
    private bool IsDebug = true;

    public LibraryKeyWordModel()
    {
        Libraries = new List<LibraryDataContainer>();
        KeyWordName = "LIBRARIES";
        Description = "Block that defines built-in and external libraries.";
    }

    // Додавання бібліотеки
    public bool AddLibrary(string libraryName, string path)
    {
        if (Libraries.Exists(l => l.DataName == libraryName))
        {
            Console.WriteLine($"Library with key '{libraryName}' already exists.");
            return false;
        }

        if (!File.Exists(path) && IsDebug == false)
        {
            Console.WriteLine($"Library file not found: {path}");
            return false;
        }

        Libraries.Add(new LibraryDataContainer(libraryName, path));
        return true;
    }

    // Видалення бібліотеки
    public bool RemoveLibrary(string libraryName)
    {
        return Libraries.RemoveAll(l => l.DataName == libraryName) > 0;
    }

    public string GetFormattedLibraries()
    {
        var formattedLibraries = new StringBuilder();
        formattedLibraries.AppendLine($"  {KeyWordName} {{");

        foreach (var library in Libraries)
        {
            formattedLibraries.AppendLine(library.GetDataRepresentation());
        }

        formattedLibraries.Append("}");

        return formattedLibraries.ToString();
    }

    public override List<AsamplKeyWordDataContainer> DataContainer
    {
        get
        {
            return Libraries.ConvertAll(x => (AsamplKeyWordDataContainer)x);
        }

        set
        {
            Libraries = value.ConvertAll(x => (LibraryDataContainer)x);
        }
    }

    public override void Execute()
    {
        foreach (var library in Libraries)
        {
            Console.WriteLine($"Library: {library.DataName}, Path: {library.DataInfo}");
        }
    }

    public override string GetFormated()
    {
        return GetFormattedLibraries();
    }
}