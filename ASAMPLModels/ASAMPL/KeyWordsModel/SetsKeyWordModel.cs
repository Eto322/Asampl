using BLL.ASAMPL.AbstractModel;
using BLL.ASAMPL.KeyWordsModel;
using System.Text;

public class SetsKeyWordModel : AsamplKeyWordModel
{
    public List<SetDataContainer> Sets { get; set; }

    public SetsKeyWordModel()
    {
        Sets = new List<SetDataContainer>();
        KeyWordName = "SETS";
        Description = "Block that defines sets.";
    }

    // Додавання множини
    public bool AddSet(string setName, string setType)
    {
        if (Sets.Exists(s => s.DataName == setName))
        {
            Console.WriteLine($"Set '{setName}' already exists.");
            return false;
        }

        Sets.Add(new SetDataContainer(setName, setType));
        return true;
    }

    // Видалення множини
    public bool RemoveSet(string setName)
    {
        return Sets.RemoveAll(s => s.DataName == setName) > 0;
    }

    public string GetFormattedSets()
    {
        var formattedSets = new StringBuilder();
        formattedSets.AppendLine($"  {KeyWordName} {{");

        foreach (var set in Sets)
        {
            formattedSets.AppendLine(set.GetDataRepresentation());
        }

        formattedSets.Append("}");

        return formattedSets.ToString();
    }

    public override List<AsamplKeyWordDataContainer> DataContainer
    {
        get
        {
            return Sets.ConvertAll(x => (AsamplKeyWordDataContainer)x);
        }

        set
        {
            Sets = Sets.ConvertAll(x => (SetDataContainer)x);
        }
    }

    public override void Execute()
    {
        foreach (var set in Sets)
        {
            Console.WriteLine($"Set: {set.DataName}, Type: {set.DataInfo}");
        }
    }

    public override string GetFormated()
    {
        return GetFormattedSets();
    }
}