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
        private System.Windows.Forms.Label lblCue;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            lblTitle = new Label();
            txtPlayerName = new TextBox();
            btnStart = new Button();
            lblTime = new Label();
            lblScore = new Label();
            btnClick = new Button();
            btnSubmit = new Button();
            btnRefresh = new Button();
            numTop = new NumericUpDown();
            gridLeaderboard = new DataGridView();
            gameTimer = new System.Windows.Forms.Timer(components);
            lblCue = new Label();
            ((System.ComponentModel.ISupportInitialize)numTop).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridLeaderboard).BeginInit();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            lblTitle.Location = new Point(105, 9);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(605, 78);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Reaction Time Game";
            // 
            // txtPlayerName
            // 
            txtPlayerName.Location = new Point(61, 134);
            txtPlayerName.Name = "txtPlayerName";
            txtPlayerName.PlaceholderText = "Masukkan nama pemain";
            txtPlayerName.Size = new Size(319, 39);
            txtPlayerName.TabIndex = 1;
            // 
            // btnStart
            // 
            btnStart.Location = new Point(407, 134);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(151, 53);
            btnStart.TabIndex = 2;
            btnStart.Text = "Start (10 s)";
            btnStart.Click += btnStart_Click;
            // 
            // lblTime
            // 
            lblTime.AutoSize = true;
            lblTime.Location = new Point(337, 87);
            lblTime.Name = "lblTime";
            lblTime.Size = new Size(115, 32);
            lblTime.TabIndex = 3;
            lblTime.Text = "Time: 10s";
            // 
            // lblScore
            // 
            lblScore.AutoSize = true;
            lblScore.Location = new Point(61, 191);
            lblScore.Name = "lblScore";
            lblScore.Size = new Size(98, 32);
            lblScore.TabIndex = 4;
            lblScore.Text = "Score: 0";
            // 
            // btnClick
            // 
            btnClick.Enabled = false;
            btnClick.Location = new Point(324, 201);
            btnClick.Name = "btnClick";
            btnClick.Size = new Size(151, 47);
            btnClick.TabIndex = 6;
            btnClick.Text = "CLICK !!";
            btnClick.Visible = false;
            // 
            // btnSubmit
            // 
            btnSubmit.Enabled = false;
            btnSubmit.Location = new Point(61, 418);
            btnSubmit.Name = "btnSubmit";
            btnSubmit.Size = new Size(133, 46);
            btnSubmit.TabIndex = 7;
            btnSubmit.Text = "Submit Score";
            btnSubmit.Click += btnSubmit_Click;
            // 
            // btnRefresh
            // 
            btnRefresh.Location = new Point(210, 418);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(170, 44);
            btnRefresh.TabIndex = 8;
            btnRefresh.Text = "Refresh Leaderboard";
            btnRefresh.Click += btnRefresh_Click;
            // 
            // numTop
            // 
            numTop.Location = new Point(650, 418);
            numTop.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numTop.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numTop.Name = "numTop";
            numTop.Size = new Size(76, 39);
            numTop.TabIndex = 9;
            numTop.Value = new decimal(new int[] { 10, 0, 0, 0 });
            // 
            // gridLeaderboard
            // 
            gridLeaderboard.AllowUserToAddRows = false;
            gridLeaderboard.AllowUserToDeleteRows = false;
            gridLeaderboard.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridLeaderboard.ColumnHeadersHeight = 46;
            gridLeaderboard.Location = new Point(61, 487);
            gridLeaderboard.Name = "gridLeaderboard";
            gridLeaderboard.ReadOnly = true;
            gridLeaderboard.RowHeadersVisible = false;
            gridLeaderboard.RowHeadersWidth = 82;
            gridLeaderboard.Size = new Size(718, 211);
            gridLeaderboard.TabIndex = 10;
            // 
            // gameTimer
            // 
            gameTimer.Interval = 1000;
            // 
            // lblCue
            // 
            lblCue.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblCue.ForeColor = Color.White;
            lblCue.Location = new Point(61, 265);
            lblCue.Name = "lblCue";
            lblCue.Size = new Size(665, 150);
            lblCue.TabIndex = 5;
            lblCue.Text = "Tunggu...";
            lblCue.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Form1
            // 
            BackColor = Color.FromArgb(30, 30, 46);
            ClientSize = new Size(857, 740);
            Controls.Add(lblTitle);
            Controls.Add(txtPlayerName);
            Controls.Add(btnStart);
            Controls.Add(lblTime);
            Controls.Add(lblScore);
            Controls.Add(lblCue);
            Controls.Add(btnClick);
            Controls.Add(btnSubmit);
            Controls.Add(btnRefresh);
            Controls.Add(numTop);
            Controls.Add(gridLeaderboard);
            KeyPreview = true;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Reaction Time Game";
            KeyDown += Form1_KeyDown;
            ((System.ComponentModel.ISupportInitialize)numTop).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridLeaderboard).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
