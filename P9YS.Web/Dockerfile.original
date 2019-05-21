FROM mcr.microsoft.com/dotnet/core/aspnet:2.1-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.1-stretch AS build
WORKDIR /src
COPY ["P9YS.Web/P9YS.Web.csproj", "P9YS.Web/"]
COPY ["GeetestSDK/GeetestSDK.csproj", "GeetestSDK/"]
COPY ["P9YS.Common/P9YS.Common.csproj", "P9YS.Common/"]
COPY ["P9YS.Services/P9YS.Services.csproj", "P9YS.Services/"]
COPY ["TxCosSDK/TxCosSDK.csproj", "TxCosSDK/"]
COPY ["P9YS.EntityFramework/P9YS.EntityFramework.csproj", "P9YS.EntityFramework/"]
RUN dotnet restore "P9YS.Web/P9YS.Web.csproj"
COPY . .
WORKDIR "/src/P9YS.Web"
RUN dotnet build "P9YS.Web.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "P9YS.Web.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "P9YS.Web.dll"]