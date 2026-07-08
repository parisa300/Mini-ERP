FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080


FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["src/MiniERP.API/MiniERP.API.csproj", "src/MiniERP.API/"]

COPY ["src/MiniERP.Application/MiniERP.Application.csproj", "src/MiniERP.Application/"]

COPY ["src/MiniERP.Domain/MiniERP.Domain.csproj", "src/MiniERP.Domain/"]

COPY ["src/MiniERP.Infrastructure/MiniERP.Infrastructure.csproj", "src/MiniERP.Infrastructure/"]

COPY ["src/MiniERP.Shared/MiniERP.Shared.csproj", "src/MiniERP.Shared/"]

RUN dotnet restore "src/MiniERP.API/MiniERP.API.csproj"

COPY . .

WORKDIR "/src/src/MiniERP.API"

RUN dotnet build "MiniERP.API.csproj" -c Release -o /app/build


FROM build AS publish

RUN dotnet publish "MiniERP.API.csproj" -c Release -o /app/publish


FROM base AS final

WORKDIR /app

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "MiniERP.API.dll"]