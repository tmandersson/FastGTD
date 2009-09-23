namespace FastGTD
{
    partial class ItemListForm<T>
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
            this._new_items_header = new System.Windows.Forms.ColumnHeader();
            this._text_box = new System.Windows.Forms.TextBox();
            this._add_button = new System.Windows.Forms.Button();
            this._delete_button = new System.Windows.Forms.Button();
            this._to_action_button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // _list_view
            // 
            this._list_view.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._list_view.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this._new_items_header});
            this._list_view.FullRowSelect = true;
            this._list_view.Location = new System.Drawing.Point(0, 67);
            this._list_view.Name = "_list_view";
            this._list_view.Size = new System.Drawing.Size(485, 413);
            this._list_view.TabIndex = 2;
            this._list_view.UseCompatibleStateImageBehavior = false;
            this._list_view.View = System.Windows.Forms.View.Details;
            // 
            // _new_items_header
            // 
            this._new_items_header.Text = "New items";
            // 
            // _text_box
            // 
            this._text_box.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._text_box.Location = new System.Drawing.Point(12, 12);
            this._text_box.Name = "_text_box";
            this._text_box.Size = new System.Drawing.Size(380, 20);
            this._text_box.TabIndex = 0;
            // 
            // _add_button
            // 
            this._add_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._add_button.Location = new System.Drawing.Point(398, 9);
            this._add_button.Name = "_add_button";
            this._add_button.Size = new System.Drawing.Size(75, 23);
            this._add_button.TabIndex = 1;
            this._add_button.Text = "Add";
            this._add_button.UseVisualStyleBackColor = true;
            // 
            // _delete_button
            // 
            this._delete_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._delete_button.Location = new System.Drawing.Point(398, 38);
            this._delete_button.Name = "_delete_button";
            this._delete_button.Size = new System.Drawing.Size(75, 23);
            this._delete_button.TabIndex = 3;
            this._delete_button.Text = "Delete";
            this._delete_button.UseVisualStyleBackColor = true;
            // 
            // _to_action_button
            // 
            this._to_action_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._to_action_button.Location = new System.Drawing.Point(12, 486);
            this._to_action_button.Name = "_to_action_button";
            this._to_action_button.Size = new System.Drawing.Size(75, 23);
            this._to_action_button.TabIndex = 4;
            this._to_action_button.Text = "To &Action";
            this._to_action_button.UseVisualStyleBackColor = true;
            // 
            // InBoxForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(485, 521);
            this.Controls.Add(this._to_action_button);
            this.Controls.Add(this._delete_button);
            this.Controls.Add(this._add_button);
            this.Controls.Add(this._text_box);
            this.Controls.Add(this._list_view);
            this.Name = "InBoxForm";
            this.Text = "InBoxForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ColumnHeader _new_items_header;
        private System.Windows.Forms.TextBox _text_box;
        protected System.Windows.Forms.ListView _list_view;
        protected System.Windows.Forms.Button _add_button;
        protected System.Windows.Forms.Button _delete_button;
        protected System.Windows.Forms.Button _to_action_button;
    }
}