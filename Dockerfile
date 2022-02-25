# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:5.0

WORKDIR /app
EXPOSE 80

COPY ./bin/Debug/net5.0/ .

ENTRYPOINT ["dotnet", "ConsoleApp_consumer.dll"]