create table MVW_SESSIONAUTH (
    ID int auto_increment not null,
    USERNAME text,
    PASSWORD text,
    SESSIONID text,
	ACTIVE int,
	APPLICATION_ROLE text,
    primary key (ID)
);

alter table MVW_MANNSCHAFT add column SESSION_ID text after GESAMTE_SPIELE;

create table MVW_TURNIER (
    ID int auto_increment not null,
    NAME text,
    TYPE text,
    TURNIERART text,
	SESSION_ID text,
    primary key (ID)
);

create table MVW_MANNSCHAFT_TURNIER (
    ID int auto_increment not null,
    MANNSCHAFT_ID int,
    TURNIER_ID int,
    primary key (ID)
);

insert into MVW_TURNIER (NAME, TYPE, TURNIERART, SESSION_ID) 
	values ('Mock Data Cup 2020', 'FUSSBALL', 'EGAL', 'ALL');

insert into MVW_MANNSCHAFT_TURNIER (MANNSCHAFT_ID, TURNIER_ID) 
	values (1, 1);