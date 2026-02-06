public class User
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string FullName { get; set; } = string.Empty;

    public ICollection<History> Histories { get; set; } = new List<History>();
}

