using System;
using IncreaseApp.Services.Repositories.Interfaces;
using IncreaseApp.ViewModels;
using IncreaseApp.ViewModels.Outgoing;
using Microsoft.AspNetCore.Mvc;

namespace IncreaseApp.Controllers
{
    [ApiController]
    [Route("Customers")]
    public class CustomersController
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomersController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        [HttpGet("{id}")]
        public ActionResult<CustomerDataVM> FindCustomerById(Guid id)
        {
            var customerInfo = _customerRepository.FindCustomerById(id);

            if (customerInfo == null)
                return new NotFoundResult();

            return customerInfo;
        }
    }
}