using BLL.ASAMPL.AbstractModel;
using BLL.ASAMPL.DataContainerModel;
using BLL.ASAMPL.KeyWordsModel;
using System.Text;

public class TupleKeyWordModel : AsamplKeyWordModel
{
    public List<TupleDataContainer> Tuples { get; set; } = null;

    public TupleKeyWordModel()
    {
        Tuples = new List<TupleDataContainer>();
        KeyWordName = "TUPLES";
        Description = "Block that defines tuples and their associations.";
    }

    // Додавання кортежу
    public bool AddTuple(string tupleName, List<SetDataContainer> associatedSets)
    {
        var existingTuple = Tuples.FirstOrDefault(t => t.DataName == tupleName);
        if (existingTuple == null)
        {
            var newTuple = new TupleDataContainer(tupleName);
            foreach (var set in associatedSets)
            {
                newTuple.AddSet(set.DataName, set.DataInfo);
            }

            Tuples.Add(newTuple);
            return true;
        }
        else
        {
            // Додавання нових множин до існуючого кортежу з перевіркою на унікальність
            foreach (var set in associatedSets)
            {
                if (!existingTuple.DataInfo.Any(s => s.DataName == set.DataName))
                {
                    existingTuple.AddSet(set.DataName, set.DataInfo);
                }
                else
                {
                    Console.WriteLine($"Set '{set.DataName}' already exists in tuple '{tupleName}'.");
                }
            }

            return true;
        }
    }

    // Видалення кортежу
    public bool RemoveTuple(string tupleName)
    {
        return Tuples.RemoveAll(t => t.DataName == tupleName) > 0;
    }

    public string GetFormattedTuples()
    {
        var formattedTuples = new StringBuilder();
        formattedTuples.AppendLine($"  {KeyWordName} {{");

        foreach (var tuple in Tuples)
        {
            formattedTuples.AppendLine($"    {tuple.DataName} = [{string.Join(", ", tuple.DataInfo.Select(s => s.DataName))}];");
        }

        formattedTuples.Append("}");

        return formattedTuples.ToString();
    }

    public override List<AsamplKeyWordDataContainer> DataContainer
    {
        get
        {
            return Tuples.ConvertAll(x => (AsamplKeyWordDataContainer)x);
        }
        set
        {
            Tuples = value.ConvertAll(x => (TupleDataContainer)x);
        }
    }

    public override void Execute()
    {
        foreach (var tuple in Tuples)
        {
            Console.WriteLine($"Tuple: {tuple.DataName}, Associated Sets: [{string.Join(", ", tuple.DataInfo.Select(s => s.DataName))}]");
        }
    }

    public override string GetFormated()
    {
        return GetFormattedTuples();
    }
}