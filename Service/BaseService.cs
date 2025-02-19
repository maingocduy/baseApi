using AutoMapper;
using BaseApi.Databases.TM;

namespace BaseApi.Service
{
    public class BaseService
    {
        protected readonly DBContext _dbContext;
        protected readonly IMapper _mapper;
        protected readonly IConfiguration _configuration;

     

        public BaseService(DBContext dbContext, IMapper mapper, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _configuration = configuration;
        }

        public DBContext DbContext { get; }
        public IMapper Mapper { get; }

        protected TResult ExecuteInTransaction<TResult>(Func<TResult> action)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    var result = action();
                    transaction.Commit();
                    return result;
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
        
    }
}
