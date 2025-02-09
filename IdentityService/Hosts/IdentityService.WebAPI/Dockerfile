# См. статью по ссылке https://aka.ms/customizecontainer, чтобы узнать как настроить контейнер отладки и как Visual Studio использует этот Dockerfile для создания образов для ускорения отладки.

# Этот этап используется при запуске из VS в быстром режиме (по умолчанию для конфигурации отладки)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# Этот этап используется для сборки проекта службы
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["IdentityService/Hosts/IdentityService.WebAPI/IdentityService.WebAPI.csproj", "IdentityService/Hosts/IdentityService.WebAPI/"]
COPY ["IdentityService/Domain/IdentityService.Domain.Entities/IdentityService.Domain.Entities.csproj", "IdentityService/Domain/IdentityService.Domain.Entities/"]
COPY ["MoneyMaster.Common/MoneyMaster.Common.Entities/MoneyMaster.Common.Entities.csproj", "MoneyMaster.Common/MoneyMaster.Common.Entities/"]
COPY ["MoneyMaster.Common/MoneyMaster.Common.Interfaces/MoneyMaster.Common.Interfaces.csproj", "MoneyMaster.Common/MoneyMaster.Common.Interfaces/"]
COPY ["MoneyMaster.Common/MoneyMaster.Common/MoneyMaster.Common.csproj", "MoneyMaster.Common/MoneyMaster.Common/"]
COPY ["IdentityService/Infrastructure/IdentityService.Infrastructure.EntityFramework/IdentityService.Infrastructure.EntityFramework.csproj", "IdentityService/Infrastructure/IdentityService.Infrastructure.EntityFramework/"]
COPY ["IdentityService/Infrastructure/IdentityService.Infrastructure.Repositories.Implementations/IdentityService.Infrastructure.Repositories.Implementations.csproj", "IdentityService/Infrastructure/IdentityService.Infrastructure.Repositories.Implementations/"]
COPY ["MoneyMaster.Common/MoneyMaster.Common.Repositories/MoneyMaster.Common.Repositories.csproj", "MoneyMaster.Common/MoneyMaster.Common.Repositories/"]
COPY ["IdentityService/Services/IdentityService.Services.Repositories.Abstractions/IdentityService.Services.Repositories.Abstractions.csproj", "IdentityService/Services/IdentityService.Services.Repositories.Abstractions/"]
COPY ["IdentityService/Services/IdentityService.Services.Abstractions/IdentityService.Services.Abstractions.csproj", "IdentityService/Services/IdentityService.Services.Abstractions/"]
COPY ["IdentityService/Services/IdentityService.Services.Contracts/IdentityService.Services.Contracts.csproj", "IdentityService/Services/IdentityService.Services.Contracts/"]
COPY ["IdentityService/Services/IdentityService.Services.Implementations/IdentityService.Services.Implementations.csproj", "IdentityService/Services/IdentityService.Services.Implementations/"]
RUN dotnet restore "./IdentityService/Hosts/IdentityService.WebAPI/IdentityService.WebAPI.csproj"
COPY . .
WORKDIR "/src/IdentityService/Hosts/IdentityService.WebAPI"
RUN dotnet build "./IdentityService.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Этот этап используется для публикации проекта службы, который будет скопирован на последний этап
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./IdentityService.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Этот этап используется в рабочей среде или при запуске из VS в обычном режиме (по умолчанию, когда конфигурация отладки не используется)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IdentityService.WebAPI.dll"]