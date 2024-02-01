A quick and impromptu demo of the [Marten .NET API](https://martendb.io/)


## Some helpful SQL Stuff.


1. Creating the database
```

create database martendemo;
create user martendemo with encrypted password 'martendemo';
GRANT ALL PRIVILEGES ON DATABASE martendemo to martendemo;

```

2. Finding activity in the database

```
select * from pg_stat_activity
```
