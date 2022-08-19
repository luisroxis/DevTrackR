namespace DevTrackR.API.Models
{
  public class PackageInputModel
  {
    public string Title { get; set; }
    public decimal Weight { get; set; }
    public string? SenderName { get; set; }
    public string? SenderEmail { get; set; }
  }
}