namespace DunderMifflin.Shared.Models;

public class Shipper
{
    public int Shipperid { get; set; }

    public string Companyname { get; set; } = null!;

    public string? Phone { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
