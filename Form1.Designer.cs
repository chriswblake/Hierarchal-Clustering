namespace HierarchalClustering
{
    partial class Form1
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
            System.Windows.Forms.Label lblImageCount;
            this.canvasInput = new Handwriting.Canvas();
            this.btnAddImage = new System.Windows.Forms.Button();
            this.tbImageCount = new System.Windows.Forms.TextBox();
            this.trbarPenSize = new System.Windows.Forms.TrackBar();
            this.btnClearCanvas = new System.Windows.Forms.Button();
            this.pboxOutput = new System.Windows.Forms.PictureBox();
            this.chartDendo = new HierarchalClustering.Dendrogram();
            this.tbResults = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnReset = new System.Windows.Forms.Button();
            lblImageCount = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.trbarPenSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pboxOutput)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblImageCount
            // 
            lblImageCount.AutoSize = true;
            lblImageCount.Location = new System.Drawing.Point(134, 29);
            lblImageCount.Name = "lblImageCount";
            lblImageCount.Size = new System.Drawing.Size(38, 13);
            lblImageCount.TabIndex = 2;
            lblImageCount.Text = "Count:";
            // 
            // canvasInput
            // 
            this.canvasInput.BackColor = System.Drawing.Color.White;
            this.canvasInput.Location = new System.Drawing.Point(3, 3);
            this.canvasInput.MaximumSize = new System.Drawing.Size(128, 128);
            this.canvasInput.MinimumSize = new System.Drawing.Size(128, 128);
            this.canvasInput.Name = "canvasInput";
            this.canvasInput.PenSize = 1;
            this.canvasInput.Size = new System.Drawing.Size(128, 128);
            this.canvasInput.TabIndex = 0;
            // 
            // btnAddImage
            // 
            this.btnAddImage.Location = new System.Drawing.Point(137, 3);
            this.btnAddImage.Name = "btnAddImage";
            this.btnAddImage.Size = new System.Drawing.Size(75, 23);
            this.btnAddImage.TabIndex = 1;
            this.btnAddImage.Text = "Add";
            this.btnAddImage.UseVisualStyleBackColor = true;
            this.btnAddImage.Click += new System.EventHandler(this.btnAddImage_Click);
            // 
            // tbImageCount
            // 
            this.tbImageCount.Location = new System.Drawing.Point(172, 29);
            this.tbImageCount.Name = "tbImageCount";
            this.tbImageCount.ReadOnly = true;
            this.tbImageCount.Size = new System.Drawing.Size(40, 20);
            this.tbImageCount.TabIndex = 3;
            // 
            // trbarPenSize
            // 
            this.trbarPenSize.Location = new System.Drawing.Point(3, 137);
            this.trbarPenSize.Maximum = 5;
            this.trbarPenSize.Minimum = 1;
            this.trbarPenSize.Name = "trbarPenSize";
            this.trbarPenSize.Size = new System.Drawing.Size(68, 45);
            this.trbarPenSize.TabIndex = 4;
            this.trbarPenSize.Value = 1;
            this.trbarPenSize.Visible = false;
            this.trbarPenSize.Scroll += new System.EventHandler(this.trbarPenSize_Scroll);
            // 
            // btnClearCanvas
            // 
            this.btnClearCanvas.Location = new System.Drawing.Point(77, 137);
            this.btnClearCanvas.Name = "btnClearCanvas";
            this.btnClearCanvas.Size = new System.Drawing.Size(54, 23);
            this.btnClearCanvas.TabIndex = 5;
            this.btnClearCanvas.Text = "Clear";
            this.btnClearCanvas.UseVisualStyleBackColor = true;
            this.btnClearCanvas.Click += new System.EventHandler(this.btnClearCanvas_Click);
            // 
            // pboxOutput
            // 
            this.pboxOutput.BackColor = System.Drawing.Color.White;
            this.pboxOutput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pboxOutput.Location = new System.Drawing.Point(218, 3);
            this.pboxOutput.Name = "pboxOutput";
            this.pboxOutput.Size = new System.Drawing.Size(128, 128);
            this.pboxOutput.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pboxOutput.TabIndex = 6;
            this.pboxOutput.TabStop = false;
            // 
            // chartDendo
            // 
            this.chartDendo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartDendo.Location = new System.Drawing.Point(368, 3);
            this.chartDendo.Name = "chartDendo";
            this.tableLayoutPanel1.SetRowSpan(this.chartDendo, 2);
            this.chartDendo.Size = new System.Drawing.Size(665, 521);
            this.chartDendo.TabIndex = 7;
            // 
            // tbResults
            // 
            this.tbResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbResults.Location = new System.Drawing.Point(3, 203);
            this.tbResults.Multiline = true;
            this.tbResults.Name = "tbResults";
            this.tbResults.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbResults.Size = new System.Drawing.Size(359, 321);
            this.tbResults.TabIndex = 8;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.chartDendo, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.tbResults, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1036, 527);
            this.tableLayoutPanel1.TabIndex = 9;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnReset);
            this.panel1.Controls.Add(this.canvasInput);
            this.panel1.Controls.Add(this.btnAddImage);
            this.panel1.Controls.Add(lblImageCount);
            this.panel1.Controls.Add(this.pboxOutput);
            this.panel1.Controls.Add(this.tbImageCount);
            this.panel1.Controls.Add(this.btnClearCanvas);
            this.panel1.Controls.Add(this.trbarPenSize);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(359, 194);
            this.panel1.TabIndex = 0;
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(218, 137);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(128, 43);
            this.btnReset.TabIndex = 7;
            this.btnReset.Text = "Reset All";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1036, 527);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Form1";
            this.ShowIcon = false;
            this.Text = "Final Project - Hierarchical Clustering";
            ((System.ComponentModel.ISupportInitialize)(this.trbarPenSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pboxOutput)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Handwriting.Canvas canvasInput;
        private System.Windows.Forms.Button btnAddImage;
        private System.Windows.Forms.TextBox tbImageCount;
        private System.Windows.Forms.TrackBar trbarPenSize;
        private System.Windows.Forms.Button btnClearCanvas;
        private System.Windows.Forms.PictureBox pboxOutput;
        private Dendrogram chartDendo;
        private System.Windows.Forms.TextBox tbResults;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnReset;
    }
}

