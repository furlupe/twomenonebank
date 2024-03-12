# run from project root directory
# build
FROM mcr.microsoft.com/dotnet/sdk:8.0 as build
WORKDIR /source
COPY . .
RUN ls -lA
RUN dotnet restore "./svc_Core/Bank.Core.App/Bank.Core.App.csproj" --disable-parallel \
&& dotnet publish "./svc_Core/Bank.Core.App/Bank.Core.App.csproj" -c debug -o /app --no-restore

# runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 
WORKDIR /app
COPY --from=build /app ./

ENTRYPOINT ["dotnet", "Bank.Core.App.dll"]
