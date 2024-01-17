using BLL.ASAMPL.AbstractModel;
using BLL.ASAMPL.KeyWordsModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ASAMPL.DataContainerModel
{
    public class TupleDataContainer : AsamplKeyWordDataContainer
    {
        public new List<SetDataContainer>? DataInfo { get; set; }

        //DataName-tupleName
        //DataInfo-List of sets which tuple contains
        public TupleDataContainer(string tupleName)
        {
            DataName = tupleName;
            DataInfo = new List<SetDataContainer>();
            /*AssociatedKeyWordModel = new TupleKeyWordModel();*/
            DataContext = "set";
        }

        public bool IsChekedForView { get; set; }

        public string DataForView
        {
            get { return GetDataForView(); }
        }

        private string GetDataForView()
        {
            return ($"Tuple: {DataName}, Associated Sets: [{string.Join(", ", DataInfo.Select(s => s.DataName))}]");
        }

        public TupleDataContainer()
        {
            /*AssociatedKeyWordModel = new TupleKeyWordModel();*/
            DataContext = "set";
        }

        // Додавання множини до кортежу
        public void AddSet(string setName, string setType)
        {
            if (DataInfo != null)
            {
                DataInfo.Add(new SetDataContainer(setName, setType));
            }
            else
            {
                DataInfo = new List<SetDataContainer>();
                DataInfo.Add(new SetDataContainer(setName, setType));
            }
        }

        // Додавання множини до кортежу
        public void AddSet(SetDataContainer setDataContainer)
        {
            if (DataInfo != null)
            {
                DataInfo.Add(setDataContainer);
            }
            else
            {
                DataInfo = new List<SetDataContainer> { setDataContainer };
            }
        }

        // Отримання представлення даних кортежу
        public override string GetDataRepresentation()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"{DataName} = {DataInfo[0].DataName};");

            return stringBuilder.ToString();
        }
    }
}