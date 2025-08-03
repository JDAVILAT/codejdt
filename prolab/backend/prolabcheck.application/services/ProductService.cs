using AutoMapper;
using prolabcheck.domain.DTOs;
using prolabcheck.domain.exceptions;
using prolabcheck.domain.interfaces;

namespace prolabcheck.application.services
{
    public class ProductService
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SearchMedicineDTO>> GetByCodeAsync(string code)
        {
            try
            {
                return await _repository.GetByCodeAsync(code);
                //return _mapper.Map<IEnumerable<ProductDTO>>(result);
            }
            catch (Exception ex)
            {
                throw new CustomException("Error al obtener los datos.");
            }
        }
    }
}
