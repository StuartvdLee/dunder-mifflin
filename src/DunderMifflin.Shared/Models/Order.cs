namespace DunderMifflin.Shared.Models;

public class Order
{
    public int Orderid { get; set; }

    public int? Customerid { get; set; }

    public int? Employeeid { get; set; }

    public DateOnly? Orderdate { get; set; }

    public DateOnly? Requireddate { get; set; }

    public DateOnly? Shippeddate { get; set; }

    public int? Shipvia { get; set; }

    public decimal? Freight { get; set; }

    public string? Shipname { get; set; }

    public string? Shipaddress { get; set; }

    public string? Shipcity { get; set; }

    public string? Shipregion { get; set; }

    public string? Shippostalcode { get; set; }

    public string? Shipcountry { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual ICollection<Orderdetail> Orderdetails { get; set; } = new List<Orderdetail>();

    public virtual Shipper? ShipviaNavigation { get; set; }
}
