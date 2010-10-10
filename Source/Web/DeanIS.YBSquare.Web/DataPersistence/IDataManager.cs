namespace DeanIS.YBSquare.Web.DataPersistence
{
    public interface IDataManager
    {
        IPersistable[] PersistableData { get; }
    }
}