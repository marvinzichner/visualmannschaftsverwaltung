create table MVW_MIGRATION (
    ID int auto_increment not null,
    VERSION int,
    NAME text,
    CREATED timestamp,
    primary key (ID)
);

create table MVW_MANNSCHAFT (
	ID int auto_increment not null,
	NAME text,
	TYP text,
	GEWONNENE_SPIELE int not null,
	GESAMTE_SPIELE int not null,
	primary key (id)
);

create table MVW_PERSON (
  ID int auto_increment not null,
  VORNAME text not null,
  NACHNAME text not null,
  GEBURTSDATUM date not null,
  primary key (ID)
);

create table MVW_MANNSCHAFT_PERSON (
    ID int auto_increment not null,
    FK_PERSON int not null,
    FK_MANNSCHAFT int not null,
    primary key (ID)
);

create table MVW_SPIEL (
  ID int auto_increment not null,
  TITEL text not null,
  MANNSCHAFT_A_FK int not null,
  MANNSCHAFT_B_FK int not null,
  RESULT_A int,
  RESULT_B int,
  primary key (ID),
  foreign key (MANNSCHAFT_A_FK) references MVW_MANNSCHAFT(ID),
  foreign key (MANNSCHAFT_B_FK) references MVW_MANNSCHAFT(ID)
);

create table MVW_SPIELERROLLE (
  ID int auto_increment not null,
  NAME text not null,
  primary key (ID)
);

create table MVW_FUSSBALLSPIELER (
  ID int auto_increment not null,
  PERSON_FK int not null,
  SPIELERROLLE_FK int not null,
  GEWONNENE_SPIELE int not null,
  LEFT_FOOT int not null,
  primary key (ID),
  foreign key (PERSON_FK) references MVW_PERSON(ID)
);

create table MVW_HANDBALLSPIELER (
  ID int auto_increment not null,
  PERSON_FK int not null,
  SPIELERROLLE_FK int not null,
  GEWONNENE_SPIELE int not null,
  LEFT_HAND int not null,
  primary key (ID),
  foreign key (PERSON_FK) references MVW_PERSON(ID),
  foreign key (SPIELERROLLE_FK) references MVW_SPIELERROLLE(ID)
);

create table MVW_TENNISSPIELER (
  ID int auto_increment not null,
  PERSON_FK int not null,
  SPIELERROLLE_FK int not null,
  GEWONNENE_SPIELE int not null,
  LEFT_ARM int not null,
  primary key (ID),
  foreign key (PERSON_FK) references MVW_PERSON(ID),
  foreign key (SPIELERROLLE_FK) references MVW_SPIELERROLLE(ID)
);

create table MVW_TRAINER (
  ID int auto_increment not null,
  PERSON_FK int not null,
  SPIELERROLLE_FK int not null,
  GEWONNENE_SPIELE int not null,
  HAS_LICENSE int not null,
  primary key (ID),
  foreign key (PERSON_FK) references MVW_PERSON(ID),
  foreign key (SPIELERROLLE_FK) references MVW_SPIELERROLLE(ID)
);

create table MVW_PHYSIOTHERAPEUT (
  ID int auto_increment not null,
  PERSON_FK int not null,
  SPIELERROLLE_FK int not null,
  HAS_LICENSE int not null,
  primary key (ID),
  foreign key (PERSON_FK) references MVW_PERSON(ID),
  foreign key (SPIELERROLLE_FK) references MVW_SPIELERROLLE(ID)
);

insert into MVW_SPIELERROLLE (name) values ('FussballSpieler');
insert into MVW_SPIELERROLLE (name) values ('TennisSpieler');
insert into MVW_SPIELERROLLE (name) values ('HandballSpieler');
insert into MVW_SPIELERROLLE (name) values ('Trainer');
insert into MVW_SPIELERROLLE (name) values ('Physiotherapeut');