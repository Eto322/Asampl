using BLL.ASAMPL.AbstractModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ASAMPL.DataContainerModel
{
    public class ConditionDataContainer : AsamplKeyWordDataContainer
    {
        public string firstArgument;
        public string secondArgument;
        public string thirdArgument;

        public ConditionDataContainer(string firstArgument, string secondArgument, string thirdArgument)
        {
            this.firstArgument = firstArgument;
            this.secondArgument = secondArgument;
            this.thirdArgument = thirdArgument;
        }

        public override string ToString()
        {
            return firstArgument + " " + secondArgument + " " + thirdArgument;
        }
    }
}