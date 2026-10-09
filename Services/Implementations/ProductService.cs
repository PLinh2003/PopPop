using PopPop.Services.Interfaces;

namespace PopPop.Services.Implementations
{
    public class ProductService : IProductService
    {
        public int Sum(int a, int b)
        {
            return a + b;
        }
    }
}
