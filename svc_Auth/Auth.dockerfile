# run from project root directory
# build
FROM mcr.microsoft.com/dotnet/sdk:8.0 as build
WORKDIR /source
COPY . .

ENV DOTNET_NUGET_SIGNATURE_VERIFICATION=false

RUN ls -lA
RUN dotnet restore "./svc_Auth/Bank.Auth.App/Bank.Auth.App.csproj" --disable-parallel \
&& dotnet publish "./svc_Auth/./Bank.Auth.App/Bank.Auth.App.csproj" -c debug -o /app --no-restore

# runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 
WORKDIR /app
COPY --from=build /app ./

ENTRYPOINT ["dotnet", "Bank.Auth.App.dll"]