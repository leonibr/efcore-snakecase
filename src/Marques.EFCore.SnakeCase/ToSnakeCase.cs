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
            return Regex.Replace(noLeadingUndescore, @"([a-z0-9])([A-Z])", "$1_$2").ToLower();
        }

        public static void ToSnakeCase(this ModelBuilder modelBuilder)
        {
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                entity.SetTableName(entity.GetTableName().ToSnakeCase());

                foreach (var property in entity.GetProperties())
                {
                    property.SetColumnName(property.GetColumnName().ToSnakeCase());
                }

                foreach (var key in entity.GetKeys())
                {
                    key.SetName(key.GetName().ToSnakeCase());
                }

                foreach (var key in entity.GetForeignKeys())
                {
                    key.SetConstraintName(key.GetConstraintName().ToSnakeCase());
                }

                foreach (var index in entity.GetIndexes())
                {
                    index.SetName(index.GetName().ToSnakeCase());
                }
            }

        }

    }
}
