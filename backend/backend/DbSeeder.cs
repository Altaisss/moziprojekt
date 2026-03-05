using backend.Context;
using backend.Models;

namespace backend.Data
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(MoziDbContext context)
        {
            // Csak akkor tölt fel, ha még üres az adatbázis
            if (context.Filmek.Any() || context.Termek.Any())
                return;

            // ── Termek ───────────────────────────────────────────────────────
            var termek = new List<Terem>
            {
                new() { TeremNev = "1-es Terem" },
                new() { TeremNev = "2-es Terem" },
                new() { TeremNev = "3-as Terem (IMAX)" },
                new() { TeremNev = "4-es Terem (VIP)" },
                new() { TeremNev = "5-ös Terem" },
            };
            context.Termek.AddRange(termek);
            await context.SaveChangesAsync();

            // ── Szekek ───────────────────────────────────────────────────────
            var szekek = new List<Szek>();
            foreach (var terem in termek)
            {
                for (int sor = 1; sor <= 8; sor++)
                {
                    for (int szam = 1; szam <= 10; szam++)
                    {
                        char oldal = szam <= 5 ? 'B' : 'J'; // Bal / Jobb
                        szekek.Add(new Szek
                        {
                            Sor = sor,
                            Szam = szam,
                            Oldal = oldal,
                            TeremId = terem.Id
                        });
                    }
                }
            }
            context.Szekek.AddRange(szekek);
            await context.SaveChangesAsync();

            // ── Filmek ───────────────────────────────────────────────────────
            var filmek = new List<Film>
            {
                new()
                {
                    Cim = "Dűne: Második rész",
                    Rendezo = "Denis Villeneuve",
                    Hossz = 166,
                    Leiras = "Paul Atreides egyesül a fremenekkel, hogy bosszút álljon azokért, akik elpusztították a családját."
                },
                new()
                {
                    Cim = "Oppenheimer",
                    Rendezo = "Christopher Nolan",
                    Hossz = 180,
                    Leiras = "Az atombomba atyjaként ismert J. Robert Oppenheimer életének drámai feldolgozása."
                },
                new()
                {
                    Cim = "Szegény párák",
                    Rendezo = "Yorgos Lanthimos",
                    Hossz = 141,
                    Leiras = "Egy fiatal nő, akit egy különc tudós keltett életre, felfedezi a világot."
                },
                new()
                {
                    Cim = "Gladiátor II",
                    Rendezo = "Ridley Scott",
                    Hossz = 148,
                    Leiras = "Évekkel a legendás Maximus halála után egy új harcos lép az arénába."
                },
                new()
                {
                    Cim = "Alien: Romulus",
                    Rendezo = "Fede Álvarez",
                    Hossz = 119,
                    Leiras = "Fiatalok egy elhagyatott űrállomáson a sorozat legfélelmetesebb lényeivel kerülnek szembe."
                },
                new()
                {
                    Cim = "Furiosa",
                    Rendezo = "George Miller",
                    Hossz = 148,
                    Leiras = "Furiosa eredettörténete – hogyan vált a Wasteland egyik leghírhedtebb harcosává."
                },
                new()
                {
                    Cim = "A bentlakók",
                    Rendezo = "Alexander Payne",
                    Hossz = 133,
                    Leiras = "Egy bostoni elit iskola tanárának élete felborul, amikor diákjai közel kerülnek hozzá."
                },
                new()
                {
                    Cim = "Konklave",
                    Rendezo = "Edward Berger",
                    Hossz = 120,
                    Leiras = "A legmagasabb rangú egyházi vezetők titkos gyűlésén drámai fordulatok következnek be."
                },
                new()
                {
                    Cim = "Twisters",
                    Rendezo = "Lee Isaac Chung",
                    Hossz = 122,
                    Leiras = "Tornádóvadászok csapnak össze az Oklahomát pusztító viharokkal – és egymással."
                },
                new()
                {
                    Cim = "A vadon szava",
                    Rendezo = "Chris Sanders",
                    Hossz = 100,
                    Leiras = "Buck, egy szelíd háziállat az aranyláz kori Alaszkában szabadon élő kutyává válik."
                },
                new()
                {
                    Cim = "Deadpool & Rozsomák",
                    Rendezo = "Shawn Levy",
                    Hossz = 127,
                    Leiras = "Wade Wilson visszatér, ezúttal a Marvel univerzum egyik legikonikusabb karaktere társaságában."
                },
                new()
                {
                    Cim = "Imaginárius barátok",
                    Rendezo = "John Krasinski",
                    Hossz = 104,
                    Leiras = "Egy fiú különleges képességre tesz szert: látja mások elfeledett képzeletbeli barátait."
                },
            };
            context.Filmek.AddRange(filmek);
            await context.SaveChangesAsync();

            // ── Vetitesek ────────────────────────────────────────────────────
            var now = DateTime.Now;
            var vetitesek = new List<Vetites>
            {
                // 1-es Terem
                new() { FilmId = filmek[0].Id, TeremId = termek[0].Id, Idopont = now.AddDays(1).Date.AddHours(10),    JegyAr = 2200, Nyelv = "Magyar",  VetitesTipus = "2D" },
                new() { FilmId = filmek[0].Id, TeremId = termek[0].Id, Idopont = now.AddDays(1).Date.AddHours(14),    JegyAr = 2200, Nyelv = "Magyar",  VetitesTipus = "2D" },
                new() { FilmId = filmek[1].Id, TeremId = termek[0].Id, Idopont = now.AddDays(1).Date.AddHours(18),    JegyAr = 2400, Nyelv = "Angol",   VetitesTipus = "2D" },
                new() { FilmId = filmek[2].Id, TeremId = termek[0].Id, Idopont = now.AddDays(2).Date.AddHours(16),    JegyAr = 2200, Nyelv = "Magyar",  VetitesTipus = "2D" },

                // 2-es Terem
                new() { FilmId = filmek[3].Id, TeremId = termek[1].Id, Idopont = now.AddDays(1).Date.AddHours(11),    JegyAr = 2200, Nyelv = "Magyar",  VetitesTipus = "2D" },
                new() { FilmId = filmek[4].Id, TeremId = termek[1].Id, Idopont = now.AddDays(1).Date.AddHours(15),    JegyAr = 2200, Nyelv = "Angol",   VetitesTipus = "2D" },
                new() { FilmId = filmek[5].Id, TeremId = termek[1].Id, Idopont = now.AddDays(2).Date.AddHours(13),    JegyAr = 2200, Nyelv = "Magyar",  VetitesTipus = "2D" },
                new() { FilmId = filmek[6].Id, TeremId = termek[1].Id, Idopont = now.AddDays(2).Date.AddHours(19),    JegyAr = 2400, Nyelv = "Angol",   VetitesTipus = "2D" },

                // 3-as Terem (IMAX)
                new() { FilmId = filmek[0].Id, TeremId = termek[2].Id, Idopont = now.AddDays(1).Date.AddHours(12),    JegyAr = 3800, Nyelv = "Magyar",  VetitesTipus = "IMAX" },
                new() { FilmId = filmek[1].Id, TeremId = termek[2].Id, Idopont = now.AddDays(1).Date.AddHours(17),    JegyAr = 3800, Nyelv = "Angol",   VetitesTipus = "IMAX" },
                new() { FilmId = filmek[3].Id, TeremId = termek[2].Id, Idopont = now.AddDays(2).Date.AddHours(15),    JegyAr = 3800, Nyelv = "Magyar",  VetitesTipus = "IMAX" },
                new() { FilmId = filmek[10].Id, TeremId = termek[2].Id, Idopont = now.AddDays(2).Date.AddHours(20),   JegyAr = 3800, Nyelv = "Magyar",  VetitesTipus = "IMAX" },

                // 4-es Terem (VIP)
                new() { FilmId = filmek[7].Id, TeremId = termek[3].Id, Idopont = now.AddDays(1).Date.AddHours(19),    JegyAr = 4500, Nyelv = "Magyar",  VetitesTipus = "VIP" },
                new() { FilmId = filmek[8].Id, TeremId = termek[3].Id, Idopont = now.AddDays(2).Date.AddHours(17),    JegyAr = 4500, Nyelv = "Angol",   VetitesTipus = "VIP" },
                new() { FilmId = filmek[9].Id, TeremId = termek[3].Id, Idopont = now.AddDays(3).Date.AddHours(15),    JegyAr = 4500, Nyelv = "Magyar",  VetitesTipus = "VIP" },

                // 5-ös Terem
                new() { FilmId = filmek[9].Id,  TeremId = termek[4].Id, Idopont = now.AddDays(1).Date.AddHours(10),   JegyAr = 2000, Nyelv = "Magyar",  VetitesTipus = "2D" },
                new() { FilmId = filmek[10].Id, TeremId = termek[4].Id, Idopont = now.AddDays(1).Date.AddHours(13),   JegyAr = 2000, Nyelv = "Magyar",  VetitesTipus = "2D" },
                new() { FilmId = filmek[11].Id, TeremId = termek[4].Id, Idopont = now.AddDays(2).Date.AddHours(11),   JegyAr = 2000, Nyelv = "Magyar",  VetitesTipus = "2D" },
                new() { FilmId = filmek[11].Id, TeremId = termek[4].Id, Idopont = now.AddDays(3).Date.AddHours(14),   JegyAr = 2000, Nyelv = "Angol",   VetitesTipus = "2D" },
            };
            context.Vetitesek.AddRange(vetitesek);
            await context.SaveChangesAsync();
        }
    }
}
