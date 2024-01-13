FROM mcr.microsoft.com/dotnet/sdk:7.0 as build
WORKDIR /app
EXPOSE 80
COPY Gateways/FreeCourse.Gateway/*.csproj Gateways/FreeCourse.Gateway/
RUN dotnet restore Gateways/FreeCourse.Gateway/*.csproj
COPY . .
RUN dotnet publish Gateways/FreeCourse.Gateway/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:7.0 as runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet","FreeCourse.Gateway.dll" ]