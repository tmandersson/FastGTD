using FastGTD.Domain;

namespace FastGTD
{
    partial class ItemListForm<T> where T:GTDItem 
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
            this._item_list = new FastGTD.ItemListControl();
            this.SuspendLayout();
            // 
            // _item_list
            // 
            this._item_list.Dock = System.Windows.Forms.DockStyle.Fill;
            this._item_list.Location = new System.Drawing.Point(0, 0);
            this._item_list.Name = "_item_list";
            this._item_list.Size = new System.Drawing.Size(485, 521);
            this._item_list.TabIndex = 0;
            // 
            // ItemListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(485, 521);
            this.Controls.Add(this._item_list);
            this.Name = "ItemListForm";
            this.ResumeLayout(false);

        }

        #endregion

        protected ItemListControl _item_list;

    }
}