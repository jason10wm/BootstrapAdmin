﻿#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY src/admin .

WORKDIR "/src/Bootstrap.Admin"
FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
COPY --from=publish ["/src/Bootstrap.Admin/BootstrapAdmin.db", "."]
COPY --from=publish ["/src/keys/Longbow.lic", "."]
COPY --from=publish ["/src/keys/appsettings.Production.json", "."]
ENTRYPOINT ["dotnet", "Bootstrap.Admin.dll"]