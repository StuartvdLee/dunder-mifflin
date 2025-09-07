using DunderMifflin.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace DunderMifflin.Shared.Data;

public partial class DunderMifflinDbContext : DbContext
{
    public DunderMifflinDbContext(DbContextOptions<DunderMifflinDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Employeestatus> Employeestatuses { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Orderdetail> Orderdetails { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Region> Regions { get; set; }

    public virtual DbSet<Shipper> Shippers { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Categoryid).HasName("categories_pkey");

            entity.ToTable("categories", "dundermifflin");

            entity.Property(e => e.Categoryid).HasColumnName("categoryid");
            entity.Property(e => e.Categoryname)
                .HasMaxLength(50)
                .HasColumnName("categoryname");
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .HasColumnName("description");
            entity.Property(e => e.Picture)
                .HasMaxLength(1)
                .HasColumnName("picture");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Customerid).HasName("customers_pkey");

            entity.ToTable("customers", "dundermifflin");

            entity.HasIndex(e => e.Customercode, "customers_customercode_key").IsUnique();

            entity.Property(e => e.Customerid).HasColumnName("customerid");
            entity.Property(e => e.Address)
                .HasMaxLength(100)
                .HasColumnName("address");
            entity.Property(e => e.City)
                .HasMaxLength(100)
                .HasColumnName("city");
            entity.Property(e => e.Companyname)
                .HasMaxLength(200)
                .HasColumnName("companyname");
            entity.Property(e => e.Contactname)
                .HasMaxLength(100)
                .HasColumnName("contactname");
            entity.Property(e => e.Contacttitle)
                .HasMaxLength(100)
                .HasColumnName("contacttitle");
            entity.Property(e => e.Country)
                .HasMaxLength(50)
                .HasColumnName("country");
            entity.Property(e => e.Customercode)
                .HasMaxLength(5)
                .HasColumnName("customercode");
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .HasColumnName("phone");
            entity.Property(e => e.Postalcode)
                .HasMaxLength(50)
                .HasColumnName("postalcode");
            entity.Property(e => e.Region)
                .HasMaxLength(50)
                .HasColumnName("region");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Employeeid).HasName("employees_pkey");

            entity.ToTable("employees", "dundermifflin");

            entity.Property(e => e.Employeeid).HasColumnName("employeeid");
            entity.Property(e => e.Address)
                .HasMaxLength(100)
                .HasColumnName("address");
            entity.Property(e => e.Birthdate).HasColumnName("birthdate");
            entity.Property(e => e.City)
                .HasMaxLength(100)
                .HasColumnName("city");
            entity.Property(e => e.Country)
                .HasMaxLength(50)
                .HasColumnName("country");
            entity.Property(e => e.Extension)
                .HasMaxLength(10)
                .HasColumnName("extension");
            entity.Property(e => e.Firstname)
                .HasMaxLength(100)
                .HasColumnName("firstname");
            entity.Property(e => e.Hiredate).HasColumnName("hiredate");
            entity.Property(e => e.Homephone)
                .HasMaxLength(100)
                .HasColumnName("homephone");
            entity.Property(e => e.Lastname)
                .HasMaxLength(100)
                .HasColumnName("lastname");
            entity.Property(e => e.Middlename)
                .HasMaxLength(100)
                .HasColumnName("middlename");
            entity.Property(e => e.Notes)
                .HasColumnType("character varying")
                .HasColumnName("notes");
            entity.Property(e => e.Photopath)
                .HasColumnType("character varying")
                .HasColumnName("photopath");
            entity.Property(e => e.Postalcode)
                .HasMaxLength(100)
                .HasColumnName("postalcode");
            entity.Property(e => e.Region)
                .HasMaxLength(100)
                .HasColumnName("region");
            entity.Property(e => e.Rehiredate).HasColumnName("rehiredate");
            entity.Property(e => e.Reportsto).HasColumnName("reportsto");
            entity.Property(e => e.Statuscode)
                .HasMaxLength(10)
                .HasColumnName("statuscode");
            entity.Property(e => e.Terminationdate).HasColumnName("terminationdate");
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .HasColumnName("title");
            entity.Property(e => e.Titleofcourtesy)
                .HasMaxLength(20)
                .HasColumnName("titleofcourtesy");

            entity.HasOne(d => d.ReportstoNavigation).WithMany(p => p.InverseReportstoNavigation)
                .HasForeignKey(d => d.Reportsto)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("employees_reportsto_fkey");

            entity.HasOne(d => d.StatuscodeNavigation).WithMany(p => p.Employees)
                .HasPrincipalKey(p => p.Statuscode)
                .HasForeignKey(d => d.Statuscode)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("employees_statuscode_fkey");
        });

        modelBuilder.Entity<Employeestatus>(entity =>
        {
            entity.HasKey(e => e.Statusid).HasName("employeestatus_pkey");

            entity.ToTable("employeestatus", "dundermifflin");

            entity.HasIndex(e => e.Statuscode, "employeestatus_statuscode_key").IsUnique();

            entity.Property(e => e.Statusid).HasColumnName("statusid");
            entity.Property(e => e.Statuscode)
                .HasMaxLength(10)
                .HasColumnName("statuscode");
            entity.Property(e => e.Statusname)
                .HasMaxLength(50)
                .HasColumnName("statusname");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Orderid).HasName("orders_pkey");

            entity.ToTable("orders", "dundermifflin");

            entity.HasIndex(e => e.Customerid, "idx_orders_customer");

            entity.HasIndex(e => new { e.Orderdate, e.Shippeddate }, "idx_orders_dates");

            entity.HasIndex(e => e.Employeeid, "idx_orders_employee");

            entity.Property(e => e.Orderid).HasColumnName("orderid");
            entity.Property(e => e.Customerid).HasColumnName("customerid");
            entity.Property(e => e.Employeeid).HasColumnName("employeeid");
            entity.Property(e => e.Freight)
                .HasPrecision(12, 2)
                .HasColumnName("freight");
            entity.Property(e => e.Orderdate).HasColumnName("orderdate");
            entity.Property(e => e.Requireddate).HasColumnName("requireddate");
            entity.Property(e => e.Shipaddress)
                .HasMaxLength(200)
                .HasColumnName("shipaddress");
            entity.Property(e => e.Shipcity)
                .HasMaxLength(100)
                .HasColumnName("shipcity");
            entity.Property(e => e.Shipcountry)
                .HasMaxLength(50)
                .HasColumnName("shipcountry");
            entity.Property(e => e.Shipname)
                .HasMaxLength(200)
                .HasColumnName("shipname");
            entity.Property(e => e.Shippeddate).HasColumnName("shippeddate");
            entity.Property(e => e.Shippostalcode)
                .HasMaxLength(50)
                .HasColumnName("shippostalcode");
            entity.Property(e => e.Shipregion)
                .HasMaxLength(100)
                .HasColumnName("shipregion");
            entity.Property(e => e.Shipvia).HasColumnName("shipvia");

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                .HasForeignKey(d => d.Customerid)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("orders_customerid_fkey");

            entity.HasOne(d => d.Employee).WithMany(p => p.Orders)
                .HasForeignKey(d => d.Employeeid)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("orders_employeeid_fkey");

            entity.HasOne(d => d.ShipviaNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.Shipvia)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("orders_shipvia_fkey");
        });

        modelBuilder.Entity<Orderdetail>(entity =>
        {
            entity.HasKey(e => new { e.Orderid, e.Productid }).HasName("orderdetails_pkey");

            entity.ToTable("orderdetails", "dundermifflin");

            entity.HasIndex(e => e.Productid, "idx_orderdetails_prod");

            entity.Property(e => e.Orderid).HasColumnName("orderid");
            entity.Property(e => e.Productid).HasColumnName("productid");
            entity.Property(e => e.Discount)
                .HasPrecision(4, 3)
                .HasDefaultValueSql("0.000")
                .HasColumnName("discount");
            entity.Property(e => e.Linetotal)
                .HasPrecision(10, 2)
                .HasColumnName("linetotal");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.Unitprice)
                .HasPrecision(10, 2)
                .HasColumnName("unitprice");

            entity.HasOne(d => d.Order).WithMany(p => p.Orderdetails)
                .HasForeignKey(d => d.Orderid)
                .HasConstraintName("orderdetails_orderid_fkey");

            entity.HasOne(d => d.Product).WithMany(p => p.Orderdetails)
                .HasForeignKey(d => d.Productid)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("orderdetails_productid_fkey");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Productid).HasName("products_pkey");

            entity.ToTable("products", "dundermifflin");

            entity.HasIndex(e => e.Categoryid, "idx_products_cat");

            entity.HasIndex(e => e.Supplierid, "idx_products_supp");

            entity.Property(e => e.Productid).HasColumnName("productid");
            entity.Property(e => e.Categoryid).HasColumnName("categoryid");
            entity.Property(e => e.Discontinued)
                .HasDefaultValue(false)
                .HasColumnName("discontinued");
            entity.Property(e => e.Productdescription)
                .HasColumnType("character varying")
                .HasColumnName("productdescription");
            entity.Property(e => e.Productname)
                .HasMaxLength(100)
                .HasColumnName("productname");
            entity.Property(e => e.Quantityperunit)
                .HasMaxLength(50)
                .HasColumnName("quantityperunit");
            entity.Property(e => e.Reorderlevel).HasColumnName("reorderlevel");
            entity.Property(e => e.Supplierid).HasColumnName("supplierid");
            entity.Property(e => e.Unitprice)
                .HasPrecision(10, 2)
                .HasColumnName("unitprice");
            entity.Property(e => e.Unitsinstock).HasColumnName("unitsinstock");
            entity.Property(e => e.Unitsonorder).HasColumnName("unitsonorder");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.Categoryid)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("products_categoryid_fkey");

            entity.HasOne(d => d.Supplier).WithMany(p => p.Products)
                .HasForeignKey(d => d.Supplierid)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("products_supplierid_fkey");
        });

        modelBuilder.Entity<Region>(entity =>
        {
            entity.HasKey(e => e.Regionid).HasName("regions_pkey");

            entity.ToTable("regions", "dundermifflin");

            entity.Property(e => e.Regionid).HasColumnName("regionid");
            entity.Property(e => e.Isactive)
                .HasDefaultValue(true)
                .HasColumnName("isactive");
            entity.Property(e => e.Regionname)
                .HasMaxLength(100)
                .HasColumnName("regionname");
        });

        modelBuilder.Entity<Shipper>(entity =>
        {
            entity.HasKey(e => e.Shipperid).HasName("shippers_pkey");

            entity.ToTable("shippers", "dundermifflin");

            entity.Property(e => e.Shipperid).HasColumnName("shipperid");
            entity.Property(e => e.Companyname)
                .HasMaxLength(40)
                .HasColumnName("companyname");
            entity.Property(e => e.Phone)
                .HasMaxLength(24)
                .HasColumnName("phone");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(e => e.Supplierid).HasName("suppliers_pkey");

            entity.ToTable("suppliers", "dundermifflin");

            entity.Property(e => e.Supplierid).HasColumnName("supplierid");
            entity.Property(e => e.Address)
                .HasMaxLength(60)
                .HasColumnName("address");
            entity.Property(e => e.City)
                .HasMaxLength(50)
                .HasColumnName("city");
            entity.Property(e => e.Companyname)
                .HasMaxLength(50)
                .HasColumnName("companyname");
            entity.Property(e => e.Contactname)
                .HasMaxLength(50)
                .HasColumnName("contactname");
            entity.Property(e => e.Contacttitle)
                .HasMaxLength(50)
                .HasColumnName("contacttitle");
            entity.Property(e => e.Country)
                .HasMaxLength(50)
                .HasColumnName("country");
            entity.Property(e => e.Fax)
                .HasMaxLength(50)
                .HasColumnName("fax");
            entity.Property(e => e.Homepage)
                .HasMaxLength(1000)
                .HasColumnName("homepage");
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .HasColumnName("phone");
            entity.Property(e => e.Postalcode)
                .HasMaxLength(50)
                .HasColumnName("postalcode");
            entity.Property(e => e.Region)
                .HasMaxLength(50)
                .HasColumnName("region");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
