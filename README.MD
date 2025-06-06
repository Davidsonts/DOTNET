
```
dotnet tool install -g dotnet-ef --version 5.0.2
```
dotnet new webapi -n ProEventos.API

## VS CODE EX - C# 1.23.17 - omnisharp

### INSTALL EF CORE
```
dotnet tool list --global OR dotnet tool list -g
```
dotnet tool uninstall -g dotnet-ef
```
dotnet tool install -g dotnet-ef --version 5.0.2 

Microsoft.EntityFrameworkCore
Microsoft.EntityFrameworkCore.Design
Microsoft.EntityFrameworkCore.Tool
Microsoft.EntityFrameworkCore.Sqlite
Microsoft.AspNetCore.Mvc.NewtonsoftJson

dotnet add package Microsoft.AspNetCore.Mvc.NewtonsoftJson --version 5.0.2

```
dotnet ef

```
dotnet ef -h
```
dotnet ef migrations -h
```
dotnet ef migrations list
```
dotnet ef migrations add Inicial -o Data/Migrations
```
dotnet ef -h
```
dotnet ef database -h
```
dotnet ef database update
```
brew install db-browser-for-sqlite
```
dotnet new sln -n ProEventos

dotnet new classlib -n ProEventos.Persistence
dotnet new classlib -n ProEventos.Domain
dotnet new classlib -n ProEventos.Application

dotnet sln ProEventos.sln add ProEventos.Application
dotnet sln ProEventos.sln add ProEventos.Domain
dotnet sln ProEventos.sln add ProEventos.Persistence
dotnet sln ProEventos.sln add ProEventos.API

dotnet add ProEventos.API/ProEventos.API.csproj reference ProEventos.Application
dotnet add ProEventos.Application/ProEventos.Application.csproj reference ProEventos.Domain
dotnet add ProEventos.Application/ProEventos.Application.csproj reference ProEventos.Persistence
dotnet add ProEventos.Persistence/ProEventos.Persistence.csproj reference ProEventos.Domain


```
### adicionar em Persistence
Microsoft.EntityFrameworkCore
Microsoft.EntityFrameworkCore.Tool

### RODANDO MIGRACAO DO PROJETO STANDARD NO FRAMEWORK CORE

```
dotnet ef migrations add Initial -p ProEventos.Persistence -s ProEventos.API

```
dotnet ef database update -s ProEventos.API
```

dotnet ef migrations list  -p ProEventos.Persistence -s ProEventos.API

dotnet ef database update 0 -p ProEventos.Persistence -s ProEventos.API

dotnet ef migrations remove -p ProEventos.Persistence -s ProEventos.API
```

## DATA TRANSFER OBJECT (Dtos)
```
cd src/ProEventos.Application/

dotnet add package AutoMapper.Extensions.Microsoft.DependencyInjection --version 8.1.1
dotnet restore 

dotnet watch run --launch-profile https

```
https://www.entityframeworktutorial.net


ctor - constructor
prop - 

angular essential
```
angular Files
```
Auto close tags
```
TSLint
```
nvm use

```
ng new ProEventos-App
```
https://fontawesome.com
Using a Package Manager
https://www.npmjs.com/package/@fortawesome/fontawesome-free
```
npm i @fortawesome/fontawesome-free
```
https://valor-software.com/ngx-bootstrap/

```
npm install ngx-bootstrap@12 --save
npm install bootstrap@4
npm i ngx-toastr@14.3.0 --save
npm install @angular/animations@14.3.0 --save
npm install ngx-currency@17.0.0 --save

```
https://bootsnipp.com
https://bootswatch.com
https://getbootstrap.com/docs/4.6/examples/
https://getbootstrap.com/docs/4.6/examples/offcanvas/
```
npm install bootswatch
import "bootswatch/dist/[theme]/bootstrap.min.css";

ng g c components/eventos/evento-detalhe --module app
ng g c components/eventos/evento-lista --module app

```
user_login_registration.html

```
ng g c components/user

```

```
dotnet watch -p ProEventos.API

dotnet watch run

https://localhost:5001/swagger/index.html
```
