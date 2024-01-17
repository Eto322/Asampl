using System.Text;
using BLL.ASAMPL.AbstractModel;
using BLL.ASAMPL.DataContainerModel;
using System.Linq;

namespace BLL.ASAMPL.KeyWordsModel
{
    public class AggregateKeyWordModel : AsamplKeyWordModel
    {
        public List<AggregateDataContainer> Aggregates { get; set; }

        public AggregateKeyWordModel()
        {
            Aggregates = new List<AggregateDataContainer>();
            KeyWordName = "AGGREGATES";
            Description = "Block that defines aggregates formed from defined tuples.";
        }

        // Додавання агрегату
        public bool AddAggregate(string aggregateName, List<TupleDataContainer> tuples)
        {
            var existingAggregate = Aggregates.FirstOrDefault(a => a.DataName == aggregateName);
            if (existingAggregate == null)
            {
                var newAggregate = new AggregateDataContainer(aggregateName);
                foreach (var tuple in tuples)
                {
                    newAggregate.AddTuple(tuple);
                }
                Aggregates.Add(newAggregate);
                return true;
            }
            else
            {
                // Логіка обробки випадку, коли кортеж вже існує в агрегаті
                foreach (var tuple in tuples)
                {
                    if (!existingAggregate.DataInfo.Any(t => t.DataName == tuple.DataName))
                    {
                        existingAggregate.AddTuple(tuple);
                    }
                    else
                    {
                        Console.WriteLine($"Tuple '{tuple.DataName}' already exists in aggregate '{aggregateName}'.");
                    }
                }
                return true;
            }
        }

        // Видалення агрегату
        public bool RemoveAggregate(string aggregateName)
        {
            return Aggregates.RemoveAll(a => a.DataName == aggregateName) > 0;
        }

        public string GetFormattedAggregates()
        {
            var formattedAggregates = new StringBuilder();
            formattedAggregates.AppendLine($"  {KeyWordName} {{");

            foreach (var aggregate in Aggregates)
            {
                var tuplesString = string.Join(", ", aggregate.DataInfo.Select(t => t.DataName));
                formattedAggregates.AppendLine($"    {aggregate.DataName} = [{tuplesString}];");
            }

            formattedAggregates.Append("}");

            return formattedAggregates.ToString();
        }

        public override List<AsamplKeyWordDataContainer> DataContainer
        {
            get
            {
                return Aggregates.ConvertAll(x => (AsamplKeyWordDataContainer)x);
            }

            set
            {
                Aggregates = value.ConvertAll(x => (AggregateDataContainer)x);
            }
        }

        public override void Execute()
        {
            foreach (var aggregate in Aggregates)
            {
                Console.WriteLine($"Aggregate: {aggregate.DataName}, Tuples: [{string.Join(", ", aggregate.DataInfo.Select(t => t.DataName))}]");
            }
        }

        public override string GetFormated()
        {
            return GetFormattedAggregates();
        }
    }
}