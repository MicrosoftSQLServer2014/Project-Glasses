namespace DAL.Interfaces
{
    public interface ICrud<T>
    {
        void Create(T item);
        T Read();
        void Update();
        void Delete();
    }
}
