CREATE TABLE IF NOT EXISTS categories (
    id              UUID            PRIMARY KEY NOT NULL,
    name            VARCHAR(50)                 NOT NULL,
    UNIQUE (name)
);