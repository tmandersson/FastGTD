using FastGTD.DataTransfer;

namespace FastGTD
{
    partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this._tab_control = new System.Windows.Forms.TabControl();
            this._inbox_tab = new System.Windows.Forms.TabPage();
            this._inbox_controls = new FastGTD.ItemListControl();
            this._actions_tab = new System.Windows.Forms.TabPage();
            this._actions_controls = new FastGTD.ItemListControl();
            this._tab_control.SuspendLayout();
            this._inbox_tab.SuspendLayout();
            this._actions_tab.SuspendLayout();
            this.SuspendLayout();
            // 
            // _tab_control
            // 
            this._tab_control.Controls.Add(this._inbox_tab);
            this._tab_control.Controls.Add(this._actions_tab);
            this._tab_control.Dock = System.Windows.Forms.DockStyle.Fill;
            this._tab_control.Location = new System.Drawing.Point(0, 0);
            this._tab_control.Name = "_tab_control";
            this._tab_control.SelectedIndex = 0;
            this._tab_control.Size = new System.Drawing.Size(521, 549);
            this._tab_control.TabIndex = 0;
            // 
            // _inbox_tab
            // 
            this._inbox_tab.Controls.Add(this._inbox_controls);
            this._inbox_tab.Location = new System.Drawing.Point(4, 22);
            this._inbox_tab.Name = "_inbox_tab";
            this._inbox_tab.Padding = new System.Windows.Forms.Padding(3);
            this._inbox_tab.Size = new System.Drawing.Size(513, 523);
            this._inbox_tab.TabIndex = 0;
            this._inbox_tab.Text = "InBox";
            this._inbox_tab.UseVisualStyleBackColor = true;
            // 
            // _inbox_controls
            // 
            this._inbox_controls.Dock = System.Windows.Forms.DockStyle.Fill;
            this._inbox_controls.Location = new System.Drawing.Point(3, 3);
            this._inbox_controls.Name = "_inbox_controls";
            this._inbox_controls.Size = new System.Drawing.Size(507, 517);
            this._inbox_controls.TabIndex = 0;
            // 
            // _actions_tab
            // 
            this._actions_tab.Controls.Add(this._actions_controls);
            this._actions_tab.Location = new System.Drawing.Point(4, 22);
            this._actions_tab.Name = "_actions_tab";
            this._actions_tab.Padding = new System.Windows.Forms.Padding(3);
            this._actions_tab.Size = new System.Drawing.Size(513, 523);
            this._actions_tab.TabIndex = 1;
            this._actions_tab.Text = "Actions";
            this._actions_tab.UseVisualStyleBackColor = true;
            // 
            // _actions_controls
            // 
            this._actions_controls.Dock = System.Windows.Forms.DockStyle.Fill;
            this._actions_controls.Location = new System.Drawing.Point(3, 3);
            this._actions_controls.Name = "_actions_controls";
            this._actions_controls.Size = new System.Drawing.Size(507, 517);
            this._actions_controls.TabIndex = 0;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(521, 549);
            this.Controls.Add(this._tab_control);
            this.Name = "MainWindow";
            this.Text = "FastGTD";
            this._tab_control.ResumeLayout(false);
            this._inbox_tab.ResumeLayout(false);
            this._actions_tab.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl _tab_control;
        private System.Windows.Forms.TabPage _inbox_tab;
        private System.Windows.Forms.TabPage _actions_tab;
        private ItemListControl _inbox_controls;
        private ItemListControl _actions_controls;
    }
}