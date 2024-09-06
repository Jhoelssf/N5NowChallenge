#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["N5Api/N5Api.csproj", "N5Api/"]
COPY ["N5Domain/N5Domain.csproj", "N5Domain/"]
COPY ["N5Infrastructure/N5Infrastructure.csproj", "N5Infrastructure/"]
COPY ["N5Application/N5Application.csproj", "N5Application/"]
RUN dotnet restore "N5Api/N5Api.csproj"
COPY . .
WORKDIR "/src/N5Api"
RUN dotnet build "N5Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "N5Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "N5Api.dll"]