using Microsoft.Azure.Cosmos.Table;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace StockSmartFunctions
{
    public class ChangeCosmosDB
    {
        private readonly ILogger _logger;
        private readonly CloudTable _logTable;

        public ChangeCosmosDB(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<ChangeCosmosDB>();

            // Inicializar la tabla en Azure
            var storageConnectionString = Environment.GetEnvironmentVariable(
                "StorageAccountConnectionString"
            );
            var storageAccount = CloudStorageAccount.Parse(storageConnectionString);
            var tableClient = storageAccount.CreateCloudTableClient(new TableClientConfiguration());
            _logTable = tableClient.GetTableReference("FunctionLogs");

            // Crea la tabla si no existe
            _logTable.CreateIfNotExists();
        }

        [Function("ChangeCosmosDB")]
        public async Task Run(
            [CosmosDBTrigger(
                databaseName: "cosmosAED",
                containerName: "productos",
                Connection = "CosmosDBConnectionString",
                LeaseContainerName = "leases",
                CreateLeaseContainerIfNotExists = true
            )]
                IReadOnlyList<MyInfo> input
        )
        {
            if (input != null && input.Count > 0)
            {
                _logger.LogInformation("Documents modified: " + input.Count);
                _logger.LogInformation("Product ID: " + input[0].ProductID);
                _logger.LogInformation("Product Name: " + input[0].ProductName);

                // Crear entrada para el log
                var logEntry = new LogEntity
                {
                    PartitionKey = "ChangeCosmosDB",
                    RowKey = Guid.NewGuid().ToString(),
                    ProductID = input[0].ProductID,
                    ProductName = input[0].ProductName,
                    LogTime = DateTime.UtcNow,
                    Message =
                        $"Documentos modificados: {input.Count}, ID del producto: {input[0].ProductID}, Nombre del producto: {input[0].ProductName}"
                };

                // Insertar log Entry en la tabla
                var insertOperation = TableOperation.Insert(logEntry);
                await _logTable.ExecuteAsync(insertOperation);
            }
        }
    }

    public class MyInfo
    {
        public string ProductID { get; set; }
        public string ProductName { get; set; }
    }

    public class LogEntity : TableEntity
    {
        public string ProductID { get; set; }
        public string ProductName { get; set; }
        public DateTime LogTime { get; set; }
        public string Message { get; set; }
    }
}
