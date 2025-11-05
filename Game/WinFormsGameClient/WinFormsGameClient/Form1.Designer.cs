namespace WinFormsGameClient
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TextBox txtPlayerName;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Label lblScore;
        private System.Windows.Forms.Button btnClick;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.NumericUpDown numTop;
        private System.Windows.Forms.DataGridView gridLeaderboard;
        private System.Windows.Forms.Timer gameTimer;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();

            lblTitle = new System.Windows.Forms.Label();
            txtPlayerName = new System.Windows.Forms.TextBox();
            btnStart = new System.Windows.Forms.Button();
            lblTime = new System.Windows.Forms.Label();
            lblScore = new System.Windows.Forms.Label();
            btnClick = new System.Windows.Forms.Button();
            btnSubmit = new System.Windows.Forms.Button();
            btnRefresh = new System.Windows.Forms.Button();
            numTop = new System.Windows.Forms.NumericUpDown();
            gridLeaderboard = new System.Windows.Forms.DataGridView();
            gameTimer = new System.Windows.Forms.Timer(components);

            ((System.ComponentModel.ISupportInitialize)numTop).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridLeaderboard).BeginInit();
            SuspendLayout();

            // ===== lblTitle =====
            lblTitle.AutoSize = true;
            lblTitle.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold);
            lblTitle.Location = new System.Drawing.Point(110, 12);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new System.Drawing.Size(285, 41);
            lblTitle.Text = "Mini Clicker Game";

            // ===== txtPlayerName =====
            txtPlayerName.Name = "txtPlayerName";
            txtPlayerName.PlaceholderText = "Masukkan nama pemain";
            txtPlayerName.Location = new System.Drawing.Point(24, 70);
            txtPlayerName.Size = new System.Drawing.Size(230, 23);

            // ===== btnStart =====
            btnStart.Name = "btnStart";
            btnStart.Text = "Start (10 s)";
            btnStart.Location = new System.Drawing.Point(270, 69);
            btnStart.Size = new System.Drawing.Size(110, 25);

            // ===== lblTime =====
            lblTime.AutoSize = true;
            lblTime.Name = "lblTime";
            lblTime.Text = "Time: 10s";
            lblTime.Location = new System.Drawing.Point(395, 74);

            // ===== lblScore =====
            lblScore.AutoSize = true;
            lblScore.Name = "lblScore";
            lblScore.Text = "Score: 0";
            lblScore.Location = new System.Drawing.Point(24, 108);

            // ===== btnClick =====
            btnClick.Name = "btnClick";
            btnClick.Text = "CLICK !!";
            btnClick.Enabled = false;
            btnClick.Location = new System.Drawing.Point(110, 102);
            btnClick.Size = new System.Drawing.Size(190, 30);

            // ===== btnSubmit =====
            btnSubmit.Name = "btnSubmit";
            btnSubmit.Text = "Submit Score";
            btnSubmit.Enabled = false;
            btnSubmit.Location = new System.Drawing.Point(24, 145);
            btnSubmit.Size = new System.Drawing.Size(120, 30);

            // ===== btnRefresh =====
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Text = "Refresh Leaderboard";
            btnRefresh.Location = new System.Drawing.Point(155, 145);
            btnRefresh.Size = new System.Drawing.Size(170, 30);

            // ===== numTop =====
            numTop.Name = "numTop";
            numTop.Location = new System.Drawing.Point(335, 149);
            numTop.Size = new System.Drawing.Size(60, 23);
            numTop.Minimum = 1;
            numTop.Maximum = 1000;
            numTop.Value = 10;

            // ===== gridLeaderboard =====
            gridLeaderboard.Name = "gridLeaderboard";
            gridLeaderboard.Location = new System.Drawing.Point(24, 190);
            gridLeaderboard.Size = new System.Drawing.Size(430, 220);
            gridLeaderboard.Anchor = (System.Windows.Forms.AnchorStyles.Top
                                     | System.Windows.Forms.AnchorStyles.Left
                                     | System.Windows.Forms.AnchorStyles.Right
                                     | System.Windows.Forms.AnchorStyles.Bottom);
            gridLeaderboard.ReadOnly = true;
            gridLeaderboard.AllowUserToAddRows = false;
            gridLeaderboard.AllowUserToDeleteRows = false;
            gridLeaderboard.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            gridLeaderboard.RowHeadersVisible = false;

            // ===== gameTimer =====
            gameTimer.Interval = 1000; // 1 detik
            gameTimer.Enabled = false;

            // ===== Form1 =====
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(480, 430);
            Controls.Add(lblTitle);
            Controls.Add(txtPlayerName);
            Controls.Add(btnStart);
            Controls.Add(lblTime);
            Controls.Add(lblScore);
            Controls.Add(btnClick);
            Controls.Add(btnSubmit);
            Controls.Add(btnRefresh);
            Controls.Add(numTop);
            Controls.Add(gridLeaderboard);
            Name = "Form1";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Mini Game";

            ((System.ComponentModel.ISupportInitialize)numTop).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridLeaderboard).EndInit();
            ResumeLayout(false);
            PerformLayout();

            btnStart.Click += btnStart_Click;
            btnClick.Click += btnClick_Click;
            btnSubmit.Click += btnSubmit_Click;
            btnRefresh.Click += btnRefresh_Click;
            gameTimer.Tick += gameTimer_Tick;

        }
    }
}
