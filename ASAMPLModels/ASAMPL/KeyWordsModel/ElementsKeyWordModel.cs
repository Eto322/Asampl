using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLL.ASAMPL.AbstractModel;
using BLL.ASAMPL.DataContainerModel;

namespace BLL.ASAMPL.KeyWordsModel
{
    public class ElementsKeyWordModel : AsamplKeyWordModel
    {
        public List<ElementsDataContainer> Elements { get; set; }

        public ElementsKeyWordModel()
        {
            Elements = new List<ElementsDataContainer>();
            KeyWordName = "ELEMENTS";
            Description = "Block that defines elements and their types or values.";
        }

        // Додавання елемента
        public bool AddElement(string elementName, SetDataContainer set)
        {
            if (!Elements.Any(e => e.DataName == elementName))
            {
                Elements.Add(new ElementsDataContainer(elementName, set));
                return true;
            }
            else
            {
                Console.WriteLine($"Element '{elementName}' already exists.");
                return false;
            }
        }

        public bool AddElement(string elementName, string elementValue)
        {
            if (!Elements.Any(e => e.DataName == elementName))
            {
                Elements.Add(new ElementsDataContainer(elementName, Elements.Last().AssociadetSet, elementValue));
                return true;
            }
            else
            {
                Console.WriteLine($"Element '{elementName}' already exists.");
                return false;
            }
        }

        public bool AddElement(string elementName, SetDataContainer set, string elementValue)
        {
            if (!Elements.Any(e => e.DataName == elementName))
            {
                Elements.Add(new ElementsDataContainer(elementName, set, elementValue));
                return true;
            }
            else
            {
                Console.WriteLine($"Element '{elementName}' already exists.");
                return false;
            }
        }

        // Видалення елемента
        public bool RemoveElement(string elementName)
        {
            return Elements.RemoveAll(e => e.DataName == elementName) > 0;
        }

        public string GetFormattedElements()
        {
            var formattedElements = new StringBuilder();
            formattedElements.AppendLine($"  {KeyWordName} {{");

            foreach (var element in Elements)
            {
                formattedElements.AppendLine($"    {element.GetDataRepresentation()};");
            }

            formattedElements.Append("}");

            return formattedElements.ToString();
        }

        public override List<AsamplKeyWordDataContainer> DataContainer
        {
            get
            {
                return Elements.ConvertAll(x => (AsamplKeyWordDataContainer)x);
            }

            set
            {
                Elements = value.ConvertAll(x => (ElementsDataContainer)x);
            }
        }

        public override void Execute()
        {
            foreach (var element in Elements)
            {
                Console.WriteLine($"Element: {element.DataName}, Type: {element.ElementType}, Value: {element.DataInfo}");
            }
        }

        public override string GetFormated()
        {
            return GetFormattedElements();
        }
    }
}