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