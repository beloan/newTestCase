namespace HistoryApi.Entities;

public class EventType
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public ICollection<History> Histories { get; set; } = new List<History>();
}

