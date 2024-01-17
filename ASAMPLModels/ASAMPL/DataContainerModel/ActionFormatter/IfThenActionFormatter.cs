using BLL.ASAMPL.AbstractModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ASAMPL.DataContainerModel.ActionFormatter
{
    public class IfThenActionFormatter : IActionFormatter
    {
        public string FormatAction(ActionDataContainer actionData)
        {
            if (actionData.DataInfo.Count < 2)
            {
                throw new InvalidOperationException("Not enough parameters for IF THEN action.");
            }

            var condition = actionData.DataInfo[0].ToString();// Логічний вираз
            var thenActions = actionData.DataInfo.Skip(1).ToList(); // Дії для THEN
            var elseActions = actionData.AdditionlInfo != 0
                ? actionData.DataInfo.Skip(1 + actionData.AdditionlInfo).ToList() // Дії для ELSE
                : new List<AsamplKeyWordDataContainer>();

            var formattedString = new StringBuilder();
            formattedString.AppendLine($"IF {condition} THEN {{");

            foreach (var action in thenActions)
            {
                formattedString.AppendLine($"    {action.GetDataRepresentation()}");
            }

            if (elseActions.Any())
            {
                formattedString.AppendLine("} ELSE {");
                foreach (var action in elseActions)
                {
                    formattedString.AppendLine($"    {action.GetDataRepresentation()}");
                }
            }

            formattedString.AppendLine("}");

            return formattedString.ToString();
        }
    }
}