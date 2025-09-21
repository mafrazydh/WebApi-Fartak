using Application.Contracts;
using AutoMapper;
using Infrastructure.Persistence;

namespace Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        #region Repository
        public IProductRepository ProductRepository { get; private set; }
        public ICategoryRepository CategoryRepository { get; private set; }
        public ICartRepository CartRepository { get; private set; }
        public IOrderRepository OrderRepository { get; private set; }
        //public ICacheManagerService CacheManagerService { get; private set; }
        #endregion

        public UnitOfWork(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            ProductRepository = new ProductRepository(_context, _mapper);
            OrderRepository = new OrderRepository(_context, _mapper);
            CartRepository = new CartRepository(_context, _mapper);
            CategoryRepository = new CategoryRepository(_context, _mapper, ProductRepository);
        }


        public void CompleteAsync()
        {
           _context.SaveChanges();
           
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
