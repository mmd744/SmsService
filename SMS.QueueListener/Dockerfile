FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app   
    
COPY ["SMS.QueueListener/SMS.QueueListener.csproj", "SMS.QueueListener/"]
COPY ["SMS.Shared/SMS.Shared.csproj", "SMS.Shared/"]

RUN dotnet restore "SMS.QueueListener/SMS.QueueListener.csproj" 

COPY . .
WORKDIR "/app/SMS.QueueListener"
RUN dotnet build "SMS.QueueListener.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SMS.QueueListener.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app 
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SMS.QueueListener.dll"]