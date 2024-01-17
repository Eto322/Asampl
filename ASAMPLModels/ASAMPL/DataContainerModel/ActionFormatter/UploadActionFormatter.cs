using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ASAMPL.DataContainerModel.ActionFormatter
{
    public class UploadActionFormatter : IActionFormatter
    {
        public string FormatAction(ActionDataContainer actionData)
        {
            if (actionData.DataInfo.Count < 2)
            {
                throw new InvalidOperationException("Not enough parameters for UPLOAD action.");
            }

            var sourceIdentifier = actionData.DataInfo[0].DataName; // Ідентифікатор джерела
            var destinationIdentifier = actionData.DataInfo[1].DataName; // Ідентифікатор призначення
            var handlerIdentifier = actionData.DataInfo.Count > 2 ? actionData.DataInfo[2].DataName : "default"; // Ідентифікатор обробника

            var formattedString = new StringBuilder();
            formattedString.Append($"UPLOAD {sourceIdentifier} TO {destinationIdentifier}");
            if (handlerIdentifier != "default")
            {
                formattedString.Append($" WITH {handlerIdentifier};");
            }
            else
            {
                formattedString.Append(";");
            }

            return formattedString.ToString();
        }
    }
}