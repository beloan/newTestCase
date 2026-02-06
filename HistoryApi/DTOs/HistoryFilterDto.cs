using System;

namespace HistoryApi.DTOs
{
    public class HistoryFilterDto
    {
        public int Id { get; set; }
        public string Text { get; set; } = "";
        public string? UserFullName{ get; set; }
        public DateTime Dt { get; set; }
        public int EventTypeId { get; set; }
        public string EventTypeName { get; set; } = "";
    }
}

