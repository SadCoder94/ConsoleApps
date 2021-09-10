select top 10 * from DemoData

insert into DemoData values (3,'somedata','somedatalabel')

create procedure dbo.Getdata
@id int 
as 
begin
	select * from DemoData where Id = @id
end

exec GetData 1

alter table demodata add [DID] int

ALTER TABLE DemoData
   ADD CONSTRAINT FK_DID FOREIGN KEY (DID)
      REFERENCES DemoTable2 (ID)
      ON DELETE CASCADE
      ON UPDATE CASCADE

create table Standard(
    StandardId int primary key,
    StandardName varchar(20)
    )

create table Student(
    StudentId int primary key,
    StudentNameName varchar(20),
    StandardRefId int Foreign Key references Standard
    )