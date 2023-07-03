# BlazoRater - General Rating Application in Blazor
Introduction
BlazoRater is a platform that allows individuals to post and share their experiences and ratings in various fields, referred to as categories. Users can create new categories or use existing ones to share their experiences. They can like other users' posts but cannot modify or delete them; only the post creator has these privileges.

## Platform Used
BlazoRater is built using Blazor WebAssembly with ASP.NET Core hosting and individual user accounts for authentication. Blazor WebAssembly apps execute directly in the browser on a WebAssembly-based .NET runtime. The advantage of using Blazor is that developers can write C# instead of JavaScript for the front-end, and the .NET runtime is downloaded with the app, eliminating the need for browser plugins or extensions.

## Development
The development starts by creating the Blazor WebAssembly application with individual user accounts authentication. Necessary migrations are applied to set up the user-related tables in the SQL server. An extra attribute, FullName, is added to the ApplicationUser model for additional user information.

On the client-side, an HttpService is configured to handle requests to the server. All Http requests have a response message in a generic format, using a Result class in the shared folder.

User-related functionalities, like getting the list of users, are implemented in the UserRepository on the client-side. A UserController is created on the server-side to handle user-related operations and interact with the UserManager service.

To handle posts, a Post entity is created on the client-side, and a PostModel is created on the server-side for mapping. The relationship between users and posts is one-to-many, and a PostModel has a foreign key UserId to specify the post owner. CategoryModel is created on the server-side for categories, and a Category entity is created on the client-side for mapping.

Controllers are created on the server-side to interact with the client-side for posts and categories. HTTP methods are implemented in the controllers for creating, retrieving, updating, and deleting posts, as well as liking and getting most liked posts.

Components are created on the client-side for categories and posts. The components are authorized, accessible only to logged-in users. Users can create, view, like, and delete posts based on their privileges.

## Authentication
Authentication is handled using individual user accounts and the ProfileService on the server-side, which creates claims for users (FullName and CurrentUserId) and passes them to the client-side. Authorization is applied using the [Authorize] attribute on components to restrict access to authorized users only.

Overall, BlazoRater is a powerful Blazor application that allows users to share and rate experiences across various categories, creating a vibrant community of reviewers and contributors.
