using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ASAMPL.DataContainerModel.ActionFormatter
{
    public class DBGActionFormatter : IActionFormatter
    {
        public string FormatAction(ActionDataContainer actionData)
        {
            var dbgIndetifire = actionData.DataInfo[0];

            var formattedString = new StringBuilder();
            formattedString.Append($"dbg({dbgIndetifire.DataName});");

            return formattedString.ToString();
        }
    }
}