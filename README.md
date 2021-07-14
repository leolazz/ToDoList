# ToDoList

[![ToDoList .Net](https://github.com/leolazz/ToDoList/actions/workflows/Integrate.yml/badge.svg)](https://github.com/leolazz/ToDoList/actions/workflows/Integrate.yml)

2BDone is a Web Based To Do List

## ERD

![diagram](assets/Database-Diagram.png)

## Installation
Target ASP.NETCore 5.0 MVC

Download ToDoList project folder and open in visual studio

## Requirements

**Dotnet SDK v5**
- https://dotnet.microsoft.com/download/dotnet/5.0

**Dotnet Core v3.1**
- https://dotnet.microsoft.com/download

Following packages available from NuGet:

AutoMapper (10.1.1)
AutoMapper.Extensions.Microsoft.DependencyInjection (8.1.1)
Microsoft.AspNetCore.Diagnotistics.EntityFrameworkCore (5.0.5)
Microsoft.AspNetCore.Identity.EntityFrameworkCore (5.0.5)
Microsoft.AspNetCore.Identity.Ui (5.0.5)
Microsoft.FrameworkCore (5.0.5)
Microsoft.FrameworkCore.SQLit (5.0.5)
Microsoft.FrameworkCore.Tools (5.0.5)
SQLite (3.13.0)
System.Data.SQLite.Core (1.0.113.7)

## Usage

```bash
# build the solution
$ dotnet build
# run the solution
$ dotnet run

# build src
$ dotnet build src
# run src
$ dotnet run --project src

# build tests
$ dotnet build tests
# run tests
$ dotnet run --project tests
```

## Docker

```bash
# pull the image
$ docker pull leolazz/2bdone
```

```bash
# build the container image
$ docker build -t <ImageName>:<TagName> .
```

```bash
# docker run the built container image
# OPTIONAL: Add a bind mount to a SQLite .db for persistance
$ docker run -it -p 5001:80 --name <ContainerName> <ImageName>
```

```bash
# push the built container image to docker hub
$ docker push leolazz/2bdone:<TagName>
```

```bash
# run the pulled image
$ docker run leolazz/2bdone:<TagName>
```

## Docker-compose

```bash
# launch with docker-compose
$ docker-compose up
```

## Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.


## License
[MIT](https://choosealicense.com/licenses/mit/)
