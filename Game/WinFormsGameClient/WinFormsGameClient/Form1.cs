using System.Diagnostics;
using WinFormsGameClient.Models;
using WinFormsGameClient.Services;

namespace WinFormsGameClient
{
    public partial class Form1 : Form
    {
        private readonly ScoreApiClient _api = new ScoreApiClient();
        private int _score = 0;
        private bool _gameRunning = false;
        private readonly Random rng = new Random();

        // Reaction system fields
        private bool canPress = false;
        private DateTime cueTime;
        private System.Windows.Forms.Timer cueTimer;
        private System.Windows.Forms.Timer reactionTimer;

        // Konfigurasi reaction
        private int minDelayMs = 1500;
        private int maxDelayMs = 4000;
        private int maxReactionMs = 1500; // jika lebih lambat dari ini -> gagal

        public Form1()
        {
            InitializeComponent();

            btnClick.Enabled = false;
            btnSubmit.Enabled = false;
            lblScore.Text = "Score: 0";
            lblTime.Text = "Reaction Test Mode";

            // --- inisialisasi timers untuk reaction ---
            cueTimer = new System.Windows.Forms.Timer();
            cueTimer.Tick += CueTimer_Tick;

            reactionTimer = new System.Windows.Forms.Timer();
            reactionTimer.Tick += ReactionTimer_Tick;

            // Key preview agar form bisa menangkap tombol Space
            this.KeyPreview = true;
            this.KeyDown += Form1_KeyDown;

            // Label cue awal
            if (lblCue != null)
            {
                lblCue.Text = "Tekan Start untuk memulai tes reaksi!";
                lblCue.ForeColor = Color.White;
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPlayerName.Text))
            {
                MessageBox.Show("Nama pemain wajib diisi", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _score = 0;
            _gameRunning = true;
            btnClick.Enabled = false; // tidak digunakan langsung
            btnSubmit.Enabled = false;

            lblScore.Text = "Score: 0";
            lblCue.Text = "Bersiap...";
            lblCue.ForeColor = Color.Yellow;

            StartReactionRound();
        }

        // =========================
        // 🔹 Reaction System Logic
        // =========================

        private void StartReactionRound()
        {
            canPress = false;

            int delay = rng.Next(minDelayMs, maxDelayMs + 1);
            cueTimer.Interval = delay;
            cueTimer.Start();
        }

        private void CueTimer_Tick(object? sender, EventArgs e)
        {
            cueTimer.Stop();

            if (!_gameRunning) return;

            lblCue.Text = "TEKAN SPASI SEKARANG!";
            lblCue.ForeColor = Color.Lime;
            canPress = true;
            cueTime = DateTime.Now;

            reactionTimer.Interval = maxReactionMs;
            reactionTimer.Start();
        }

        private void ReactionTimer_Tick(object? sender, EventArgs e)
        {
            reactionTimer.Stop();

            if (!_gameRunning) return;

            if (canPress)
            {
                canPress = false;
                lblCue.Text = "Terlambat!";
                lblCue.ForeColor = Color.Red;
                EndReactionTest();
            }
        }

        private void Form1_KeyDown(object? sender, KeyEventArgs e)
        {
            if (!_gameRunning) return;

            if (e.KeyCode == Keys.Space)
            {
                if (!canPress)
                {
                    // terlalu cepat
                    lblCue.Text = "Terlalu cepat!";
                    lblCue.ForeColor = Color.OrangeRed;
                    EndReactionTest();
                }
                else
                {
                    canPress = false;
                    reactionTimer.Stop();

                    double reactionMs = (DateTime.Now - cueTime).TotalMilliseconds;

                    // Skor berdasarkan kecepatan reaksi
                    // Jika 0ms (instan) → 1000 poin
                    // Jika 1000ms → 0 poin
                    _score = Math.Max(0, 1000 - (int)reactionMs);

                    lblCue.Text = $"Reaksi: {reactionMs:F0} ms\nSkor: {_score}";
                    lblCue.ForeColor = Color.Cyan;

                    // Simulasi mekanik klik lama agar kompatibel
                    try
                    {
                        btnClick?.PerformClick();
                    }
                    catch { }

                    EndReactionTest();
                }
            }
        }

        private void EndReactionTest()
        {
            _gameRunning = false;
            canPress = false;

            cueTimer.Stop();
            reactionTimer.Stop();

            btnClick.Enabled = false;
            btnSubmit.Enabled = true;

            MessageBox.Show($"Tes selesai!\nSkor kamu: {_score}", "Hasil", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private async void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                btnSubmit.Enabled = false;
                await _api.SubmitAsync(txtPlayerName.Text.Trim(), _score);
                MessageBox.Show("Skor berhasil dikirim!", "Berhasil", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await RefreshLeaderboardAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Gagal submit skor: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnSubmit.Enabled = true;
            }
        }

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            await RefreshLeaderboardAsync();
        }

        private async Task RefreshLeaderboardAsync()
        {
            try
            {
                int n = (int)numTop.Value;
                var data = await _api.GetTopAsync(n);
                gridLeaderboard.DataSource = data.Select((x, idx) => new
                {
                    Rank = idx + 1,
                    x.PlayerName,
                    x.Score,
                    x.CreatedAt
                }).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Gagal mengambil leaderboard: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
