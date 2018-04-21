FROM mysql

MAINTAINER rafael-miceli

ENV MYSQL_DATABASE=mydb \
    MYSQL_ROOT_PASSWORD=123

ADD schema.sql /docker-entrypoint-initdb.d


EXPOSE 3306