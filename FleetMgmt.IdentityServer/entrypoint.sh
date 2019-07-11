#!/bin/bash
set -e
run_cmd="dotnet FleetMgmt.IdentityServer.dll --server.urls http://*:80"

until dotnet ef database update --context ConfigurationDbContext; do
>&2 echo "SQL Server is starting up"
sleep 1
done

>&2 echo "SQL Server is up - executing command"
exec $run_cmd