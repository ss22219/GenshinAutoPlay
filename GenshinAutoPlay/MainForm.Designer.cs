namespace GenshinAutoPlay
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            GenshinAutoPlay.HotKey hotKey1 = new GenshinAutoPlay.HotKey();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.numKeySpeed = new System.Windows.Forms.NumericUpDown();
            this.numSpaceSpeed = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lbCurrent = new System.Windows.Forms.Label();
            this.hotKeyTextBox1 = new GenshinAutoPlay.HotKeyTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.numKeySpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSpaceSpeed)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(600, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "启动热键";
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(12, 77);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(776, 361);
            this.textBox1.TabIndex = 2;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // numKeySpeed
            // 
            this.numKeySpeed.Location = new System.Drawing.Point(93, 15);
            this.numKeySpeed.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numKeySpeed.Name = "numKeySpeed";
            this.numKeySpeed.Size = new System.Drawing.Size(88, 21);
            this.numKeySpeed.TabIndex = 3;
            this.numKeySpeed.Value = new decimal(new int[] {
            80,
            0,
            0,
            0});
            this.numKeySpeed.ValueChanged += new System.EventHandler(this.numKeySpeed_ValueChanged);
            // 
            // numSpaceSpeed
            // 
            this.numSpaceSpeed.Location = new System.Drawing.Point(263, 15);
            this.numSpaceSpeed.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numSpaceSpeed.Name = "numSpaceSpeed";
            this.numSpaceSpeed.Size = new System.Drawing.Size(92, 21);
            this.numSpaceSpeed.TabIndex = 3;
            this.numSpaceSpeed.Value = new decimal(new int[] {
            180,
            0,
            0,
            0});
            this.numSpaceSpeed.ValueChanged += new System.EventHandler(this.numSpaceSpeed_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(34, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "按键间隔";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(204, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "空音间隔";
            // 
            // lbCurrent
            // 
            this.lbCurrent.AutoSize = true;
            this.lbCurrent.Location = new System.Drawing.Point(36, 46);
            this.lbCurrent.Name = "lbCurrent";
            this.lbCurrent.Size = new System.Drawing.Size(0, 12);
            this.lbCurrent.TabIndex = 5;
            // 
            // hotKeyTextBox1
            // 
            hotKey1.Key = System.Windows.Forms.Keys.None;
            hotKey1.Modifiers = GenshinAutoPlay.KeyModifiers.None;
            this.hotKeyTextBox1.HotKey_ = hotKey1;
            this.hotKeyTextBox1.Location = new System.Drawing.Point(659, 17);
            this.hotKeyTextBox1.Name = "hotKeyTextBox1";
            this.hotKeyTextBox1.Size = new System.Drawing.Size(113, 21);
            this.hotKeyTextBox1.TabIndex = 0;
            this.hotKeyTextBox1.Text = "None";
            this.hotKeyTextBox1.TextChanged += new System.EventHandler(this.hotKeyTextBox1_TextChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lbCurrent);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.numSpaceSpeed);
            this.Controls.Add(this.numKeySpeed);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.hotKeyTextBox1);
            this.Name = "MainForm";
            this.Text = "GenshinAutoPlay";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numKeySpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSpaceSpeed)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private HotKeyTextBox hotKeyTextBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.NumericUpDown numKeySpeed;
        private System.Windows.Forms.NumericUpDown numSpaceSpeed;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbCurrent;
    }
}

