using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Text.RegularExpressions;

namespace Marques.EFCore.SnakeCase
{
    public static class SnakeToCamelCase
    {
        [Flags]
        public enum ConvertOptions 
        {
            All = 2 << 0,
            Tables = 2 << 1,
            Properties = 2 << 2,
            Keys = 2 << 3,
            ForeignKeys = 2 << 4,
            Indexes = 2 << 5,
        };
        private static Regex leadingUndescoreRegex = new Regex(@"^_", RegexOptions.Compiled);
        private static Regex camelCaseRegex = new Regex(@"(?:(?<l>[a-z0-9])(?<r>[A-Z])|(?<l>[A-Z])(?<r>[A-Z][a-z0-9]))", RegexOptions.Compiled);
        
        public static string ToSnakeCase(this string input)
        {
            if (string.IsNullOrEmpty(input)) { return input; }

            var noLeadingUndescore = leadingUndescoreRegex.Replace(input, "");
            return camelCaseRegex.Replace(noLeadingUndescore, "${l}_${r}").ToLower();
        }

        public static void ToSnakeCase(this ModelBuilder modelBuilder, ConvertOptions options = ConvertOptions.All)
        {
            bool convertAll = (options & ConvertOptions.All) == ConvertOptions.All;
            bool convertTables = (options & ConvertOptions.Tables) == ConvertOptions.Tables;
            bool convertProperties = (options & ConvertOptions.Properties) == ConvertOptions.Properties;
            bool convertKeys = (options & ConvertOptions.Keys) == ConvertOptions.Keys;
            bool convertForeignKeys = (options & ConvertOptions.ForeignKeys) == ConvertOptions.ForeignKeys;
            bool convertIndexes = (options & ConvertOptions.Indexes) == ConvertOptions.Indexes;

            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                if (convertAll || convertTables)
                    if (entity.BaseType == null)
                        entity.SetTableName(entity.GetTableName().ToSnakeCase());

                if (convertAll || convertProperties)
                    foreach (var property in entity.GetProperties())
                    {
                        property.SetColumnName(
                                property
                                    .GetColumnName(StoreObjectIdentifier.Table(entity.GetTableName(), entity.GetSchema()))
                                    .ToSnakeCase());
                    }

                if (convertAll || convertKeys)
                    foreach (var key in entity.GetKeys())
                    {
                        key.SetName(key.GetName().ToSnakeCase());
                    }

                if (convertAll || convertForeignKeys)
                    foreach (var key in entity.GetForeignKeys())
                    {
                        key.SetConstraintName(key.GetConstraintName().ToSnakeCase());
                    }

                if (convertAll || convertIndexes)
                    foreach (var index in entity.GetIndexes())
                    {
                        index.SetDatabaseName(index.GetDatabaseName().ToSnakeCase());
                    }
            }
        }

    }
}
