Start a rabbitmq instance on docker with the following line

`docker run -d -e RABBITMQ_DEFAULT_USER=user -e RABBITMQ_DEFAULT_PASS=password -p 15672:15672 -p 5672:5672 --hostname rabbitmq --name rabbitmq rabbitmq:3-management`

Then run the application and follow the memory usage. It takes time to accumulate so be patience.

If you comment the following line on `Program.cs` you would not see any memory leak:

`((RabbitMqReceiveSettings)((RabbitMqReceiveEndpointConfiguration)endCfg).Settings).NoAck = true;`
