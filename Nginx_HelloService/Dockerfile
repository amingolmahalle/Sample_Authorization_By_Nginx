FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build

EXPOSE 5000

WORKDIR /app 
COPY . ./
RUN dotnet restore 
RUN dotnet publish -c Release -o /app/out/

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
WORKDIR /app 
COPY --from=build /app/out ./
ENTRYPOINT ["dotnet", "Nginx_HelloService.dll"] 
