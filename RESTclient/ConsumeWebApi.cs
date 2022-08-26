using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RESTclient
{
    public class ConsumeWebApi
    {
         HttpClient client = new HttpClient();
        private List<Customer> CustomerStore()
        {

            Random r = new Random();
            int minAge = 10;
            int maxAge = 90;
            List<Customer> customerList = new List<Customer>();
            customerList.Add(new Customer() { firstName = "Leia", lastName = "Liberty", age = r.Next(minAge, maxAge), id = 25 });
            customerList.Add(new Customer() { firstName = "Sadie", lastName = "Ray", age = r.Next(minAge, maxAge), id = 26 });
            customerList.Add(new Customer() { firstName = "Jose", lastName = "Harrison", age = r.Next(minAge, maxAge), id = 27 });
            customerList.Add(new Customer() { firstName = "Sara", lastName = "Ronan", age = r.Next(minAge, maxAge), id = 28 });
            customerList.Add(new Customer() { firstName = "Frank", lastName = "Drew", age = r.Next(minAge, maxAge), id = 29 });
            customerList.Add(new Customer() { firstName = "Dewey", lastName = "Powell", age = r.Next(minAge, maxAge), id = 30 });
            customerList.Add(new Customer() { firstName = "Tomas", lastName = "Larsen", age = r.Next(minAge, maxAge), id = 31 });
            customerList.Add(new Customer() { firstName = "Joel", lastName = "Chan", age = r.Next(minAge, maxAge), id = 32 });
            customerList.Add(new Customer() { firstName = "Lukas", lastName = "Anderson", age = r.Next(minAge, maxAge), id = 33 });
            customerList.Add(new Customer() { firstName = "Carlos", lastName = "Lane", age = r.Next(minAge, maxAge), id = 34 });
            return customerList;
        }

        public async Task<IEnumerable<Customer>> PostCustomer(IEnumerable<Customer> customerList)
        {
            var response = await client
                .PostAsync(
                    "https://localhost:44324/api/customer",
                    new StringContent(JsonSerializer.Serialize(customerList), Encoding.UTF8, "application/json"))
                .ConfigureAwait(false);

            var users = JsonSerializer.Deserialize<IEnumerable<Customer>>(await response.Content.ReadAsStringAsync());

            return users;
        }

        public async Task<IEnumerable<Customer>> PostCustomersInParallel()
        {
          
            var tasks = new List<Task<IEnumerable<Customer>>>();
         
                var customerList1 = CustomerStore().Take(2);
                var customerList2 = CustomerStore().Skip(2).Take(3);
                var customerList3 = CustomerStore().Skip(5).Take(5);

            tasks.Add(PostCustomer(customerList1));
            tasks.Add(PostCustomer(customerList2));
            tasks.Add(PostCustomer(customerList3));

            return (await Task.WhenAll(tasks)).SelectMany(u => u);
        }
     }
}
