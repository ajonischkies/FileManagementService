#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["FileManagementService/FileManagementService.csproj", "FileManagementService/"]
COPY ["FileManagementService_Contracts/FileManagementService_Contracts.csproj", "FileManagementService_Contracts/"]
COPY ["FileManagementService_Business/FileManagementService_Business.csproj", "FileManagementService_Business/"]
COPY ["FileManagementService_Data/FileManagementService_Data.csproj", "FileManagementService_Data/"]
RUN dotnet restore "FileManagementService/FileManagementService.csproj"
COPY . .
WORKDIR "/src/FileManagementService"
RUN dotnet build "FileManagementService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FileManagementService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FileManagement.Api.dll"]