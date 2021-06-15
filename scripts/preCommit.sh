#!/bin/sh
dotnet restore \
&& dotnet build ToDoList.sln --no-restore \
&& dotnet test --no-build --verbosity normal