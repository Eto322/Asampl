using BLL.ASAMPL.AbstractModel;
using BLL.ASAMPL.KeyWordsModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ASAMPL.DataContainerModel
{
    public class SourceDataContainer : AsamplKeyWordDataContainer
    {
        public SourceDataContainer()
        {
            /* AssociatedKeyWordModel = new SourceKeyWordModel();*/
            DataContext = "path";
        }

        //DataName-Name of Source
        //DataInfo-Path to Source
        public SourceDataContainer(string sourceName, string sourcePath)
        {
            DataName = sourceName;
            DataInfo = sourcePath;
            /* AssociatedKeyWordModel = new SourceKeyWordModel();*/
            DataContext = "path";
        }

        public override string GetDataRepresentation()
        {
            return $"    {DataName} from '{DataInfo}';";
        }
    }
}