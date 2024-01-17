using BLL.ASAMPL.AbstractModel;
using BLL.ASAMPL.KeyWordsModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ASAMPL.DataContainerModel
{
    public class LibraryDataContainer : AsamplKeyWordDataContainer
    {
        public LibraryDataContainer()
        {
            /*AssociatedKeyWordModel = new LibraryKeyWordModel();*/
            DataContext = "path";
        }

        //DataName-LibraryName
        //DataInfo-LibraryPath
        public LibraryDataContainer(string libraryName, string libraryPath)
        {
            DataName = libraryName;
            DataInfo = libraryPath;
            /*AssociatedKeyWordModel = new LibraryKeyWordModel();*/
            DataContext = "path";
        }

        public override string GetDataRepresentation()
        {
            return $"    {DataName} Is '{DataInfo}';";
        }
    }
}