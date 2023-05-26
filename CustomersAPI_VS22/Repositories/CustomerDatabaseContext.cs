using CustomersAPI.Dtos;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
 
namespace CustomersAPI_VS22.Repositories
{
    
    public class CustomerDatabaseContext : DbContext
    {
        public CustomerDatabaseContext(DbContextOptions<CustomerDatabaseContext> options)
            : base(options) //ejecuta el constructor de padre
        {

        }
        //hau balio du DBtik balioak lortzeko, tablaren izena ipini behar diogu
        public DbSet<CustomerEntity> U_customer { get; set; }
        // Balioak jasotzeko 
        public async Task<CustomerEntity?> Get(int id)
        {
            return await U_customer.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<CustomerEntity> Add(CreateCustomerDto customerDto)
        {
            CustomerEntity entity = new CustomerEntity()
            {
                Id = null,
                Address = customerDto.Address,
                Email = customerDto.Email,
                FirstName = customerDto.FirstName,
                LastName = customerDto.LastName,
                Phone = customerDto.Phone
            };
            EntityEntry<CustomerEntity> response = await U_customer.AddAsync(entity);
            await SaveChangesAsync();
            return await Get(response.Entity.Id ?? throw new Exception("no se ha podido guardar"));
        }
        public async Task<bool> Delete(int id)
        {
            var entity = await Get(id);
            U_customer.Remove(entity);
            await SaveChangesAsync();
            return true;
        }
        public async Task<bool> Update(CustomerEntity customerEntity)
        {
            U_customer.Update(customerEntity);
            await SaveChangesAsync();
            return true;
        }
    }
    public class CustomerEntity
    {
        public int? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public CustomerDto ToDto()
        {
            return new CustomerDto()
            {
                Address = Address,
                Email = Email,
                FirstName = FirstName,
                LastName = LastName,
                Phone = Phone,
                Id = Id ?? throw new Exception("El id no puede ser null")
            };
        }
    }
}
