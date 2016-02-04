
namespace PersistedAttachedProperties.AttachedProperties
{
    public interface IAttachedDataRepositoryLong
    {
        T GetValue<T>(long id, T defauts = null) where T : class, IAttachedData;
        void SetValue<T>(T value) where T : class, IAttachedData;
    }
}
