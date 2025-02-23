# CargoPay

Add in Package of CargoPay.API the package Microsoft.EntityFrameworkCore.Tools
or user the command Install-Package Microsoft.EntityFrameworkCore.Tools

Add in Package of CargoPay.Infraestructure the package 
Microsoft.EntityFrameworkCore.Design
or user the command Install-Package Microsoft.EntityFrameworkCore.Design



For each change in data models, execute the following command to generate the new data models:
```dotnet tool install --global dotnet-ef

It's necesary change the connectionString in the file 
CargoPay.Infraestructure/Persistence/ApplicationDBContext.cs
Change into OnConfiguring function string "YourConnectionString" to your connection DB


After that, in folder CargoPay.Infraestructure, execute: (Note: -o parameter can be used to change the location of the models to migrate)
```dotnet ef migrations add "Initial" -o "Migrations"
```dotnet ef database update


