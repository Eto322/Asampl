using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.ASAMPL.AbstractModel;
using BLL.ASAMPL.KeyWordsModel;

namespace BLL.ASAMPL.DataContainerModel
{
    public class HandlerDataContainer : AsamplKeyWordDataContainer
    {
        public HandlerDataContainer()
        {
            /* AssociatedKeyWordModel = new HandlerKeyWordModel();*/
            DataContext = "path";
        }

        //DataName-Name of tha Handler
        //DataInfo-handler Distanation
        public HandlerDataContainer(string handlerName, string handelrDistanation)
        {
            DataName = handlerName;
            DataInfo = handelrDistanation;
            /*AssociatedKeyWordModel = new HandlerKeyWordModel();*/
            DataContext = "path";
        }

        public override string GetDataRepresentation()
        {
            return $"    {DataName} from '{DataInfo}';";
        }
    }
}