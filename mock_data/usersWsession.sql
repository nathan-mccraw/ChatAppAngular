-- Table: public.users

-- DROP TABLE public.users;

CREATE TABLE public.users
(
    id bigint NOT NULL DEFAULT nextval('users_id_seq'::regclass),
    session_id bigint NOT NULL,
    username text COLLATE pg_catalog."default" NOT NULL,
    password text COLLATE pg_catalog."default" NOT NULL,
    first_name text COLLATE pg_catalog."default" NOT NULL,
    last_name text COLLATE pg_catalog."default" NOT NULL,
    date_created timestamp without time zone NOT NULL,
    CONSTRAINT users_pkey PRIMARY KEY (id),
    -- CONSTRAINT fk_user_session FOREIGN KEY (session_id)
    --     REFERENCES public.user_sessions (id) MATCH SIMPLE
    --     ON UPDATE NO ACTION
    --     ON DELETE NO ACTION
    --     NOT VALID
)

TABLESPACE pg_default;

ALTER TABLE public.users
    OWNER to postgres;
-- Index: fki_fk_user_session

-- DROP INDEX public.fki_fk_user_session;

-- CREATE INDEX fki_fk_user_session
--     ON public.users USING btree
--     (session_id ASC NULLS LAST)
--     TABLESPACE pg_default;

insert into users (session_id, username, password, first_name, last_name, date_created) values (1, 'vmoulds0', 'p6mMfrZqk', 'Vilma', 'Moulds', '2021-10-23 13:29:04');
insert into users (session_id, username, password, first_name, last_name, date_created) values (2, 'ehise1', 'AJyNcw', 'Edan', 'Hise', '2021-10-02 16:27:38');
insert into users (session_id, username, password, first_name, last_name, date_created) values (3, 'tcalbaithe2', 'WrEvClY', 'Tabina', 'Calbaithe', '2021-10-24 14:13:54');
insert into users (session_id, username, password, first_name, last_name, date_created) values (4, 'rricker3', 'gliHR0kEqH9h', 'Roze', 'Ricker', '2021-10-10 02:31:25');
insert into users (session_id, username, password, first_name, last_name, date_created) values (5, 'khoffmann4', 'XA7yF3Hx3DPp', 'Kermie', 'Hoffmann', '2021-11-12 12:47:08');
insert into users (session_id, username, password, first_name, last_name, date_created) values (6, 'cgrutchfield5', 'l9qls52jz1AQ', 'Concettina', 'Grutchfield', '2021-11-01 19:52:16');
insert into users (session_id, username, password, first_name, last_name, date_created) values (7, 'jevenett6', 'tD5axc', 'Jordain', 'Evenett', '2021-10-02 19:38:11');
insert into users (session_id, username, password, first_name, last_name, date_created) values (8, 'jgovier7', 'ge8MAdasOq', 'Julianna', 'Govier', '2021-11-14 23:19:34');
insert into users (session_id, username, password, first_name, last_name, date_created) values (9, 'tbuckney8', 'oULSR4Wm', 'Tadd', 'Buckney', '2021-10-12 11:36:24');
insert into users (session_id, username, password, first_name, last_name, date_created) values (10, 'satton9', 'ewpxFJUQUTQu', 'Sarette', 'Atton', '2021-11-15 04:38:25');
insert into users (session_id, username, password, first_name, last_name, date_created) values (11, 'mlogesa', '3zmdIwnu2n', 'Mahmoud', 'Loges', '2021-10-21 18:13:34');
insert into users (session_id, username, password, first_name, last_name, date_created) values (12, 'hanneslieb', '71xAwFO1F', 'Hedwiga', 'Anneslie', '2021-10-23 12:31:11');
insert into users (session_id, username, password, first_name, last_name, date_created) values (13, 'cpalphramandc', 'IxFnx86', 'Clareta', 'Palphramand', '2021-10-27 21:07:08');
insert into users (session_id, username, password, first_name, last_name, date_created) values (14, 'fmackeyd', 'qmccqSKrLOXr', 'Fayette', 'MacKey', '2021-10-02 05:10:19');
insert into users (session_id, username, password, first_name, last_name, date_created) values (15, 'atofanoe', '7XKuFc', 'Alexina', 'Tofano', '2021-10-08 05:10:52');
insert into users (session_id, username, password, first_name, last_name, date_created) values (16, 'nsemensf', 'R6uWJW93tloo', 'Nikola', 'Semens', '2021-10-28 01:13:19');
insert into users (session_id, username, password, first_name, last_name, date_created) values (17, 'lfeachamg', 'lIpFDru3W4tS', 'Lief', 'Feacham', '2021-11-18 10:44:43');
insert into users (session_id, username, password, first_name, last_name, date_created) values (18, 'fpeascodh', 'ano6LdqxKNl', 'Fonz', 'Peascod', '2021-11-20 09:38:35');
insert into users (session_id, username, password, first_name, last_name, date_created) values (19, 'dpickstoni', 'WR90KL5w9td', 'Dreddy', 'Pickston', '2021-10-20 03:12:22');
insert into users (session_id, username, password, first_name, last_name, date_created) values (20, 'arickerj', '7n6Zl3K0Yf', 'Alric', 'Ricker', '2021-10-10 09:14:25');
insert into users (session_id, username, password, first_name, last_name, date_created) values (21, 'cscheuk', 'el44KrK', 'Costanza', 'Scheu', '2021-10-21 08:25:14');
insert into users (session_id, username, password, first_name, last_name, date_created) values (22, 'jduntonl', '6fFd8t3cCTIZ', 'Judye', 'Dunton', '2021-10-28 16:14:57');
insert into users (session_id, username, password, first_name, last_name, date_created) values (23, 'laskawm', 'cLG4fC1IeIh0', 'Lisette', 'Askaw', '2021-10-16 12:08:28');
insert into users (session_id, username, password, first_name, last_name, date_created) values (24, 'rkendaln', 'bRSaAO8A', 'Rosene', 'Kendal', '2021-10-08 05:13:15');
insert into users (session_id, username, password, first_name, last_name, date_created) values (25, 'gwaymano', '73CnQma', 'Gil', 'Wayman', '2021-10-21 13:11:00');
