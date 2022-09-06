CREATE SCHEMA mapping;

DROP TABLE IF EXISTS mapping.items;
CREATE TABLE mapping.items
(Id bigint IDENTITY (1,1) NOT NULL PRIMARY KEY,
LinkCode varchar(255) NOT NULL,
SourceValue varchar(1000) NOT NULL,
TargetValue varchar(1000) NOT NULL);