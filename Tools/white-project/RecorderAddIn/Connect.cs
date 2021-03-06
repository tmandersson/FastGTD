using System;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Windows.Forms;
using EnvDTE;
using EnvDTE80;
using Extensibility;
using Microsoft.VisualStudio.CommandBars;

namespace RecorderAddIn
{
    public class Connect : IDTExtensibility2, IDTCommandTarget
    {
        private const string CLASS_GEN_CONTEXT_MENU_CMD_DESCIPTION = "Generate Class";
        private const string CLASS_GEN_CONTEXT_MENU_CMD_NAME = "RecorderAddInContextMenu";
        private const string CLASS_GEN_CONTEXT_MENU_NAME = "Generate Class";
        private const string GENERATE_CLASS_CONTEXT_MENU = "RecorderAddIn.Connect.RecorderAddInContextMenu";
        private const string GENERATE_CLASS_TOOLS_MENU = "RecorderAddIn.Connect.RecorderAddIn";
        private const string PROJECT_COMMAND_BAR = "Project";
        private const string PROJECT_MENU_BAR = "MenuBar";
        private const string RECORDER_ADD_IN_TOOLS_MENU_ITEM_NAME = "RecorderAddIn";
        private const string RESOURCE_NAME = "RecorderAddIn.CommandBar";
        private const string TOOLS_COMMAND_DESCRIPTION = "Executes the command for RecorderAddIn";
        private const string TOOLS_COMMAND_NAME = "RecorderAddIn";
        private const string TOOLS_MENU_NAME = "Tools";

        private AddIn addIn;
        private DTE2 application;

        /// <summary>Implements the QueryStatus method of the IDTCommandTarget interface. This is called when the command's availability is updated</summary>
        /// <param term='commandName'>The name of the command to determine state for.</param>
        /// <param term='neededText'>Text that is needed for the command.</param>
        /// <param term='status'>The state of the command in the user interface.</param>
        /// <param term='commandText'>Text requested by the neededText parameter.</param>
        /// <seealso class='Exec' />
        public void QueryStatus(string commandName, vsCommandStatusTextWanted neededText, ref vsCommandStatus status, ref object commandText)
        {
            if (neededText == vsCommandStatusTextWanted.vsCommandStatusTextWantedNone)
            {
                if (GENERATE_CLASS_TOOLS_MENU == commandName || GENERATE_CLASS_CONTEXT_MENU == commandName)
                    status = activeDocumentIsProject() ? vsCommandStatus.vsCommandStatusSupported | vsCommandStatus.vsCommandStatusEnabled
                        : vsCommandStatus.vsCommandStatusInvisible | vsCommandStatus.vsCommandStatusUnsupported;
            }
        }

        /// <summary>This is called when the command is invoked.</summary>
        /// <param term='commandName'>The name of the command to execute.</param>
        /// <param term='executeOption'>Describes how the command should be run.</param>
        /// <param term='varIn'>Parameters passed from the caller to the command handler.</param>
        /// <param term='varOut'>Parameters passed from the command handler to the caller.</param>
        /// <param term='handled'>Informs the caller if the command was handled or not.</param>
        /// <seealso class='Exec' />
        public void Exec(string commandName, vsCommandExecOption executeOption, ref object varIn, ref object varOut, ref bool handled)
        {
            handled = false;
            if (executeOption == vsCommandExecOption.vsCommandExecOptionDoDefault)
            {
                if (GENERATE_CLASS_TOOLS_MENU == commandName || GENERATE_CLASS_CONTEXT_MENU == commandName)
                    new ClassGenerator(application).LaunchScreenObjectGenerator();
                handled = true;
            }
        }

        /// <summary>Implements the OnConnection method of the IDTExtensibility2 interface. Receives notification that the Add-in is being loaded.</summary>
        /// <param term='application'>Root object of the host application.</param>
        /// <param term='connectMode'>Describes how the Add-in is being loaded.</param>
        /// <param term='addInInst'>Object representing this Add-in.</param>
        /// <seealso class='IDTExtensibility2' />
        public void OnConnection(object applicationObject, ext_ConnectMode connectMode, object addInInst, ref Array custom)
        {
            application = (DTE2) applicationObject;
            addIn = (AddIn) addInInst;
            if (connectMode == ext_ConnectMode.ext_cm_UISetup)
            {
                object[] contextGUIDS = new object[] {};
                Commands2 commands = (Commands2) application.Commands;
                AddCommandsForClassGenerator(commands, contextGUIDS);
            }
        }

        /// <summary>Implements the OnDisconnection method of the IDTExtensibility2 interface. Receives notification that the Add-in is being unloaded.</summary>
        /// <param term='disconnectMode'>Describes how the Add-in is being unloaded.</param>
        /// <param term='custom'>Array of parameters that are host application specific.</param>
        /// <seealso class='IDTExtensibility2' />
        public void OnDisconnection(ext_DisconnectMode disconnectMode, ref Array custom) {}

        /// <summary>Implements the OnAddInsUpdate method of the IDTExtensibility2 interface. Receives notification when the collection of Add-ins has changed.</summary>
        /// <param term='custom'>Array of parameters that are host application specific.</param>
        /// <seealso class='IDTExtensibility2' />		
        public void OnAddInsUpdate(ref Array custom) {}

        /// <summary>Implements the OnStartupComplete method of the IDTExtensibility2 interface. Receives notification that the host application has completed loading.</summary>
        /// <param term='custom'>Array of parameters that are host application specific.</param>
        /// <seealso class='IDTExtensibility2' />
        public void OnStartupComplete(ref Array custom) {}

        /// <summary>Implements the OnBeginShutdown method of the IDTExtensibility2 interface. Receives notification that the host application is being unloaded.</summary>
        /// <param term='custom'>Array of parameters that are host application specific.</param>
        /// <seealso class='IDTExtensibility2' />
        public void OnBeginShutdown(ref Array custom) {}

        private void AddCommandsForClassGenerator(Commands2 commands, object[] contextGUIDS)
        {
            AddCommandToMenu(GetLocalizedVersionOfToolsMenuName(), commands, contextGUIDS);
            AddCommandToContextMenu(contextGUIDS, CLASS_GEN_CONTEXT_MENU_CMD_NAME, CLASS_GEN_CONTEXT_MENU_NAME, CLASS_GEN_CONTEXT_MENU_CMD_DESCIPTION,
                                    PROJECT_COMMAND_BAR);
        }

        private bool activeDocumentIsProject()
        {
            return Helper.IsActiveDocumentAProject(application);
        }

        private void AddCommandToMenu(string toolsMenuName, Commands2 commands, object[] contextGUIDS)
        {
            CommandBar menuCommandBar = GetMenuCommandBar();
            CommandBarPopup toolsPopup = GetToolsPopup(menuCommandBar, toolsMenuName);

            try
            {
                Command command = GetNewCommandAddedToDTECommands2(commands, contextGUIDS);
                AddControlToToolsMenu(command, toolsPopup);
            }
            catch (ArgumentException)
            {
                //If we are here, then the exception is probably because a command with that name
                //  already exists. If so there is no need to recreate the command and we can 
                //  safely ignore the exception.
            }
        }

        private Command GetNewCommandAddedToDTECommands2(Commands2 commands, object[] contextGUIDS)
        {
            return
                commands.AddNamedCommand2(addIn, TOOLS_COMMAND_NAME, RECORDER_ADD_IN_TOOLS_MENU_ITEM_NAME, TOOLS_COMMAND_DESCRIPTION, true, null,
                                          ref contextGUIDS, (int) vsCommandStatus.vsCommandStatusSupported + (int) vsCommandStatus.vsCommandStatusEnabled,
                                          (int) vsCommandStyle.vsCommandStyleText, vsCommandControlType.vsCommandControlTypeButton);
        }

        private Command GetNewCommandAddedToDTECommands(object[] contextGUIDS, string name, string buttonText, string toolTip)
        {
            return
                application.Commands.AddNamedCommand(addIn, name, buttonText, toolTip, true, 0, ref contextGUIDS,
                                                     (int) vsCommandStatus.vsCommandStatusSupported + (int) vsCommandStatus.vsCommandStatusEnabled);
        }

        private void AddControlToToolsMenu(Command command, CommandBarPopup toolsPopup)
        {
            if (command != null && toolsPopup != null)
                command.AddControl(toolsPopup.CommandBar, 1);
        }

        private CommandBarPopup GetToolsPopup(CommandBar menuCommandBar, string toolsMenuName)
        {
            CommandBarControl toolsControl = menuCommandBar.Controls[toolsMenuName];
            return (CommandBarPopup) toolsControl;
        }

        private CommandBar GetMenuCommandBar()
        {
            return ((CommandBars) application.CommandBars)[PROJECT_MENU_BAR];
        }

        private void AddCommandToContextMenu(object[] contextGUIDS, string name, string buttonText, string toolTip, string commandBarIndex)
        {
            try
            {
                Command command = GetNewCommandAddedToDTECommands(contextGUIDS, name, buttonText, toolTip);
                CommandBar commandBar = ((CommandBars) application.CommandBars)[commandBarIndex];
                if (commandBar != null) command.AddControl(commandBar, 1);
                else MessageBox.Show("Cannot get the Project  Commandbar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (ArgumentException) {}
        }

        private string GetLocalizedVersionOfToolsMenuName()
        {
            string toolsMenuName;
            try
            {
                ResourceManager resourceManager = new ResourceManager(RESOURCE_NAME, Assembly.GetExecutingAssembly());
                CultureInfo cultureInfo = new CultureInfo(application.LocaleID);
                string resourceName = String.Concat(cultureInfo.TwoLetterISOLanguageName, TOOLS_MENU_NAME);
                toolsMenuName = resourceManager.GetString(resourceName);
            }
            catch
            {
                toolsMenuName = TOOLS_MENU_NAME;
            }
            return toolsMenuName;
        }
    }
}