namespace Assets._Project.Develop.Runtime.Utilitis.DataManagment
{
    public interface IDataSerializer
    {
        string Serialize<TData>(TData data);

        TData Deserialize<TData>(string serializedData);
    }
}
