# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["4.Servicio/Lucky.Ecommerce.Services.WebApi/Lucky.Ecommerce.Services.WebApi.csproj", "4.Servicio/Lucky.Ecommerce.Services.WebApi/"]
COPY ["1.Dominio/Lucky.Ecommerce.Domain.Core/Lucky.Ecommerce.Domain.Core.csproj", "1.Dominio/Lucky.Ecommerce.Domain.Core/"]
COPY ["3.Infraestructura/Lucky.Ecommerce.Infrastructure.Interface/Lucky.Ecommerce.Infrastructure.Interface.csproj", "3.Infraestructura/Lucky.Ecommerce.Infrastructure.Interface/"]
COPY ["1.Dominio/Lucky.Ecommerce.Domain.Entity/Lucky.Ecommerce.Domain.Entity.csproj", "1.Dominio/Lucky.Ecommerce.Domain.Entity/"]
COPY ["1.Dominio/Lucky.Ecommerce.Domain.Interface/Lucky.Ecommerce.Domain.Interface.csproj", "1.Dominio/Lucky.Ecommerce.Domain.Interface/"]
COPY ["2.Aplicacion/Lucky.Ecommerce.Application.Dto/Lucky.Ecommerce.Application.Dto.csproj", "2.Aplicacion/Lucky.Ecommerce.Application.Dto/"]
COPY ["2.Aplicacion/Lucky.Ecommerce.Application.Interface/Lucky.Ecommerce.Application.Interface.csproj", "2.Aplicacion/Lucky.Ecommerce.Application.Interface/"]
COPY ["5.Transversal/Lucky.Ecommerce.Transversal.Common/Lucky.Ecommerce.Transversal.Common.csproj", "5.Transversal/Lucky.Ecommerce.Transversal.Common/"]
COPY ["2.Aplicacion/Lucky.Ecommerce.Application.Main/Lucky.Ecommerce.Application.Main.csproj", "2.Aplicacion/Lucky.Ecommerce.Application.Main/"]
COPY ["3.Infraestructura/Lucky.Ecommerce.Infrastructure.Data/Lucky.Ecommerce.Infrastructure.Data.csproj", "3.Infraestructura/Lucky.Ecommerce.Infrastructure.Data/"]
COPY ["3.Infraestructura/Lucky.Ecommerce.Infrastructure.Repository/Lucky.Ecommerce.Infrastructure.Repository.csproj", "3.Infraestructura/Lucky.Ecommerce.Infrastructure.Repository/"]
COPY ["5.Transversal/Lucky.Ecommerce.Transversal.Mapper/Lucky.Ecommerce.Transversal.Mapper.csproj", "5.Transversal/Lucky.Ecommerce.Transversal.Mapper/"]
RUN dotnet restore "./4.Servicio/Lucky.Ecommerce.Services.WebApi/Lucky.Ecommerce.Services.WebApi.csproj"
COPY . .
WORKDIR "/src/4.Servicio/Lucky.Ecommerce.Services.WebApi"
RUN dotnet build "./Lucky.Ecommerce.Services.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Lucky.Ecommerce.Services.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Lucky.Ecommerce.Services.WebApi.dll"]