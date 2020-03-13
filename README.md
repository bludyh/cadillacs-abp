# A Better Portal

## Overview

This project attempts to combine some of the most essential features from [Canvas](https://fhict.instructure.com/), [Progress](https://progresswww.nl/) and [Portal](https://portal.fhict.nl/) into a single Student Management System, which we call **A Better Portal**. The final product will be a web application with a user interface and several back-end services deployed as a distributed system.

The technologies we use are:
- Microservices Architecture Pattern
- Front-end: [Angular](https://angular.io/)
- Back-end: [ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/?view=aspnetcore-3.1)
- Database: [Microsoft SQL Server](https://hub.docker.com/_/microsoft-mssql-server)
- Containerization: [Docker](https://www.docker.com/)

## Group Members

- Dimitar Parpulov: [d.parpulov@student.fontys.nl](mailto:d.parpulov@student.fontys.nl)
- Phat Tran: [phat.tran@student.fontys.nl](mailto:phat.tran@student.fontys.nl)
- Dung Luong Trung: [d.luong@student.fontys.nl](mailto:d.luong@student.fontys.nl)
- Borislav Gramatikov: [b.gramatikov@student.fontys.nl](mailto:b.gramatikov@student.fontys.nl)
- Thanh Nguyen: [n.tthanh@student.fontys.nl](mailto:n.tthanh@student.fontys.nl)
- Win Tran: [n.tran@student.fontys.nl](mailto:n.tran@student.fontys.nl)

## Setup Development Environment

### 1. Visual Studio 2019 (for back-end development)

- Install [Visual Studio 2019 Community Edition](https://visualstudio.microsoft.com/) or modify existing installation by using **Visual Studio Installer**
- Select to add **.NET Core cross-platform development** when asked

  ![.NET Core cross-platform development](/images/setup/vs2019-1.png)

### 2. Visual Studio Code or other Code Editor (for front-end development)

- Install [Visual Studio Code](https://visualstudio.microsoft.com/)

### 3. Docker Desktop

- Install [Docker Desktop](https://www.docker.com/products/docker-desktop) following this [guide](https://docs.docker.com/docker-for-windows/install/)
- Verify **Docker** is installed and running

### 4. Angular (for front-end development)

- Install [Node.js LTS version](https://nodejs.org/en/)
- Follow **Step 1** from this [guide](https://angular.io/guide/setup-local)

### 5. Clone A Better Portal Project

- Clone this project to local machine
- Checkout `develop` branch

### 6. Build & Run Project

#### Back-end

- Open `cadillacs-abp/src/Services.sln` with **Visual Studio 2019**
- Set `docker-compose` as startup project if not yet

  ![Set as Startup project](/images/setup/vs2019-2.png)

- Select **Docker Compose**

  ![Docker Compose](/images/setup/vs2019-3.png)

- Wait for `docker-compose` to build and run the project

  At this step, `docker-compose`, which is a program comes with **Docker Desktop**, build project's **Docker** images following the instructions given in `Dockerfile`s in each project. The instructions includes:

  1. Pull the base **ASP.NET Core Runtime** image from [Docker Hub](https://hub.docker.com/)
  2. Set up `app` directory in the **base** container
  3. Open **HTTP/HTTPS** port on the **base** container at runtime
  4. Pull **SDK** image in order to build the project
  5. Set up `src` directory in the **build** container to hold project source code
  6. Copy the `.csproj` file from host machine to project's directory in the **build** container
  7. Restore the project's dependencies using the `.csproj` file
  8. Copy the remaining files in the project from host machine
  9. Build the project using the source code in the **build** container
  10. Publish the project to **publish** container
  11. Copy published files from the **publish** container to the **base** container
  12. Instruct the container to start the **ASP.NET Core** app
    
  ![Dockerfile](/images/setup/dockerfile.png)

  After building all project's images, `docker-compose` starts up all the applications in their container.

- Verify containers are up and running using `docker ps`

  ![docker ps](/images/setup/dockerps.png)

- Verify `ApiGateway` is working by going to

  - `http://localhost:5000/api/identity`
  - `http://localhost:5000/api/course`
  - `http://localhost:5000/api/progress`
  - `http://localhost:5000/api/schedule`
  - `http://localhost:5000/api/announcement`

  ![Verify ApiGateway](/images/setup/browser-1.png)

  Port `5000` can be changed in case it is used by another process by editing the `ports` configuration of `apigateway` service in `docker-compose.override.yml`.

  ![Change port](/images/setup/ports.png)

#### Front-end

- Navigate to `cadillacs-abp/src/Web/Spa`
- Run `ng serve --open`
