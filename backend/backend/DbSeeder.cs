using backend.Context;
using backend.Models;

namespace backend.Data
{
    public static class VetitesSeeder
    {
        public static async Task SeedAsync(MoziDbContext db)
        {
            if (db.Vetitesek.Any()) return;

            var filmek = db.Filmek.ToList();
            var teremIds = db.Termek.Select(t => t.Id).ToList();

            if (!filmek.Any() || !teremIds.Any())
            {
                Console.WriteLine("No films or rooms found, skipping screenings.");
                return;
            }

            var nyelvek = new[] { "Magyar", "Eredeti", "Szinkronizált" };
            var arak = new[] { 1800, 2000, 2200, 2500 };
            var today = DateTime.Today;
            var startHours = new[] { 10, 13, 16, 19 };
            var rng = new Random(42);

            // track occupied slots per room per day: key = (teremId, date, hour)
            var foglalt = new HashSet<(int teremId, DateTime nap, int ora)>();

            var vetitesek = new List<Vetites>();

            foreach (var film in filmek)
            {
                var days = Enumerable.Range(0, 7)
                    .OrderBy(_ => rng.Next())
                    .Take(rng.Next(4, 7))
                    .OrderBy(d => d)
                    .ToList();

                foreach (var day in days)
                {
                    var nap = today.AddDays(day);

                    // try to find a free slot in a random room
                    var shuffledTermek = teremIds.OrderBy(_ => rng.Next()).ToList();
                    var shuffledHours = startHours.OrderBy(_ => rng.Next()).ToList();

                    bool placed = false;
                    foreach (var teremId in shuffledTermek)
                    {
                        foreach (var hour in shuffledHours)
                        {
                            // check if this slot conflicts with any already placed screening
                            var incoming = nap.AddHours(hour);
                            var incomingEnd = incoming.AddMinutes(film.Hossz);

                            bool conflicts = vetitesek.Any(v =>
                                v.TeremId == teremId &&
                                v.Idopont.Date == nap.Date &&
                                incoming < v.Idopont.AddMinutes(filmek.First(f => f.Id == v.FilmId).Hossz) &&
                                incomingEnd > v.Idopont
                            );

                            if (!conflicts)
                            {
                                vetitesek.Add(new Vetites
                                {
                                    FilmId = film.Id,
                                    TeremId = teremId,
                                    Idopont = incoming,
                                    JegyAr = arak[rng.Next(arak.Length)],
                                    Nyelv = nyelvek[rng.Next(nyelvek.Length)],
                                });
                                placed = true;
                                break;
                            }
                        }
                        if (placed) break;
                    }

                    if (!placed)
                        Console.WriteLine($"Could not place screening for film {film.Id} on day {day} — all slots taken.");
                }
            }

            await db.Vetitesek.AddRangeAsync(vetitesek);
            await db.SaveChangesAsync();
            Console.WriteLine($"Seeded {vetitesek.Count} screenings.");
        }
    }
}