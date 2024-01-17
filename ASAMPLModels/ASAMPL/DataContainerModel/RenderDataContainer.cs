using BLL.ASAMPL.AbstractModel;
using BLL.ASAMPL.KeyWordsModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ASAMPL.DataContainerModel
{
    public class RendererDataContainer : AsamplKeyWordDataContainer
    {
        public RendererDataContainer()
        {
            /*AssociatedKeyWordModel = new RenderersKeyWordModel();*/
            DataContext = "path";
        }

        //DataName-RenderName
        //DataInfo-renderPath
        public RendererDataContainer(string rendererName, string rendererPath)
        {
            DataName = rendererName;
            DataInfo = rendererPath;
            /* AssociatedKeyWordModel = new RenderersKeyWordModel();*/
            DataContext = "path";
        }

        public override string GetDataRepresentation()
        {
            return $"    {DataName} Is '{DataInfo}';";
        }
    }
}