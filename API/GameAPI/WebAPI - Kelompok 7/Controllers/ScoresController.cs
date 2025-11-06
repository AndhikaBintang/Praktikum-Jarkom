using GameApi.Data;
using GameApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ScoresController : ControllerBase
    {
        private readonly AppDbContext _db;
        public ScoresController(AppDbContext db) => _db = db; // DI menyuntikkan AppDbContext

        // GET /api/scores?page=1&pageSize=10
        // Mengambil daftar skor dengan paginasi, diurutkan skor tertinggi.
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlayerScore>>> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            page = page < 1 ? 1 : page;
            pageSize = pageSize is < 1 or > 100 ? 10 : pageSize;

            var query = _db.PlayerScores.AsNoTracking()
                                        .OrderByDescending(s => s.Score)
                                        .ThenBy(s => s.Id); // tie-breaker deterministik

            var total = await query.CountAsync();
            var data = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            Response.Headers["X-Total-Count"] = total.ToString(); // mengirim total untuk front-end
            return Ok(data);
        }

        // GET /api/scores/{id}
        // Mengambil satu skor berdasarkan Id; 404 jika tidak ditemukan.
        [HttpGet("{id:int}")]
        public async Task<ActionResult<PlayerScore>> GetById(int id)
        {
            var item = await _db.PlayerScores.FindAsync(id);
            return item is null ? NotFound() : Ok(item);
        }

        // GET /api/scores/top/{n}
        // Mengambil N skor tertinggi; default aman 10..100.
        [HttpGet("top/{n:int}")]
        public async Task<ActionResult<IEnumerable<PlayerScore>>> TopN(int n)
        {
            n = n is < 1 or > 100 ? 10 : n;
            var data = await _db.PlayerScores.AsNoTracking()
                                             .OrderByDescending(s => s.Score)
                                             .ThenBy(s => s.Id)
                                             .Take(n)
                                             .ToListAsync();
            return Ok(data);
        }

        // GET /api/scores/rank/{playerName}
        // Menghitung peringkat seorang pemain berdasarkan skor TERBARU miliknya.
        [HttpGet("rank/{playerName}")]
        public async Task<ActionResult<object>> GetRank(string playerName)
        {
            playerName = playerName.Trim();
            if (string.IsNullOrWhiteSpace(playerName)) return BadRequest("playerName required");

            var latest = await _db.PlayerScores
                                  .Where(p => p.PlayerName == playerName)
                                  .OrderByDescending(p => p.CreatedAt)
                                  .ThenByDescending(p => p.Id)
                                  .FirstOrDefaultAsync();
            if (latest is null) return NotFound("player not found");

            var betterCount = await _db.PlayerScores.CountAsync(p => p.Score > latest.Score);
            var rank = betterCount + 1; // jumlah yang lebih tinggi + 1

            return Ok(new { player = playerName, score = latest.Score, rank });
        }

        // POST /api/scores
        // Membuat skor baru; validasi minimal: nama wajib, skor >= 0. Kembalikan 201 + Location.
        [HttpPost]
        public async Task<ActionResult<PlayerScore>> Create([FromBody] PlayerScore input)
        {
            if (string.IsNullOrWhiteSpace(input.PlayerName))
                return BadRequest("PlayerName is required");
            if (input.Score < 0)
                return BadRequest("Score must be >= 0");

            _db.PlayerScores.Add(input);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = input.Id }, input);
        }

        // PUT /api/scores/{id}
        // *Full update* baris data. Menolak jika route id != body id. 404 jika barisnya tak ada.
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] PlayerScore input)
        {
            if (id != input.Id) return BadRequest("Route id != body id");

            var exists = await _db.PlayerScores.AnyAsync(s => s.Id == id);
            if (!exists) return NotFound();

            _db.Entry(input).State = EntityState.Modified; // tandai semua kolom berubah
            await _db.SaveChangesAsync();
            return NoContent(); // 204 saat berhasil
        }

        // DELETE /api/scores/{id}
        // Menghapus skor berdasarkan Id; 404 jika tidak ada.
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _db.PlayerScores.FindAsync(id);
            if (item is null) return NotFound();

            _db.PlayerScores.Remove(item);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
