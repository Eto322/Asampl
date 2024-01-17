using System;
using System.Collections.Generic;
using BLL.ASAMPL.AbstractModel;
using BLL.ASAMPL.DataContainerModel.ActionFormatter;
using BLL.ASAMPL.KeyWordsModel;

namespace BLL.ASAMPL.DataContainerModel
{
    public class ActionDataContainer : AsamplKeyWordDataContainer
    {
        public string DataName { get; set; }
        public List<AsamplKeyWordDataContainer> DataInfo { get; set; }
        public string DataContext { get; set; }
        public int AdditionlInfo { get; set; }

        public bool IsChekedForView { get; set; }

        public string DataForView
        {
            get => GetDataRepresentation();
        }

        private Dictionary<string, IActionFormatter> formatters;

        public ActionDataContainer()
        {
            formatters = new Dictionary<string, IActionFormatter>
            {
                { "DOWNLOAD", new DownloadActionFormatter() },
                {"UPLOAD",new UploadActionFormatter()},
                { "TIMELINE", new TimelineActionFormatter() },
                {"IF THEN", new IfThenActionFormatter()},
                {"RENDER",new RenderActionFormatter() },
                {"SUBSTITUTE",new SubstituteActionFormatter()},
                {"SEQUENCE",new SequenceActionFormatter()},
                {"DBG",new DBGActionFormatter()}
            };
        }

        public ActionDataContainer(string dataName, List<AsamplKeyWordDataContainer> dataInfo, int additionlInfo = 0)
        {
            DataName = dataName;
            DataInfo = dataInfo;
            /*AssociatedKeyWordModel = new ActionsKeyWordModel();*/
            DataContext = "Action";
            AdditionlInfo = additionlInfo;

            formatters = new Dictionary<string, IActionFormatter>
            {
                { "DOWNLOAD", new DownloadActionFormatter() },
                {"UPLOAD",new UploadActionFormatter()},
                { "TIMELINE", new TimelineActionFormatter() },
                {"IF THEN", new IfThenActionFormatter()},
                {"RENDER",new RenderActionFormatter() },
                {"SUBSTITUTE",new SubstituteActionFormatter()},
                {"SEQUENCE",new SequenceActionFormatter()},
                {"DBG",new DBGActionFormatter()}
            };
        }

        public override string GetDataRepresentation()
        {
            if (formatters.TryGetValue(DataName.ToUpper(), out var formatter))
            {
                return formatter.FormatAction(this);
            }
            else
            {
                throw new InvalidOperationException($"Unknown action type: {DataName}");
            }
        }
    }
}