using Microsoft.AspNetCore.Mvc;

namespace AspNetAPI.Controllers
{
    [ApiController]
    [Route("demo")]
    public class DemoController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            return "Hello from the API";
        }

        [HttpPost]
        public string Post([FromBody] string name)
        {
            return $"Hello, {name.ToUpper()}";
        }

        // Using query parameters, via https://localhost:7150/demo/greeting?name=Andrew&age=50
        [HttpGet]
        [Route("greeting/{name}/{age}")]
        //public string GetGreeting([FromQuery] string name, [FromQuery] int age)
        public string GetGreeting( string name,  int age)
        {
            return $"Hello {name}, you are {age} years old";
        }

        // Collecting information from URL path, via https://localhost:7150/customer/567
        [HttpGet]
        [Route("customer/{id}")]
        public object GetCustomer([FromRoute] int id)
        {
            var customer = new
            {
                FirstName = "Helen",
                LastName = "Smith",
                CustomerNumber = $"AX-{id}",
                Balance = 1234.56
            };
            return customer;
        }

        // Receive and return headers
        [HttpPost]
        [Route("headers")]
        public string PostHeaders()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.AppendLine("You submitted headers...");
            string additionalData = string.Empty;
            foreach (string header in Request.Headers.Keys)
            {
                sb.AppendLine($"- {header} = {Request.Headers[header]}");
                if (header == "XYZ")
                {
                    Response.Headers.Add(header, Request.Headers[header] + 4);
                    additionalData = $"Returning header: {header} = {Request.Headers[header]}";
                }
            }
            sb.AppendLine(additionalData);
            //Response.Headers.Add("XYZ", "123");
            return sb.ToString();
        }

        // Extract payload as strongly-typed JSON object
        [HttpPost]
        [Route("order")]
        public string PostNewOrder([FromBody] Order order)
        {
            return $"Customer {order.CustomerName} of {order.CustomerAddress} ordered {order.Quantity} x {order.ProductName}";
        }
    }

    public class Order
    {
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
    }
}
