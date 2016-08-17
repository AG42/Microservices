using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy;

namespace HelloWorld
{
    public class UserModule : NancyModule
    {
        public UserModule()
        {
            Get["/"] = parameters => { return "Hello World!"; };

            Get["/UserList"] = _ =>
            {
                return Response.AsJson<List<User>>(GetUserList() , HttpStatusCode.OK);
            };

            // Get["/UserList/Search/{id}", runAsync: true] = async (_, token) = > { GetUserInfo;};
            Get["/UserList/Search/{id}"] = GetUserInfo; 
        }

        private List<User> GetUserList()
        {
            return new List<User> {
                new User {  Id = 1, FName = "Abhishek", LName = "Gupta", Status = "Active" },
                new User {  Id = 2, FName = "Vinod", LName = "Bhoite", Status = "Active" },
                new User {  Id = 3, FName = "Wasim", LName = "Khan", Status = "InActive" },
                new User {  Id = 4, FName = "Amit", LName = "Lonkar", Status = "Active" },
                new User {  Id = 5, FName = "Rama", LName = "Ranjan", Status = "InActive" }
            };
        }
        private dynamic GetUserInfo(dynamic parameters)
        {
            var result = GetUserList().Where(x => x.Id == parameters.id).FirstOrDefault();
            return Response.AsJson(result);
        }
    }

    public class User
    {
        public int Id { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string Status { get; set; }
    }

}
