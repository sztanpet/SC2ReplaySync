namespace SC2ReplaySync
{
    partial class MainGUI
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
            this.ApplicationLabel = new System.Windows.Forms.Label();
            this.IPAddressLabel = new System.Windows.Forms.Label();
            this.IPTextBox = new System.Windows.Forms.TextBox();
            this.PortLabel = new System.Windows.Forms.Label();
            this.PortTextBox = new System.Windows.Forms.TextBox();
            this.CreateServerButton = new System.Windows.Forms.Button();
            this.ConnectButton = new System.Windows.Forms.Button();
            this.StartReplayButton = new System.Windows.Forms.Button();
            this.StatusStrip = new System.Windows.Forms.StatusStrip();
            this.ToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.ConnParamsTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.StartButtonTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.AppLabelTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.ControlButtonsTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.LogTextBox = new System.Windows.Forms.TextBox();
            this.StatusStrip.SuspendLayout();
            this.ConnParamsTableLayoutPanel.SuspendLayout();
            this.StartButtonTableLayoutPanel.SuspendLayout();
            this.AppLabelTableLayoutPanel.SuspendLayout();
            this.ControlButtonsTableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // ApplicationLabel
            // 
            this.ApplicationLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ApplicationLabel.AutoSize = true;
            this.ApplicationLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.ApplicationLabel.Location = new System.Drawing.Point(3, 0);
            this.ApplicationLabel.Name = "ApplicationLabel";
            this.ApplicationLabel.Size = new System.Drawing.Size(325, 24);
            this.ApplicationLabel.TabIndex = 0;
            this.ApplicationLabel.Text = "SC2ReplaySync";
            this.ApplicationLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // IPAddressLabel
            // 
            this.IPAddressLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.IPAddressLabel.AutoSize = true;
            this.IPAddressLabel.Location = new System.Drawing.Point(3, 0);
            this.IPAddressLabel.Name = "IPAddressLabel";
            this.IPAddressLabel.Size = new System.Drawing.Size(64, 30);
            this.IPAddressLabel.TabIndex = 2;
            this.IPAddressLabel.Text = "IP address:";
            // 
            // IPTextBox
            // 
            this.IPTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.IPTextBox.Location = new System.Drawing.Point(73, 3);
            this.IPTextBox.Name = "IPTextBox";
            this.IPTextBox.Size = new System.Drawing.Size(99, 20);
            this.IPTextBox.TabIndex = 3;
            // 
            // PortLabel
            // 
            this.PortLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.PortLabel.AutoSize = true;
            this.PortLabel.Location = new System.Drawing.Point(178, 0);
            this.PortLabel.Name = "PortLabel";
            this.PortLabel.Size = new System.Drawing.Size(44, 30);
            this.PortLabel.TabIndex = 4;
            this.PortLabel.Text = "Port:";
            // 
            // PortTextBox
            // 
            this.PortTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.PortTextBox.Location = new System.Drawing.Point(228, 3);
            this.PortTextBox.Name = "PortTextBox";
            this.PortTextBox.Size = new System.Drawing.Size(100, 20);
            this.PortTextBox.TabIndex = 5;
            // 
            // CreateServerButton
            // 
            this.CreateServerButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.CreateServerButton.Location = new System.Drawing.Point(3, 3);
            this.CreateServerButton.Name = "CreateServerButton";
            this.CreateServerButton.Size = new System.Drawing.Size(159, 22);
            this.CreateServerButton.TabIndex = 6;
            this.CreateServerButton.Text = "Create server";
            this.CreateServerButton.UseVisualStyleBackColor = true;
            this.CreateServerButton.Click += new System.EventHandler(this.CreateServerButton_Click);
            // 
            // ConnectButton
            // 
            this.ConnectButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ConnectButton.Location = new System.Drawing.Point(168, 3);
            this.ConnectButton.Name = "ConnectButton";
            this.ConnectButton.Size = new System.Drawing.Size(160, 22);
            this.ConnectButton.TabIndex = 7;
            this.ConnectButton.Text = "Connect";
            this.ConnectButton.UseVisualStyleBackColor = true;
            this.ConnectButton.Click += new System.EventHandler(this.ConnectButton_Click);
            // 
            // StartReplayButton
            // 
            this.StartReplayButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.StartReplayButton.Location = new System.Drawing.Point(3, 3);
            this.StartReplayButton.Name = "StartReplayButton";
            this.StartReplayButton.Size = new System.Drawing.Size(325, 23);
            this.StartReplayButton.TabIndex = 8;
            this.StartReplayButton.Text = "Start replay";
            this.StartReplayButton.UseVisualStyleBackColor = true;
            this.StartReplayButton.Click += new System.EventHandler(this.StartReplayButton_Click);
            // 
            // StatusStrip
            // 
            this.StatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripStatusLabel});
            this.StatusStrip.Location = new System.Drawing.Point(0, 318);
            this.StatusStrip.Name = "StatusStrip";
            this.StatusStrip.Size = new System.Drawing.Size(331, 22);
            this.StatusStrip.TabIndex = 9;
            // 
            // ToolStripStatusLabel
            // 
            this.ToolStripStatusLabel.Name = "ToolStripStatusLabel";
            this.ToolStripStatusLabel.Size = new System.Drawing.Size(115, 17);
            this.ToolStripStatusLabel.Text = "ToolStripStatusLabel";
            // 
            // ConnParamsTableLayoutPanel
            // 
            this.ConnParamsTableLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ConnParamsTableLayoutPanel.ColumnCount = 4;
            this.ConnParamsTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.ConnParamsTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.ConnParamsTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.ConnParamsTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.ConnParamsTableLayoutPanel.Controls.Add(this.IPAddressLabel, 0, 0);
            this.ConnParamsTableLayoutPanel.Controls.Add(this.IPTextBox, 1, 0);
            this.ConnParamsTableLayoutPanel.Controls.Add(this.PortLabel, 2, 0);
            this.ConnParamsTableLayoutPanel.Controls.Add(this.PortTextBox, 3, 0);
            this.ConnParamsTableLayoutPanel.Location = new System.Drawing.Point(0, 30);
            this.ConnParamsTableLayoutPanel.Name = "ConnParamsTableLayoutPanel";
            this.ConnParamsTableLayoutPanel.RowCount = 1;
            this.ConnParamsTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.ConnParamsTableLayoutPanel.Size = new System.Drawing.Size(331, 30);
            this.ConnParamsTableLayoutPanel.TabIndex = 10;
            // 
            // StartButtonTableLayoutPanel
            // 
            this.StartButtonTableLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.StartButtonTableLayoutPanel.ColumnCount = 1;
            this.StartButtonTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.StartButtonTableLayoutPanel.Controls.Add(this.StartReplayButton, 0, 0);
            this.StartButtonTableLayoutPanel.Location = new System.Drawing.Point(0, 86);
            this.StartButtonTableLayoutPanel.Name = "StartButtonTableLayoutPanel";
            this.StartButtonTableLayoutPanel.RowCount = 1;
            this.StartButtonTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.StartButtonTableLayoutPanel.Size = new System.Drawing.Size(331, 29);
            this.StartButtonTableLayoutPanel.TabIndex = 11;
            // 
            // AppLabelTableLayoutPanel
            // 
            this.AppLabelTableLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.AppLabelTableLayoutPanel.ColumnCount = 1;
            this.AppLabelTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.AppLabelTableLayoutPanel.Controls.Add(this.ApplicationLabel, 0, 0);
            this.AppLabelTableLayoutPanel.Location = new System.Drawing.Point(0, 3);
            this.AppLabelTableLayoutPanel.Name = "AppLabelTableLayoutPanel";
            this.AppLabelTableLayoutPanel.RowCount = 1;
            this.AppLabelTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.AppLabelTableLayoutPanel.Size = new System.Drawing.Size(331, 24);
            this.AppLabelTableLayoutPanel.TabIndex = 12;
            // 
            // ControlButtonsTableLayoutPanel
            // 
            this.ControlButtonsTableLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ControlButtonsTableLayoutPanel.ColumnCount = 2;
            this.ControlButtonsTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.ControlButtonsTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.ControlButtonsTableLayoutPanel.Controls.Add(this.CreateServerButton, 0, 0);
            this.ControlButtonsTableLayoutPanel.Controls.Add(this.ConnectButton, 1, 0);
            this.ControlButtonsTableLayoutPanel.Location = new System.Drawing.Point(0, 59);
            this.ControlButtonsTableLayoutPanel.Name = "ControlButtonsTableLayoutPanel";
            this.ControlButtonsTableLayoutPanel.RowCount = 1;
            this.ControlButtonsTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.ControlButtonsTableLayoutPanel.Size = new System.Drawing.Size(331, 28);
            this.ControlButtonsTableLayoutPanel.TabIndex = 13;
            // 
            // LogTextBox
            // 
            this.LogTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.LogTextBox.Location = new System.Drawing.Point(0, 115);
            this.LogTextBox.Multiline = true;
            this.LogTextBox.Name = "LogTextBox";
            this.LogTextBox.ReadOnly = true;
            this.LogTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.LogTextBox.Size = new System.Drawing.Size(328, 200);
            this.LogTextBox.TabIndex = 14;
            // 
            // MainGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(331, 340);
            this.Controls.Add(this.LogTextBox);
            this.Controls.Add(this.ControlButtonsTableLayoutPanel);
            this.Controls.Add(this.AppLabelTableLayoutPanel);
            this.Controls.Add(this.StartButtonTableLayoutPanel);
            this.Controls.Add(this.ConnParamsTableLayoutPanel);
            this.Controls.Add(this.StatusStrip);
            this.Name = "MainGUI";
            this.Text = "SC2ReplaySync";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_Closing);
            this.Load += new System.EventHandler(this.MainGUI_Load);
            this.StatusStrip.ResumeLayout(false);
            this.StatusStrip.PerformLayout();
            this.ConnParamsTableLayoutPanel.ResumeLayout(false);
            this.ConnParamsTableLayoutPanel.PerformLayout();
            this.StartButtonTableLayoutPanel.ResumeLayout(false);
            this.AppLabelTableLayoutPanel.ResumeLayout(false);
            this.AppLabelTableLayoutPanel.PerformLayout();
            this.ControlButtonsTableLayoutPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label ApplicationLabel;
        private System.Windows.Forms.Label IPAddressLabel;
        private System.Windows.Forms.TextBox IPTextBox;
        private System.Windows.Forms.Label PortLabel;
        private System.Windows.Forms.TextBox PortTextBox;
        private System.Windows.Forms.Button CreateServerButton;
        private System.Windows.Forms.Button ConnectButton;
        private System.Windows.Forms.Button StartReplayButton;
        private System.Windows.Forms.StatusStrip StatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel ToolStripStatusLabel;
        private System.Windows.Forms.TableLayoutPanel ConnParamsTableLayoutPanel;
        private System.Windows.Forms.TableLayoutPanel StartButtonTableLayoutPanel;
        private System.Windows.Forms.TableLayoutPanel AppLabelTableLayoutPanel;
        private System.Windows.Forms.TableLayoutPanel ControlButtonsTableLayoutPanel;
        private System.Windows.Forms.TextBox LogTextBox;
    }
}

