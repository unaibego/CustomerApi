using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CustomersAPI.Dtos
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }

    public class CreateCustomerDto
    {
        [Required (ErrorMessage ="El nombre propio tiene que estar especificado")]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        //[RegularExpression("^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\\.[a-zA-Z0-9-.]+$", ErrorMessage = "The email is wrong")]
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}