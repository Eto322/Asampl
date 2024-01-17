namespace BLL.ASAMPL.AbstractModel
{
    public abstract class AsamplKeyWordModel
    {
        public string KeyWordName { get; set; }
        public string Description { get; set; }

        public abstract List<AsamplKeyWordDataContainer> DataContainer { get; set; }

        public abstract void Execute();

        public abstract string GetFormated();
    }
}