using CustomersAPI.Dtos;
using CustomersAPI_VS22.CasosDeUso;
using CustomersAPI_VS22.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CustomersAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : Controller
    {
        private readonly CustomerDatabaseContext _customerDatabaseContext;
        private readonly IUpdateCustomerUseCase _updateCustomerUseCase;


        public CustomerController(CustomerDatabaseContext customerDatabaseContext,
            IUpdateCustomerUseCase updateCustomerUseCase)
        {
            _customerDatabaseContext = customerDatabaseContext;
            _updateCustomerUseCase = updateCustomerUseCase;
        }
        [HttpGet]
        public async Task<IActionResult> GetCustomers()
        {
            var result = _customerDatabaseContext.U_customer.Select(c=>c.ToDto()).ToList();
            return new OkObjectResult(result);

        }
        //api/customer/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomerDto))] // para que lo expecifique en el swagger y en el json lo que puede responder sin tener que ejecutar nada
        public async Task<IActionResult> GetCustomer(int id)
        {
            CustomerEntity result = await _customerDatabaseContext.Get(id);

            return new OkObjectResult(result.ToDto());
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var result = await _customerDatabaseContext.Delete(id);

            return new OkObjectResult(result);
        }

        [HttpPost]
        [Route("create")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CustomerDto))]
        public async Task<IActionResult> CreateCustomer(CreateCustomerDto customer)
        {
            CustomerEntity result = await _customerDatabaseContext.Add(customer);
            
            return new CreatedResult($"http://localhost:7010/api/customer/{result.Id}", null);

        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> UpdateCustomer(CustomerDto customer)
        {
            CustomerDto? result = await _updateCustomerUseCase.Execute(customer);
            if (result == null)
            {
                return new NotFoundResult();
            }
            return new OkObjectResult(result);

        }


    }
}
