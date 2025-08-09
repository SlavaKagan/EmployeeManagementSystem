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
RUN dotnet publish -c Release -o /out

# -------- Runtime stage --------
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /out .

# Expose the port mentioned in the assignment
EXPOSE 8080

# Entry point
ENTRYPOINT ["dotnet", "EmployeeManagementSystem.API.dll"]