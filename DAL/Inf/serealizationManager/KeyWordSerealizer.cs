using BLL.ASAMPL.AbstractModel;
using BLL.ASAMPL.KeyWordsModel;
using Newtonsoft.Json;

namespace DAL.Inf.serealizationManager;

public class KeywordSerializer
{
    public string SerializeKeyWord(AsamplKeyWordModel keyWord)
    {
        /* if (keyWord == null)
             throw new ArgumentNullException(nameof(keyWord));

         var settings = new JsonSerializerSettings
         {
             TypeNameHandling = TypeNameHandling.Auto,
             Formatting = Formatting.Indented // Для кращої читабельності виведеного JSON
         };

         return JsonConvert.SerializeObject(keyWord, settings);*/

        if (keyWord == null)
            throw new ArgumentNullException(nameof(keyWord));

        return keyWord switch
        {
            ActionsKeyWordModel actionsKeyWord => JsonConvert.SerializeObject(actionsKeyWord, Formatting.Indented),
            AggregateKeyWordModel aggregateKeyWord => JsonConvert.SerializeObject(aggregateKeyWord, Formatting.Indented),
            ElementsKeyWordModel elementsKeyWord => JsonConvert.SerializeObject(elementsKeyWord, Formatting.Indented),
            HandlerKeyWordModel handlerKeyWord => JsonConvert.SerializeObject(handlerKeyWord, Formatting.Indented),
            LibraryKeyWordModel libraryKeyWord => JsonConvert.SerializeObject(libraryKeyWord, Formatting.Indented),
            RenderersKeyWordModel renderersKeyWord => JsonConvert.SerializeObject(renderersKeyWord, Formatting.Indented),
            SetsKeyWordModel setKeyWord => JsonConvert.SerializeObject(setKeyWord, Formatting.Indented),
            SourceKeyWordModel sourceKeyWord => JsonConvert.SerializeObject(sourceKeyWord, Formatting.Indented),
            TupleKeyWordModel tupleKeyWord => JsonConvert.SerializeObject(tupleKeyWord, Formatting.Indented),
            _ => throw new InvalidOperationException("Неідентифікований тип ключового слова")
        };
    }
}