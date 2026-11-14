// using RabbitMQ.Client;
// using RabbitMQ.Client.Events;
// using System.Text;

// var factory = new ConnectionFactory { HostName = "localhost" , VirtualHost = "Clarent"};
// using var connection = await factory.CreateConnectionAsync();
// using var channel = await connection.CreateChannelAsync();

// await channel.QueueDeclareAsync(queue : "Notification", durable : true , exclusive : false , autoDelete : false, arguments : null);

// Console.WriteLine("[*] Waiting for messages");

// var consumer = new AsyncEventingBasicConsumer(channel);
// consumer.ReceivedAsync += (model , ea) =>
// {
//   var body = ea.Body.ToArray();
//   var message = Encoding.UTF8.GetString(body);
//   Console.WriteLine($"[x] Received { message}");
//   return Task.CompletedTask;  
// };

// await channel.BasicConsumeAsync("Notification" , autoAck : true , consumer : consumer);

// Console.WriteLine("press [enter] to exit.");
// Console.ReadLine();

             //Multi receiver.......
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

var factory = new ConnectionFactory { HostName = "localhost" , VirtualHost = "Clarent"};
using var connection = await factory.CreateConnectionAsync();
using var channel = await connection.CreateChannelAsync();

await channel.ExchangeDeclareAsync(exchange: "logs", type: ExchangeType.Fanout);

// declare a server-named queue
QueueDeclareOk queueDeclareResult = await channel.QueueDeclareAsync();
string queueName = queueDeclareResult.QueueName;

await channel.QueueBindAsync(queue:queueName , exchange: "logs", routingKey: string.Empty);

Console.WriteLine(" [*] Waiting for logs.");

var consumer = new AsyncEventingBasicConsumer(channel);
consumer.ReceivedAsync += (model, ea) =>
{
    byte[] body = ea.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    Console.WriteLine($" [x] {message}");
    return Task.CompletedTask;
};

await channel.BasicConsumeAsync(queueName, autoAck: true, consumer: consumer);

Console.WriteLine(" Press [enter] to exit.");
Console.ReadLine();