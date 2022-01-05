#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["src/AwsS3.WebApi/AwsS3.WebApi.csproj", "src/AwsS3.WebApi/"]
RUN dotnet restore "src/AwsS3.WebApi/AwsS3.WebApi.csproj"
COPY . .
WORKDIR "/src/src/AwsS3.WebApi"
RUN dotnet build "AwsS3.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AwsS3.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AwsS3.WebApi.dll"]