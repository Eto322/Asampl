using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ASAMPL.DataContainerModel.ActionFormatter
{
    public class SequenceActionFormatter : IActionFormatter
    {
        public string FormatAction(ActionDataContainer actionData)
        {
            if (actionData.DataName != "SEQUENCE")
            {
                throw new InvalidOperationException("Invalid action type for SequenceActionFormatter.");
            }

            var formattedString = new StringBuilder();
            formattedString.AppendLine("SEQUENCE {");

            foreach (var action in actionData.DataInfo)
            {
                formattedString.AppendLine($"    {action.GetDataRepresentation()}");
            }

            formattedString.AppendLine("}");

            return formattedString.ToString();
        }
    }
}