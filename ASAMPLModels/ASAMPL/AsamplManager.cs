using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.ASAMPL.AbstractModel;
using BLL.ASAMPL.DataContainerModel;
using BLL.ASAMPL.KeyWordsModel;

namespace BLL.ASAMPL
{
    public class AsamplManager
    {
        public AsamplManager()
        {
        }

        public List<AsamplKeyWordModel> FillKeyWords()
        {
            var KeyWords = new List<AsamplKeyWordModel>()
            {
                new LibraryKeyWordModel(),
                new HandlerKeyWordModel(),
                new RenderersKeyWordModel(),
                new SourceKeyWordModel(),
                new SetsKeyWordModel(),
                new ElementsKeyWordModel(),
                new TupleKeyWordModel(),
                new AggregateKeyWordModel(),
                new ActionsKeyWordModel()
            };

            return KeyWords;
        }

        public List<string> FillActionKeys()
        {
            var KeyWords = new List<string>()
            {
                "RENDER",
                "DOWNLOAD",
                "UPLOAD",
                "TIMELINE",
                "IF THEN",
                "SUBSTITUTE",
                "SEQUENCE"
            };

            return KeyWords;
        }
    }
}