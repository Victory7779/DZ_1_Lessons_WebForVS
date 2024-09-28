namespace DZ_1
{
    //public class User
    //{
    //    public int id { get; set; }
    //    public string Name { get; set; }
    //    public int Age { get; set; }
    //    public string Email { get; set; }
    //    public User(int id, string name, int age, string email)
    //    {
    //        this.id = id;
    //        Name = name;
    //        Age = age;
    //        Email = email;
    //    }
    //    public User() { }
    //}
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var app = builder.Build();

            app.MapGet("/", () => "Hello World!");

            app.Run(async (context) => 
            {
                var response = context.Response;
                var request = context.Request;
                var path = request.Path;
                response.ContentType = "text/html; charset=utf-8";

                //Информация о приложении 
                var stringBuilder = new System.Text.StringBuilder("<table>");
                foreach (var header in request.Headers)
                {
                    stringBuilder.Append($"<tr><td>{header.Key}</td><td>{header.Value}</td></tr>");
                }
                stringBuilder.Append("</table>");

                ////Инициализация класса
                //var users = new List<User>()
                //{
                //   new User(123, "Alex", 21, "alex@gmail.com"),
                //   new User(654, "Alica", 25, "alica@gmail.com"),
                //   new User(417, "Bob", 17, "bob@gmail.com"),
                //   new User(987, "Tom", 37, "tom@gmail.com"),
                //   new User(754, "Janny", 19, "janny@gmail.com")
                //};


                //Простой маршрутизатор
                if (path == "/")
                {
                    await response.WriteAsync("<h1>Hello World!</h1>");
                }
                if (path=="/about")
                {
                    await response.WriteAsync(stringBuilder.ToString());
                }
                if (path=="/echo")
                {
                    response.ContentType = "text/html; charset=utf-8";
                    await response.SendFileAsync("wwwroot/index.html");

                    ////await response.WriteAsJsonAsync(users);
                    //await response.SendFileAsync("wwwroot/index.html");
                    //var form = request.Form;
                    //int id =int.Parse(form["Id"]);
                    //string name = form["Name"];
                    //int age = int.Parse(form["Age"]);
                    //string email = form["Email"];
                    //if (true)
                    //{
                    //    await response.WriteAsync("Hello");
                    //}

                    
                }
                if(path=="/result")
                {
                    var message = "Not information";
                    try
                    {
                        response.ContentType = "text/json; charset=utf-8";
                        var user = await request.ReadFromJsonAsync<User>();
                        if (user != null)
                        {
                           message = $"\"id\": \"{user.id}\" \"name\": \"{user.name}\" \"age\": \"{user.age}\" \"email\": \"{user.email}\"";
                            
                        }
                        
                    }
                    catch {  }
                    await response.WriteAsJsonAsync(new { innerText = message });//НЕ виводиться json форматі - не читає
                }
            });

            app.Run();
        }
        public record User(int id, string name, int age, string email);
    }
}
