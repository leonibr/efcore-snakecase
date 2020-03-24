using Microsoft.EntityFrameworkCore;
using System;
using System.Text.RegularExpressions;

namespace Marques.EFCore.SnakeCase
{
    public static class SnakeToCamelCase
    {
        public static string ToSnakeCase(this string input)
        {
            if (string.IsNullOrEmpty(input)) { return input; }

            var noLeadingUndescore = Regex.Replace(input, @"^_", "");
            return Regex.Replace(noLeadingUndescore, @"(?:(?<l>[a-z0-9])(?<r>[A-Z])|(?<l>[A-Z])(?<r>[A-Z][a-z0-9]))", "${l}_${r}").ToLower();
        }

        public static void ToSnakeCase(this ModelBuilder modelBuilder)
        {
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                entity.Relational().TableName = entity.Relational().TableName.ToSnakeCase();

                foreach (var property in entity.GetProperties())
                {
                    property.Relational().ColumnName = property.Name.ToSnakeCase();
                }

                foreach (var key in entity.GetKeys())
                {
                    key.Relational().Name = key.Relational().Name.ToSnakeCase();
                }

                foreach (var key in entity.GetForeignKeys())
                {
                    key.Relational().Name = key.Relational().Name.ToSnakeCase();
                }

                foreach (var index in entity.GetIndexes())
                {
                    index.Relational().Name = index.Relational().Name.ToSnakeCase();
                }
            }

        }

    }
}
