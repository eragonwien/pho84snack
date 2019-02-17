
use [PHO84SNACK];
GO

drop table if exists [MenuProduct], [Menu], [Product], [Category], [Image], [OpenHour], [Contact], [Restaurant];
drop table if exists [User], [Role];
GO

CREATE TABLE [Restaurant] (
    [Id] int NOT NULL IDENTITY,
	[Name] nvarchar(50) UNIQUE NOT NULL,
	[IsActive] bit NOT NULL DEFAULT 0,
	[WelcomeTitle] nvarchar(max),
    [WelcomeText] nvarchar(max),
    CONSTRAINT [PK_Restaurant] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Contact] (
    [Id] int NOT NULL IDENTITY,
	[RestaurantId] INT NOT NULL,
	[Address1] nvarchar(50),
	[Address2] nvarchar(50),
	[PLZ] nvarchar(30),
	[City] nvarchar(30),
    [Phone] nvarchar(20),
    [Facebook] nvarchar(50),
    [Email] nvarchar(50),
	[IsActive] bit NOT NULL DEFAULT 0,
    CONSTRAINT [PK_Contact] PRIMARY KEY ([Id]),
	CONSTRAINT [FK_Contact_Restaurant_RestaurantId] FOREIGN KEY ([RestaurantId]) REFERENCES [Restaurant] ([Id])
);
GO

CREATE TABLE [OpenHour] (
    [Id] int NOT NULL IDENTITY,
	[ContactId] INT NOT NULL,
    [Day] nvarchar(10),
	[IsOpen] bit,
    [Open] nvarchar(5),
    [Close] nvarchar(5),
    CONSTRAINT [PK_OpenHour] PRIMARY KEY ([Id]),
	CONSTRAINT [FK_OpenHour_Contact_ContactId] FOREIGN KEY ([ContactId]) REFERENCES [Contact] ([Id])
);
GO

CREATE TABLE [Role] (
    [Id] int NOT NULL IDENTITY,
    [Title] nvarchar(30),
	[IsActive] bit NOT NULL DEFAULT 0,
    [Description] nvarchar(max),
    CONSTRAINT [PK_Role] PRIMARY KEY ([Id])
);
GO


CREATE TABLE [User] (
    [Id] int NOT NULL IDENTITY,
    [Email] nvarchar(50) UNIQUE NOT NULL,
	[Name] nvarchar(max) NOT NULL,
    [Password] nvarchar(max) NOT NULL,
    [Description] nvarchar(max),
    [ExtraInfo] nvarchar(max),
    [IsActive] bit NOT NULL DEFAULT 0,
    [RoleId] int NOT NULL,
    CONSTRAINT [PK_User] PRIMARY KEY ([Id]),
	CONSTRAINT [FK_User_Role_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [Role] ([Id])
);
GO

CREATE TABLE [Image] (
    [Id] int NOT NULL IDENTITY,
	[Data] nvarchar(max),
	[Name] nvarchar(max),
	[MimeType] nvarchar(64),
	[IsActive] bit NOT NULL DEFAULT 0,
    CONSTRAINT [PK_Image] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Category] (
    [Id] int NOT NULL IDENTITY,
    [RestaurantId] int,
	[ImageId] int,
	[Name] nvarchar(50),
	[IsActive] bit NOT NULL DEFAULT 0,
    CONSTRAINT [PK_Category] PRIMARY KEY ([Id]),
	CONSTRAINT [FK_Category_Restaurant_RestaurantId] FOREIGN KEY ([RestaurantId]) REFERENCES [Restaurant] ([Id]),
	CONSTRAINT [FK_Category_Image_ImageId] FOREIGN KEY ([ImageId]) REFERENCES [Image] ([Id])
);
GO

CREATE TABLE [Product] (
    [Id] int NOT NULL IDENTITY,
	[CategoryId] int NOT NULL,
	[ImageId] int,
    [Name] nvarchar(50),
    [Description] nvarchar(max),
    [Price] money NOT NULL,
    [Currency] nvarchar(3) NOT NULL,
    [IsFeatured] bit,
	[IsActive] bit NOT NULL DEFAULT 0,
    CONSTRAINT [PK_Product] PRIMARY KEY ([Id]),
	CONSTRAINT [FK_Product_Category_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Category] ([Id]),
	CONSTRAINT [FK_Product_Image_ImageId] FOREIGN KEY ([ImageId]) REFERENCES [Image] ([Id])
);
GO

CREATE TABLE [Menu] (
    [Id] int NOT NULL IDENTITY,
	[RestaurantId] int NOT NULL,
	[ImageId] int,
    [Name] nvarchar(50),
    [Description] nvarchar(max),
    [Price] money NOT NULL,
    [Currency] nvarchar(3) NOT NULL,
    [IsFeatured] bit NOT NULL DEFAULT 0,
	[IsActive] bit NOT NULL DEFAULT 0,
    CONSTRAINT [PK_Menu] PRIMARY KEY ([Id]),
	CONSTRAINT [FK_Menu_Restaurant_RestaurantId] FOREIGN KEY ([RestaurantId]) REFERENCES [Restaurant] ([Id]),
	CONSTRAINT [FK_Menu_Image_ImageId] FOREIGN KEY ([ImageId]) REFERENCES [Image] ([Id])
);
GO

CREATE TABLE [MenuProduct] (
	[MenuId] int NOT NULL,
	[ProductId] int NOT NULL,
    [Name] nvarchar(50),
    [Description] nvarchar(max),
    [Price] money NOT NULL,
    [Currency] nvarchar(3) NOT NULL,
	[IsActive] bit NOT NULL  DEFAULT 0,
    CONSTRAINT [PK_MenuProducts] PRIMARY KEY ([MenuId], [ProductId]),
	CONSTRAINT [FK_MenuProducts_Product] FOREIGN KEY ([ProductId]) REFERENCES [Product] ([Id]),
	CONSTRAINT [FK_MenuProducts_Menu] FOREIGN KEY ([MenuId]) REFERENCES [Menu] ([Id])
);
GO




