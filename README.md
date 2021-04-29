# Software Development Assignment - Patrick Nobbe

## Preface

I took the liberty to update `ASP .NET Core` from `3.1` to `5.0`, primarily to take advantage of some C# 9 language features. Specifically for the `record` types used in the DTOs.

## Running the application

This application requires the latest [.NET 5 SDK](https://dotnet.microsoft.com/download/dotnet/5.0) in order to run. When using Visual Studio 2019 make sure that you are using _atleast_ v16.7 in order to use thee C# 9 language features.

The product API provided to me for this exercise is added in the `contrib/api` folder and should be run in parallel to our endpoints. Simply open a new terminal and run `dotnet contrib/api/ProductData.Api.dll` in the root directory to expose the endpoint on port 5002.

1. Open a command prompt in the root project folder and run `dotnet restore`.
2. Ensure a valid `appsettings.json` config is present in the `src/Insurance.Api` directory. A sample file is included under the `contrib/etc` folder.
3. In the same command prompt, run `dotnet run --project src/Insurance.Api`. This starts the application in the background and exposes our controllers.

## Architecture

The solution is divided into three separate projects, `Api`, `Core` and `Data`.

The `Core` project represents our domain entities and associated business logic, and is responsible for providing us with the framework to construct our solution around. `Core` is independent and provides interfaces for clients to interface with the domain.

`Data` is responsible for providing our the raw data to populate our domain entities. It provides implementations of our repository interfaces provided by `Core`, and independently adds them to our dependency injection container through extension methods defined in the `/Extensions` subdirectory.

`Api` is our ASP .NET Core MVC endpoint that allows us to interface with our domain through a web API. It provides `Controllers` that route and handle our requests in an appropriately, and is able to convert our domain entities to DTOs through the `AutoMapper` extension.

## Testing

Several unit tests have been added in our test directory to confirm the correct implementation of both the existing functionality as well as the specific behaviour requested in the specific tasks for this assignment. More tests could be added to cover more cases (ex: assert correct error behavior, integration tests).

The tests can be run by running `dotnet test` in the root directory.

## Dependencies
------------------
### Added

### **`AutoMapper` and `AutoMapper.Extensions.Microsoft.DependencyInjection`**

Used to set up our `MapperProfile` to map our DTOs to domain entities and vice-versa.

### **`Microsoft.Extensions.Configuration.Abstractions`**

Allows us to use the `IConfiguration` key/value properties, provided by our `appsettings.<env>.json`.

### **`Microsoft.Extensions.DependencyInjection.Abstractions`**

Allows us to configure an `IServiceCollection` to provide dependency injection to our app.

### **`Microsoft.Extensions.Http`**

Allows the use of a `IHttpClientFactory` to inject a `HttpClient` into our repositories through dependency injection.

----------------
### Removed

`Newtonsoft.Json`: Replaced in favor of `System.Text.Json`. The [featureset](https://docs.microsoft.com/en-us/dotnet/standard/serialization/system-text-json-migrate-from-newtonsoft-how-to?pivots=dotnet-5-0) of `System.Text.Json` was sufficient for the purpose of this project, and since it is part of the `System` namespace we don't need to include a separate NuGet package.

----------------

## Assumptions

### Task 3 - Feature 1

> Now we want to calculate the insurance cost for an order and for this, we are going to provide all the products that are in a shopping cart.

This can be implemented by either providing the full product definition in our JSON request body, or by simply referring to a product ID. Both of these should be accompanied by a quantitiy because multiples of a product can be ordered.

For my implementation (controller endpoint `api/insurance/order`) I simply assumed that the entire product definition would be present, and created a new DTO `OrderEntry` to process this construction. This kind of eliminates the need for the `/products` endpoint in the api provided for this assignment, but it also reduces the amount of HTTP requests necessary to complete this request as now only the `/product_types` endpoint needs to be called.

Reducing the HTTP requests to a minimum is also why I chose to retrieve the entire list of product types in a single call at the start of the request, instead of requesting each product type individually for all present products in the order. I preferred a single request with a larger payload over `x` individual requests for each product present in the order, as this adds extra overhead for each request.

### Task 5 - Feature 3

> As a part of this story we need to provide the administrators/back office staff with a new endpoint that will allow them to upload surcharge rates per product type. This surcharge will then  need to be added to the overall insurance value for the product type.

This task seemed a little curious to me, as the endpoint to retrieve product types is outside of my control, meaning that I can never directly influence the root data that is returned to me as a client. In order to still provide a solution that would somewhat fulfill the requirement, I simply provided an endpoint stub that _would_ do exactly as this story describes, and proved this by adding a unit tests that mocks actual surcharge data. However this does mean that if the `/set_surcharge` endpoint is called in succession with a product or order insurance calculation, the updated surcharge data is not actually persisted and will not be included in the subsequent calculation.

### Other

> Our technical values at Coolblue:
>
>We value team communication so we document our design/architecture choices

Outside of the architectural documentation in this document, the code has been thoroughly documented through comments and summaries where applicable.

>We have a strong culture of Test Driven Development, before we write production code we prefer to write tests first

While I have to admit I did not write the tests beforehand (as I still need to work on my TDD skills), I made sure to test the requested features and the crucial business logic to ensure the code works as intended.

>We like to know what our applications and services are doing in production without necessarily having to debug code or without needing the customer to tell us something is wrong so we proactively monitor them and invest in logging.

I added elementary console error logging to the repositories and controller endpoints, but this could (and should) be expanded upon in a real life scenario. Logs should preferably be written to a separate sink such as files or a database.

>We ensure our applications and systems are self-healing and resilient in face of failures

I added health checks to help diagnosing possible health issues for our endpoints, and graceful error handling to make sure the application does not crash. In case that it does end up crashing in a production environment, it should (preferably) be restarted automatically in a deployed environment through the use of Docker/Kubernetes instances.
