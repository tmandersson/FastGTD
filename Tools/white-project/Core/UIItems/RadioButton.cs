using System.Windows.Automation;
using White.Core.Recording;
using White.Core.UIItemEvents;
using White.Core.UIItems.Actions;

namespace White.Core.UIItems
{
    public class RadioButton : SelectionItem
    {
        private AutomationEventHandler handler;
        protected RadioButton() {}
        public RadioButton(AutomationElement automationElement, ActionListener actionListener) : base(automationElement, actionListener) {}

        public override void HookEvents(UIItemEventListener eventListener)
        {
            handler = delegate { eventListener.EventOccured(new RadioButtonEvent(this)); };
            Automation.AddAutomationEventHandler(SelectionItemPattern.ElementSelectedEvent, automationElement, TreeScope.Element, handler);
        }

        public override void UnHookEvents()
        {
            Automation.RemoveAutomationEventHandler(SelectionItemPattern.ElementSelectedEvent, automationElement, handler);
        }

        public override void SetValue(object value)
        {
            if (!(value is bool)) throw new UIActionException("Cannot set non bool value to a RadioButton. Trying to set: " + value);
            IsSelected = (bool) value;
        }
    }
}