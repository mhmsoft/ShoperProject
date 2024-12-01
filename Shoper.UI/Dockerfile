
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app/ShoperProject
EXPOSE 8080
EXPOSE 8081
ENV ASPNETCORE_URLS=https://+:5000



COPY *.sln .
COPY Shoper.UI/*.csproj             ./Shoper.UI/
COPY Shoper.Data/*.csproj          ./Shoper.Data/
COPY Shoper.Entities/*.csproj      ./Shoper.Entities/
COPY Shoper.BusinessLogic/*.csproj ./Shoper.BusinessLogic/
COPY Shoper.Management/*.csproj    ./Shoper.Management/
RUN dotnet restore "Shoper.UI/Shoper.UI.csproj"

COPY Shoper.UI/. ./Shoper.UI/
COPY Shoper.Management/. ./Shoper.Management/
COPY Shoper.Data/. ./Shoper.Data/
COPY Shoper.Entities/. ./Shoper.Entities/
COPY Shoper.BusinessLogic/. ./Shoper.BusinessLogic/


WORKDIR /app/ShoperProject
RUN dotnet publish  -c release -o /app


FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app ./


ENTRYPOINT ["dotnet", "Shoper.UI.dll", "--server.urls", "https://0.0.0.0:5001"]


