using System;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.Logging;

namespace Campus.DAL
{
    public sealed class QueryManager
    {
        private readonly ILogger<QueryManager> _logger;
        
        private string ContentRoot { get; set; }

        public QueryManager([NotNull] string contentRootPath, ILogger<QueryManager> logger)
        {
            _logger = logger;
            ContentRoot = new QueriesRootDirectoryLocator().Find(contentRootPath);
        }

        public string GetContent(MethodBase method, string fileName) 
        {
            if (string.IsNullOrWhiteSpace(ContentRoot))
            {
                throw new Exception("QueryManager wa not Initialized. Please call `QueryManager.Initialize()` before using query manager.");
            }
            
            try
            {
                var path = QueryFilePathBuilder.Build(ContentRoot, method?.DeclaringType?.Name, fileName);
                
                _logger.LogInformation($"Try to load SQL script from: `{path}`");
                
                return File.ReadAllText(path);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error in QueryManager.GetContent");
                throw;
            }
        }
    }

    public class QueryFilePathBuilder
    {
        public static string Build(string queriesRootDirectory, string directoryName, string fileName) =>
            $"{queriesRootDirectory}/SQL/{directoryName}/{fileName}.sql";
    }

    internal class QueriesRootDirectoryLocator
    {
        private bool ValidateRootDirectory(string rootDirectory)
        {
            var path = QueryFilePathBuilder.Build(rootDirectory, "AccountQueryManager", "GetUserByEmail");
            return File.Exists(path);
        }

        public string Find(string contentRootPath)
        {
            var paths = new[]
            {
                contentRootPath,
                $"{Path.GetDirectoryName(contentRootPath)}/Rozklad.DAL"
            };

            foreach (var path in paths)
            {
                if (ValidateRootDirectory(path))
                {
                    return path;
                }
            }

            throw new DirectoryNotFoundException("Proper directory for QueryManager not found");
        }
    }
}