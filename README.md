# Vanderbilt Farms
## Homeowner’s Association Application

## Overview
This project showcases how easy it is to stand up a lot of functionality in a C# .NET environment within an extremely limited time-frame, while keeping a focus on modernization, maintainability, and extensability. 

The solution contains a .net core backend (non-api) which services four separate front end projects utilizing a different flavor of .NET. There is an MVC project, two different Blazor WASM projects, and a Blazor Server project. They each have vastly different pros and cons, and the resuts were surprising. 

## Project Descriptions
### Back End
__VanderbiltFarms.Model__<br />
__VanderbiltFarms.DataAccess__<br />
__VanderbiltFarms.Service__<br /><br />
The outer layer is a shared Services project. This is, of course, dependency injected into the front ends to decouple the user interface from persistance. The Service layer directs calls to the repository, which maps back to database objects. 

To state that another way, the repository Data Access projects correspond to their respective database objects, and the Service layer is responsible for "joining" between those objects. This will allow the project to easily expand to different database tables, or support more complex business logic. 

### MVC 
__VanderbiltFarms.Web.Mvc__<br /><br />
This is a traditional Model View Controller format that has been around since the 70's. Controllers handle the routing and the logic, models hold the data, and views display it. It excells in CRUD operations representing multiple database tables with a cascading Show-List -> Show-Detail page structure. 

Everything is relatively standardized, there is a __ton__ of documenation on the framework, and we sit on the other side of 50 years of trial and error refining the framework, making MVC applications really easy to work on and switch between - leaving the complexity down to the business logic.

This type of a project is a good canditate for replacing System of Record applications where speed is not necessarily a primary concern. Think "any app with more than 10 screens" or something where database tables correspond to different screens. 

### BlazorWebAssembly - Client Hosted
__VanderbiltFarms.Web.BlazorWASMClientHosted__<br /><br />
This is the opposite thing. It is a project that utilizes Blazor WebAssembly (the Blazor server that runs __entirely__ in the client browser). It is the absolute fastest way to construct a C# web application. It took between 6-8 total hours of work setting up something like this for a total noob. There are __no__ controllers present, and the pages themselves handle their own routing and logic. 

This particular project is not hooked up to the database; the data is mocked up in json. This client hosted server pattern relies on an api back end to make HTTP calls to. Postgres does not support making direct connections. This is the importance of the .Net Core hosted checkbox in the project creation wizard. More on that later. 

Unfortunately, it is probably not suitable for production applications due to security concerns, as well as long term maintenance/extensability issues due to a more limited ability to segment your code. It is also hard to debug - you can not use breakpoints on any of the code since it all runs in the client.

### BlazorWebAssembly - .Net Core Hosted
__VanderbiltFarms.Web.BlazorWASMCoreHosted.Client__<br />
__VanderbiltFarms.Web.BlazorWASMCoreHosted.Server__<br />
__VanderbiltFarms.Shared.ViewModel*__<br /><br />
This is an interesting compromise. It is a Blazor WASM template, but you check the .NET Core hosted check box in the project creation wizard. When this is done, three separate projects will be generated in your solution:

- A client project that hosts your Blazor pages which actually run in the brower - makes http calls to the server project. 
- Another Blazor server project that hosts controllers - designed to feed calls into your .NET backend which would be hosted on the server. 
- A project to store view models shared by both of them. 

This project is noteworthy because it was about as fast to stand up as the client hosted project, but the structure seems much more throught-out and extensable. Security is much better. This project not only has page authorization enabled, but also JWT authentication in the server side api/controllers. 

This project was the dark horse. I think it would be a strong candidate for smaller client facing production applications that interact with a limited number of database tables.

<sub>*The Shared ViewModel project was generated with the explicit purpose of holding the models in a spearate csproj file that could be shared by both the client and server project. It would not work without it. In this solution, it made sense to share the viewmodels between all the UI's to contain it all in one place. In a real world environment, this project would only exist with this architecture pattern. Otherwise the ViewModels will be stored directly in the UI as is convention. </sub>

### BlazorServerMvcPattern 
__VanderbiltFarms.Web.BlazorServerMvcPattern__<br /><br />
This is a completely different animal from all the rest. It utilizes Blazor server: the Blazor server that actually runs on the server __entirely__. But it is constructed in a hub and spoke pattern with signal-R broadcasting.

It utilizes an event bus to abstract away the effect of an event from the cause for every action in the application. Each controller loads one view and is responsible for processing the events. 

The upshot of this is the pages are completely dumb, meaning you can load break points on pretty much all the code in the entire project. This was a massive benefit, and you still retain all of the fun, responsive UI elements from the other Blazor projects. 

Once you wrap your head around the different design philosophy (events and the 1 to 1 relationship between views and controllers), it was really easy to build out and test new functionality. This is because each new feature event is completely independent. It was the most entertaining to build, but took the longest by far due to the steep initial learning curve. 

I believe with more work refining the event bus (to implement logging, retrys, and custom exceptions), this could be suitible for a large monolithc application. It could produce a very high quality product, but the architecture/file structure would need to be well thought out, with documentation quite thorough.

## How to run
1. Clone down the repository and open sln in visual studio
2. Right click on the desired WEB project you would like to run (For the __Core__ Hosted project, select the __.Server__ project)
3. Click 'Set as Startup Project'
4. Click the 'Start' button at the top of Visual Studio
