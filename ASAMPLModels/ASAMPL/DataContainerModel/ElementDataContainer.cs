using System.Data.Common;
using System.Xml.Linq;
using BLL.ASAMPL.AbstractModel;
using BLL.ASAMPL.KeyWordsModel;

public class ElementsDataContainer : AsamplKeyWordDataContainer
{
    public string ElementType { get; set; }

    public SetDataContainer AssociadetSet { get; set; }

    //DataName - Name of the Elemeent
    //DataInfo - Element Value if its null than it type Declaration
    public ElementsDataContainer(string elementName, SetDataContainer set, string Value = null)
    {
        DataName = elementName;
        DataInfo = Value;
        ElementType = set.DataName;
        /* AssociatedKeyWordModel = new ElementsKeyWordModel();*/
        AssociadetSet = set;
    }

    public ElementsDataContainer()
    {
    }

    public string getForView
    {
        get { return GetForView(); }
    }

    public string GetForView()
    {
        return $"Element: {DataName}, Type: {ElementType}, Value: {DataInfo}";
    }

    public override string GetDataRepresentation()
    {
        if (DataInfo == null)
        {
            return $"{DataName} is {ElementType};";
        }
        else
        {
            return $"{DataName} = {DataInfo};";
        }
    }
}