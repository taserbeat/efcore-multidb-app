# https://hub.docker.com/layers/library/postgres/16.9-alpine/images/sha256-5f297814cc17cdd51517486d5ae5e5d145f537a8c55be40b873f7facd49f18b6
FROM postgres:16.9-alpine

COPY ./docker-entrypoint-initdb.d/* /docker-entrypoint-initdb.d/
