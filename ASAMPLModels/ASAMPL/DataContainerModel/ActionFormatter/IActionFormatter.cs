using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ASAMPL.DataContainerModel.ActionFormatter
{
    public interface IActionFormatter
    {
        string FormatAction(ActionDataContainer actionData);
    }
}