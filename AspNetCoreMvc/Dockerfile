From mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY /bin/Release/net6.0/publish .
ENV ASPNETCORE_URLS="http://*:4300"
ENV ASPNETCORE_ENVIRONMENT="Application"
Env MssqlCon="Uzak Sunucu"
ENTRYPOINT ["dotnet","AspNetCoreMvc.dll"]