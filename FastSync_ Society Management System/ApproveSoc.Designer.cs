namespace FastSync1
{
    partial class ApproveSoc
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.cancelbuttonbutton1 = new System.Windows.Forms.Button();
            this.rejectcolumn = new System.Windows.Forms.DataGridViewButtonColumn();
            this.approvecolumn = new System.Windows.Forms.DataGridViewButtonColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(-5, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(812, 57);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(297, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(188, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "Societies Requests";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.rejectcolumn,
            this.approvecolumn});
            this.dataGridView1.Location = new System.Drawing.Point(104, 76);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(684, 225);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // cancelbuttonbutton1
            // 
            this.cancelbuttonbutton1.Location = new System.Drawing.Point(385, 368);
            this.cancelbuttonbutton1.Name = "cancelbuttonbutton1";
            this.cancelbuttonbutton1.Size = new System.Drawing.Size(122, 23);
            this.cancelbuttonbutton1.TabIndex = 2;
            this.cancelbuttonbutton1.Text = "Cancel";
            this.cancelbuttonbutton1.UseVisualStyleBackColor = true;
            this.cancelbuttonbutton1.Click += new System.EventHandler(this.cancelbuttonbutton1_Click);
            // 
            // rejectcolumn
            // 
            this.rejectcolumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.NullValue = "Reject";
            this.rejectcolumn.DefaultCellStyle = dataGridViewCellStyle1;
            this.rejectcolumn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.rejectcolumn.HeaderText = "Reject";
            this.rejectcolumn.Name = "rejectcolumn";
            this.rejectcolumn.Text = "0";
            this.rejectcolumn.Width = 49;
            // 
            // approvecolumn
            // 
            this.approvecolumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            dataGridViewCellStyle2.NullValue = "Approve";
            this.approvecolumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.approvecolumn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.approvecolumn.HeaderText = "Approve";
            this.approvecolumn.Name = "approvecolumn";
            this.approvecolumn.Text = "1";
            this.approvecolumn.Width = 58;
            // 
            // ApproveSoc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::FastSync1.Properties.Resources._5166950;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.cancelbuttonbutton1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.panel1);
            this.Name = "ApproveSoc";
            this.Text = "ApproveSoc";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button cancelbuttonbutton1;
        private System.Windows.Forms.DataGridViewButtonColumn rejectcolumn;
        private System.Windows.Forms.DataGridViewButtonColumn approvecolumn;
    }
}