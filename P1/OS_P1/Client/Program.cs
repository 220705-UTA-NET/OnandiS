
using Client.Models;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json;

Console.WriteLine("---------Starting User Client--------");
string ServiceProviderUrl = "https://localhost:7184/api/User";

var outFromCode = false;
while (!outFromCode)
{
    Console.WriteLine("Choose option by pressing the following : ");
    Console.WriteLine("1 - Login and get your user information");
    Console.WriteLine("2 - Register new user");

    var choice = Console.ReadKey(true).Key;
    Console.WriteLine("");
    Console.Clear();
    switch (choice)
    {
        case ConsoleKey.D1:
            {
                Console.WriteLine("Enter your username");
                var username = Console.ReadLine();
                Console.WriteLine("Enter your password");
                var password = Console.ReadLine();

                var response = await SignIn(username, password);

                if (response != null)
                {
                    Console.WriteLine(response.message);
                    Console.WriteLine("***********User InformationO****************");
                    Console.WriteLine("First Name " + response?.user?.Name);
                    Console.WriteLine("Last Name " + response?.user?.Surname);
                    Console.WriteLine("Username " + response?.user?.Username);
                    Console.WriteLine("Password " + response?.user?.Password);
                    Console.WriteLine("Email " + response?.user?.Email);
                    Console.WriteLine("********************************************");

                }
                else
                {
                    Console.WriteLine("Failed to login");
                }

                //Login
                break;
            }
        case ConsoleKey.D2:
            {
                User user = new User();
                //Register new user 
                Console.WriteLine("Enter user information ****************");
                Console.WriteLine("Enter your First Name");
                user.Name = Console.ReadLine();

                Console.WriteLine("Enter your Last name");
                user.Surname = Console.ReadLine();

                Console.WriteLine("Enter your Email");
                user.Email = Console.ReadLine();

                Console.WriteLine("Enter your Username");
                user.Username = Console.ReadLine();

                Console.WriteLine("Enter your Password");
                user.Password = Console.ReadLine();

                Console.WriteLine("**********************************");

                var response = await SignUp(user);

                if (response != null)
                {
                    Console.WriteLine(response);
                }
                else
                {
                    Console.WriteLine("Faild to signup");
                }

                break;
            }
        default:
            Console.WriteLine("Please enter your full information ");
            break;
    }


    Console.WriteLine("Do you want to continue Y for yes or any character for NO");
    var keyOut = Console.ReadKey().Key;
    Console.WriteLine("");
    Console.Clear();

    if (keyOut != ConsoleKey.Y)
    {
        outFromCode = true;
    }
}


async Task<UserResponse> SignIn(string username, string password)
{
    try
    {
        using var client = new HttpClient();
        var url = ServiceProviderUrl + "/LoginUser?username=" + username + "&password=" + password;
        string response = await client.GetStringAsync(url);
        var result = System.Text.Json.JsonSerializer.Deserialize<UserResponse>(response, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        return result;
    }
    catch (Exception ex)
    {
        return null;
    }

}

async Task<Object> SignUp(User model)
{
    try
    {
        var url = ServiceProviderUrl + "/Signup";

        var json = JsonConvert.SerializeObject(model);
        var data = new StringContent(json, Encoding.UTF8, "application/json");

        using var client = new HttpClient();

        var response = await client.PostAsync(url, data);

        var result = await response.Content.ReadAsStringAsync();

        return result;
    }
    catch (Exception ex)
    {
        return null;
    }

}




