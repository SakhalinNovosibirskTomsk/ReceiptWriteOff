FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
ENV ASPNETCORE_URLS=http://*:5002

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY Src/ReceiptWriteOff/*.sln .
COPY Src/ReceiptWriteOff/ReceiptWriteOff/ReceiptWriteOff.csproj ReceiptWriteOff/
COPY Src/ReceiptWriteOff/ReceiptWriteOff.Application.Settings ReceiptWriteOff.Application.Settings/
COPY Src/ReceiptWriteOff/ReceiptWriteOff.Contracts ReceiptWriteOff.Contracts/
COPY Src/ReceiptWriteOff/ReceiptWriteOff.Domain.Entities ReceiptWriteOff.Domain.Entities/
COPY Src/ReceiptWriteOff/ReceiptWriteOff.Domain.Entities.Abstractions ReceiptWriteOff.Domain.Entities.Abstractions/
COPY Src/ReceiptWriteOff/ReceiptWriteOff.Infrastructure.EntityFramework.Abstractions ReceiptWriteOff.Infrastructure.EntityFramework.Abstractions/
COPY Src/ReceiptWriteOff/ReceiptWriteOff.Infrastructure.EntityFramework.Implementation ReceiptWriteOff.Infrastructure.EntityFramework.Implementation/
COPY Src/ReceiptWriteOff/ReceiptWriteOff.Infrastructure.EntityFramework.Migration ReceiptWriteOff.Infrastructure.EntityFramework.Migration/
COPY Src/ReceiptWriteOff/ReceiptWriteOff.Infrastructure.Repositories.Abstractions ReceiptWriteOff.Infrastructure.Repositories.Abstractions/
COPY Src/ReceiptWriteOff/ReceiptWriteOff.Infrastructure.Repositories.Implementation ReceiptWriteOff.Infrastructure.Repositories.Implementation/
COPY Src/ReceiptWriteOff/ReceiptWriteOff.Mapping ReceiptWriteOff.Mapping/
COPY Src/ReceiptWriteOff/ReceiptWriteOff.Application.Abstractions ReceiptWriteOff.Application.Abstractions/
COPY Src/ReceiptWriteOff/ReceiptWriteOff.Application.Contracts ReceiptWriteOff.Application.Contracts/
COPY Src/ReceiptWriteOff/ReceiptWriteOff.Application.Implementations ReceiptWriteOff.Application.Implementations/
COPY Src/ReceiptWriteOff/ReceiptWriteOff.Application.Mapping ReceiptWriteOff.Application.Mapping/
COPY Src/ReceiptWriteOff/ReceiptWriteOff.Infrastructure.EntityFramework.Tests ReceiptWriteOff.Infrastructure.EntityFramework.Tests/
COPY Src/ReceiptWriteOff/ReceiptWriteOff.Application.Tests ReceiptWriteOff.Application.Tests/
COPY Src/ReceiptWriteOff/ReceiptWriteOff.Tests ReceiptWriteOff.Tests/
COPY Src/ReceiptWriteOff/ReceiptWriteOff.Infrastructure.Repositories.Tests ReceiptWriteOff.Infrastructure.Repositories.Tests/

RUN dotnet restore
COPY Src/ReceiptWriteOff/. .
RUN dotnet publish "ReceiptWriteOff/ReceiptWriteOff.csproj" -c Release -o /app/publish --no-restore

FROM base AS final
COPY --from=build /app/publish ./
CMD dotnet ReceiptWriteOff.dll