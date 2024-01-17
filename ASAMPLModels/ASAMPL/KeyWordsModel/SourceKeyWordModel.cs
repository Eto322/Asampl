using BLL.ASAMPL.AbstractModel;
using BLL.ASAMPL.DataContainerModel;
using System.Text;

public class SourceKeyWordModel : AsamplKeyWordModel
{
    public List<SourceDataContainer> Sources { get; set; }

    public SourceKeyWordModel()
    {
        Sources = new List<SourceDataContainer>();
        KeyWordName = "SOURCES";
        Description = "Block that defines sources for data.";
    }

    // Додавання джерела даних
    public bool AddSource(string sourceName, string path)
    {
        if (Sources.Exists(s => s.DataName == sourceName))
        {
            Console.WriteLine($"Source '{sourceName}' already exists.");
            return false;
        }

        Sources.Add(new SourceDataContainer(sourceName, path));
        return true;
    }

    // Видалення джерела даних
    public bool RemoveSource(string sourceName)
    {
        return Sources.RemoveAll(s => s.DataName == sourceName) > 0;
    }

    public string GetFormattedSources()
    {
        var formattedSources = new StringBuilder();
        formattedSources.AppendLine($"  {KeyWordName} {{");

        foreach (var source in Sources)
        {
            formattedSources.AppendLine(source.GetDataRepresentation());
        }

        formattedSources.Append("}");

        return formattedSources.ToString();
    }

    public override List<AsamplKeyWordDataContainer> DataContainer
    {
        get
        {
            return Sources.ConvertAll(x => (AsamplKeyWordDataContainer)x);
        }

        set
        {
            Sources = value.ConvertAll(x => (SourceDataContainer)x);
        }
    }

    public override void Execute()
    {
        foreach (var source in Sources)
        {
            Console.WriteLine($"Source: {source.DataName}, Path: {source.DataInfo}");
        }
    }

    public override string GetFormated()
    {
        return GetFormattedSources();
    }
}