use pho84snack;

drop table if exists ProductSizeMap, ProductSize, ProductMap, Product, Category, CompanyInfo;

create table CompanyInfo (
	Id int not null auto_increment,
    Name varchar(32) unique not null,
    Description varchar(256),
    Address varchar(64),
    AddressExtra varchar(64),
    Zip varchar(32),
    City varchar(32),
    Phone varchar(16),
    Email varchar(32),
    Facebook varchar(32),
    primary key (Id)
);

create table Category (
	Id int not null auto_increment,
    Name varchar(32) unique not null,
    Description varchar(256),
    primary key (Id)
);

create table Product (
	Id int not null auto_increment,
    Name varchar(32) unique not null,
    Description varchar(256),
    primary key (Id)
);

create table ProductMap (
	Id int not null auto_increment,
    CategoryId int not null,
    ProductId int not null,
    primary key (Id),
    foreign key (CategoryId) references Category(Id),
    foreign key (ProductId) references Product(Id),
    constraint Unique_Map unique (CategoryId, ProductId)
);

create table ProductSize (
	Id int not null auto_increment,
    ShortName varchar(2) unique not null,
    LongName varchar(16),
    primary key (Id)
);

create table ProductSizeMap (
	Id int not null auto_increment,
    ProductSizeId int not null,
    ProductId int not null,
    Price decimal,
    primary key (Id),
    foreign key (ProductSizeId) references ProductSize(Id),
    foreign key (ProductId) references Product(Id),
    constraint Unique_Map unique (ProductSizeId, ProductId)
);