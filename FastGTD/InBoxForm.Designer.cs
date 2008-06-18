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
            this.listViewInBoxItems = new System.Windows.Forms.ListView();
            this.textBox = new System.Windows.Forms.TextBox();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listViewInBoxItems
            // 
            this.listViewInBoxItems.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.listViewInBoxItems.Location = new System.Drawing.Point(0, 59);
            this.listViewInBoxItems.Name = "listViewInBoxItems";
            this.listViewInBoxItems.Size = new System.Drawing.Size(485, 462);
            this.listViewInBoxItems.TabIndex = 2;
            this.listViewInBoxItems.UseCompatibleStateImageBehavior = false;
            this.listViewInBoxItems.View = System.Windows.Forms.View.List;
            // 
            // textBox
            // 
            this.textBox.Location = new System.Drawing.Point(12, 12);
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size(380, 20);
            this.textBox.TabIndex = 0;
            // 
            // buttonAdd
            // 
            this.buttonAdd.Location = new System.Drawing.Point(398, 9);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(75, 23);
            this.buttonAdd.TabIndex = 1;
            this.buttonAdd.Text = "Add";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // InBoxForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(485, 521);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.textBox);
            this.Controls.Add(this.listViewInBoxItems);
            this.Name = "InBoxForm";
            this.Text = "InBoxForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox textBox;
        public System.Windows.Forms.ListView listViewInBoxItems;
        public System.Windows.Forms.Button buttonAdd;
    }
}