using System.Text;
using BLL.ASAMPL.DataContainerModel;
using BLL.ASAMPL.DataContainerModel.ActionFormatter;

public class TimelineActionFormatter : IActionFormatter
{
    public string FormatAction(ActionDataContainer actionData)
    {
        if (actionData.DataInfo.Count < 1)
        {
            throw new InvalidOperationException("Not enough parameters for TIMELINE action.");
        }

        var formattedString = new StringBuilder();
        formattedString.Append("TIMELINE ");

        // Визначення типу TIMELINE
        var firstParam = actionData.DataInfo[0];
        if (firstParam is TupleDataContainer || firstParam is ElementsDataContainer)
        {
            // Якщо перший параметр є TupleDataContainer, то це кортеж
            formattedString.AppendLine($"AS {firstParam.DataName} {{");
        }
        else if (firstParam is ActionDataContainer)
        {
            // Якщо перший параметр є ActionDataContainer, то це логічний вираз
            formattedString.AppendLine($"UNTIL {firstParam.GetDataRepresentation()} {{");
        }
        else
        {
            // Якщо перший параметр є стандартним часом
            var timeStart = firstParam.DataName;
            var timeEnd = actionData.DataInfo[1].DataName;
            var timeStep = actionData.DataInfo[2].DataName;
            formattedString.AppendLine($"{timeStart} : {timeStep} : {timeEnd} {{");
        }

        // Додавання дій
        foreach (var action in actionData.DataInfo.Skip(3))
        {
            formattedString.AppendLine($"    {action.GetDataRepresentation()}");
        }

        formattedString.AppendLine("}");

        return formattedString.ToString();
    }
}