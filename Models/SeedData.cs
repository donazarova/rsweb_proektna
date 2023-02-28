using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MVCTurizam.Areas.Identity.Data;
using MVCTurizam.Data;

namespace MVCTurizam.Models
{
    public class SeedData
    {
        public static async Task CreateUserRoles(IServiceProvider serviceProvider)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<MVCTurizamUser>>();
            IdentityResult roleResult;
            //Add Admin Role
            var roleCheck = await RoleManager.RoleExistsAsync("Admin");
            if (!roleCheck) { roleResult = await RoleManager.CreateAsync(new IdentityRole("Admin")); }
            MVCTurizamUser user = await UserManager.FindByEmailAsync("admin@mvcturizam.com");
            if (user == null)
            {
                var User = new MVCTurizamUser();
                User.Email = "admin@mvcturizam.com";
                User.UserName = "admin@mvcturizam.com";
                string userPWD = "Admin123";
                IdentityResult chkUser = await UserManager.CreateAsync(User, userPWD);
                //Add default User to Role Admin
                if (chkUser.Succeeded) { var result1 = await UserManager.AddToRoleAsync(User, "Admin"); }
            }

            var roleCheck1 = await RoleManager.RoleExistsAsync("Vodic");
            if (!roleCheck1) { roleResult = await RoleManager.CreateAsync(new IdentityRole("Vodic")); }
            MVCTurizamUser user1 = await UserManager.FindByEmailAsync("dafina@travel.com");
            if (user == null)
            {
                var User = new MVCTurizamUser();
                User.Email = "dafina@travel.com";
                User.UserName = "dafina@travel.com";
                string userPWD = "Vodic123";
                IdentityResult chkUser = await UserManager.CreateAsync(User, userPWD);
                //Add default User to Role Admin
                if (chkUser.Succeeded) { var result1 = await UserManager.AddToRoleAsync(User, "Vodic"); }
            }

            var roleCheck2 = await RoleManager.RoleExistsAsync("Klient");
            if (!roleCheck2) { roleResult = await RoleManager.CreateAsync(new IdentityRole("Klient")); }
            MVCTurizamUser user2 = await UserManager.FindByEmailAsync("viktorija@gmail.com");
            if (user == null)
            {
                var User = new MVCTurizamUser();
                User.Email = "viktorija@gmail.com";
                User.UserName = "viktorija@gmail.com";
                string userPWD = "Klient123";
                IdentityResult chkUser = await UserManager.CreateAsync(User, userPWD);
                //Add default User to Role Admin
                if (chkUser.Succeeded) { var result1 = await UserManager.AddToRoleAsync(User, "Klient"); }
            }
            MVCTurizamUser user3 = await UserManager.FindByEmailAsync("sofija@gmail.com");
            if (user == null)
            {
                var User = new MVCTurizamUser();
                User.Email = "sofija@gmail.com";
                User.UserName = "sofija@gmail.com";
                string userPWD = "Klient123";
                IdentityResult chkUser = await UserManager.CreateAsync(User, userPWD);
                //Add default User to Role Admin
                if (chkUser.Succeeded) { var result1 = await UserManager.AddToRoleAsync(User, "Klient"); }
            }
        }

        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MVCTurizamContext(
            serviceProvider.GetRequiredService<
            DbContextOptions<MVCTurizamContext>>()))
            {
                CreateUserRoles(serviceProvider).Wait();
                // Look for any movies.
                if (context.Destinacija.Any() || context.Vodic.Any() || context.Klient.Any() || context.Patuvanje.Any())
                {
                    return; // DB has been seeded
                }

                context.Vodic.AddRange(
                    new Vodic { /* id = 1 */ImePrezime = "Svetlana Manevska", Email = "svetlana@travel.com", Telefon = 072947110, Iskustvo = 4 },
                    new Vodic { /* id = 2 */ImePrezime = "Darijan Vesovski", Email = "darijan@travel.com", Telefon = 076394596, Iskustvo = 1 },
                    new Vodic { /* id = 3 */ImePrezime = "Dafina Bogdanova", Email = "dafina@travel.com", Telefon = 077523892, Iskustvo = 9 }
                    );
                context.SaveChanges();

                context.Klient.AddRange(
                    new Klient { /* id = 1 */ImePrezime = "Mirko Stojanov", Email = "mirko@gmail.com", Telefon = 071857394 },
                    new Klient { /* id = 2 */ImePrezime = "Viktorija Atanasova", Email = "viktorija@gmail.com", Telefon = 077394738 },
                    new Klient { /* id = 3 */ImePrezime = "Joana Andreeva", Email = "joana@gmail.com", Telefon = 078339956 },
                    new Klient { /* id = 4 */ImePrezime = "Gorazd Stefanov", Email = "gorazd@gmail.com", Telefon = 072900457 },
                    new Klient { /* id = 5 */ImePrezime = "Georgi Andonov", Email = "georgi@gmail.com", Telefon = 071876548 },
                    new Klient { /* id = 6 */ImePrezime = "Tatjana Ilieva", Email = "tatjana@gmail.com", Telefon = 075948458 },
                    new Klient { /* id = 7 */ImePrezime = "Ajse Sulejmani", Email = "ajse@gmail.com", Telefon = 072050501 },
                    new Klient { /* id = 8 */ImePrezime = "Omer Ahmeti", Email = "omer@gmail.com", Telefon = 077232887 },
                    new Klient { /* id = 9 */ImePrezime = "Sofija Petkova", Email = "sofija@gmail.com", Telefon = 070212567 }
                    );
                context.SaveChanges();

                context.Destinacija.AddRange(
                    new Destinacija
                    {
                        //Id = 1
                        Ime = "Rim",
                        Drzava = "Italija",
                        Kontinent = "Evropa",
                        Dalecina = 1023,
                        Temperatura = 25,
                        CenaKarta = 11274,
                        VodicId = context.Vodic.Single(d => d.ImePrezime == "Dafina Bogdanova").Id
                    },
                    new Destinacija
                    {
                        //Id = 2
                        Ime = "Kazablanka",
                        Drzava = "Maroko",
                        Kontinent = "Afrika",
                        Dalecina = 3913,
                        Temperatura = 27,
                        CenaKarta = 29136,
                        VodicId = context.Vodic.Single(d => d.ImePrezime == "Darijan Vesovski").Id
                    },
                    new Destinacija
                    {
                        //Id = 1
                        Ime = "Tokio",
                        Drzava = "Japonija",
                        Kontinent = "Azija",
                        Dalecina = 9333,
                        Temperatura = 16,
                        CenaKarta = 59122,
                        VodicId = context.Vodic.Single(d => d.ImePrezime == "Svetlana Manevska").Id
                    }
                    );
                context.SaveChanges();

                context.Patuvanje.AddRange(
                    new Patuvanje
                    {
                        DatumOd = DateTime.Parse("2023-5-5"),
                        DatumDo = DateTime.Parse("2023-5-10"),
                        KlientId = context.Klient.Single(d => d.ImePrezime == "Viktorija Atanasova").Id,
                        DestinacijaId = context.Destinacija.Single(d => d.Ime == "Rim").Id
                    },
                    new Patuvanje
                    {
                        DatumOd = DateTime.Parse("2023-2-14"),
                        DatumDo = DateTime.Parse("2023-2-20"),
                        KlientId = context.Klient.Single(d => d.ImePrezime == "Gorazd Stefanov").Id,
                        DestinacijaId = context.Destinacija.Single(d => d.Ime == "Kazablanka").Id
                    },
                    new Patuvanje
                    {
                        DatumOd = DateTime.Parse("2023-3-14"),
                        DatumDo = DateTime.Parse("2023-5-28"),
                        KlientId = context.Klient.Single(d => d.ImePrezime == "Omer Ahmeti").Id,
                        DestinacijaId = context.Destinacija.Single(d => d.Ime == "Tokio").Id
                    },
                    new Patuvanje
                    {
                        DatumOd = DateTime.Parse("2023-5-2"),
                        DatumDo = DateTime.Parse("2023-5-31"),
                        KlientId = context.Klient.Single(d => d.ImePrezime == "Joana Andreeva").Id,
                        DestinacijaId = context.Destinacija.Single(d => d.Ime == "Tokio").Id
                    },
                    new Patuvanje
                    {
                        DatumOd = DateTime.Parse("2023-9-22"),
                        DatumDo = DateTime.Parse("2023-10-2"),
                        KlientId = context.Klient.Single(d => d.ImePrezime == "Ajse Sulejmani").Id,
                        DestinacijaId = context.Destinacija.Single(d => d.Ime == "Kazablanka").Id
                    },
                    new Patuvanje
                    {
                        DatumOd = DateTime.Parse("2023-12-20"),
                        DatumDo = DateTime.Parse("2023-2-1"),
                        KlientId = context.Klient.Single(d => d.ImePrezime == "Tatjana Ilieva").Id,
                        DestinacijaId = context.Destinacija.Single(d => d.Ime == "Tokio").Id
                    },
                    new Patuvanje
                    {
                        DatumOd = DateTime.Parse("2023-9-3"),
                        DatumDo = DateTime.Parse("2023-9-10"),
                        KlientId = context.Klient.Single(d => d.ImePrezime == "Mirko Stojanov").Id,
                        DestinacijaId = context.Destinacija.Single(d => d.Ime == "Rim").Id
                    },
                    new Patuvanje
                    {
                        DatumOd = DateTime.Parse("2023-5-5"),
                        DatumDo = DateTime.Parse("2023-5-10"),
                        KlientId = context.Klient.Single(d => d.ImePrezime == "Sofija Petkova").Id,
                        DestinacijaId = context.Destinacija.Single(d => d.Ime == "Rim").Id
                    },
                    new Patuvanje
                    {
                        DatumOd = DateTime.Parse("2023-11-11"),
                        DatumDo = DateTime.Parse("2023-11-30"),
                        KlientId = context.Klient.Single(d => d.ImePrezime == "Georgi Andonov").Id,
                        DestinacijaId = context.Destinacija.Single(d => d.Ime == "Kazablanka").Id
                    }
                    );
                context.SaveChanges();
            }
        }
    }
}
