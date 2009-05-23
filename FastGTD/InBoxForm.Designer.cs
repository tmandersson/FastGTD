namespace FastGTD
{
    partial class InBoxForm
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
            this._list_view = new System.Windows.Forms.ListView();
            this.newItemsHeader = new System.Windows.Forms.ColumnHeader();
            this._textBox = new System.Windows.Forms.TextBox();
            this._buttonAdd = new System.Windows.Forms.Button();
            this._buttonDelete = new System.Windows.Forms.Button();
            this._button_to_action = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // _list_view
            // 
            this._list_view.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._list_view.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.newItemsHeader});
            this._list_view.FullRowSelect = true;
            this._list_view.Location = new System.Drawing.Point(0, 67);
            this._list_view.Name = "_list_view";
            this._list_view.Size = new System.Drawing.Size(485, 413);
            this._list_view.TabIndex = 2;
            this._list_view.UseCompatibleStateImageBehavior = false;
            this._list_view.View = System.Windows.Forms.View.Details;
            // 
            // newItemsHeader
            // 
            this.newItemsHeader.Text = "New items";
            // 
            // _textBox
            // 
            this._textBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._textBox.Location = new System.Drawing.Point(12, 12);
            this._textBox.Name = "_textBox";
            this._textBox.Size = new System.Drawing.Size(380, 20);
            this._textBox.TabIndex = 0;
            // 
            // _buttonAdd
            // 
            this._buttonAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._buttonAdd.Location = new System.Drawing.Point(398, 9);
            this._buttonAdd.Name = "_buttonAdd";
            this._buttonAdd.Size = new System.Drawing.Size(75, 23);
            this._buttonAdd.TabIndex = 1;
            this._buttonAdd.Text = "Add";
            this._buttonAdd.UseVisualStyleBackColor = true;
            // 
            // _buttonDelete
            // 
            this._buttonDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._buttonDelete.Location = new System.Drawing.Point(398, 38);
            this._buttonDelete.Name = "_buttonDelete";
            this._buttonDelete.Size = new System.Drawing.Size(75, 23);
            this._buttonDelete.TabIndex = 3;
            this._buttonDelete.Text = "Delete";
            this._buttonDelete.UseVisualStyleBackColor = true;
            // 
            // _button_to_action
            // 
            this._button_to_action.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._button_to_action.Location = new System.Drawing.Point(12, 486);
            this._button_to_action.Name = "_button_to_action";
            this._button_to_action.Size = new System.Drawing.Size(75, 23);
            this._button_to_action.TabIndex = 4;
            this._button_to_action.Text = "To &Action";
            this._button_to_action.UseVisualStyleBackColor = true;
            // 
            // InBoxForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(485, 521);
            this.Controls.Add(this._button_to_action);
            this.Controls.Add(this._buttonDelete);
            this.Controls.Add(this._buttonAdd);
            this.Controls.Add(this._textBox);
            this.Controls.Add(this._list_view);
            this.Name = "InBoxForm";
            this.Text = "InBoxForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ColumnHeader newItemsHeader;
        private System.Windows.Forms.TextBox _textBox;
        protected System.Windows.Forms.ListView _list_view;
        protected System.Windows.Forms.Button _buttonAdd;
        protected System.Windows.Forms.Button _buttonDelete;
        protected System.Windows.Forms.Button _button_to_action;
    }
}