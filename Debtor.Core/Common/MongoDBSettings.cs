namespace Debtor.Core.Common;

public class MongoDBSettings : IMongoDBSettings
{
    public string ConnectionString { get; set; } = string.Empty;
    public string DatabaseName { get; set; } = string.Empty;
    public string UserCollectionName { get; set; } = string.Empty;
    public string TransactionCollectionName { get; set; } = string.Empty;
    public string JointCollectionName { get; set; } = string.Empty;
}