FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app   
    
COPY ["SMS.WebService/SMS.WebService.csproj", "SMS.WebService/"]
COPY ["SMS.Shared/SMS.Shared.csproj", "SMS.Shared/"]

RUN dotnet restore "SMS.WebService/SMS.WebService.csproj" 

COPY . .
WORKDIR "/app/SMS.WebService"
RUN dotnet build "SMS.WebService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SMS.WebService.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app 
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SMS.WebService.dll"]