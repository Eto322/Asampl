using BLL.ASAMPL.AbstractModel;
using BLL.ASAMPL.KeyWordsModel;
using Newtonsoft.Json;

namespace DAL.Inf.deserealizationManager
{
    public class DeserealizationManager
    {
        /*public List<AsamplKeyWordModel> DeserializeKeyWordListFromFile(string filePath)
        {
            var keyWords = new List<AsamplKeyWordModel>();

            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            };

            string fileContent = File.ReadAllText(filePath);
            var keyWordList = JsonConvert.DeserializeObject<List<AsamplKeyWordModel>>(fileContent, settings);

            return keyWordList ?? new List<AsamplKeyWordModel>();
        }*/

        public List<AsamplKeyWordModel> DeserializeKeyWordListFromFile(string filePath)
        {
            var keyWords = new List<AsamplKeyWordModel>();

            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto,
                Converters = new List<JsonConverter> { new KeyWordDataContainerConverter() }
            };

            using (var sr = new StreamReader(filePath))
            using (var reader = new JsonTextReader(sr))
            {
                JsonSerializer serializer = JsonSerializer.Create(settings);
                int index = 0;

                while (reader.Read())
                {
                    if (reader.TokenType == JsonToken.StartObject)
                    {
                        AsamplKeyWordModel keyWord = DeserializeBasedOnIndex(index, reader, serializer);
                        keyWords.Add(keyWord);
                        index++;
                    }
                }
            }

            return keyWords;
        }

        private AsamplKeyWordModel DeserializeBasedOnIndex(int index, JsonReader reader, JsonSerializer serializer)
        {
            switch (index)
            {
                case 0: return serializer.Deserialize<LibraryKeyWordModel>(reader);
                case 1: return serializer.Deserialize<HandlerKeyWordModel>(reader);
                case 2: return serializer.Deserialize<RenderersKeyWordModel>(reader);
                case 3: return serializer.Deserialize<SourceKeyWordModel>(reader);
                case 4: return serializer.Deserialize<SetsKeyWordModel>(reader);
                case 5: return serializer.Deserialize<ElementsKeyWordModel>(reader);
                case 6: return serializer.Deserialize<TupleKeyWordModel>(reader);
                case 7: return serializer.Deserialize<AggregateKeyWordModel>(reader);
                case 8:
                    return serializer.Deserialize<ActionsKeyWordModel>(reader);
                    break;

                default: throw new JsonSerializationException($"Невідома позиція ключового слова: {index}");
            }
        }
    }
}