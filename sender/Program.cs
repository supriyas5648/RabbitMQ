// using System.Text;
// using RabbitMQ.Client;

// var factory = new ConnectionFactory
// {
//     HostName = "localhost", VirtualHost = "Clarent"
// };
// using var connection = await factory.CreateConnectionAsync();
// using var channel = await connection.CreateChannelAsync();

// await channel.QueueDeclareAsync
// (
//   queue : "Notification",
//   durable : true,
//   exclusive : false,
//   autoDelete : false,
//   arguments : null
// );

// Console.WriteLine("enter UserName");
// string username = Console.ReadLine();

// Console.WriteLine("enter mssg : ");
// string mssg = Console.ReadLine();

// string fullmessage = $"{username} : {mssg}";

// var body = Encoding.UTF8.GetBytes(fullmessage);

// await channel.BasicPublishAsync(exchange : string.Empty , routingKey : "Notification" , body : body);
// Console.WriteLine($"[x] Sent {fullmessage}");

// Console.WriteLine("Press [enter] to exit.");
// Console.ReadLine();

     //[[[[Multi receiver]]]]
using RabbitMQ.Client;
using System.Text;

var factory = new ConnectionFactory { HostName = "localhost", VirtualHost = "Clarent" };
using var connection = await factory.CreateConnectionAsync();
using var channel = await connection.CreateChannelAsync();

await channel.ExchangeDeclareAsync(exchange: "logs", type: ExchangeType.Fanout);

Console.WriteLine("enter UserName");
string username = Console.ReadLine();

Console.WriteLine("enter mssg : ");
string mssg = Console.ReadLine();

string message = $"{username} : {mssg}";

var body = Encoding.UTF8.GetBytes(message);

await channel.BasicPublishAsync(exchange: "logs", routingKey: string.Empty , body: body);
Console.WriteLine($" [x] Sent {message}");

Console.WriteLine(" Press [enter] to exit.");
Console.ReadLine();
