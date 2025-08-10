# -------- Build stage --------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy everything and restore
COPY *.sln .
COPY EmployeeManagementSystem.API/*.csproj ./EmployeeManagementSystem.API/
COPY EmployeeManagementSystem.Application/*.csproj ./EmployeeManagementSystem.Application/
COPY EmployeeManagementSystem.Infrastructure/*.csproj ./EmployeeManagementSystem.Infrastructure/
COPY EmployeeManagementSystem.Domain/*.csproj ./EmployeeManagementSystem.Domain/

RUN dotnet restore

# Copy rest of the source
COPY . .
WORKDIR /app/EmployeeManagementSystem.API

RUN dotnet tool install -g Microsoft.Web.LibraryManager.Cli \
 && /root/.dotnet/tools/libman restore


RUN dotnet publish -c Release -o /out /p:UseAppHost=false

# -------- Runtime stage --------
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app


# Expose the port mentioned in the assignment
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080


COPY --from=build /out .
# Entry point
ENTRYPOINT ["dotnet", "EmployeeManagementSystem.API.dll"]