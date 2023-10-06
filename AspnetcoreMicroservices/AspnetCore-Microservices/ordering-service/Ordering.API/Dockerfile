#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ordering-service/Ordering.API/Ordering.API.csproj", "ordering-service/Ordering.API/"]
COPY ["ordering-service/Ordering.Application/Ordering.Application.csproj", "ordering-service/Ordering.Application/"]
COPY ["ordering-service/Ordering.Domain/Ordering.Domain.csproj", "ordering-service/Ordering.Domain/"]
COPY ["ordering-service/Ordering.Infrastructure/Ordering.Infrastructure.csproj", "ordering-service/Ordering.Infrastructure/"]
RUN dotnet restore "ordering-service/Ordering.API/Ordering.API.csproj"
COPY . .
WORKDIR "/src/ordering-service/Ordering.API"
RUN dotnet build "Ordering.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ordering.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ordering.API.dll"]