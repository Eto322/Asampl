using System.Text;
using BLL.ASAMPL.AbstractModel;
using System.Collections.Generic;
using BLL.ASAMPL.DataContainerModel;

namespace BLL.ASAMPL.KeyWordsModel
{
    public class HandlerKeyWordModel : AsamplKeyWordModel
    {
        public List<HandlerDataContainer> Handlers { get; set; }
        private bool IsDebug = true;

        public HandlerKeyWordModel()
        {
            Handlers = new List<HandlerDataContainer>();
            KeyWordName = "HANDLERS";
            Description = "Block that defines handlers for data transformation.";
        }

        // Додавання обробника
        public bool AddHandler(string handlerName, string source)
        {
            if (Handlers.Exists(h => h.DataName == handlerName))
            {
                Console.WriteLine($"Handler with key '{handlerName}' already exists.");
                return false;
            }

            if (!File.Exists(source + "\\" + handlerName) && IsDebug == false)
            {
                Console.WriteLine($"Handler file not found: {source + handlerName}");
                return false;
            }

            Handlers.Add(new HandlerDataContainer(handlerName, source));
            return true;
        }

        // Видалення обробника
        public bool RemoveHandler(string handlerName)
        {
            return Handlers.RemoveAll(h => h.DataName == handlerName) > 0;
        }

        public string GetFormattedHandlers()
        {
            var formattedHandlers = new StringBuilder();
            formattedHandlers.AppendLine($"  {KeyWordName} {{");

            foreach (var handler in Handlers)
            {
                formattedHandlers.AppendLine(handler.GetDataRepresentation());
            }

            formattedHandlers.Append("}");

            return formattedHandlers.ToString();
        }

        public override List<AsamplKeyWordDataContainer> DataContainer
        {
            get
            {
                return Handlers.ConvertAll(x => (AsamplKeyWordDataContainer)x);
            }

            set
            {
                Handlers = value.ConvertAll(x => (HandlerDataContainer)x);
            }
        }

        public override void Execute()
        {
            foreach (var handler in Handlers)
            {
                Console.WriteLine($"Handler: {handler.DataName}, Source: {handler.DataInfo}");
            }
        }

        public override string GetFormated()
        {
            return GetFormattedHandlers();
        }
    }
}