## A quick & dirty implementation of how to work with a Kafka flow + local data store.

######Dotnet version: 6
Nugets:
- ConfluentKafka for .NET
- Newtonsoft.Json
- Microsoft hosting extensions. ( Microsoft.Extensions.Hosting )

#### How to install and run the Sample.

1. Install docker desktop. https://www.docker.com/products/docker-desktop/
2. Open a Command Line Interface (CLI) and navigate to KafkaScratch folder in this solution and run docker componse script to setup.
```sh
docker compose -f kafka-local-docker.yml up -d
```
3. From CLI, exec the command below to execute a Kafka broker:
```sh
docker exec -it kafka-broker /bin/bash
```
> After open the broker, the CLI should show like this: 
```sh
I have no name!@474f1019cab8:/$
```

4. From here you can run some console commands to test the message consumption from Kafka or produce messages to Kafka.
- Produce messaeg console:
    ```sh
    kafka-console-producer.sh --bootstrap-server localhost:9092 --topic first_topic --property "parse.key=true" --property "key.separator=:"
    ```

- Consume message console.
    ```sh
    kafka-console-consumer.sh --bootstrap-server localhost:9092 --topic first_topic --from-beginning
    ```
- For more, please check for Kafka Documentation. https://kafka.apache.org/documentation/

5. After setting up the console, you can try to run the solution and see messages produced in Kafka CLI.