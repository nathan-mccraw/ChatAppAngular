-- Table: public.messages

-- DROP TABLE public.messages;

CREATE TABLE public.messages
(
    id bigint NOT NULL DEFAULT nextval('messages_id_seq'::regclass),
    user_id bigint NOT NULL,
    message text COLLATE pg_catalog."default" NOT NULL,
    date_created timestamp without time zone NOT NULL,
    CONSTRAINT messages_pkey PRIMARY KEY (id),
    -- CONSTRAINT fk_message_user FOREIGN KEY (user_id)
    --     REFERENCES public.users (id) MATCH SIMPLE
    --     ON UPDATE NO ACTION
    --     ON DELETE NO ACTION
    --     NOT VALID
)

TABLESPACE pg_default;

ALTER TABLE public.messages
    OWNER to postgres;
-- Index: fki_fk_message_user

-- DROP INDEX public.fki_fk_message_user;

-- CREATE INDEX fki_fk_message_user
--     ON public.messages USING btree
--     (user_id ASC NULLS LAST)
--     TABLESPACE pg_default;

insert into messages (user_id, message, date_created) values (20, 'Proin at turpis a pede posuere nonummy. Integer non velit.', '2021-10-07 22:50:14');
insert into messages (user_id, message, date_created) values (5, 'Cras mi pede, malesuada in, imperdiet et, commodo vulputate, justo. In blandit ultrices enim. Lorem ipsum dolor sit amet, consectetuer adipiscing elit.', '2021-11-10 06:28:19');
insert into messages (user_id, message, date_created) values (13, 'Nulla tellus. In sagittis dui vel nisl.', '2021-10-27 22:12:44');
insert into messages (user_id, message, date_created) values (21, 'Integer tincidunt ante vel ipsum. Praesent blandit lacinia erat.', '2021-11-04 21:09:33');
insert into messages (user_id, message, date_created) values (5, 'Maecenas rhoncus aliquam lacus. Morbi quis tortor id nulla ultrices aliquet.', '2021-10-23 15:48:23');
insert into messages (user_id, message, date_created) values (15, 'Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Vivamus vestibulum sagittis sapien. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus.', '2021-11-17 15:12:53');
insert into messages (user_id, message, date_created) values (14, 'Aenean sit amet justo. Morbi ut odio.', '2021-11-11 05:02:23');
insert into messages (user_id, message, date_created) values (23, 'Mauris lacinia sapien quis libero. Nullam sit amet turpis elementum ligula vehicula consequat. Morbi a ipsum.', '2021-10-22 03:08:40');
insert into messages (user_id, message, date_created) values (20, 'Aliquam erat volutpat.', '2021-11-16 22:52:45');
insert into messages (user_id, message, date_created) values (14, 'Etiam pretium iaculis justo. In hac habitasse platea dictumst. Etiam faucibus cursus urna.', '2021-10-03 17:49:15');
insert into messages (user_id, message, date_created) values (2, 'Nam nulla. Integer pede justo, lacinia eget, tincidunt eget, tempus vel, pede.', '2021-10-02 09:00:17');
insert into messages (user_id, message, date_created) values (8, 'Mauris sit amet eros.', '2021-11-20 09:12:05');
insert into messages (user_id, message, date_created) values (3, 'Etiam justo. Etiam pretium iaculis justo.', '2021-11-21 02:24:46');
insert into messages (user_id, message, date_created) values (24, 'Proin interdum mauris non ligula pellentesque ultrices. Phasellus id sapien in sapien iaculis congue.', '2021-11-12 21:23:48');
insert into messages (user_id, message, date_created) values (4, 'In est risus, auctor sed, tristique in, tempus sit amet, sem.', '2021-11-15 04:43:06');
insert into messages (user_id, message, date_created) values (17, 'Morbi non quam nec dui luctus rutrum.', '2021-10-18 13:57:50');
insert into messages (user_id, message, date_created) values (14, 'Sed accumsan felis.', '2021-11-04 18:42:33');
insert into messages (user_id, message, date_created) values (2, 'Nulla suscipit ligula in lacus.', '2021-10-10 03:15:21');
insert into messages (user_id, message, date_created) values (8, 'Donec diam neque, vestibulum eget, vulputate ut, ultrices vel, augue.', '2021-10-05 18:40:59');
insert into messages (user_id, message, date_created) values (22, 'Proin eu mi. Nulla ac enim.', '2021-11-04 14:02:38');
insert into messages (user_id, message, date_created) values (11, 'In eleifend quam a odio.', '2021-11-08 00:47:12');
insert into messages (user_id, message, date_created) values (23, 'Maecenas rhoncus aliquam lacus. Morbi quis tortor id nulla ultrices aliquet.', '2021-10-04 08:41:56');
insert into messages (user_id, message, date_created) values (8, 'Duis ac nibh. Fusce lacus purus, aliquet at, feugiat non, pretium quis, lectus.', '2021-11-04 00:55:04');
insert into messages (user_id, message, date_created) values (8, 'Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Proin risus. Praesent lectus.', '2021-11-10 13:32:00');
insert into messages (user_id, message, date_created) values (20, 'Cras mi pede, malesuada in, imperdiet et, commodo vulputate, justo.', '2021-10-02 05:49:52');
