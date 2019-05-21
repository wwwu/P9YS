FROM mcr.microsoft.com/dotnet/core/aspnet:2.1-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.1-stretch AS build
WORKDIR /src
COPY ["P9YS.Manage/P9YS.Manage.csproj", "P9YS.Manage/"]
COPY ["P9YS.HangfireJobs/P9YS.HangfireJobs.csproj", "P9YS.HangfireJobs/"]
COPY ["P9YS.Services/P9YS.Services.csproj", "P9YS.Services/"]
COPY ["P9YS.Common/P9YS.Common.csproj", "P9YS.Common/"]
COPY ["TxCosSDK/TxCosSDK.csproj", "TxCosSDK/"]
COPY ["P9YS.EntityFramework/P9YS.EntityFramework.csproj", "P9YS.EntityFramework/"]
RUN dotnet restore "P9YS.Manage/P9YS.Manage.csproj"
COPY . .
WORKDIR "/src/P9YS.Manage"
RUN dotnet build "P9YS.Manage.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "P9YS.Manage.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "P9YS.Manage.dll"]