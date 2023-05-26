using CustomersAPI.Dtos;
using CustomersAPI_VS22.Repositories;
using Microsoft.IdentityModel.Tokens;

namespace CustomersAPI_VS22.CasosDeUso
{
    public interface IUpdateCustomerUseCase
    {
        Task<CustomerDto?> Execute(CustomerDto customer);
    }
    public class UpdateCustomerUseCase : IUpdateCustomerUseCase
    {
        private readonly CustomerDatabaseContext _customerDatabaseContext;

        public UpdateCustomerUseCase(CustomerDatabaseContext customerDatabaseContext)
        {
            _customerDatabaseContext = customerDatabaseContext;
        }
        public async Task<CustomerDto?> Execute(CustomerDto customer) 
        { 
            var entity = await _customerDatabaseContext.Get(customer.Id);
            if (entity == null)
                return null;
            entity.FirstName = customer.FirstName;
            entity.LastName = customer.LastName;
            entity.Email = customer.Email;
            entity.Phone = customer.Phone;
            entity.Address = customer.Address;

            await _customerDatabaseContext.Update(entity);
            return entity.ToDto();


        }
    }
}
