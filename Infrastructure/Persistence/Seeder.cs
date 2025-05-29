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
using Domain.ProstheticStatuses;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class Seeder(ApplicationDbContext context)
{
    public async Task SeedAsync()
    {
        if (!context.Roles.Any()) await SeedRolesAsync();
        if (!context.Users.Any()) await SeedUsersAsync();
        if (!context.Statuses.Any()) await SeedStatusesAsync();
        if (!context.ProstheticStatuses.Any()) await SeedProstheticStatusesAsync();
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

    private async Task SeedProstheticStatusesAsync()
    {
        var statuses = new List<ProstheticStatus>
        {
            ProstheticStatus.New(new ProstheticStatusId(Guid.NewGuid()), "Доступно"),
            ProstheticStatus.New(new ProstheticStatusId(Guid.NewGuid()), "Не доступно")
        };
        context.ProstheticStatuses.AddRange(statuses);
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
        var types = context.ProstheticTypes.ToList();
        var materials = context.Materials.ToList();
        var functionalities = context.Functionalities.ToList();
        var amputationLevels = context.AmputationLevels.ToList();
        var prostheticStatuses = context.ProstheticStatuses.ToList();
        var availableStatus = prostheticStatuses.First(s => s.Title == "Доступно");
        var unavailableStatus = prostheticStatuses.First(s => s.Title == "Не доступно");

        var prosthetics = new List<Prosthetic>
        {
            Prosthetic.New(new ProstheticId(Guid.NewGuid()), "Pro Arm 3000", "Lightweight prosthetic arm for work", 1.5,
                types.First(t => t.Title == "Функціональні").Id,
                materials.First(m => m.Title == "Пластик").Id,
                functionalities.First(f => f.Title == "Механічні").Id,
                amputationLevels.First(a => a.Title == "Плечовий").Id,
                availableStatus.Id
            ),
            Prosthetic.New(new ProstheticId(Guid.NewGuid()), "Bionic Hand X", "Advanced bionic hand with sensors", 2.1,
                types.First(t => t.Title == "Функціональні").Id,
                materials.First(m => m.Title == "Карбон").Id,
                functionalities.First(f => f.Title == "Біонічні").Id,
                amputationLevels.First(a => a.Title == "Кистьовий").Id,
                unavailableStatus.Id
            ),
            Prosthetic.New(new ProstheticId(Guid.NewGuid()), "Runner Leg Pro", "Light leg for sports activities", 1.8,
                types.First(t => t.Title == "Спортивні").Id,
                materials.First(m => m.Title == "Карбон").Id,
                functionalities.First(f => f.Title == "Пасивні").Id,
                amputationLevels.First(a => a.Title == "Гомілковий").Id,
                availableStatus.Id
            ),
            Prosthetic.New(new ProstheticId(Guid.NewGuid()), "Work Arm 100", "Basic mechanical arm for labor", 2.4,
                types.First(t => t.Title == "Робочі").Id,
                materials.First(m => m.Title == "Металеві сплави").Id,
                functionalities.First(f => f.Title == "Механічні").Id,
                amputationLevels.First(a => a.Title == "Плечовий").Id,
                availableStatus.Id
            ),
            Prosthetic.New(new ProstheticId(Guid.NewGuid()), "Elegant Arm", "Cosmetic arm prosthesis", 1.2,
                types.First(t => t.Title == "Косметичні").Id,
                materials.First(m => m.Title == "Пластик").Id,
                functionalities.First(f => f.Title == "Пасивні").Id,
                amputationLevels.First(a => a.Title == "Передплічний").Id,
                unavailableStatus.Id
            ),
            Prosthetic.New(new ProstheticId(Guid.NewGuid()), "MicroArm Lite", "Microelectric advanced control arm", 2.0,
                types.First(t => t.Title == "Функціональні").Id,
                materials.First(m => m.Title == "Металеві сплави").Id,
                functionalities.First(f => f.Title == "Мікроелектричні").Id,
                amputationLevels.First(a => a.Title == "Передплічний").Id,
                availableStatus.Id
            ),
            Prosthetic.New(new ProstheticId(Guid.NewGuid()), "Carbon Leg XT", "Flexible leg for runners", 1.6,
                types.First(t => t.Title == "Спортивні").Id,
                materials.First(m => m.Title == "Карбон").Id,
                functionalities.First(f => f.Title == "Пасивні").Id,
                amputationLevels.First(a => a.Title == "Стегновий").Id,
                availableStatus.Id
            ),
            Prosthetic.New(new ProstheticId(Guid.NewGuid()), "SteelWorker Arm", "Heavy-duty working arm", 3.0,
                types.First(t => t.Title == "Робочі").Id,
                materials.First(m => m.Title == "Металеві сплави").Id,
                functionalities.First(f => f.Title == "Механічні").Id,
                amputationLevels.First(a => a.Title == "Плечовий").Id,
                unavailableStatus.Id
            ),
            Prosthetic.New(new ProstheticId(Guid.NewGuid()), "Beauty Arm", "Designed for natural appearance", 1.0,
                types.First(t => t.Title == "Косметичні").Id,
                materials.First(m => m.Title == "Пластик").Id,
                functionalities.First(f => f.Title == "Пасивні").Id,
                amputationLevels.First(a => a.Title == "Кистьовий").Id,
                availableStatus.Id
            ),
            Prosthetic.New(new ProstheticId(Guid.NewGuid()), "Pro Arm 4000", "Next-gen bionic arm", 2.5,
                types.First(t => t.Title == "Функціональні").Id,
                materials.First(m => m.Title == "Карбон").Id,
                functionalities.First(f => f.Title == "Біонічні").Id,
                amputationLevels.First(a => a.Title == "Плечовий").Id,
                availableStatus.Id
            ),
            Prosthetic.New(new ProstheticId(Guid.NewGuid()), "Sport Leg Z", "Z-shaped spring leg", 1.4,
                types.First(t => t.Title == "Спортивні").Id,
                materials.First(m => m.Title == "Карбон").Id,
                functionalities.First(f => f.Title == "Пасивні").Id,
                amputationLevels.First(a => a.Title == "Гомілковий").Id,
                unavailableStatus.Id
            ),
            Prosthetic.New(new ProstheticId(Guid.NewGuid()), "MicroLeg V2", "Advanced microelectric leg", 2.3,
                types.First(t => t.Title == "Функціональні").Id,
                materials.First(m => m.Title == "Металеві сплави").Id,
                functionalities.First(f => f.Title == "Мікроелектричні").Id,
                amputationLevels.First(a => a.Title == "Стопа").Id,
                availableStatus.Id
            ),
            Prosthetic.New(new ProstheticId(Guid.NewGuid()), "Helper Arm", "Functional everyday arm", 1.9,
                types.First(t => t.Title == "Функціональні").Id,
                materials.First(m => m.Title == "Пластик").Id,
                functionalities.First(f => f.Title == "Механічні").Id,
                amputationLevels.First(a => a.Title == "Передплічний").Id,
                availableStatus.Id
            ),
            Prosthetic.New(new ProstheticId(Guid.NewGuid()), "Classic Cosmetic Leg", "Simple cosmetic leg", 1.3,
                types.First(t => t.Title == "Косметичні").Id,
                materials.First(m => m.Title == "Пластик").Id,
                functionalities.First(f => f.Title == "Пасивні").Id,
                amputationLevels.First(a => a.Title == "Гіп-дізарткуляційний").Id,
                unavailableStatus.Id
            ),
            Prosthetic.New(new ProstheticId(Guid.NewGuid()), "Titan Leg 300", "Titanium leg for extreme conditions", 3.5,
                types.First(t => t.Title == "Робочі").Id,
                materials.First(m => m.Title == "Металеві сплави").Id,
                functionalities.First(f => f.Title == "Механічні").Id,
                amputationLevels.First(a => a.Title == "Стегновий").Id,
                availableStatus.Id
            ),
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
        var prosthetics = context.Prosthetics.Take(5).ToList();

        var reviews = new List<Review>
        {
            Review.New(new ReviewId(Guid.NewGuid()),
                "Після поранення я відчував, що залишився сам на сам зі своєю проблемою. Але коли знайшов цей сайт, все змінилося. Команда волонтерів зв'язалась зі мною вже наступного дня після подачі заявки, вислухала, порадила, підібрала потрібну модель протезу. Все було зроблено дуже людяно та швидко. Я відчув, що комусь не байдуже. Велика подяка кожному, хто долучений до цієї справи. Це не просто сайт — це проєкт із великим серцем, що повертає гідність і впевненість у завтрашньому дні.",
                "Дуже легкий, зручний, не натирає, дозволяє багато рухатися",
                "Невелика затримка з підгонкою під мій розмір",
                user.Id, prosthetics[0].Id),

            Review.New(new ReviewId(Guid.NewGuid()),
                "Я не очікував, що все буде настільки організовано. Заявка була оброблена швидко, мене супроводжували на кожному етапі. Консультація, підбір, доставка — усе безкоштовно, із щирим бажанням допомогти. Це дійсно важливо — знати, що ти не забутий. Волонтери — професіонали, які розуміють, що таке служити, і знають, як підтримати. Дякую за людяність і за якісний результат. Завдяки вам я знову можу нормально пересуватися.",
                "Міцний і добре тримається, не обмежує в побутових діях",
                "Досить важкий для довгих прогулянок",
                user.Id, prosthetics[1].Id),

            Review.New(new ReviewId(Guid.NewGuid()),
                "Волонтери цього проєкту змінили моє уявлення про допомогу. Вони не просто забезпечують протези — вони підтримують морально, пояснюють, надихають. Було видно, що вони дійсно вболівають за кожного ветерана. Мені все пояснили: як користуватись, які вправи робити, як звикати. Протез отримав уже за кілька тижнів, абсолютно безкоштовно. Пишаюсь тим, що в нашій країні є такі ініціативи.",
                "Реалістичний вигляд і комфорт при носінні — виглядає майже як справжня кінцівка",
                "Не надто стійкий до подряпин, потребує догляду",
                user.Id, prosthetics[2].Id),

            Review.New(new ReviewId(Guid.NewGuid()),
                "Я займався спортом ще до війни, і після втрати кінцівки думав, що вже не повернусь до активного життя. Але цей сервіс відкрив для мене нову сторінку. Спеціалісти порадили саме спортивну модель протезу, і це стало ключовим. Тренуюсь щодня. Волонтери завжди на зв'язку, допомагають, мотивують. Дякую вам за шанс рухатись далі!",
                "Дуже легкий, пружний, ідеально підходить для бігу",
                "Немає змінних накладок — довелось докупити",
                user.Id, prosthetics[3].Id),

            Review.New(new ReviewId(Guid.NewGuid()),
                "Цей волонтерський проєкт став рятівним колом, коли всі офіційні установи лиш годували обіцянками. Тут же — жива підтримка, швидкий зворотний зв'язок, відчуття турботи. Заявку оформили просто, пояснили кожен етап. Протез отримав швидко, ще й з інструкціями. Величезна подяка кожному, хто працює в цьому проєкті — це більше, ніж просто допомога.",
                "Виглядає добре — естетичний і пасує під одяг",
                "Майже не функціональний — радше косметичний варіант",
                user.Id, prosthetics[4].Id),
        };

        context.Reviews.AddRange(reviews);
        await context.SaveChangesAsync();
    }
}