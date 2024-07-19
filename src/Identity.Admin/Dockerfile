FROM mcr.microsoft.com/dotnet/runtime-deps:6.0-alpine AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
RUN apk update && apk add icu-libs

FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build
WORKDIR /app
COPY ["src/Identity.Admin/Identity.Admin.csproj", "src/Identity.Admin/"]
COPY ["src/Identity.Admin.EntityFramework.MySql/Identity.Admin.EntityFramework.MySql.csproj", "src/Identity.Admin.EntityFramework.MySql/"]
COPY ["src/Identity.Admin.EntityFramework.Identity/Identity.Admin.EntityFramework.Identity.csproj", "src/Identity.Admin.EntityFramework.Identity/"]
COPY ["src/Identity.Admin.EntityFramework/Identity.Admin.EntityFramework.csproj", "src/Identity.Admin.EntityFramework/"]
COPY ["src/Identity.Admin.EntityFramework.Extensions/Identity.Admin.EntityFramework.Extensions.csproj", "src/Identity.Admin.EntityFramework.Extensions/"]
COPY ["src/Identity.Admin.EntityFramework.Shared/Identity.Admin.EntityFramework.Shared.csproj", "src/Identity.Admin.EntityFramework.Shared/"]
COPY ["src/Identity.Admin.EntityFramework.Configuration/Identity.Admin.EntityFramework.Configuration.csproj", "src/Identity.Admin.EntityFramework.Configuration/"]
COPY ["src/Identity.Admin.BusinessLogic/Identity.Admin.BusinessLogic.csproj", "src/Identity.Admin.BusinessLogic/"]
COPY ["src/Identity.Admin.BusinessLogic.Shared/Identity.Admin.BusinessLogic.Shared.csproj", "src/Identity.Admin.BusinessLogic.Shared/"]
COPY ["src/Identity.Admin.EntityFramework.PostgreSQL/Identity.Admin.EntityFramework.PostgreSQL.csproj", "src/Identity.Admin.EntityFramework.PostgreSQL/"]
COPY ["src/Identity.Admin.BusinessLogic.Identity/Identity.Admin.BusinessLogic.Identity.csproj", "src/Identity.Admin.BusinessLogic.Identity/"]
COPY ["src/Identity.Admin.UI/Identity.Admin.UI.csproj", "src/Identity.Admin.UI/"]
COPY ["src/Identity.Shared.Configuration/Identity.Shared.Configuration.csproj", "src/Identity.Shared.Configuration/"]
COPY ["src/Identity.Shared/Identity.Shared.csproj", "src/Identity.Shared/"]
COPY ["src/Identity.Admin.EntityFramework.SqlServer/Identity.Admin.EntityFramework.SqlServer.csproj", "src/Identity.Admin.EntityFramework.SqlServer/"]
RUN dotnet restore "src/Identity.Admin/Identity.Admin.csproj"

FROM build AS publish
WORKDIR /app
COPY ["src/Identity.Admin/", "src/Identity.Admin/"]
COPY ["src/Identity.Admin.EntityFramework.MySql/", "src/Identity.Admin.EntityFramework.MySql/"]
COPY ["src/Identity.Admin.EntityFramework.Identity/", "src/Identity.Admin.EntityFramework.Identity/"]
COPY ["src/Identity.Admin.EntityFramework/", "src/Identity.Admin.EntityFramework/"]
COPY ["src/Identity.Admin.EntityFramework.Extensions/", "src/Identity.Admin.EntityFramework.Extensions/"]
COPY ["src/Identity.Admin.EntityFramework.Shared/", "src/Identity.Admin.EntityFramework.Shared/"]
COPY ["src/Identity.Admin.EntityFramework.Configuration/", "src/Identity.Admin.EntityFramework.Configuration/"]
COPY ["src/Identity.Admin.BusinessLogic/", "src/Identity.Admin.BusinessLogic/"]
COPY ["src/Identity.Admin.BusinessLogic.Shared/", "src/Identity.Admin.BusinessLogic.Shared/"]
COPY ["src/Identity.Admin.EntityFramework.PostgreSQL/", "src/Identity.Admin.EntityFramework.PostgreSQL/"]
COPY ["src/Identity.Admin.BusinessLogic.Identity/", "src/Identity.Admin.BusinessLogic.Identity/"]
COPY ["src/Identity.Admin.UI/", "src/Identity.Admin.UI/"]
COPY ["src/Identity.Shared.Configuration/", "src/Identity.Shared.Configuration/"]
COPY ["src/Identity.Shared/", "src/Identity.Shared/"]
COPY ["src/Identity.Admin.EntityFramework.SqlServer/", "src/Identity.Admin.EntityFramework.SqlServer/"]
ARG CONFIG=Release
RUN dotnet publish ./src/Identity.Admin/Identity.Admin.csproj -c ${CONFIG} \
    --property:PublishDir=out \
    --runtime alpine-x64 \
    --self-contained true \
    --property:PublishTrimmed=true \
    --property:IncludeAllContentForSelfExtract=true \
    --property:PublishSingleFile=true

FROM base AS final
WORKDIR /app
ARG ENVIRONMENT=DockerDevelopment
ENV ASPNETCORE_ENVIRONMENT=${ENVIRONMENT}
ENV ASPNETCORE_FORWARDEDHEADERS_ENABLED=true
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=0
COPY --from=publish /app/src/Identity.Admin/out .
ENTRYPOINT ["./Identity.Admin"]