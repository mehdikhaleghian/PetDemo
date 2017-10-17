## Purpose
Creating a code example to test a json api hosted on Azure from AGL

## Overall architecture
This is ASP.Net MVC Core Application with a very simple view, presenting a button to get the list of cats from API
There is not persistence layer as the focus is on providing a code base with good practices of software development in
Calling the API with a maintainable and testable code


## Technology Stack
# ASP.Net MVC Core 2
* .Net Framework Core 2
* Mewtonsoft.Json 10.0.3
* Mewtonsoft.Json.Schema 3.0.4
* HttpClient for calling the API
* NUnit3 3.8.1 (testing framework)
* Fluen Assertions 4.19.4 (enhance testing experience with lots of assertion extension methods)
* Moq 4.7.142 (mocking framework)
* Serilog 2.5.0 (for structured logging)


## Client side tech
* bower.json
* jquery
* bootstrap
