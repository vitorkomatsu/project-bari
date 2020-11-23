FROM mcr.microsoft.com/dotnet/core/runtime:2.2-stretch-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["src/Barigui.MicroService/Barigui.MicroService.csproj", "src/Barigui.MicroService/"]
COPY ["src/Barigui.Infra.CrossCutting.IoC/Barigui.Infra.CrossCutting.IoC.csproj", "src/Barigui.Infra.CrossCutting.IoC/"]
COPY ["src/Barigui.Infra/Barigui.Infra.csproj", "src/Barigui.Infra/"]
COPY ["src/Barigui.Domain/Barigui.Domain.csproj", "src/Barigui.Domain/"]
COPY ["src/Barigui.Domain.Core/Barigui.Domain.Core.csproj", "src/Barigui.Domain.Core/"]
COPY ["src/Barigui.Broker.RabbitMQ/Barigui.Broker.RabbitMQ.csproj", "src/Barigui.Broker.RabbitMQ/"]
RUN dotnet restore "src/Barigui.MicroService/Barigui.MicroService.csproj"
COPY . .
WORKDIR "/src/src/Barigui.MicroService"
RUN dotnet build "Barigui.MicroService.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Barigui.MicroService.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Barigui.MicroService.dll"]
