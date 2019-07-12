#!/bin/bash
database=FleetMgmt
wait_time=15s
password=Abcd@1234

# # wait for SQL Server to come up
# echo importing data will start in $wait_time...
# sleep $wait_time
# echo importing data...

# wait for MSSQL server to start
export STATUS=1
i=0

while [[ $STATUS -ne 0 ]] && [[ $i -lt 30 ]]; do
	i=$i+1
	/opt/mssql-tools/bin/sqlcmd -t 1 -U sa -P $password -Q "select 1" >> /dev/null
	STATUS=$?
done

if [ $STATUS -ne 0 ]; then
	echo "Error: MSSQL SERVER took more than thirty seconds to start up."
	exit 1
fi

# run the init script to create the DB and the tables in /table
echo "======= MSSQL SERVER STARTED ========" | tee -a ./config.log
/opt/mssql-tools/bin/sqlcmd -S 0.0.0.0 -U sa -P $password -d master -i ./init.sql
echo "======= MSSQL CONFIG COMPLETE =======" | tee -a ./config.log
# for entry in "table/*.sql"
# do
#   echo executing $entry
#   /opt/mssql-tools/bin/sqlcmd -S 0.0.0.0 -U sa -P $password -i $entry
# done

#import the data from the csv files
# for entry in "data/*.csv"
# do
#   # i.e: transform /data/MyTable.csv to MyTable
#   shortname=$(echo $entry | cut -f 1 -d '.' | cut -f 2 -d '/')
#   tableName=$database.dbo.$shortname
#   echo importing $tableName from $entry
#   /opt/mssql-tools/bin/bcp $tableName in $entry -c -t',' -F 2 -S 0.0.0.0 -U sa -P $password
# done