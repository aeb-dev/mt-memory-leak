using mt_memory_leak;
using MassTransit;
using MassTransit.RabbitMqTransport.Configuration;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
        services.AddScoped<TestConsumer>();
        services.AddMassTransit(
            x =>
            {
                x.UsingRabbitMq(
                    (context, cfg) =>
                    {
                        cfg.Host(
                            "localhost", "/", h =>
                            {
                                h.Username("user");
                                h.Password("password");
                            }
                        );

                        cfg.Message<TestMessage>(x => x.SetEntityName("test-exchange"));
                        cfg.Publish<TestMessage>(
                            x =>
                            {
                                x.Durable = false;
                                x.AutoDelete = true;
                            }
                        );

                        cfg.ReceiveEndpoint(
                            "test-queue", endCfg =>
                            {
                                ((RabbitMqReceiveSettings)((RabbitMqReceiveEndpointConfiguration)endCfg).Settings).NoAck = true;
                                endCfg.PublishFaults = false;
                                endCfg.ConfigureConsumeTopology = false;
                                endCfg.Durable = false;
                                endCfg.AutoDelete = true;

                                endCfg.Consumer<TestConsumer>(context);
                                endCfg.Bind("test-exchange", excCfg =>
                                {
                                    excCfg.Durable = false;
                                    excCfg.AutoDelete = true;
                                });
                            }
                        );
                    }
                );
            }
        );
    })
    .Build();

host.Run();
