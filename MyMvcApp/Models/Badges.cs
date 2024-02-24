namespace MyMvcApp.Models
{
    public class Badges
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int UserId { get; set; }
    public DateTime Date { get; set; }
}

}
