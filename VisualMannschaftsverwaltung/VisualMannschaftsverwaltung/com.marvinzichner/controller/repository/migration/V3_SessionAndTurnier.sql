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