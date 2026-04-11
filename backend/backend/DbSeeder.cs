using backend.Context;
using backend.Models;

namespace backend.Data
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(MoziDbContext db)
        {
            //if (db.Termek.Any())
              //  return;

            // ── Rooms ──────────────────────────────────────────
            var termek = new List<Terem>
            {
                new Terem { TeremNev = "1. Terem" },
                new Terem { TeremNev = "2. Terem" },
                new Terem { TeremNev = "3. Terem" }
            };

            await db.Termek.AddRangeAsync(termek);
            await db.SaveChangesAsync();

            // ── Seats ──────────────────────────────────────────
            var configurations = new[]
            {
                (termek[0], rows: 8, cols: 10),
                (termek[1], rows: 8, cols: 8),
                (termek[2], rows: 6, cols: 8)
            };

            var szekek = new List<Szek>();
            foreach (var (terem, rows, cols) in configurations)
            {
                for (int sor = 1; sor <= rows; sor++)
                {
                    for (int szam = 1; szam <= cols; szam++)
                    {
                        szekek.Add(new Szek
                        {
                            Sor = sor,
                            Szam = szam,
                            Oldal = szam <= cols / 2 ? 'B' : 'J',
                            TeremId = terem.Id
                        });
                    }
                }
            }

            await db.Szekek.AddRangeAsync(szekek);
            await db.SaveChangesAsync();

            // ── Screenings ─────────────────────────────────────
            var filmIds = db.Filmek.Select(f => f.Id).ToList();
            if (!filmIds.Any())
            {
                Console.WriteLine("No films found, skipping screenings.");
                return;
            }
            var teremIds = termek.Select(t => t.Id).ToList();
            var nyelvek = new[] { "Magyar", "Eredeti", "Szinkronizált" };
            var tipusok = new[] { "2D", "3D" };
            var arak = new[] { 1800, 2000, 2200, 2500 };

            var today = DateTime.Today;
            var startHours = new[] { 11, 13, 15, 17, 19, 21 };

            var vetitesek = new List<Vetites>();
            var rng = new Random(42);

            // Assign each film a base slot so every film gets at least 4 screenings
            // spread across the week
            foreach (var filmId in filmIds)
            {
                // Pick 4+ days randomly from the 7-day window
                var days = Enumerable.Range(0, 7)
                    .OrderBy(_ => rng.Next())
                    .Take(rng.Next(4, 7)) // 4 to 6 screenings per film
                    .OrderBy(d => d)
                    .ToList();

                foreach (var day in days)
                {
                    var hour = startHours[rng.Next(startHours.Length)];
                    vetitesek.Add(new Vetites
                    {
                        FilmId = filmId,
                        TeremId = teremIds[rng.Next(teremIds.Count)],
                        Idopont = today.AddDays(day).AddHours(hour),
                        JegyAr = arak[rng.Next(arak.Length)],
                        Nyelv = nyelvek[rng.Next(nyelvek.Length)],
                        VetitesTipus = tipusok[rng.Next(tipusok.Length)]
                    });
                }
            }

            await db.Vetitesek.AddRangeAsync(vetitesek);
            await db.SaveChangesAsync();
        }
    }
}