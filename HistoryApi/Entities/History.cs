using HistoryApi.Entities;

public class History
{
    public int Id { get; set; }

    public string UserId { get; set; } = null!;
    public User User { get; set; } = null!;
    public string Text { get; set; } = string.Empty;

    public int EventTypeId { get; set; }
    public EventType EventType { get; set; } = null!;

    public DateTime Dt { get; set; }
}


