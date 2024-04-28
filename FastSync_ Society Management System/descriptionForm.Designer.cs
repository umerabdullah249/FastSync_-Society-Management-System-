namespace form1
{
    partial class descriptionForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(descriptionForm));
            this.labelSocietyName = new System.Windows.Forms.Label();
            this.textBoxDescription = new System.Windows.Forms.RichTextBox();
            this.backbuttondesc = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelSocietyName
            // 
            this.labelSocietyName.AutoSize = true;
            this.labelSocietyName.BackColor = System.Drawing.Color.Transparent;
            this.labelSocietyName.Font = new System.Drawing.Font("Bookman Old Style", 25.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSocietyName.ForeColor = System.Drawing.Color.GhostWhite;
            this.labelSocietyName.Location = new System.Drawing.Point(92, 51);
            this.labelSocietyName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelSocietyName.Name = "labelSocietyName";
            this.labelSocietyName.Size = new System.Drawing.Size(267, 41);
            this.labelSocietyName.TabIndex = 0;
            this.labelSocietyName.Text = "Society Name";
            // 
            // textBoxDescription
            // 
            this.textBoxDescription.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.textBoxDescription.Font = new System.Drawing.Font("Times New Roman", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxDescription.Location = new System.Drawing.Point(98, 112);
            this.textBoxDescription.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.Size = new System.Drawing.Size(330, 152);
            this.textBoxDescription.TabIndex = 1;
            this.textBoxDescription.Text = "";
            this.textBoxDescription.TextChanged += new System.EventHandler(this.textBoxDescription_TextChanged);
            // 
            // backbuttondesc
            // 
            this.backbuttondesc.BackColor = System.Drawing.Color.MediumPurple;
            this.backbuttondesc.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.backbuttondesc.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.backbuttondesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.backbuttondesc.Location = new System.Drawing.Point(98, 288);
            this.backbuttondesc.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.backbuttondesc.Name = "backbuttondesc";
            this.backbuttondesc.Size = new System.Drawing.Size(92, 29);
            this.backbuttondesc.TabIndex = 2;
            this.backbuttondesc.Text = "Back";
            this.backbuttondesc.UseVisualStyleBackColor = false;
            this.backbuttondesc.Click += new System.EventHandler(this.backbuttondesc_Click);
            // 
            // descriptionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(600, 366);
            this.Controls.Add(this.backbuttondesc);
            this.Controls.Add(this.textBoxDescription);
            this.Controls.Add(this.labelSocietyName);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "descriptionForm";
            this.Text = "descriptionForm";
            this.Load += new System.EventHandler(this.descriptionForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelSocietyName;
        private System.Windows.Forms.RichTextBox textBoxDescription;
        private System.Windows.Forms.Button backbuttondesc;
    }
}