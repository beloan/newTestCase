using HistoryApi.Entities;

namespace HistoryApi.Data;

public static class DataSeeder
{
    public static void Seed(AppDbContext db)
    {
        if (!db.Users.Any())
        {
            var users = new List<User>
            {
                new User { Id = "3ee3febf-5cc2-45c0-8b23-8b0ea500fa10" },
                new User { Id = "853ff783-f09a-4b2c-b9a2-84f9c95cf602" },
                new User { Id = "9f96e26b-a539-4dcc-8f0f-dd07a87e427d" }
            };

            db.Users.AddRange(users);
            db.SaveChanges();
        }

        if (!db.EventTypes.Any())
        {
            var eventTypes = new List<EventType>
            {
                new EventType { Id = 1, Name = "Login" },
                new EventType { Id = 2, Name = "Logout" },
                new EventType { Id = 3, Name = "Action" }
            };

            db.EventTypes.AddRange(eventTypes);
            db.SaveChanges();
        }

        if (db.Histories.Any())
            return;

        var usersFromDb = db.Users.ToList();
        var eventTypesFromDb = db.EventTypes.ToList();

        var random = new Random();
        var histories = new List<History>();

        for (int i = 0; i < 1000; i++)
        {
            var user = usersFromDb[random.Next(usersFromDb.Count)];
            var eventType = eventTypesFromDb[random.Next(eventTypesFromDb.Count)];

            histories.Add(new History
            {
                UserId = user.Id,
                EventTypeId = eventType.Id,
                Dt = DateTime.UtcNow.AddMinutes(-random.Next(0, 100000)),
                Text = $"Generated event #{i + 1}"
            });
        }

        db.Histories.AddRange(histories);
        db.SaveChanges();

    }
}


