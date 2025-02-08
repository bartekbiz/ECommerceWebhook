# ECommerceWebhook

## Overview
ECommerceWebhook is a lightweight microservice designed for managing webhooks in an e-commerce system. It listens for specific events and notifies pre-registered URLs via HTTP POST requests. The application follows a clean and maintainable hexagonal architecture.

## Prerequisites

To run this application, you need:
- [.NET 8](https://dotnet.microsoft.com/en-us/download/dotnet/8.0) (tested with version 8.0.12)
- Docker (optional for containerization)

## Installation and Usage
### 1. Clone the repository:
```sh
git clone https://github.com/bartekbiz/ECommerceWebhook.git
cd ECommerceWebhook
```

### 2. Run the app locally:

#### Development Environment
```sh
dotnet run --project ECommerceWebhook.Api
```

#### Production Environment
```sh
dotnet run --project ECommerceWebhook.Api --environment Production
```

## Running with Docker

### 1. Build the Docker Image
```sh
docker build -t ecommerce-webhook .
```

### 2. Run the Docker Container
```sh
docker run -p 8080:8080 ecommerce-webhook
```

The application will be accessible at `http://localhost:8080`.

## API Documentation
This project includes Swagger (OpenAPI) documentation for easy API exploration. To use is follow the steps:

1. Start the application.
2. Navigate to: [http://localhost:8080/swagger](http://localhost:8080/swagger)

## Testing
The solution includes unit tests and you can run them using:
```sh
dotnet test
```

## Project structure
This project follows **Hexagonal Architecture (Ports and Adapters)** for maintainable and scalable development.

```scss
Solution
├── ECommerceWebhook.Api (input adapter)
│   └── Controllers
├── ECommerceWebhook.Application (business logic)
│   └── Services
├── ECommerceWebhook.Domain (domain model and ports)
│   └── Entities
│   └── Ports
├── ECommerceWebhook.Infrastructure (output adapters)
│   └── Repositories
│   └── Notifiers
└── ECommerceWebhook.Tests
```

## License  
This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for more details.
