use master
go

Create database kpmSchoolDB
go

use kpmSchoolDB
Create TABLE Student(
	StudentID int primary key IDENTITY,
	StudentName varchar(50) NOT NULL,
	Address varchar(50) NOT NULL,
	Phone nvarchar(20) NOT NULL
	)
go

use kpmSchoolDB
Create TABLE ClassDetails(
	ClassDetailsID int IDENTITY NOT NULL,
	StudentID int references Student(StudentID),
	ClassName varchar(50) NOT NULL,
   SubjectName varchar(50) NOT NULL,
	AdmissionDate datetime NOT NULL,
	AdmissionFee decimal(10, 2) NOT NULL
	)
go

select * from Student
select * from ClassDetails