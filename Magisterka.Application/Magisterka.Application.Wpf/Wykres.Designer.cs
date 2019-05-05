namespace Magisterka.Application.Wpf
{
    partial class Wykres
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.WindowsFormsChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.BFindRoute = new System.Windows.Forms.Button();
            this.BReset = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.WindowsFormsChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // WindowsFormsChart
            // 
            chartArea1.Name = "ChartArea1";
            this.WindowsFormsChart.ChartAreas.Add(chartArea1);
            this.WindowsFormsChart.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.WindowsFormsChart.Legends.Add(legend1);
            this.WindowsFormsChart.Location = new System.Drawing.Point(0, 0);
            this.WindowsFormsChart.Name = "WindowsFormsChart";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.WindowsFormsChart.Series.Add(series1);
            this.WindowsFormsChart.Size = new System.Drawing.Size(941, 532);
            this.WindowsFormsChart.TabIndex = 0;
            this.WindowsFormsChart.Text = "chart1";
            this.WindowsFormsChart.Click += new System.EventHandler(this.WindowsFormsChart_Click);
            this.WindowsFormsChart.MouseDown += new System.Windows.Forms.MouseEventHandler(this.WindowsFormsChart_MouseDown);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.BReset);
            this.splitContainer1.Panel1.Controls.Add(this.BFindRoute);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.WindowsFormsChart);
            this.splitContainer1.Size = new System.Drawing.Size(1095, 532);
            this.splitContainer1.SplitterDistance = 150;
            this.splitContainer1.TabIndex = 1;
            // 
            // BFindRoute
            // 
            this.BFindRoute.Location = new System.Drawing.Point(12, 12);
            this.BFindRoute.Name = "BFindRoute";
            this.BFindRoute.Size = new System.Drawing.Size(75, 23);
            this.BFindRoute.TabIndex = 0;
            this.BFindRoute.Text = "Szukaj trasy";
            this.BFindRoute.UseVisualStyleBackColor = true;
            this.BFindRoute.Click += new System.EventHandler(this.BFindRoute_Click);
            // 
            // BReset
            // 
            this.BReset.Location = new System.Drawing.Point(12, 41);
            this.BReset.Name = "BReset";
            this.BReset.Size = new System.Drawing.Size(75, 23);
            this.BReset.TabIndex = 1;
            this.BReset.Text = "Resetuj";
            this.BReset.UseVisualStyleBackColor = true;
            this.BReset.Click += new System.EventHandler(this.BReset_Click);
            // 
            // Wykres
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1095, 532);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Wykres";
            this.Text = "Wykres";
            this.Load += new System.EventHandler(this.Wykres_Load);
            ((System.ComponentModel.ISupportInitialize)(this.WindowsFormsChart)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart WindowsFormsChart;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button BFindRoute;
        private System.Windows.Forms.Button BReset;
    }
}