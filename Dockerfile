# gets the asp.net 5.0 image
# sets the working directory to the application 
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /app

# copy csproj and restore(nuget based dependencies) as discinct layers for docker cache utilization
# copy the all .sln and .csproj files into the container in a folder name ToDoList
COPY *.sln ./
COPY *.csproj ./
RUN dotnet restore

# copies the all remaining files into the container and builds the app.
# sets the working directory
# compiles the application as a release, sets the output in the container, and skips restore command.
COPY . ./
WORKDIR /app
RUN dotnet publish -c debug -o /app --no-restore

# selects the asp.net 5.0 image
# sets the working directory
# copies the build from the local applicationt to the container application destination
# sets the images main command
FROM mcr.microsoft.com/dotnet/aspnet:5.0 as base
WORKDIR /app
COPY --from=build /app ./

ENTRYPOINT [ "dotnet", "ToDoList.dll", "--environment=Production" ]

