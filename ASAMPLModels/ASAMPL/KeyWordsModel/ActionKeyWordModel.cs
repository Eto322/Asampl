using System;
using System.Collections.Generic;
using System.Text;
using BLL.ASAMPL.AbstractModel;
using BLL.ASAMPL.DataContainerModel;

namespace BLL.ASAMPL.KeyWordsModel
{
    public class ActionsKeyWordModel : AsamplKeyWordModel
    {
        public List<ActionDataContainer> Actions { get; set; }

        public ActionsKeyWordModel()
        {
            Actions = new List<ActionDataContainer>();
            KeyWordName = "ACTIONS";
            Description = "Block that defines actions for data processing.";
        }

        // Додавання дії
        /*public void AddAction(string actionType, string actionDetail)
        {
            Actions.Add(new ActionDataContainer(actionType,))
        }*/

        public void AddAction(ActionDataContainer action)
        {
            Actions.Add(action);
        }

        // Видалення дії
        public bool RemoveAction(string actionType)
        {
            return Actions.RemoveAll(a => a.DataName == actionType) > 0;
        }

        public string GetFormattedActions()
        {
            var formattedActions = new StringBuilder();
            foreach (var action in Actions)
            {
                formattedActions.AppendLine(action.GetDataRepresentation());
            }
            return formattedActions.ToString();
        }

        public override List<AsamplKeyWordDataContainer> DataContainer
        {
            get
            {
                return Actions.ConvertAll(x => (AsamplKeyWordDataContainer)x);
            }

            set
            {
                Actions = value.ConvertAll(x => (ActionDataContainer)x);
            }
        }

        public override void Execute()
        {
            foreach (var action in Actions)
            {
                Console.WriteLine($"Action: {action.GetDataRepresentation()}");
            }
        }

        public override string GetFormated()
        {
            return GetFormattedActions();
        }
    }
}