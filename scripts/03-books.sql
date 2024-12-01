CREATE TABLE IF NOT EXISTS books (
    id                  UUID            PRIMARY KEY NOT NULL,
    title               VARCHAR(50)                 NOT NULL,
    resume              VARCHAR(500)                NOT NULL,
    summary             TEXT                        NULL,
    price               DECIMAL(13, 2)              NOT NULL CHECK (price >= 20.00),
    number_of_pages     SMALLINT                    NOT NULL CHECK (number_of_pages > 0),
    isbn                VARCHAR(13)                 NOT NULL,
    release             DATE                        NOT NULL CHECK (release > NOW()::date),
    category_id         UUID                        NOT NULL,
    author_id           UUID                        NOT NULL,
    UNIQUE (title),
    UNIQUE (isbn),
    CONSTRAINT fk_category
        FOREIGN KEY(category_id)
            REFERENCES categories(id),
    CONSTRAINT fk_author
        FOREIGN KEY(author_id)
            REFERENCES authors(id)
);