# Payment Gateway API
The Payment Gateway API allow a merchant to offer a way for their shoppers to pay for their product. It contain 2 main funcionality 
* process a payment through the payment gateway and receive either a successful or unsuccessful response
* retrieve the details of a previously made payment

The full documentaion about request and response could be found in swagger https://localhost:44342/index.html

# Developer consideraton
* Payment Gateway API is configure now to use database in memory but could be chanaged any database provider https://docs.microsoft.com/en-us/ef/core/providers/?tabs=dotnet-core-cli
* As this was small exercise I kept Services, Database, Models in Checkout.Payment Gateway Api. In enterprise project this should be moved to separate projects.
* Solution contains unit tests and integration tests. The integration tests are using web host and in-memory test server following this article https://docs.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-3.0 
* In the enterprise project we would need also acceptance test which should be run after deployments
*  Acquiring Bank API url could be changed by changing value in appsettings.json -> AcquiringBank -> BaseAddress. We could introduce multiple enviroments files 
appsettings.{Environment}.json. More info https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration/?view=aspnetcore-3.0 https://docs.microsoft.com/en-us/aspnet/core/fundamentals/environments?view=aspnetcore-3.0


