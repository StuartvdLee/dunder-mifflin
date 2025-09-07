namespace DunderMifflin.Shared.Models;

public class Product
{
    public int Productid { get; set; }

    public string Productname { get; set; } = null!;

    public string? Productdescription { get; set; }

    public int? Supplierid { get; set; }

    public int? Categoryid { get; set; }

    public string? Quantityperunit { get; set; }

    public decimal? Unitprice { get; set; }

    public int? Unitsinstock { get; set; }

    public int? Unitsonorder { get; set; }

    public int? Reorderlevel { get; set; }

    public bool Discontinued { get; set; }

    public virtual Category? Category { get; set; }

    public virtual ICollection<Orderdetail> Orderdetails { get; set; } = new List<Orderdetail>();

    public virtual Supplier? Supplier { get; set; }
}
