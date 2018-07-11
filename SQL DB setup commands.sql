--CREATE DATABASE Project1DB;

--CREATE SCHEMA PizzaShop;

--DROP TABLE PizzaShop.Users
CREATE TABLE PizzaShop.Users
(
	Username nvarchar(128) PRIMARY KEY,
	FirstName nvarchar(128),
	LastName nvarchar(128),
	Email nvarchar(128),
	Phone nvarchar(128),
);


--DROP TABLE PizzaShop.SizingPricing
CREATE TABLE PizzaShop.SizingPricing
(
	Size nvarchar(128) PRIMARY KEY,
	BasePrice money,
	ToppingPrice money,
	IngredientUsageScalar int
);

--DROP TABLE PizzaShop.Locations
CREATE TABLE PizzaShop.Locations
(
	Name nvarchar(128)PRIMARY KEY 
)

--DROP TABLE PizzaShop.Orders
CREATE TABLE PizzaShop.Orders
(
	ID int PRIMARY KEY,
	Timestamp datetime,
	LocationID nvarchar(128) FOREIGN KEY REFERENCES PizzaShop.Locations(Name),
	UserID nvarchar(128) FOREIGN KEY REFERENCES PizzaShop.Users(Username),
	Price money
)

ALTER TABLE PizzaShop.Users
ADD FavLocation nvarchar(128) FOREIGN KEY REFERENCES PizzaShop.Locations(Name);

--DROP TABLE PizzaShop.Pizzas
CREATE TABLE PizzaShop.Pizzas
(
	ID int PRIMARY KEY IDENTITY,
	SizeID nvarchar(128) FOREIGN KEY REFERENCES PizzaShop.SizingPricing(Size),
	Price money
);

--DROP TABLE PizzaShop.OrderPizzaJunction
CREATE TABLE PizzaShop.OrderPizzaJunction
(
	PizzaID int FOREIGN KEY REFERENCES PizzaShop.Pizzas(ID),
	Quantity int CHECK (Quantity > 0),
	OrderID int FOREIGN KEY REFERENCES PizzaShop.Orders(ID),
	PRIMARY KEY (PizzaID, OrderID)
)

--DROP TABLE PizzaShop.Ingredients
CREATE TABLE PizzaShop.Ingredients
(
	Name nvarchar(128) PRIMARY KEY,
	Type nvarchar(128) CHECK (Type='crust' OR Type='sauce' OR Type='topping'),
)
ALTER TABLE PizzaShop.Ingredients
DROP COLUMN Quantity;


CREATE TABLE PizzaShop.PizzaIngredientJunction
(
	PizzaID int FOREIGN KEY REFERENCES PizzaShop.Pizzas(ID),
	IngredientID nvarchar(128) FOREIGN KEY REFERENCES PizzaShop.Ingredients(Name),
	PRIMARY KEY (PizzaID, IngredientID)
)

CREATE TABLE PizzaShop.LocationIngredientJunction
( 
	LocationID nvarchar(128) FOREIGN KEY REFERENCES PizzaShop.Locations(Name),
	Quantity int CHECK (Quantity > 0),
	IngredientID nvarchar(128) FOREIGN KEY REFERENCES PizzaShop.Ingredients(Name),
	PRIMARY KEY (LocationID, IngredientID)
)
DELETE FROM PizzaShop.Users;
INSERT INTO PizzaShop.Users
VALUES ('test', 'First', 'Last', 'a@a.com', '1234567890', 'Herndon'),
		('test2', 'Tess', 'Est', 'winner@gmail.com', '9307452132', 'Reston');

SELECT * FROM PizzaShop.Users;

INSERT INTO PizzaShop.Locations
VALUES ('Reston'), ('Herndon')

SELECT * FROM PizzaShop.Locations;

INSERT INTO PizzaShop.SizingPricing
VALUES ('small', 5, .5, 1), ('medium', 7.5, .75, 2), ('large', 10, 1, 3), ('party-sized', 40, 4, 4);

SELECT * FROM PizzaShop.SizingPricing Order By (IngredientUsageScalar);


INSERT INTO PizzaShop.Ingredients
VALUES ('classic crust', 'crust'), ('thin crust', 'crust'), ('deep dish crust', 'crust')

INSERT INTO PizzaShop.Ingredients
VALUES ('classic sauce', 'sauce'), ('garlic white sauce', 'sauce'), ('bbq sauce', 'sauce')

INSERT INTO PizzaShop.Ingredients
VALUES ('cheese', 'topping'), ('sausage', 'topping'), ('pepperoni', 'topping'), ('peppers', 'topping'), ('onions', 'topping')

SELECT * FROM PizzaShop.Ingredients Order By (Type)


INSERT INTO PizzaShop.LocationIngredientJunction
VALUES ('Reston', 20, 'thin crust'), ('Reston', 15, 'cheese'), ('Reston', 10, 'sausage'), ('Reston', 20, 'classic sauce');

INSERT INTO PizzaShop.LocationIngredientJunction
VALUES ('Herndon', 5, 'thin crust'), ('Herndon', 5, 'deep dish crust'),('Herndon', 15, 'cheese'), ('Herndon', 10, 'pepperoni'), ('Herndon', 20, 'bbq sauce');

INSERT INTO PizzaShop.LocationIngredientJunction
VALUES ('Reston', 20, 'classic crust');



SELECT * FROM PizzaShop.LocationIngredientJunction;


SELECT * FROM PizzaShop.Orders;

DELETE FROM PizzaShop.Orders WHERE ID = -1332610449;

