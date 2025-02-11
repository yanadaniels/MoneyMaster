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
COPY ["MoneyMasterService/Hosts/MoneyMasterService.WebAPI/MoneyMasterService.WebAPI.csproj", "MoneyMasterService/Hosts/MoneyMasterService.WebAPI/"]
COPY ["MoneyMasterService/Infrastructure/MoneyMasterService.Infrastructure.EntityFramework/MoneyMasterService.Infrastructure.EntityFramework.csproj", "MoneyMasterService/Infrastructure/MoneyMasterService.Infrastructure.EntityFramework/"]
COPY ["MoneyMasterService/Domain/MoneyMasterService.Domain.Entities/MoneyMasterService.Domain.Entities.csproj", "MoneyMasterService/Domain/MoneyMasterService.Domain.Entities/"]
COPY ["MoneyMaster.Common/MoneyMaster.Common.Entities/MoneyMaster.Common.Entities.csproj", "MoneyMaster.Common/MoneyMaster.Common.Entities/"]
COPY ["MoneyMaster.Common/MoneyMaster.Common.Interfaces/MoneyMaster.Common.Interfaces.csproj", "MoneyMaster.Common/MoneyMaster.Common.Interfaces/"]
COPY ["MoneyMaster.Common/MoneyMaster.Common/MoneyMaster.Common.csproj", "MoneyMaster.Common/MoneyMaster.Common/"]
COPY ["MoneyMasterService/Infrastructure/MoneyMasterService.Infrastructure.Repositories.Implementations/MoneyMasterService.Infrastructure.Repositories.Implementations.csproj", "MoneyMasterService/Infrastructure/MoneyMasterService.Infrastructure.Repositories.Implementations/"]
COPY ["MoneyMaster.Common/MoneyMaster.Common.Repositories/MoneyMaster.Common.Repositories.csproj", "MoneyMaster.Common/MoneyMaster.Common.Repositories/"]
COPY ["MoneyMasterService/Services/MoneyMasterService.Services.Repositories.Abstractions/MoneyMasterService.Services.Repositories.Abstractions.csproj", "MoneyMasterService/Services/MoneyMasterService.Services.Repositories.Abstractions/"]
COPY ["MoneyMasterService/Services/MoneyMasterService.Services.Contracts/MoneyMasterService.Services.Contracts.csproj", "MoneyMasterService/Services/MoneyMasterService.Services.Contracts/"]
COPY ["MoneyMasterService/Services/MoneyMasterService.Services.Abstractions/MoneyMasterService.Services.Abstractions.csproj", "MoneyMasterService/Services/MoneyMasterService.Services.Abstractions/"]
COPY ["MoneyMasterService/Services/MoneyMasterService.Services.Implementations/MoneyMasterService.Services.Implementations.csproj", "MoneyMasterService/Services/MoneyMasterService.Services.Implementations/"]
RUN dotnet restore "./MoneyMasterService/Hosts/MoneyMasterService.WebAPI/MoneyMasterService.WebAPI.csproj"
COPY . .
WORKDIR "/src/MoneyMasterService/Hosts/MoneyMasterService.WebAPI"
RUN dotnet build "./MoneyMasterService.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Этот этап используется для публикации проекта службы, который будет скопирован на последний этап
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MoneyMasterService.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Этот этап используется в рабочей среде или при запуске из VS в обычном режиме (по умолчанию, когда конфигурация отладки не используется)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MoneyMasterService.WebAPI.dll"]