# 🛒 E-commerce Microservices Project

A distributed, modular e-commerce platform built with .NET 9 and modern microservice practices. It includes user management, product catalog, cart and order processing, payments with Stripe, and real-time notifications – all containerized with Docker.

## 📚 Table of Contents

- [Architecture](#-architecture)
- [Tech Stack](#-tech-stack)
- [Services Breakdown](#-services-breakdown)
- [Getting Started](#-getting-started)

## 🧱 Architecture

- **Microservices** – Each feature is its own service.
- **RESTful APIs** – Public endpoints for external access.
- **gRPC** – Internal communication between services.
- **AMQP** – RabbitMQ as a message broker.
- **JWT-based Authentication** – Secure user authentication across services.
- **API Gateway** – YARP routes external requests.
- **Dockerized** – Services run in containers with Docker Compose.

```
Client ⇄ API Gateway ⇄ Microservices
                       ↳ User Service
                       ↳ Product Service
                       ↳ Cart Service
                       ↳ Order Service
                       ↳ Payment Service
                       ↳ Notification Service
```

---

## 🧰 Tech Stack

- **.NET 9** (ASP.NET Core)
- **PostgreSQL**, **Redis**
- **REST API**, **gRPC**, **SignalR**
- **RabbitMQ**, **MassTransit** – Async messaging
- **Stripe** – Payments
- **YARP** – API Gateway
- **Docker**, **Docker Compose**
- **Libs:** EF Core, Marten, Mapster, AutoMapper, MediatR, FluentValidation, Carter, BCrypt.Net, Scrutor

---

## 🧩 Services Breakdown

### 🧑 [User Service](./UserService)

**Patterns:** Clean Architecture, CQRS

**Authentication Endpoints**

- `POST /api/users/register`
- `POST /api/users/login`
- `POST /api/users/refresh-token`
- `POST /api/users/logout` _(Auth Required)_

**User Endpoints**

- `GET /api/users/me` _(Auth Required)_
- `PUT /api/users/me` _(Auth Required)_
- `PUT /api/users/me/change-password` _(Auth Required)_

**Address Endpoints**

- `POST /api/users/me/addresses` _(Auth Required)_
- `GET /api/users/me/addresses` _(Auth Required)_
- `GET /api/users/me/addresses/{id}` _(Auth Required)_

---

### 🛍 [Product Service](./ProductService)

**Patterns:** Vertical Slice Architecture (VSA), CQRS

**API Endpoints**

- `POST /api/products` _(Auth Required)_
- `PUT /api/products/{id}` _(Auth Required)_
- `GET /api/products/{id}`
- `GET /api/products?search=&category=&minPrice=&maxPrice=&pageSize=&pageNumber=`

**gRPC:** `GetProduct()`

---

### 🛒 [Cart Service](./CartService)

**Patterns:** Vertical Slice Architecture (VSA), Repository Pattern, CQRS

**API Endpoints** _(Auth Required)_

- `GET /api/cart`
- `POST /api/cart/items`
- `PUT /api/cart/items/{id}`
- `DELETE /api/cart/items/{id}`
- `DELETE /api/cart`
- `POST /api/cart/checkout`

**RabbitMQ Events:** CartCheckoutEvent (Publisher)

---

### 💳 [Payment Service](./PaymentService)

**gRPC:** `Charge()`

**API Endpoints**

- `GET /api/payment/key` - Get stripe public key

---

### 📦 [Order Service](./OrderService)

**Patterns:** Vertical Slice Architecture (VSA), CQRS

**API Endpoints** _(Auth Required)_

- `GET /api/orders`
- `GET /api/orders/{id}`
- `PUT /api/orders/{id}/cancel`

**RabbitMQ Events:**

- OrderCreatedConsumer (Consumer)
- NotificationCreatedEvent (Publisher)

---

### 🔔 [Notification Service](./NotificationService)

**Patterns:** VSA, CQRS

**API Endpoints** _(Auth Required)_

- `GET /api/notifications`
- `PUT /api/notifications`
- `PUT /api/notifications/{id}`

**Hubs:**

- `/api/notifications/hub` _(SignalR)_

**RabbitMQ Events:** NotificationCreatedEvent (Consumer)

---

### 🌐 [API Gateway](./ApiGateway)

**Tool:** YARP Reverse Proxy  
**Routes:**

- `/user-service`
- `/product-service`
- `/cart-service`
- `/payment-service`
- `/order-service`
- `/notification-service`

---

### 📦 [Building Blocks (Shared)](./BuildingBlocks)

Includes shared logic like exception handling, extensions, middleware, and cross-cutting concerns (e.g., constants, events, proto contracts), as well as common packages.

## 🚀 Getting Started

### Prerequisites

- Docker
- Docker Compose
- .NET SDK (for local development and building)

### 📦 Docker Compose Setup

To spin up the system:

```bash
docker-compose -f docker-compose.yml -f docker-compose.override.yml up --build
```

### 🐳 Services Port

> _Note: Local = host machine, Docker = mapped container ports, Inside = internal container ports._

| Service              | Local Ports | Docker Ports | Docker-Inside Ports |
| -------------------- | ----------- | ------------ | ------------------- |
| API Gateway          | 5060, 5061  | 6060, 6061   | 8080, 8081          |
| User Service         | 5000, 5001  | 6000, 6001   | 8080, 8081          |
| Product Service      | 5010, 5011  | 6010, 6011   | 8080, 8081          |
| Cart Service         | 5020, 5021  | 6020, 6021   | 8080, 8081          |
| Payment Service      | 5030, 5031  | 6030, 6031   | 8080, 8081          |
| Order Service        | 5040, 5041  | 6040, 6041   | 8080, 8081          |
| Notification Service | 5050, 5051  | 6050, 6051   | 8080, 8081          |
| Redis                | 6379        | 6379         | 6379                |
| RabbitMQ             | 5672, 15672 | 5672, 15672  | 5672, 15672         |

> **Note**: All services run in development mode. Ensure HTTPS dev certs and user-secrets are available for .NET services.

### 🔐 Environment Variables

Each service expects a set of environment variables defined in `docker-compose.override.yml`. Sensitive values such as Stripe keys and DB passwords should be managed securely (e.g., `.env` or secrets manager).

---

## 📄 License

This project is licensed under the [MIT License](./LICENSE).
