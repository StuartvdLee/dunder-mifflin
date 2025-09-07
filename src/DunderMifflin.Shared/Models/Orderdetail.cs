namespace DunderMifflin.Shared.Models;

public class Orderdetail
{
    public int Orderid { get; set; }

    public int Productid { get; set; }

    public decimal Unitprice { get; set; }

    public int Quantity { get; set; }

    public decimal Discount { get; set; }

    public decimal? Linetotal { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
