using BLL.ASAMPL.AbstractModel;
using BLL.ASAMPL.DataContainerModel;
using BLL.ASAMPL.KeyWordsModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DAL.Inf.deserealizationManager
{
    public class KeyWordDataContainerConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(AsamplKeyWordDataContainer));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jsonObject = JObject.Load(reader);
            var keyWordName = jsonObject["KeyWordName"]?.ToString();

            AsamplKeyWordDataContainer dataContainer = null;

            // Визначаємо, який тип даних використовувати на основі KeyWordName або іншої логіки
            switch (keyWordName)
            {
                case "LIBRARIES":
                    dataContainer = new LibraryDataContainer();
                    break;

                case "HANDLERS":
                    dataContainer = new HandlerDataContainer();
                    break;

                case "RENDERERS":
                    dataContainer = new RendererDataContainer();
                    break;

                case "SOURCES":
                    dataContainer = new SourceDataContainer();
                    break;

                case "SETS":
                    dataContainer = new SetDataContainer();
                    break;

                case "ELEMENTS":
                    dataContainer = new ElementsDataContainer();
                    break;

                case "TUPLES":
                    dataContainer = new TupleDataContainer();
                    break;

                case "AGGREGATES":
                    dataContainer = new AggregateDataContainer();
                    break;

                case "ACTIONS":
                    dataContainer = new ActionDataContainer();
                    break;

                    // Додайте випадки для інших типів
            }

            if (dataContainer != null)
            {
                serializer.Populate(jsonObject.CreateReader(), dataContainer);
            }

            return dataContainer;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}