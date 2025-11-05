using System.Diagnostics;
using WinFormsGameClient.Models;
using WinFormsGameClient.Services;

namespace WinFormsGameClient
{
    public partial class Form1 : Form
    {
        private readonly ScoreApiClient _api = new ScoreApiClient();
        private int _score = 0;
        private int _timeLeft = 10; // detik
        private bool _gameRunning = false;

        public Form1()
        {
            InitializeComponent();
            btnClick.Enabled = false;
            btnSubmit.Enabled = false;
            lblScore.Text = "Score: 0";
            lblTime.Text = "Time: 10s";
            numTop.Value = 10;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPlayerName.Text))
            {
                MessageBox.Show("Nama pemain wajib diisi", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _score = 0;
            _timeLeft = 10;
            _gameRunning = true;
            btnClick.Enabled = true;
            btnSubmit.Enabled = false;
            lblScore.Text = "Score: 0";
            lblTime.Text = $"Time: {_timeLeft}s";
            gameTimer.Start();
        }

        private void btnClick_Click(object sender, EventArgs e)
        {
            if (!_gameRunning) return;
            _score++;
            lblScore.Text = $"Score: {_score}";
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
                btnSubmit.Enabled = true; // biar bisa coba lagi
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

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            _timeLeft--;
            lblTime.Text = $"Time: {_timeLeft}s";

            if (_timeLeft <= 0)
            {
                gameTimer.Stop();
                _gameRunning = false;
                btnClick.Enabled = false;
                btnSubmit.Enabled = true; // boleh submit
                MessageBox.Show($"Waktu habis! Skor kamu: {_score}", "Selesai", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
