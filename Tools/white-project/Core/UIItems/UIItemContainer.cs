using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Automation;
using Bricks.Core;
using Bricks.RuntimeFramework;
using Castle.Core.Interceptor;
using White.Core.AutomationElementSearch;
using White.Core.Factory;
using White.Core.InputDevices;
using White.Core.Interceptors;
using White.Core.Sessions;
using White.Core.UIA;
using White.Core.UIItems.Actions;
using White.Core.UIItems.Container;
using White.Core.UIItems.Custom;
using White.Core.UIItems.Finders;
using White.Core.UIItems.Scrolling;
using White.Core.UIItems.TabItems;
using White.Core.UIItems.WindowStripControls;

namespace White.Core.UIItems
{
    //BUG: Allow finding more than one item, also ability to do this within a container
    //TODO: Dont let people compile code is they are trying to find UIItem which are secondary or window
    public class UIItemContainer : UIItem, IUIItemContainer, VerticalSpanProvider
    {
        protected readonly CurrentContainerItemFactory currentContainerItemFactory;
        protected WindowSession windowSession = new NullWindowSession();

        protected UIItemContainer()
        {
        }

        public UIItemContainer(AutomationElement automationElement, ActionListener actionListener,
                               InitializeOption initializeOption,
                               WindowSession windowSession) : base(automationElement, actionListener)
        {
            this.windowSession = windowSession;
            currentContainerItemFactory = new CurrentContainerItemFactory(factory, initializeOption, automationElement,
                                                                          ChildrenActionListener);
        }

        public UIItemContainer(AutomationElement automationElement, ActionListener actionListener)
            : this(automationElement, actionListener, InitializeOption.NoCache, new NullWindowSession())
        {
        }

        /// <summary>
        /// Finds UIItem which matches specified type. Useful for non managed applications where controls are not identified by AutomationId, like in 
        /// Managed applications. In case of multiple items of this type the first one found would be returned which cannot be guaranteed to be the same 
        /// across multiple invocations.
        /// </summary>
        /// <typeparam name="T">UIItem type e.g. Button, TextBox</typeparam>
        /// <returns>First item of supplied type</returns>
        public virtual T Get<T>() where T : UIItem
        {
            return Get<T>(SearchCriteria.All);
        }

        /// <summary>
        /// Finds UIItem which matches specified type and identification. 
        /// In case of multiple items of this type the first one found would be returned which cannot be guaranteed to be the same across multiple 
        /// invocations. For managed applications this is name given to controls in the application code.
        /// For unmanaged applications this is text of the control or label next to it if it doesn't have well defined text.
        /// e.g. TextBox doesn't have any predefined text of its own as it can be changed at runtime by user, hence is identified by the label next to it.
        /// If there is no label then Get<T> or Get<T>(SearchCriteria) method can be used.
        /// </summary>
        /// <typeparam name="T">UIItem type</typeparam>
        /// <param name="primaryIdentification">For managed application this is the name provided in application code and unmanaged application this is 
        /// the text or label next to it based identification</param>
        /// <returns>First item of supplied type and identification</returns>
        public virtual T Get<T>(string primaryIdentification) where T : UIItem
        {
            return Get<T>(SearchCriteria.ByAutomationId(primaryIdentification));
        }

        /// <summary>
        /// Finds UIItem which matches specified type and searchCriteria. Type supplied need not be supplied again in SearchCondition.
        /// e.g. in Get<Button>(SearchCriteria.ByAutomationId("OK").ByControlType(typeof(Button)).Indexed(1) the ByControlType(typeof(Button)) part 
        /// is redundant. Look at documentation of SearchCriteria for details on it.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="searchCriteria">Criteria provided to search UIItem</param>
        /// <returns>First items matching the type and criteria</returns>
        public virtual T Get<T>(SearchCriteria searchCriteria) where T : UIItem
        {
            return (T) Get(searchCriteria.AndControlType(typeof (T)));
        }

        /// <summary>
        /// Finds UIItem which matches specified type and searchCriteria. Look at documentation of SearchCriteria for details on it.
        /// </summary>
        /// <param name="searchCriteria">Criteria provided to search UIItem</param>
        /// <returns>First items matching the criteria</returns>
        public virtual IUIItem Get(SearchCriteria searchCriteria)
        {
            try
            {
                IUIItem uiItem = currentContainerItemFactory.Find(searchCriteria, windowSession);
                HandleIfCustomUIItem(uiItem);
                HandleIfUIItemContainer(uiItem);
                return uiItem;
            }
            catch (Exception e)
            {
                throw new WhiteException(Debug.Details(automationElement), e);
            }
        }

        private void HandleIfUIItemContainer(IUIItem uiItem)
        {
            var uiItemContainer = uiItem as UIItemContainer;
            if (uiItemContainer == null) return;
            uiItemContainer.Associate(windowSession);
        }

        private void HandleIfCustomUIItem(IUIItem uiItem)
        {
            var customUIItem = uiItem as CustomUIItem;
            if (customUIItem == null) return;
            FieldInfo interceptorField = customUIItem.GetType().GetField("__interceptors",
                                                                         BindingFlags.NonPublic | BindingFlags.Public |
                                                                         BindingFlags.Instance);
            var interceptors = (IInterceptor[]) interceptorField.GetValue(customUIItem);
            var realCustomUIItem = (CustomUIItem) ((CoreInterceptor) interceptors[0]).Context.UiItem;
            realCustomUIItem.SetContainer(new UIItemContainer(customUIItem.AutomationElement, ChildrenActionListener,
                                                              InitializeOption.NoCache, windowSession));
        }

        /// <summary>
        /// Applicable only if CacheMode is used. This is for internal purpose of white and should not be used, as caching by itself is not supported
        /// </summary>
        /// <param name="option"></param>
        public virtual void ReInitialize(InitializeOption option)
        {
            currentContainerItemFactory.ReInitialize(option);
        }

        protected virtual ActionListener ChildrenActionListener
        {
            get { return HasActionInterceptionBehaviour() ? this : actionListener; }
        }

        private bool HasActionInterceptionBehaviour()
        {
            return ScrollBars.CanScroll;
        }

        //BUG: Try this method out with all windows on the desktop and see if it works
        /// <summary>
        /// Returns a list of UIItems contained in the container/window. This is not the same as AutomationElements because white needs to translate
        /// AutomationElements to UIItem. Hence for certain AE there might not be corresponding UIItem type.
        /// </summary>
        public virtual UIItemCollection Items
        {
            get { return currentContainerItemFactory.FindAll(); }
        }

        /// <summary>
        /// Returns a keyboard which is associated to this window. Any operation performed using the mouse would wait till the window is busy after this
        /// operation. Before any operation is performed the window is brought to focus.
        /// </summary>
        public virtual AttachedKeyboard Keyboard
        {
            get { return new AttachedKeyboard(this, keyboard); }
        }

        /// <summary>
        /// Returns a mouse which is associated to this window. Any operation performed using the mouse would wait till the window is busy after this
        /// operation. Before any operation is performed the window is brought to focus.
        /// </summary>
        public virtual AttachedMouse Mouse
        {
            get { return new AttachedMouse(mouse, this); }
        }

        public virtual IUIItem[] GetMultiple(SearchCriteria criteria)
        {
            return currentContainerItemFactory.FindAll(criteria).ToArray();
        }

        internal virtual void Associate(WindowSession session)
        {
            windowSession = session;
        }

        public virtual VerticalSpan VerticalSpan
        {
            get { return new VerticalSpan(Bounds); }
        }

        public override void ActionPerforming(UIItem uiItem)
        {
            Focus();
            var screenItem = new ScreenItem(uiItem, ScrollBars);
            screenItem.MakeVisible(this);
        }

        public virtual MenuBar MenuBar
        {
            get { return (MenuBar) Get(SearchCriteria.ForMenuBar(Framework)); }
        }

        public virtual MenuBar GetMenuBar(SearchCriteria searchCriteria)
        {
            return (MenuBar) Get(SearchCriteria.ForMenuBar(Framework).Merge(searchCriteria));
        }

        public virtual List<MenuBar> MenuBars
        {
            get { return new BricksCollection<MenuBar>(GetMultiple(SearchCriteria.ForMenuBar(Framework))); }
        }

        public virtual ToolTip ToolTip
        {
            get { return factory.ToolTip; }
        }

        public virtual ToolTip GetToolTipOn(UIItem uiItem)
        {
            Mouse.Location = uiItem.Bounds.Center();
            var finder = new AutomationElementFinder(automationElement);
            Clock.Do perform = () => finder.Descendant(AutomationSearchCondition.ByControlType(ControlType.ToolTip));
            return ToolTipFinder.FindToolTip(perform);
        }

        public virtual ToolStrip ToolStrip
        {
            get
            {
                Focus();
                return (ToolStrip) Get(SearchCriteria.ByControlType(ControlType.ToolBar));
            }
        }

        public virtual List<Tab> Tabs
        {
            get { return currentContainerItemFactory.FindAll<Tab>(); }
        }

        public virtual ToolStrip GetToolStrip(string primaryIdentification)
        {
            var toolStrip = (ToolStrip) Get(SearchCriteria.ByAutomationId(primaryIdentification));
            if (toolStrip == null) return null;
            toolStrip.Associate(windowSession);
            return toolStrip;
        }

        /// <summary>
        /// Find all the UIItems which belongs to a window and are within (bounds of) another UIItem.
        /// </summary>
        /// <param name="containingItem">Containing item</param>
        /// <returns>List of all the items.</returns>
        public virtual List<UIItem> ItemsWithin(UIItem containingItem)
        {
            UIItemCollection itemsWithin = factory.ItemsWithin(containingItem.Bounds, this);
            var items = new List<UIItem>();
            foreach (var item in itemsWithin)
                if (!item.Equals(containingItem)) items.Add((UIItem) item);
            return items;
        }
    }
}