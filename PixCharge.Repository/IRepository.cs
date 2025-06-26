using System.Linq.Expressions;

namespace PixCharge.Repository;
public interface IRepository<T> where T : class, new()
{
    public void Save(ref T entity);
    public void Update(ref T entity);
    public void Delete(T entity);
    public IEnumerable<T> GetAll();
    public T GetById(Guid id);
    public IEnumerable<T> Find(Expression<Func<T, bool>> expression);
    public bool Exists(Expression<Func<T, bool>> expression);
}