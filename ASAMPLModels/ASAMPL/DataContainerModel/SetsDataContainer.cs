using BLL.ASAMPL.AbstractModel;
using BLL.ASAMPL.KeyWordsModel;
using System;
using System.Collections.Generic;

namespace BLL.ASAMPL.KeyWordsModel
{
    public class SetDataContainer : AsamplKeyWordDataContainer
    {
        private static readonly HashSet<string> ValidTypes = new HashSet<string>
        {
            "ATIME", "RTIME", // Абсолютний і відносний час
            "ADATE", "RDATE", // Абсолютна і відносна дата
            "INTEGER", // Ціле число
            "REAL", // Дійсне число
            "DOUBLE", // Дійсне число подвійної точності
            "FRAME", // Кадр
            "TEXT", // Текст
            "LINK", // Посилання або повне ім’я файлу
            "LOGIC", // Логічний тип
            "EVENT", // Подія
            "DBG"
        };

        public SetDataContainer()
        {
            /*AssociatedKeyWordModel = new SetsKeyWordModel();*/
            DataContext = "Type";
        }

        public bool IsChekedForView { get; set; } = false;

        public string DataForView
        {
            get { return GetDataForView(); }
        }

        //DataName-Name Of the Set
        //DataInfo-Set type avalible types contains in  ValidTypes

        public SetDataContainer(string setName, string setType)
        {
            if (!ValidTypes.Contains(setType.ToUpper()))
            {
                throw new ArgumentException($"Invalid type '{setType}'. Valid types are: {string.Join(", ", ValidTypes)}");
            }

            DataName = setName;
            DataInfo = setType;
            /* AssociatedKeyWordModel = new SetsKeyWordModel();*/
            DataContext = "Type";
        }

        public override string GetDataRepresentation()
        {
            return $"    {DataName} From {DataInfo};";
        }

        public string GetDataForView()
        {
            return $"{DataName} Is {DataInfo};";
        }
    }
}