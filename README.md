# Messaging packages

There are some packages related to async microservices communications.
Supported implementations:
1. Azure Service Bus

# Build
Please follow these steps to pack and push packages to the github nuget provider

1. Pack projects
```shell
dotnet pack -c Release -o "./pack"
```
2. Push packages
```shell
dotnet nuget push -s github "pack\*.nupkg"
```
