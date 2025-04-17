#!/bin/sh

echo "Waiting for PostgreSQL to be ready..."
./wait-for notification-service-db:5432

echo "Starting NotificationService..."
dotnet NotificationService.dll
