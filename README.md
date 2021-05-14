 # Introduction

**What is the Purpose of the Project?**

Rater in pure English means a person who estimates or determines a rating; hence we chose the name BlazoRater and this indicates the platform that gives individuals the ability to post their experiences with others regarding any field they want (we refer to these fields as a category), Users can create new categories so others can use them or create a new one and share their experience.
They can also show their reactions to the others posts by liking them, but they can’t delete or modify the other user’s post, only the creator of the post can use these actions.

**Which platform has been used for this application?**

Blazor WebAssembly(Asp.net Core hosted and having individual user Accounts as the authentication), let's get a closer look at Blazor, to understand what it is, what it does, and why we chose this framework to work with:
Blazor WebAssembly apps execute directly in the browser on a WebAssembly-based .NET runtime. Blazor WebAssembly apps function in a similar way to front-end JavaScript frameworks like Angular or React. However, instead of writing JavaScript you write C#. The .NET runtime is downloaded with the app along with the app assembly and any required dependencies. No browser plugins or extensions are required.
WebAssembly (abbreviated “Wasm”) is an instruction set designed to run on any host capable of interpreting those instructions or compiling them to native machine code and executing them.
Wasm is an instruction set that is formatted in a specific binary format. Any host (hardware or software) that adheres to this specification is therefore capable of reading binaries and executing them – either interpreted or by compiling directly to machine language specific to the device.
Wasm is akin to the common instruction set (Common Intermediate Language) that .NET source code compiles to. Just like .NET, Wasm can be generated from higher languages such as C#.
**How Is the Structure of a BlazorWebassembly Project?**

The client project will contain the public code which will be downloaded to the user browser while the server project contains the private code which you don’t want to download to the user machine. The server code will run on the server machine and the client will interact with the server through an API connection and shared folder is the place for the shared component which can be accessed both by client side and server side.

# Development
We start the development by creating the BlazorWebassembly , Asp.net core hosted and having the individual user Accounts as its authentication.
Because of the individual user Accounts authentication, there are some migrations by default we apply this migration by updating the data base from package manager console to get the user’s related table in the SQL server.

Having the user’s table, we next will modify it and add an extra attribute to it, to do so in the
ApplicationUser.cs which is predefined model we add an extra property ,FullName , by scaffolding we add a new identity for page ‘/Account/Manage/index’ as a result we get the new folder, which is Account, inside the Account’s folder we need to configure class InputModel and method LoadAsync(), based on the new property we added for the ApplicationUser , we also need to create the corresponding form for it , to get the data from user and do a migration to update the table with the new attribute.
On Client-side we configured a service, which is HttpService , this service will specify the type of request to the server and use the result of that in the dedicated repositories, this service and all other services we use throughout the development will take a place inside the Services folder on client-side.

All Http requests will have a response message, to get this response message in a generic format we add a new class Result, in a shared-folder, Result has a method GetBody(), and
what this method does, is getting the body of a request as a string, then we desterilize this string in the HttpService.
HttpService , has HttpClient injected and it's using it for having a GET request and POST request on a given URL from repositories, when we get the response of a GET request, we deserialize it (Case Insensitively) with the help of JsonSerializer and return the data to the corresponding repository. and in case of POST request, we do the opposite and serialize the data.

After having HttpService , configured we can use its functionality inside our repositories, the first repository we are going to have is the UserRepository .
(This repository and all other repositories will take a place inside the Repositories Folder on client-side).
The purpose of this repository is to get the list of whole users, in order to do so first we create the User, in the Shared folder.
(All entities will take place in folder with the name Entities).

This User entity has an Id which is string here, because the identity class itself is using string for ids, so when we have our property as string, we will not face an issue while mapping, besides Id it has Email & FullName as property.

Now we have the repository for our specified entity, this repository needs a listener on the server side which will be our controller, we extend the current Controller folder with the new class, which is UserController , this controller will get the list of users, in order to do that we need to use the specific service which is giving us the ability to get the users, this service is called UserManager , and must be injected to controller.
The users we are getting from UserManager aren’t the same type as the newly created User entity, we need to have some sort of mapping between them, this mapping is such that we create a new empty list of User entity, and we retrieve the list of UserManager’s result list, at each step we map the properties of it to User entity and add that item to list.

With this conversion we will be able to use the list of current users on the client-side.

After having list of users, we can create our main entities, models and make a relationship between them.

In the program we have models from identity class on the Server Side, and we will create the Post entity on the client side, so basically, we can’t have a relation between them, one solution to this problem is to transfer entity to model and use it on the server side, what I mean by transfer is creating one entity Post on the client side and one model PostModel on the server side.

PostModel is a model we need for creating relation between users and posts, this relation will be a one-to-many relation, each user can have multiple posts, to specify this relationship we add a User (object of type ApplicationUser) and UserId (String which specifies the id of user) as a foreign key to PostModel properties and extending the ApplicationUser model to have a list of PostModels .

To configure these changes to our data base we use DbSet inside the ApplictionDbContext , to get a table of PostModel in database and with the help of OnModelCreating method we specify how the relations must be, based on the foreign keys, in simple word it means: we have an entity which is PostModel, this entity is owned by a user and this user owns multiple posts, and they have foreign key which is UserId.

Each Post will have a dedicated category, so we create a CategoryModel on the server side and Category as an entity in the shared folder both model and entity have the Id and Name property in common , Name here is a required filed since without name having a category is not making any sense, then we make a one-to-many relation between each PostModel and CategoryModel .

one-to-many relation here indicates that each category can have multiple posts, so inside the CategoryModel we consider a list of PostModels and we update the PostModel properties to have new property, CategoryID and a new object of type CategoryModel .

To configure these changes to our data base we use DbSet inside the ApplictionDbContext , to get a table of CategoryModel , and by using OnModelCreating method we specify how the relations must be, based on the foreign keys, this relation shows that each PostModel has category, which each category can have multiple posts and they have a foreign key Id.

We now have all the relations and configuration for our data base, next we need to configure the relevant controllers on the server side to be able to access these data on the client side, the new controller will be CategoryController , this controller will have a Create() method , what this method does, is getting a Category entity from the body of the request and make a copy of its properties and put it for a new instance of CategoryModel , then it will add this model to data base , also there is another method Categories() ,which does the opposite way of this mapping , it gets a list of CategoryModels from data base , and creating the new empty list of Category entities , it will iterate through the given list of CategoryModels , and on each iteration the properties of that item will be copied and used for new object of type Category , then this newly created object will be added to empty list .

On the client side we create an Interface & repository for Category, the repository will be injected with HttpService , and with that it can send a request to the server for creating a new Category or listing the current available Categories.

There are two corresponding components on the client side regarding Category (All the category related components are inside CategoryComponents folder) , one is for creating the category, the other one is for listing the current categories, also we want these components be accessible only for those who are logged in to the system, other than that ,they shouldn’t have access to these pages neither from URL nor from nav menu , to do so inside the nav menu we put these links inside the <AuthorizeView> and <Authorized> and by having @attribute [Authorize] on the top of our components ,we prevent the authorized access from the URL directly.

We follow the same procedure for to create Controller On the serve side for Posts, this controller is PostController , and its responsible for operation on the Posts:

**1-Creation** : For creation we have a HTTP Post method which is CreatePost([FromBody] Post post) , this method will get a Post entity from the body as an input and copies the properties of this entity to a newly created instance of PostModel and add it to the data base , just here we map the value of the UserId , by assign it to the current user’s Id, this has to be done with the help of UserManager service injected into our controller , UserManager will get the data of the user from client side and check it , if It does not be null the post will be created .

**2- Retrieving data** : In order to preview all the past records , we use a HTTP Get method which is Posts(), to get the list of PostModels , from the database and map them to Post entitles.

**3-Deletion** : For deleting a specific record , we use a HTTP Delete method which is DeletePost([FromBody] int Id) , what this method does is getting an Id of a specific post from the body of the request and then go through all the records to find the corresponding Id , if it exist it will remove it from data base ,this will perform an operation on the data base and user will not
immediately see it and the pages need to be refreshed to modifications be visible , to prevent this issue on the client side we should delete this specific post from the list we have .

**4-Update** :For updating a specific record, we use a HTTP Post method which is UpdatePost([FromBody] Post post) ,this method has a reverse type of mapping because here it receives a Post entity and it should map it to PostModel , so it can configure these changes on data base .

**5-Like** : we use a HTTP Post method which is LikePost([FromBody]int Id) , it gets an Id of a specific PostModels from the body of request and go through all the records until it finds the excepted record then it will increase the value of the like by one , this operation will modify the like property value only on the server side and user needs to refresh the page and send a new request to get the modified value , in order to prevent this issue we increase the amount of like by on the client side to show the modification instantly .

**6-Like Oder** : for getting the Most liked posts we are using a LikeOrderPosts() , which is of a type HTTP GET, this method will get a list of PostModels , and mapping their properties to new instances of Post entity in a form of a list which is stetted by descending order regarding to the amount of their like.
On the Client side we create the repository and interface for the corresponding actions we want to have with the Post entity. This repository will have HttpService injected to be able to send and receive requests on the given URL to the server.
All the components related to Post entity are inside the folder Postcomponenets on the client side.

*All the components are authorized such you must be logged in to the system to visit them, other than that if you do not be a system user you can only visit the index page which is a list of all the posts and trending ones , even if you try it with the URL to visit those specific Componentes, you will not be able to because they have @attribute [Authorize] , attribute .

Besides this basic authentication, we have used the one extra service on the server side, which is ProfileService , this service is responsible for creating claims for the user and pass it to the client side , there are two claims, which one of them is FullName , and the other one is CurrentUserId , this service must be configured as a service for IdentityServer .

In the CreatePostComponent , by injecting AuthenticationStateProvider service, we can get the current user id and set the UserId property of a Post with it.
We get the list of Post in two separate components in the program, one is the index Copmonent , and the other one is ListPost , the differences are :

- index Copmonent , is visible to everyone but the ListPost , is only accessible for members of the website.
- in the index Copmonent , we show all the posts (sorted and not sorted) and will have an option to redirect users to other component which will show the post in a wider area and gives them the possibility of reading the description and liking them also ListPost will have this ability to redirect
users to another page for previewing and liking the post but besides these it will allow only to the creator of the post to be able to delete or edit the post , he created before , by checking his Id .

We send a request to a server to get the post based on its id, once the post arrives, we check the UserId property of the post with the current user’s id which can be achieved with the help of AuthenticationStateProvider .
Here Edit and Delete are separate components and we redirect to users to them, and they can delete or edit specific post based on its id, both components have been protected against direct URL access by the other users than the one who created the post by checking their ID.# Blazor_rating-application
General rating application in Blazor 
