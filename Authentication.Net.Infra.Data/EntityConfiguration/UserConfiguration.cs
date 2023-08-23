using Authentication.Net.Domain.Entities;
using K4os.Hash.xxHash;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Authentication.Net.Infra.Data.EntityConfiguration
{
	public class UserConfiguration : IEntityTypeConfiguration<User>
	{
		public void Configure(EntityTypeBuilder<User> builder)
		{
			builder.HasKey(x => x.Id);
			builder.Property(x => x.CreatedAt).HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP").IsRequired();
			builder.Property(x => x.FullName).HasMaxLength(255).IsRequired();
			builder.Property(x => x.Email).HasMaxLength(255).IsRequired();
			builder.Property(x => x.Cpf).HasMaxLength(14).IsRequired();
			builder.Property(x => x.Password).HasMaxLength(255).IsRequired();
			builder.Property(x => x.IsEmailConfirmed).HasColumnType("TINYINT(1)").IsRequired();
			builder.Property(x => x.IsActive).HasColumnType("TINYINT(1)").IsRequired();
			builder.Property(x => x.IsAdmin).HasColumnType("TINYINT(1)").IsRequired();
			builder.Property(x => x.Auth).HasMaxLength(255);
		}
	}
}
