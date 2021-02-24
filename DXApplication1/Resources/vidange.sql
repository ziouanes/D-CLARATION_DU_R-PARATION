select * from odm where  [KILOMÉTRAGE] = (select MAX([KILOMÉTRAGE]) from odm where  [VEHICULE] =44 and  [KILOMÉTRAGE]  NOT like '[^0-9]')



select MAX([KILOMÉTRAGE]) from odm where [VEHICULE] =44 and  [KILOMÉTRAGE]  NOT like '%[^0-9]%'


select * from odm where [VEHICULE] =44 and  [KILOMÉTRAGE]  NOT like '%[^0-9]%'




SELECT [KILOMÉTRAGE] from odm WHERE [VEHICULE]  =44

drop trigger t1

create Trigger t1

On [ODM] 
After insert , update
As
Begin
		Declare @date date,
				@vehicule int,
				@kilometrage int,
				@first_kilo int,
				@taux int
				


 set @vehicule  = (select top 1 [VEHICULE] from inserted)
 set @date  = (select [DATE] from odm where  [KILOMÉTRAGE] = (select MAX([KILOMÉTRAGE]) from odm where  [VEHICULE] =@vehicule and  [KILOMÉTRAGE]  NOT like '%[^0-9]%'  and year([DATE])  = year(getdate()) ) )
 set @kilometrage  = (select [KILOMÉTRAGE] from odm where  [KILOMÉTRAGE] = (select MAX([KILOMÉTRAGE]) from odm where  [VEHICULE] =@vehicule and  [KILOMÉTRAGE]  NOT like '%[^0-9]%' and year([DATE])  = year(getdate()) ) )
 --set @first_kilo  = (select [first_kilometrage] from [Reaparation] where  [first_kilometrage] = (select MAX([first_kilometrage]) from [Reaparation] where  [vehecule] = @vehicule and  [first_kilometrage]  NOT like '[^0-9]'))
 

	 If Exists (select [km_V] from [dbo].[vehicules] 
	 where [km_V]  NOT like '%[^0-9]%' and id = @vehicule )
	 Begin
	 set @first_kilo  = (select [km_V] from [dbo].[vehicules] where [km_V]  NOT like '%[^0-9]%' and id = @vehicule )

	 set @taux  = ((@kilometrage-@first_kilo)*100)/10000
	 end
	 else
	 begin
	  set @taux = 0
	 end

	if Exists (select  [VEHICULE] from [NOUVEAU] where [VEHICULE] = @vehicule )

begin

update [NOUVEAU] set [DATE]  = @date  , [KILOMÉTRAGE]  = @kilometrage  , [Taux]  = @taux  where [VEHICULE] = @vehicule

end

else 

 insert into [NOUVEAU] values (@date,@vehicule,@kilometrage,@taux)

 END
 

 	select [first_kilometrage] from [Reaparation] where  [first_kilometrage] = (select MAX([first_kilometrage]) from [Reaparation] where  [vehecule] = 49 and  [first_kilometrage]  NOT like '%[^0-9]%')
	
--not_work

alter Trigger t2

On  [Reaparation]

After insert , update
As
Begin
		Declare @date_v date,
				@vehicule_v int,
				@kilometrage_v int,
				@vidange int
		
 set @vehicule_v  = (select top 1 [vehecule] from inserted)
 set @date_v  = (select max([_date]) from [Reaparation] where [vehecule]=@vehicule_v)
 --spicifique vidange
 set @kilometrage_v =(select [first_kilometrage] from [Reaparation]  where [_date]= (select max( [_date])from [Reaparation] where  [vehecule]=@vehicule_v ))

 if(Exists (select id from [dbo].[descriptions] where [id_Reaparation]  = (select id from inserted ) and [_description] like '%vidange%'))
 
 set @vidange = 1
 
 else
 set @vidange = 0

 if  (@vidange = 1)
 
 update [vehicules] 
 set [km_V]= @kilometrage_v , [date_V] = @date_v
 where [id] = @vehicule_v 
 



SELECT n.id as id ,v.Marque as Marque,v.matricule as Immatriculation , v.id as id_v , n.[KILOMÉTRAGE] as KILOMÉTRAGE_V , n.[DATE] as Date_V , n.[Taux] as Taux from vehicules v inner join [NOUVEAU] n on n.[VEHICULE] = v.id where v.id in (select distinct id from vehicules)
SELECT n.id  , v.id from vehicules v inner join [NOUVEAU] n on n.[VEHICULE] = v.id where  n.[DATE] = (select max([DATE])  from NOUVEAU)



--big brain here

select id ,Marque,Immatriculation  ,id_v,  KILOMÉTRAGE_V ,  Date_V ,  Taux  
from (select  n.id as id ,v.Marque as Marque,v.matricule as Immatriculation , v.id as id_v , n.[KILOMÉTRAGE] as KILOMÉTRAGE_V , n.[DATE] as Date_V , n.[Taux] as Taux,
             row_number() over(partition by v.id  order by n.[DATE]  desc) as rn
      from  vehicules v inner join [NOUVEAU] n on n.[VEHICULE] = v.id) as T
where rn = 1  ;
select n.id as id ,v.Marque as Marque,v.matricule as Immatriculation , v.id as id_v , n.[KILOMÉTRAGE] as KILOMÉTRAGE_V , n.[DATE] as Date_V , n.[Taux] as Taux from vehicules v inner join [NOUVEAU] n on n.[VEHICULE] = v.id

--big brain here

select * from [dbo].[Reaparation]
select r.id ,r.[nom] as 'Nom',r.[Carburant] as Carburant ,r.[first_kilometrage] as kilometrage,v.Marque as Marque,v.matricule as Immatriculation , v.id as id_vehecule ,r.[_date] as 'Date' from [Reaparation] r inner join vehicules v on r.vehecule = v.id


create table [descriptions]([id] int IDENTITY(1,1) primary key ,[_description] varchar(30),[id_Reaparation] int foreign key references [Reaparation](id) on delete cascade on update cascade)