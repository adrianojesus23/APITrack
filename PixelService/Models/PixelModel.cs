namespace PixelServices.Models
{
    public record PixelModel
    {
        public string Referrer { get; set; } = string.Empty;
        public string UserAgent { get; set; } = string.Empty;
        public string IpAddress { get; set; } = string.Empty;
    }
}
