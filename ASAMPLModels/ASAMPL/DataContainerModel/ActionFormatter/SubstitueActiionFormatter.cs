using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ASAMPL.DataContainerModel.ActionFormatter
{
    public class SubstituteActionFormatter : IActionFormatter
    {
        public string FormatAction(ActionDataContainer actionData)
        {
            if (actionData.DataInfo.Count < 2)
            {
                throw new InvalidOperationException("Not enough parameters for SUBSTITUTE action.");
            }

            var originalVariable = actionData.DataInfo[0].DataName; // Ідентифікатор оригінальної змінної
            var newVariable = actionData.DataInfo[1].DataName; // Ідентифікатор нової змінної

            var formattedString = new StringBuilder();
            formattedString.AppendLine($"SUBSTITUTE {originalVariable} FOR {newVariable} ;");

            return formattedString.ToString();
        }
    }
}