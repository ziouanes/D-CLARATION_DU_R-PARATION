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
 set @date_v  = (select TOP 1 [_date] from inserted )
 --spicifique vidange
 set @kilometrage_v =(select  top 1 [first_kilometrage] from inserted  )

 if(Exists (select id from [dbo].[descriptions] where [id_Reaparation]  = (select id from inserted ) and [_description] like '%nge%'))
 begin
 set @vidange = 1
 end
 else
 begin
 set @vidange = 0
 end
 if  (@vidange = 1)
 
 update [vehicules] 
 set [km_V]= @kilometrage_v , [date_V] = @date_v
 where [id] = @vehicule_v 
 UPDATE [NOUVEAU] 
 SET [Taux] = 0 
 where [VEHICULE] = @vehicule_v
 
 end

 create Trigger  t3
 on [vehicules] 
 after insert 
 as begin
Declare @vehicule int
set @vehicule =  (select top 1 [id] from inserted)
update  [vehicules]  set [km_V] = 0 , [date_V]  =  GETDATE() where [id] = @vehicule

end



