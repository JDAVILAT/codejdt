using prolabcheck.domain.DTOs;
using prolabcheck.domain.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prolabcheck.domain.interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<SearchMedicineDTO>> GetByCodeAsync(string code);
    }
}
