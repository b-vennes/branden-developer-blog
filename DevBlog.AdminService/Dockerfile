FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

# Copy everything else and build
COPY . ./

WORKDIR /app/DevBlog.AdminService
RUN dotnet restore
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build-env /app/DevBlog.AdminService/out .
ENTRYPOINT ["dotnet", "DevBlog.AdminService.dll"]