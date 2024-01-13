FROM mcr.microsoft.com/dotnet/sdk:7.0 as build
WORKDIR /app
EXPOSE 80
COPY Shared/FreeCourse.Shared/*.csproj Shared/FreeCourse.Shared/
COPY Services/Catalog/FreeCourse.Services.Catalog/*.csproj Services/Catalog/FreeCourse.Services.Catalog/
RUN dotnet restore Services/Catalog/FreeCourse.Services.Catalog/*.csproj
COPY . .
RUN dotnet publish Services/Catalog/FreeCourse.Services.Catalog/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:7.0 as runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet","FreeCourse.Services.Catalog.dll" ]