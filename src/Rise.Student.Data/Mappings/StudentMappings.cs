using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rise.Core.Constants;
using Rise.Students.Domain;

namespace Rise.Students.Data.Mappings
{
    internal sealed class StudentMappings : IEntityTypeConfiguration<Student>
    {
        internal const string Table = "tb_student";

        public void Configure(EntityTypeBuilder<Student> b)
        {
            b.ToTable(Table);

            b.HasKey(u => u.Id);

            b.OwnsOne(u => u.Email, e =>
            {
                e.Property(em => em.Address).IsRequired().HasMaxLength(ConstSizes.EmailAddressMax).HasColumnName("Email");
            });
            b.OwnsOne(u => u.Name, n =>
            {
                n.Property(na => na.FirstName).IsRequired().HasMaxLength(ConstSizes.PersonFirstNameMin).HasColumnName("FirstName");
                n.Property(na => na.Surname).IsRequired().HasMaxLength(ConstSizes.PersonSurnameMax).HasColumnName("Surname");
            });

            b.OwnsOne(u => u.Credentials, c =>
            {
                c.HasIndex(cr => cr.Login).IsUnique();
                c.Property(cr => cr.Login).IsRequired().HasMaxLength(ConstSizes.StudentLoginMax).HasColumnName("Login");
                c.Property(cr => cr.Password).IsRequired().HasMaxLength(32).IsFixedLength().HasColumnName("Password");
                c.Property(cr => cr.Active).HasColumnName("Active");
            });

            b.OwnsOne(s => s.Image, i =>
            {
                i.Property(im => im.Url).HasMaxLength(ConstSizes.ImageUrlMax).HasColumnName("ImageUrl");
                i.Property(im => im.PublicId).HasMaxLength(ConstSizes.ImagePublicIdMax).HasColumnName("ImagePublicId");
            });

            b.OwnsOne(u => u.Address, a =>
            {
                a.Property(ad => ad.ZipCode).HasMaxLength(ConstSizes.AddressZipCodeMax).HasColumnName("AddressZipCode");
                a.Property(ad => ad.Street).HasMaxLength(ConstSizes.AddressStreetMax).HasColumnName("AddressStreet");
                a.Property(ad => ad.Number).HasMaxLength(ConstSizes.AddressNumberMax).HasColumnName("AddressNumber");
                a.Property(ad => ad.Complement).HasMaxLength(ConstSizes.AddressComplementMax).HasColumnName("AddressComplement");
                a.Property(ad => ad.District).HasMaxLength(ConstSizes.AddressDistrictMax).HasColumnName("AddressDistrict");
                a.Property(ad => ad.City).HasMaxLength(ConstSizes.AddressCityMax).HasColumnName("AddressCity");
                a.Property(ad => ad.State).HasMaxLength(ConstSizes.AddressStateMax).HasColumnName("AddressState");
            });
        }
    }
}