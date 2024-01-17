using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ASAMPL.AbstractModel
{
    public abstract class AsamplKeyWordDataContainer
    {
        public virtual string DataName { get; set; }
        public virtual string DataInfo { get; set; }

        /*  public AsamplKeyWordModel AssociatedKeyWordModel { get; set; }*/

        public string DataContext { get; set; }

        public virtual void AddData(string dataName, string dataInfo)
        { }

        public virtual string GetDataRepresentation()
        {
            return string.Empty;
        }
    }
}