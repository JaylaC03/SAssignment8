#!/bin/sh
set -e

echo "Waiting for PostgreSQL to be ready..."

# Wait for PostgreSQL to be ready
until PGPASSWORD=postgres psql -h postgres -U postgres -d GeometryDb -c '\q' 2>/dev/null; do
  echo "PostgreSQL is unavailable - sleeping"
  sleep 1
done

echo "PostgreSQL is ready!"

# Start the API (migrations will run automatically in Development mode)
echo "Starting Cube API..."
exec dotnet CubeApi.dll

