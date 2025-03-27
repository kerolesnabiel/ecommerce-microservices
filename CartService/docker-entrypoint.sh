#!/bin/sh

echo "Waiting for PostgreSQL to be ready..."
./wait-for cart-service-db:5432

echo "Starting CartService..."
dotnet CartService.dll
