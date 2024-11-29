CREATE TABLE IF NOT EXISTS authors (
    id              UUID            PRIMARY KEY NOT NULL,
    name            VARCHAR(50)                 NOT NULL,
    email           VARCHAR(255)                NOT NULL,
    description     VARCHAR(400)                NOT NULL,
    registered      TIMESTAMP                   NOT NULL,
    UNIQUE (email)
);