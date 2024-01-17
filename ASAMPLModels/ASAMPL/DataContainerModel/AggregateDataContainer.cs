using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.ASAMPL.AbstractModel;

namespace BLL.ASAMPL.DataContainerModel
{
    public class AggregateDataContainer : AsamplKeyWordDataContainer
    {
        public new List<TupleDataContainer> DataInfo { get; set; }

        //DataName-AggregateName
        //DataInfo-List that contains tuples which Aggregate contains.
        public AggregateDataContainer(string aggregateName)
        {
            DataName = aggregateName;
            DataInfo = new List<TupleDataContainer>();
        }

        public AggregateDataContainer()
        {
        }

        // Додавання кортежу до агрегату
        public void AddTuple(TupleDataContainer tuple)
        {
            DataInfo.Add(tuple);
        }

        // Отримання представлення даних агрегату
        public override string GetDataRepresentation()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append($"{DataName} = [");
            foreach (var tuple in DataInfo)
            {
                stringBuilder.Append($"    {tuple.DataName},");
            }
            stringBuilder.Append("];");
            return stringBuilder.ToString();
        }
    }
}