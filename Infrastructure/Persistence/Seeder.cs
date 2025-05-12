using Domain.AmputationLevels;
using Domain.Functionalities;
using Domain.Materials;
using Domain.ProstheticTypes;
using Domain.Prosthetics;
using Domain.Request;
using Domain.Reviews;
using Domain.Roles;
using Domain.Statuses;
using Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class Seeder(ApplicationDbContext context)
{
    public async Task SeedAsync()
    {
        if (!context.Roles.Any()) await SeedRolesAsync();
        if (!context.Users.Any()) await SeedUsersAsync();
        if (!context.Statuses.Any()) await SeedStatusesAsync();
        if (!context.AmputationLevels.Any()) await SeedAmputationLevelsAsync();
        if (!context.Functionalities.Any()) await SeedFunctionalitiesAsync();
        if (!context.Materials.Any()) await SeedMaterialsAsync();
        if (!context.ProstheticTypes.Any()) await SeedProstheticTypesAsync();
        if (!context.Prosthetics.Any()) await SeedProstheticsAsync();
        if (!context.Requests.Any()) await SeedRequestsAsync();
        if (!context.Reviews.Any()) await SeedReviewsAsync();
    }

    private async Task SeedRolesAsync()
    {
        var roles = new List<Role>
        {
            Role.New(new RoleId(Guid.NewGuid()), "Admin"),
            Role.New(new RoleId(Guid.NewGuid()), "User")
        };
        context.Roles.AddRange(roles);
        await context.SaveChangesAsync();
    }

    private async Task SeedUsersAsync()
    {
        var adminRole = context.Roles.First(r => r.Title == "Admin");
        var userRole = context.Roles.First(r => r.Title == "User");

        var users = new List<User>
        {
            User.New(
                new UserId(Guid.NewGuid()),
                "Admin User",
                "admin@gmail.com",
                "password",
                adminRole.Id,
                "+1234567890",
                new DateTime(1990, 1, 1)
            ),
            User.New(
                new UserId(Guid.NewGuid()),
                "Regular User",
                "user@gmail.com",
                "password",
                userRole.Id,
                "+0987654321",
                new DateTime(1995, 5, 5)
            )
        };
        context.Users.AddRange(users);
        await context.SaveChangesAsync();
    }

    private async Task SeedStatusesAsync()
    {
        var statuses = new List<Status>
        {
            Status.New(new StatusId(Guid.NewGuid()), "В обробці"),
            Status.New(new StatusId(Guid.NewGuid()), "Затверджено"),
            Status.New(new StatusId(Guid.NewGuid()), "Відхилено")
        };
        context.Statuses.AddRange(statuses);
        await context.SaveChangesAsync();
    }

    private async Task SeedAmputationLevelsAsync()
    {
        var levels = new List<AmputationLevel>
        {
            AmputationLevel.New(new AmputationLevelId(Guid.NewGuid()), "Кистьовий"),
            AmputationLevel.New(new AmputationLevelId(Guid.NewGuid()), "Передплічний"),
            AmputationLevel.New(new AmputationLevelId(Guid.NewGuid()), "Плечовий"),
            AmputationLevel.New(new AmputationLevelId(Guid.NewGuid()), "Стопа"),
            AmputationLevel.New(new AmputationLevelId(Guid.NewGuid()), "Гомілковий"),
            AmputationLevel.New(new AmputationLevelId(Guid.NewGuid()), "Стегновий"),
            AmputationLevel.New(new AmputationLevelId(Guid.NewGuid()), "Гіп-дізарткуляційний")
        };
        context.AmputationLevels.AddRange(levels);
        await context.SaveChangesAsync();
    }

    private async Task SeedFunctionalitiesAsync()
    {
        var funcs = new List<Functionality>
        {
            Functionality.New(new FunctionalityId(Guid.NewGuid()), "Пасивні"),
            Functionality.New(new FunctionalityId(Guid.NewGuid()), "Механічні"),
            Functionality.New(new FunctionalityId(Guid.NewGuid()), "Біонічні"),
            Functionality.New(new FunctionalityId(Guid.NewGuid()), "Мікроелектричні")
        };
        context.Functionalities.AddRange(funcs);
        await context.SaveChangesAsync();
    }

    private async Task SeedMaterialsAsync()
    {
        var materials = new List<Material>
        {
            Material.New(new MaterialId(Guid.NewGuid()), "Карбон"),
            Material.New(new MaterialId(Guid.NewGuid()), "Пластик"),
            Material.New(new MaterialId(Guid.NewGuid()), "Металеві сплави"),
        };
        context.Materials.AddRange(materials);
        await context.SaveChangesAsync();
    }

    private async Task SeedProstheticTypesAsync()
    {
        var types = new List<ProstheticType>
        {
            ProstheticType.New(new TypeId(Guid.NewGuid()), "Функціональні"),
            ProstheticType.New(new TypeId(Guid.NewGuid()), "Косметичні"),
            ProstheticType.New(new TypeId(Guid.NewGuid()), "Спортивні"),
            ProstheticType.New(new TypeId(Guid.NewGuid()), "Робочі"),
        };
        context.ProstheticTypes.AddRange(types);
        await context.SaveChangesAsync();
    }

    private async Task SeedProstheticsAsync()
    {
        var type = context.ProstheticTypes.First(t => t.Title == "Функціональні");
        var material = context.Materials.First(m => m.Title == "Пластик");
        var functionality = context.Functionalities.First(f => f.Title == "Механічні");
        var amputationLevel = context.AmputationLevels.First(a => a.Title == "Плечовий");

        var prosthetics = new List<Prosthetic>
        {
            Prosthetic.New(
                new ProstheticId(Guid.NewGuid()),
                "Pro Arm 3000",
                "Lightweight prosthetic arm for work",
                1.5,
                type.Id,
                material.Id,
                functionality.Id,
                amputationLevel.Id
            )
        };
        context.Prosthetics.AddRange(prosthetics);
        await context.SaveChangesAsync();
    }

    private async Task SeedRequestsAsync()
    {
        var user = context.Users.First(u => u.Email == "user@gmail.com");
        var prosthetic = context.Prosthetics.First();
        var request = Request.New(
            new RequestId(Guid.NewGuid()),
            "Request for a new prosthetic arm",
            user.Id,
            prosthetic.Id
        );
        context.Requests.Add(request);
        await context.SaveChangesAsync();
    }

    private async Task SeedReviewsAsync()
    {
        var user = context.Users.First(u => u.Email == "user@gmail.com");
        var prosthetic = context.Prosthetics.First();
        var review = Review.New(
            new ReviewId(Guid.NewGuid()),
            "Great product!",
            "Light and comfortable",
            "None",
            user.Id,
            prosthetic.Id
        );
        context.Reviews.Add(review);
        await context.SaveChangesAsync();
    }
}
