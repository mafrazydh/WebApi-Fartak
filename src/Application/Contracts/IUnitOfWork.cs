namespace Application.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository ProductRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        ICartRepository CartRepository { get; }
        IOrderRepository OrderRepository { get; }
        void CompleteAsync();
    }
}
