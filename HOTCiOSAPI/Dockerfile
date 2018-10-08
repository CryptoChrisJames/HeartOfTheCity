#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM microsoft/dotnet:2.1-aspnetcore-runtime-nanoserver-sac2016 AS base
WORKDIR /app
EXPOSE 64223
EXPOSE 44385

FROM microsoft/dotnet:2.1-sdk-nanoserver-sac2016 AS build
WORKDIR /src
COPY ["HOTCiOSAPI/HOTCiOSAPI.csproj", "HOTCiOSAPI/"]
RUN dotnet restore "HOTCiOSAPI/HOTCiOSAPI.csproj"
COPY . .
WORKDIR "/src/HOTCiOSAPI"
RUN dotnet build "HOTCiOSAPI.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "HOTCiOSAPI.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "HOTCiOSAPI.dll"]