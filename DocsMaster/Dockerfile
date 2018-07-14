FROM microsoft/dotnet:latest
COPY ./ /app
RUN cd app && dotnet restore
EXPOSE 8080
WORKDIR /app
ENTRYPOINT ["dotnet", "run"]