#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM maven:3.6.3-openjdk-14-slim AS base
WORKDIR /app

FROM maven:3.6.3-openjdk-14-slim AS build
WORKDIR /src
COPY ./app/Infrastructure/Microservices.Demo.DiscoveryServer/discovery-server .
RUN ls
RUN mvn install
RUN mvn compile

FROM build AS publish
RUN mvn package
RUN ls target

FROM base AS final
WORKDIR /app
COPY --from=publish /src/target/*.jar ./app.jar
ENTRYPOINT ["java", "-jar", "/app/app.jar"]