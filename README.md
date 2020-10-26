# IRECKONU Assignment
Developed with ♥ and ASP.NET Core by Arley Pádua 😎


## Getting started

1. run `docker pull mongo` to pull the latest mongo image
2. run `docker run -d -p 27017-27019:27017-27019 --name mongodb mongo` to run it locally
3. run the project, using Visual Studio and selecting `ImportFile.Api` as the debug option
4. send a request to `POST https://{localhost}:{port}/inventory/import/csv` with the following body:
```json
{
    "fileUrl": "https://goo.gl/tJWo1f",
    "containsHeader": true
}
```
5. the file will be generated on the application path

## Running with docker

1. Edit the file: `appsettings.Developtment.json` and put the connection string of the desired MongoDb instance
2. Run the docker file
3. Submit the request described on the "Getting started" session

## Observations

1. The solution is fully synchronous, but can easily accept an implementation with RabbitMQ, Azure Service Bus or any other messaging mechanism. This way we could scale individual parts of the solution.
2. I used a hexagonal architecture to organize the project, where the core implements all use cases and requires contracts (ports) to be implemented by adapters.
3. The unit tests provided are testing the main functionality asked in the assignment.
4. Some comments were provided giving clarity on the decisions taken
5. There are a few things that I left aside to avoid bloating the scope: exception handling, concurrency management (optimistic concurrency when creating/updating), asynchronous through messaging.