-- Table: public.user_sessions

-- DROP TABLE public.user_sessions;

CREATE TABLE public.user_sessions
(
    id bigint NOT NULL DEFAULT nextval('user_sessions_id_seq'::regclass),
    user_id bigint NOT NULL,
    user_token uuid NOT NULL,
    token_expiration_date timestamp without time zone NOT NULL,
    last_active timestamp without time zone NOT NULL,
    CONSTRAINT user_sessions_pkey PRIMARY KEY (id),
    -- CONSTRAINT fk_session_user FOREIGN KEY (user_id)
    --     REFERENCES public.users (id) MATCH SIMPLE
    --     ON UPDATE NO ACTION
    --     ON DELETE NO ACTION
    --     NOT VALID
)

TABLESPACE pg_default;

ALTER TABLE public.user_sessions
    OWNER to postgres;
-- Index: fki_fk_session_user

-- DROP INDEX public.fki_fk_session_user;

-- CREATE INDEX fki_fk_session_user
--     ON public.user_sessions USING btree
--     (user_id ASC NULLS LAST)
--     TABLESPACE pg_default;

insert into user_sessions (user_id, last_active, user_token, token_expiration_date) values (2, '2021-11-04 19:06:49', '925dd708-88ed-456c-b7f8-1578553dfbb3', '2021-11-04 19:21:49');
insert into user_sessions (user_id, last_active, user_token, token_expiration_date) values (1, '2021-11-02 23:05:22', '665e3ba6-5020-4cd9-b8ca-f9fb22e00df9', '2021-11-02 23:20:22');
insert into user_sessions (user_id, last_active, user_token, token_expiration_date) values (3, '2021-11-16 15:27:45', '6282908e-592e-41ac-9833-c1033d524756', '2021-11-16 15:42:45');
insert into user_sessions (user_id, last_active, user_token, token_expiration_date) values (4, '2021-11-09 13:25:39', '4760b597-8b91-4490-b313-80056197262f', '2021-11-09 13:40:39');
insert into user_sessions (user_id, last_active, user_token, token_expiration_date) values (5, '2021-11-08 11:32:12', 'c9d3ae2b-eed1-484c-aaf1-fef81b3cb26b', '2021-11-08 11:47:12');
insert into user_sessions (user_id, last_active, user_token, token_expiration_date) values (6, '2021-11-13 06:57:21', '15d098a5-8091-4cc2-80ac-b510ca99040b', '2021-11-13 07:12:21');
insert into user_sessions (user_id, last_active, user_token, token_expiration_date) values (7, '2021-11-04 08:01:33', '3b9a91b4-cc55-420b-9103-99721eaff562', '2021-11-04 08:16:33');
insert into user_sessions (user_id, last_active, user_token, token_expiration_date) values (8, '2021-11-13 11:43:39', '0706d750-1306-42aa-8a65-c9f2e33cfa64', '2021-11-13 11:58:39');
insert into user_sessions (user_id, last_active, user_token, token_expiration_date) values (9, '2021-11-16 11:36:28', '131919aa-ee10-4ebb-9f9f-6b0e15917efc', '2021-11-16 11:51:28');
insert into user_sessions (user_id, last_active, user_token, token_expiration_date) values (10, '2021-11-18 21:02:00', 'dd4bebfc-430f-477f-9086-7360c9159e5c', '2021-11-18 21:17:00');
insert into user_sessions (user_id, last_active, user_token, token_expiration_date) values (11, '2021-11-14 22:24:37', '5ca7c92d-7bce-4eeb-a660-7a021507a113', '2021-11-14 22:39:37');
insert into user_sessions (user_id, last_active, user_token, token_expiration_date) values (12, '2021-11-02 01:03:54', 'da56aba8-2dc2-4331-89f1-5081746b5eea', '2021-11-02 01:18:54');
insert into user_sessions (user_id, last_active, user_token, token_expiration_date) values (13, '2021-11-09 00:45:08', '3ad970e6-c913-4c76-a80c-3f3bd2f7c054', '2021-11-09 01:00:08');
insert into user_sessions (user_id, last_active, user_token, token_expiration_date) values (14, '2021-11-03 07:19:33', 'df7205e5-79cf-4f19-94bb-60bf8d0f2354', '2021-11-03 07:34:33');
insert into user_sessions (user_id, last_active, user_token, token_expiration_date) values (15, '2021-11-18 02:02:02', '371f1a0f-2962-4211-8312-e101ae1eaaa5', '2021-11-18 02:17:02');
insert into user_sessions (user_id, last_active, user_token, token_expiration_date) values (16, '2021-11-07 04:22:18', '6dd10e69-0a0b-4c8e-ba94-13e111153564', '2021-11-07 04:37:18');
insert into user_sessions (user_id, last_active, user_token, token_expiration_date) values (17, '2021-11-18 23:00:47', 'c02246c5-8213-4de5-bb43-705d52bbed8c', '2021-11-18 23:15:47');
insert into user_sessions (user_id, last_active, user_token, token_expiration_date) values (18, '2021-11-13 07:42:35', '2215b853-e718-417a-b0db-bf36adcb97ca', '2021-11-13 07:57:35');
insert into user_sessions (user_id, last_active, user_token, token_expiration_date) values (19, '2021-11-19 06:24:58', 'a1331182-62b4-4861-9faf-ee9066e85919', '2021-11-19 06:39:58');
insert into user_sessions (user_id, last_active, user_token, token_expiration_date) values (20, '2021-11-14 21:53:30', '5d7d9462-ab8b-4018-845a-241ae85e76b3', '2021-11-14 22:08:30');
insert into user_sessions (user_id, last_active, user_token, token_expiration_date) values (21, '2021-11-11 16:23:22', '19aa42ae-aab9-4db5-8bb2-1fbd055ed154', '2021-11-11 16:38:22');
insert into user_sessions (user_id, last_active, user_token, token_expiration_date) values (22, '2021-11-04 19:30:03', '06280c97-a4c1-4d05-af4a-55784c190a55', '2021-11-04 19:45:03');
insert into user_sessions (user_id, last_active, user_token, token_expiration_date) values (23, '2021-11-21 05:06:45', '9a5cce6e-e970-426f-ac56-3da25d54fcd7', '2021-11-21 05:21:45');
insert into user_sessions (user_id, last_active, user_token, token_expiration_date) values (24, '2021-11-16 01:56:48', '6d5079cd-579f-4bf0-ac62-27497d3f3ebc', '2021-11-16 02:11:48');
insert into user_sessions (user_id, last_active, user_token, token_expiration_date) values (25, '2021-11-10 00:45:37', '6719a403-3814-4860-acd5-c67b099864e6', '2021-11-10 01:00:37');