using BlazoDemo;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Data.SqlClient;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();

Console.Write("Enter your username: ");
string username = Console.ReadLine();

Console.Write("Enter your password: ");
string password = Console.ReadLine();

string connectionString = "your_connection_string_here";
using (SqlConnection connection = new SqlConnection(connectionString))
{
    connection.Open();
    string sql = $"SELECT * FROM Users WHERE Username = '{username}' AND Password = '{password}'";
    using (SqlCommand command = new SqlCommand(sql, connection))
    {
        using (SqlDataReader reader = command.ExecuteReader())
        {
            if (reader.HasRows)
            {
                Console.WriteLine("Login successful!");
            }
            else
            {
                Console.WriteLine("Invalid username or password.");
            }
        }
    }
}
