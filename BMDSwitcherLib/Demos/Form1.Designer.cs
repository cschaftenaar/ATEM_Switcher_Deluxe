namespace Demos
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
            this.b_connect = new System.Windows.Forms.Button();
            this.b_disconnect = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // b_connect
            // 
            this.b_connect.Location = new System.Drawing.Point(12, 238);
            this.b_connect.Name = "b_connect";
            this.b_connect.Size = new System.Drawing.Size(75, 23);
            this.b_connect.TabIndex = 0;
            this.b_connect.Text = "Connect";
            this.b_connect.UseVisualStyleBackColor = true;
            this.b_connect.Click += new System.EventHandler(this.b_connect_Click);
            // 
            // b_disconnect
            // 
            this.b_disconnect.Location = new System.Drawing.Point(93, 238);
            this.b_disconnect.Name = "b_disconnect";
            this.b_disconnect.Size = new System.Drawing.Size(75, 23);
            this.b_disconnect.TabIndex = 1;
            this.b_disconnect.Text = "Disconnect";
            this.b_disconnect.UseVisualStyleBackColor = true;
            this.b_disconnect.Click += new System.EventHandler(this.b_disconnect_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 273);
            this.Controls.Add(this.b_disconnect);
            this.Controls.Add(this.b_connect);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button b_connect;
        private System.Windows.Forms.Button b_disconnect;
    }
}

