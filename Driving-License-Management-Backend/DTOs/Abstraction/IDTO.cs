namespace Driving_License_Management_Backend.DTOs.Abstraction
{
    public interface IDTO<T>
    {
        public void SetValuesFromEntity(T entity);
        public void MapValuesToEntity(T entity);
    }
}
