using MunicipalityApplicatiion.UI;
using MunicipalityApplicatiion.Forms; // ServiceRequestStatusForm, ReportIssue, LocalEvents
using System;
using System.Drawing;
using System.Windows.Forms;

namespace MunicipalityApplicatiion.UI
{
    partial class MainMenu : Form
    {
        private System.ComponentModel.IContainer components = null;

        // Designer fields
        private Panel panel1;
        private TextBox textBox1;
        private PictureBox pictureBox1;
        private TextBox textBox2;
        private TextBox textBox3;
        private Button button1;
        private Button button2;
        private Button button3;
        private Button buttonViewReports; // Add this field to match the other buttons
        private PictureBox pictureBox2;
        private PictureBox pictureBox3;
        private PictureBox pictureBox4;
        private PictureBox pictureBox5;
        private PictureBox pictureBox6;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            panel1 = new Panel();
            textBox3 = new TextBox();
            textBox2 = new TextBox();
            textBox1 = new TextBox();
            pictureBox1 = new PictureBox();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            buttonViewReports = new Button();
            pictureBox2 = new PictureBox();
            pictureBox3 = new PictureBox();
            pictureBox4 = new PictureBox();
            pictureBox5 = new PictureBox();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.DarkSeaGreen;
            panel1.Controls.Add(textBox3);
            panel1.Controls.Add(textBox2);
            panel1.Controls.Add(textBox1);
            panel1.Controls.Add(pictureBox1);
            panel1.Location = new Point(1, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(390, 676);
            panel1.TabIndex = 0;
            // 
            // textBox3
            // 
            textBox3.BackColor = Color.DarkSeaGreen;
            textBox3.BorderStyle = BorderStyle.None;
            textBox3.Font = new Font("Verdana", 10F, FontStyle.Italic, GraphicsUnit.Point, 0);
            textBox3.Location = new Point(4, 597);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(383, 17);
            textBox3.TabIndex = 5;
            textBox3.Text = "We Care, We Serve, We Improve, We Innovate.";
            textBox3.TextAlign = HorizontalAlignment.Center;
            // 
            // textBox2
            // 
            textBox2.BackColor = Color.DarkSeaGreen;
            textBox2.BorderStyle = BorderStyle.None;
            textBox2.Font = new Font("Verdana", 20F, FontStyle.Bold);
            textBox2.ForeColor = Color.Black;
            textBox2.Location = new Point(11, 461);
            textBox2.Multiline = true;
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(369, 65);
            textBox2.TabIndex = 4;
            textBox2.Text = "MUNICIPAL SERVICE PLATFORM";
            textBox2.TextAlign = HorizontalAlignment.Center;
            // 
            // textBox1
            // 
            textBox1.BackColor = Color.DarkSeaGreen;
            textBox1.BorderStyle = BorderStyle.None;
            textBox1.Font = new Font("Verdana", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            textBox1.Location = new Point(90, 440);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(207, 17);
            textBox1.TabIndex = 3;
            textBox1.Text = "Welcome to the";
            textBox1.TextAlign = HorizontalAlignment.Center;
            // 
            // pictureBox1
            // 
            pictureBox1.BackgroundImageLayout = ImageLayout.None;
            pictureBox1.Image = Properties.Resources.MunicipalityCover;
            pictureBox1.Location = new Point(0, 67);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(387, 316);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 2;
            pictureBox1.TabStop = false;
            // 
            // button1
            // 
            button1.BackgroundImageLayout = ImageLayout.Center;
            button1.Font = new Font("Verdana", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button1.Location = new Point(538, 103);
            button1.Name = "button1";
            button1.Size = new Size(328, 85);
            button1.TabIndex = 1;
            button1.Text = "Report Issues";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Font = new Font("Verdana", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button2.Location = new Point(538, 347);
            button2.Name = "button2";
            button2.Size = new Size(328, 85);
            button2.TabIndex = 2;
            button2.Text = "Local Events and Announcements";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Font = new Font("Verdana", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button3.Location = new Point(538, 472);
            button3.Name = "button3";
            button3.Size = new Size(328, 85);
            button3.TabIndex = 3;
            button3.Text = "Service Request Status";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // buttonViewReports
            // 
            buttonViewReports.Font = new Font("Verdana", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            buttonViewReports.Location = new Point(538, 225);
            buttonViewReports.Name = "buttonViewReports";
            buttonViewReports.Size = new Size(328, 85);
            buttonViewReports.TabIndex = 2;
            buttonViewReports.Text = "View Reports";
            buttonViewReports.UseVisualStyleBackColor = true;
            buttonViewReports.Click += buttonViewReports_Click;
            // 
            // pictureBox2
            // 
            pictureBox2.BackgroundImageLayout = ImageLayout.Zoom;
            pictureBox2.Image = Properties.Resources.Report;
            pictureBox2.Location = new Point(473, 103);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(87, 85);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 4;
            pictureBox2.TabStop = false;
            // 
            // pictureBox3
            // 
            pictureBox3.Image = Properties.Resources.Announcement;
            pictureBox3.Location = new Point(473, 347);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(87, 85);
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox3.TabIndex = 5;
            pictureBox3.TabStop = false;
            pictureBox3.Click += pictureBox3_Click;
            // 
            // pictureBox4
            // 
            pictureBox4.Image = Properties.Resources.Status;
            pictureBox4.Location = new Point(473, 472);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(87, 85);
            pictureBox4.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox4.TabIndex = 6;
            pictureBox4.TabStop = false;
            pictureBox4.Click += pictureBox4_Click;
            // 
            // pictureBox5
            // 
            pictureBox5.Image = Properties.Resources.ViewReport;
            pictureBox5.Location = new Point(473, 225);
            pictureBox5.Name = "pictureBox5";
            pictureBox5.Size = new Size(87, 85);
            pictureBox5.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox5.TabIndex = 8;
            pictureBox5.TabStop = false;
            // 
            // MainMenu
            // 
            AutoScaleDimensions = new SizeF(8F, 14F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            BackColor = Color.OldLace;
            BackgroundImageLayout = ImageLayout.None;
            ClientSize = new Size(914, 676);
            Controls.Add(pictureBox5);
            Controls.Add(buttonViewReports);
            Controls.Add(pictureBox4);
            Controls.Add(pictureBox3);
            Controls.Add(pictureBox2);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(panel1);
            Font = new Font("Verdana", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            MaximizeBox = false;
            Name = "MainMenu";
            Padding = new Padding(2);
            RightToLeft = RightToLeft.No;
            RightToLeftLayout = true;
            Text = "South Africa Municipal Services - Main Menu";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).EndInit();
            ResumeLayout(false);
        }
        #endregion
        //private PictureBox pictureBox6;
    }
}
