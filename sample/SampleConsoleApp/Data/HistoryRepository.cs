using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Sqlite.Migrations.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace SampleConsoleApp.Data
{
    public class HistoryRepository: SqliteHistoryRepository
    {
        public HistoryRepository(HistoryRepositoryDependencies dependencies)
           : base(dependencies)
        {
        }
        protected override void ConfigureTable(EntityTypeBuilder<HistoryRow> history)
        {
            base.ConfigureTable(history);
           
            history.Property(h => h.MigrationId).HasColumnName("migration_id");
            history.Property(h => h.ProductVersion).HasColumnName("product_version");

        }

    }

}
