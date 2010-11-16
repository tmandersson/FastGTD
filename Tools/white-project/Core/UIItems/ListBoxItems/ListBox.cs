using System.Windows.Automation;
using White.Core.Recording;
using White.Core.UIItemEvents;
using White.Core.UIItems.Actions;

namespace White.Core.UIItems.ListBoxItems
{
    public class ListBox : ListControl
    {
        private AutomationPropertyChangedEventHandler handler;

        protected ListBox() {}
        public ListBox(AutomationElement automationElement, ActionListener actionListener) : base(automationElement, actionListener) {}

        public virtual bool IsChecked(string itemText)
        {
            return Item(itemText).Checked;
        }

        public virtual void Check(string itemText)
        {
            Item(itemText).Check();
        }

        public virtual bool IsSelected(string itemText)
        {
            return Item(itemText).IsSelected;
        }

        public virtual void UnCheck(string itemText)
        {
            Item(itemText).UnCheck();
        }

        public override void HookEvents(UIItemEventListener eventListener)
        {
            handler = delegate(object sender, AutomationPropertyChangedEventArgs e)
                          {
                              if (e.NewValue.Equals(1)) return;
                              eventListener.EventOccured(new ListBoxEvent(this, SelectedItemText));
                          };
            Automation.AddAutomationPropertyChangedEventHandler(automationElement, TreeScope.Descendants, handler, SelectionItemPattern.IsSelectedProperty);
        }

        public override void UnHookEvents()
        {
            Automation.RemoveAutomationPropertyChangedEventHandler(automationElement, handler);
        }
    }
}