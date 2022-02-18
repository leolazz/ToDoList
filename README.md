# 2BeDone

[![ToDoList .Net](https://github.com/leolazz/ToDoList/actions/workflows/Integrate.yml/badge.svg)](https://github.com/leolazz/ToDoList/actions/workflows/Integrate.yml)

# Live Deployment: https://2bedone.lazz.tech/

# Description:

2BeDone is a Web Based project and task tracker.

### Why I made this:

After spending hundreds of hours watching course lectures, working through examples, and challenge projects, I wanted to create something on my own. Separate from any guided material. At the time I wasn't confident with my skill level so I used a contrived project idea. This helped me to start immediately and focus more on the development process and larger patterns. Rather than having an extended planning and design phase that could easily result from a more original idea.

- Exercise skills & knowledge from various courses
- Serve as a cumulative demonstration of knowledge and ability at the time
- Learn additional skills, while solidifying prior learning
- Break out of hand holding learning process of courses

### Constraints & Challenges:

- Beginner level experience and knowledge
- Learning and adhering to S.O.L.I.D. principles
  - Rapidly learning:
  - Docker and how to dockerize the application
  - Kubernetes and deployment
- Working to complete the project quickly while solidifying new information
- Not getting lost in the minutiae of less relevant aspects

### Results:

Solidified lessons from months of courses, learned  Dev-Ops technologies, greatly increased confidence in skill level, and gained a clearer understanding of the reality of software development.


![screenshot](/2bedone.png)

## ERD

![diagram](assets/Database-Diagram.png)

## Installation

Target ASP.NETCore 5.0 MVC

Download ToDoList project folder and open in visual studio

## Requirements

**Dotnet SDK v5**

- https://dotnet.microsoft.com/download/dotnet/5.0

**Dotnet Core v3.1**

- https://dotnet.microsoft.com/download

## Usage

```bash
# build the solution
$ dotnet build
# run the solution
$ dotnet run

# build src
$ dotnet build src
# run src
$ dotnet run --project src

# build tests
$ dotnet build tests
# run tests
$ dotnet run --project tests
```

## Docker

```bash
# pull the image
$ docker pull leolazz/2bdone
```

```bash
# build the container image
$ docker build -t <ImageName>:<TagName> .
```

```bash
# docker run the built container image
# OPTIONAL: Add a bind mount to a SQLite .db for persistance
$ docker run -it -p 5001:80 --name <ContainerName> <ImageName>
```

```bash
# push the built container image to docker hub
$ docker push leolazz/2bdone:<TagName>
```

```bash
# run the pulled image
$ docker run leolazz/2bdone:<TagName>
```

## Docker-compose

```bash
# launch with docker-compose
$ docker-compose up
```

## Contributing

Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

## License

[MIT](https://choosealicense.com/licenses/mit/)
