#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["DataAccess/Web.csproj", "DataAccess/"]
COPY ["Models/Models.csproj", "Models/"]
COPY ["Database/Database.csproj", "Database/"]
COPY ["Services/Services.csproj", "Services/"]
RUN dotnet restore "DataAccess/Web.csproj"
COPY . .
WORKDIR "/src/DataAccess"
RUN dotnet build "Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Web.dll"]