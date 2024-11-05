using AutoMapper;
using TaskMonitor.Databases.TM;
using TaskMonitor.Models.DataInfo;
using static TaskMonitor.Repository.BaseRepository;

namespace TaskMonitor.Repository
{
    public interface IBaseRepository
    {
     
        DBContext GetDBContext();
        T UpdateItem<T>(T item);

        T CreateItem<T>(T entity) where T : class;



    }
    public class BaseRepository : IBaseRepository
    {
        protected readonly DBContext _dbContext;

        public BaseRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public DBContext GetDBContext()
        {
            return _dbContext;
        }

        public T UpdateItem<T>(T item)
        {

            _dbContext.SaveChanges();

            return item;

        }


        public T CreateItem<T>(T entity) where T : class
        {
            _dbContext.Set<T>().Add(entity);
            _dbContext.SaveChanges();
            return entity;
        }
        

    }
}
