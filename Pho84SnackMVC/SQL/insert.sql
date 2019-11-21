use pho84snack;

insert into companyinfo(name, description, address, zip, city, phone, email, facebook) values('Test Company', 'Test description', 'Test street 21', '1212', 'Test city', '+123456789', 'test@pho84snack.at', 'fb@pho84snack.at');
insert into category(name, description) values('Test category', 'Test description category');
insert into product(name, description) values('Test product', 'Test description product');
insert into productmap(categoryid, productid) values((select id from category where name='Test category'), (select id from product where name='Test product'));

insert into size(shortname, longname) values('S', 'Small');
insert into size(shortname, longname) values('M', 'Medium');
insert into size(shortname, longname) values('L', 'Large');
insert into size(shortname, longname) values('R', 'Regular');