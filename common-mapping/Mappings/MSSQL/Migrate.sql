CREATE SCHEMA mapping;

DROP TABLE IF EXISTS mapping.types;
CREATE TABLE mapping.types
(Id int IDENTITY (1,1) NOT NULL PRIMARY KEY,
Name varchar(50) NOT NULL,
Description Text NOT NULL);

DROP TABLE IF EXISTS mapping.links;
CREATE TABLE mapping.links
(Id int IDENTITY (1,1) NOT NULL PRIMARY KEY,
SourceId int NOT NULL,
TargetId int NOT NULL);

ALTER TABLE mapping.links 
ADD CONSTRAINT fk_mappinglinks_types
FOREIGN KEY(SourceId)
REFERENCES mapping.types(Id);
ALTER TABLE mapping.links 
ADD CONSTRAINT fk_mappinglinks_types2
FOREIGN KEY(TargetId)
REFERENCES mapping.types(Id);

DROP TABLE IF EXISTS mapping.items;
CREATE TABLE mapping.items
(Id bigint IDENTITY (1,1) NOT NULL PRIMARY KEY,
LinkId int NOT NULL,
SourceValue varchar(1000) NOT NULL,
TargetValue varchar(1000) NOT NULL);

ALTER TABLE mapping.items 
ADD CONSTRAINT fk_mappingitems
FOREIGN KEY(LinkId)
REFERENCES mapping.links(Id);