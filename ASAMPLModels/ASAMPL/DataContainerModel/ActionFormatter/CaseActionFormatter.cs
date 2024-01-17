using System.Text;
using BLL.ASAMPL.DataContainerModel;
using BLL.ASAMPL.DataContainerModel.ActionFormatter;

public class CaseActionFormatter : IActionFormatter
{
    public string FormatAction(ActionDataContainer actionData)
    {
        if (actionData.DataInfo.Count < 2)
        {
            throw new InvalidOperationException("Not enough parameters for CASE action.");
        }

        var caseVariable = actionData.DataInfo[0].DataName; // Ідентифікатор змінної для CASE
        var formattedString = new StringBuilder();
        formattedString.AppendLine($"CASE {caseVariable} OF {{");

        // Обробка кожного випадку в CASE
        for (int i = 1; i < actionData.DataInfo.Count; i++)
        {
            var caseOption = actionData.DataInfo[i];
            if (caseOption is ElementsDataContainer)
            {
                var element = (ElementsDataContainer)caseOption;
                formattedString.AppendLine($"    {element.DataName}: {element.GetDataRepresentation()};");
            }
            else if (caseOption is ActionDataContainer)
            {
                var action = (ActionDataContainer)caseOption;
                formattedString.AppendLine($"    {action.DataName}: {action.GetDataRepresentation()};");
            }
        }

        // Обробка ELSE, якщо воно є
        if (actionData.AdditionlInfo != 0)
        {
            var elseActionsIndex = actionData.AdditionlInfo;
            formattedString.AppendLine("ELSE {");
            for (int i = elseActionsIndex; i < actionData.DataInfo.Count; i++)
            {
                var elseAction = actionData.DataInfo[i];
                formattedString.AppendLine($"    {elseAction.GetDataRepresentation()};");
            }
            formattedString.AppendLine("}");
        }

        formattedString.AppendLine("}");

        return formattedString.ToString();
    }
}