alter table MVW_SPIEL add column SPIELTAG text after RESULT_B;
alter table MVW_SPIEL add column TURNIER_FK text after SPIELTAG;
alter table MVW_SPIEL add column SESSION_ID  text after TURNIER_FK;

create table MVW_AUTH (
	ID int auto_increment not null,
    usernamePlain text,
    passwordPlain text,
    role text,
    primary key (ID)
);

insert into MVW_AUTH (usernamePlain, passwordPlain, role) values ('mustermann.admin', 'password', 'ADMIN');
insert into MVW_AUTH (usernamePlain, passwordPlain, role) values ('mustermann', '1234', 'USER');

-- insert into MVW_AUTH (usernamePlain, passwordPlain, role) values ('fcbla.admin', 'sonnenblume', 'ADMIN');
-- insert into MVW_AUTH (usernamePlain, passwordPlain, role) values ('fcbla', 'simplepass', 'USER');