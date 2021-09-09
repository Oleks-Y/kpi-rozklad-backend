using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Data;
using Microsoft.Extensions.Logging;

namespace Rozklad.Core.Data
{
    public abstract class RepositoryBase
    {
        protected ILogger Logger { get; private set; }

        protected IDatabase Database { get; private set; }

        protected RepositoryBase(IDatabase database, ILogger logger)
        {
            Database = database;
            Logger = logger;
        }

        
        protected static IReadOnlyList<Item> PopulateItemFromDb(DataTable queryResult)
        {
            var data = new List<Item>();

            foreach (DataRow row in queryResult.Rows)
            {
                var iterator = 0;

                var item = new Item
                {
                    Id = int.Parse(row.ItemArray[iterator++].ToString()),
                    Name = row.ItemArray[iterator].ToString()
                };

                data.Add(item);
            }

            return data.ToImmutableList();
        }
		
        protected static string GenerateFilterClause(string sortField, Direction direction)
        {
            return $" ORDER BY  {sortField} {ConvertToString(direction)} LIMIT @skip, @size";
        }

        private static string ConvertToString(Direction direction)
        {
            return direction switch
            {
                Direction.Asc => "ASC",
                Direction.Desc => "DESC",
                _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
            };
        }
    }
}