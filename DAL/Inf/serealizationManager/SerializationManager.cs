using BLL.ASAMPL.AbstractModel;
using Newtonsoft.Json.Linq;

namespace DAL.Inf.serealizationManager;

public class SerializationManager
{
    private KeywordSerializer _keywordSerializer = new KeywordSerializer();

    public void SerializeKeyWordListToFile(List<AsamplKeyWordModel> keyWords, string directoryPath = null)
    {
        if (keyWords == null)
            throw new ArgumentNullException(nameof(keyWords));

        // Використання поточного робочого каталогу, якщо шлях до директорії не переданий
        if (string.IsNullOrWhiteSpace(directoryPath))
        {
            directoryPath = Directory.GetCurrentDirectory();
        }

        // Переконатися, що директорія існує
        Directory.CreateDirectory(directoryPath);

        // Створення шляху до файлу JSON у вказаній директорії
        string filePath = Path.Combine(directoryPath, "SerializedKeywords.json");

        var keyWordArray = new JArray();
        foreach (var keyWord in keyWords)
        {
            string serializedKeyWord = _keywordSerializer.SerializeKeyWord(keyWord);
            keyWordArray.Add(JToken.Parse(serializedKeyWord));
        }

        File.WriteAllText(filePath, keyWordArray.ToString());

        /*var stringBuilder = new StringBuilder();
        foreach (var keyWord in keyWords)
        {
            string serializedKeyWord = _keywordSerializer.SerializeKeyWord(keyWord);
            stringBuilder.AppendLine(serializedKeyWord);
            stringBuilder.Append("\n");
        }

        File.WriteAllText(filePath, stringBuilder.ToString());*/
    }

    public void SerealizeCodeList(List<string> codeList, string directoryPath = null)
    {
        if (codeList == null)
            throw new ArgumentNullException(nameof(codeList));

        // Використання поточного робочого каталогу, якщо шлях до директорії не переданий
        if (string.IsNullOrWhiteSpace(directoryPath))
        {
            directoryPath = Directory.GetCurrentDirectory();
        }
        Directory.CreateDirectory(directoryPath);

        // Створення шляху до файлу JSON у вказаній директорії
        string filePath = Path.Combine(directoryPath, "SerializedASACode.json");
    }
}