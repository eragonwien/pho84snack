CREATE VIEW `v_category` AS
select 
	c.id,
    c.name,
    c.description,
    p.id as productid,
    p.name as productname,
    p.description as productdescription
from category c 
left join productmap m on c.id=m.categoryid 
left join product p on m.productid=p.id