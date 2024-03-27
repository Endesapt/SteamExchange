This is a .Net MAUI based mobile application for P2P trading in CSGO. Authorization is carried out through the Steam server and an intermediary in the form of 
authorization server for OpenID Connect authorization (Steam only supports OpenID 2.0). The application also provides simple
messenger for correspondence between users. The entire server part with which the mobile application interacts is written in C# (ASP.NET and SignalR).
Initial information about user inventories and profiles is downloaded from the Steam servers and stored in the MySql database.

For convenient access to the Steam authorization service, a Docker container from https://github.com/byo-software/steam-openid-connect-provider was used
