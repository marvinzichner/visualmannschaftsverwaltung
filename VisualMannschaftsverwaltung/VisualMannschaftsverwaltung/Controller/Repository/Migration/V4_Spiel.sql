alter table MVW_SPIEL add column SPIELTAG text after RESULT_B;
alter table MVW_SPIEL add column TURNIER_FK text after SPIELTAG;
alter table MVW_SPIEL add column SESSION_ID  text after TURNIER_FK;