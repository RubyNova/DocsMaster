# Builder
FROM microsoft/dotnet:2.1-sdk AS builder
WORKDIR /app

COPY *.csproj ./
RUN dotnet restore

COPY . .
RUN dotnet publish -c Release -o out

# Build the runtime image
# I'm using the dotnet-nightly images here, because of the benefits that come from using the runtime image on alpine
# If there are problems with the nightly version, we should switch to microsoft/dotnet:2.1-aspnetcore-runtime
FROM microsoft/dotnet:2.1-aspnetcore-runtime-alpine
WORKDIR /app

COPY --from=builder /app/out .
#COPY --from=builder /app/appsettings.json .

EXPOSE 8080
ENTRYPOINT ["dotnet", "DocsMaster.dll"]