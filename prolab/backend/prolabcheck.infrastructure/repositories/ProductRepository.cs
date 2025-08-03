using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using prolabcheck.domain.DTOs;
using prolabcheck.domain.interfaces;
using prolabcheck.infrastructure.data;

namespace prolabcheck.infrastructure.repositories
{
    public class ProductRepository: IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SearchMedicineDTO>> GetByCodeAsync(string code)
        {
            var parameter = new SqlParameter("@InternalCode", code);
            //return _context.tb_productos
            //    .FromSqlRaw("EXEC SP_GetProductByInternalCode @InternalCode", parameter)
            //    .AsEnumerable()
            //    .ToList();
            return await _context.SearchMedicineDTO
                .FromSqlRaw("EXEC SP_GetProductByInternalCode @InternalCode", parameter)
                .ToListAsync();
        }

    }
}
