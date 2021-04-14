#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
COPY . .
ENTRYPOINT ["dotnet", "EstatisticaAPI.dll"]

CMD ASPNETCORE_URLS=http://*:$PORT dotnet EstatisticaAPI.dll
