namespace Debtor.Core.Common;

public interface IMongoDBSettings
{
    public string ConnectionString { get; set; }
    public string DatabaseName { get; set; }
    public string UsersCollectionName { get; set; }
    public string TransactionCollectionName { get; set; }
    public string JointCollectionName { get; set; }

}