﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoneyMaster.Domain.Entities;

namespace MoneyMaster.Infrastructure.EntityFramework.Configurations
{
    /// <summary>Конфигурация для таблицы типы учетной записи</summary>
    public class AccountTypeConfiguration : IEntityTypeConfiguration<AccountType>
    {
        public void Configure(EntityTypeBuilder<AccountType> builder)
        {
            builder.ToTable("AccountTypes").HasKey(x => x.Id);

            //Связь с таблицей Account один к многим
            builder.HasMany(x => x.Accounts)
                .WithOne(x => x.Type)
                .HasForeignKey(x => x.AccountId);
        }
    }
}