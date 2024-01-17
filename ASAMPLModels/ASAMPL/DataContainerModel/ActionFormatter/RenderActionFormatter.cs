using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ASAMPL.DataContainerModel.ActionFormatter
{
    public class RenderActionFormatter : IActionFormatter
    {
        public string FormatAction(ActionDataContainer actionData)
        {
            if (actionData.DataInfo.Count < 2)
            {
                throw new InvalidOperationException("Not enough parameters for RENDER action.");
            }

            var aggregateIdentifier = actionData.DataInfo[0].DataName; // Ідентифікатор агрегату
            var renderers = actionData.DataInfo.Skip(1).Select(r => r.DataName); // Ідентифікатори рендерів

            var formattedString = new StringBuilder();
            formattedString.Append($"RENDER {aggregateIdentifier} WITH [");
            formattedString.Append(string.Join(", ", renderers));
            formattedString.Append("];");

            return formattedString.ToString();
        }
    }
}