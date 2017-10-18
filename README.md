## Purpose
Creating a code example to test a json api hosted on Azure from AGL

## Overall architecture
This is ASP.Net MVC Core Application with a very simple view; presenting a button to get the list of cats from a hosted JSON API by AGL.
There is not any persistence layer as the focus is on providing a code base with good practices of software development like SOLID.
The code is intended to be maintainable, readable, easy to scale with an acceptable level of test coverage.



## Technology Stack
* ASP.Net MVC Core 2
* .Net Framework Core 2
* Mewtonsoft.Json 10.0.3
* Mewtonsoft.Json.Schema 3.0.4
* HttpClient for calling the API
* NUnit 3.8.1 (testing framework)
* Fluent Assertions 4.19.4 (enhance testing experience with lots of assertion extension methods)
* Moq 4.7.142 (mocking framework)
* Serilog 2.5.0 (for structured logging)


## Client side tech
* bower.json
* jquery
* bootstrap
