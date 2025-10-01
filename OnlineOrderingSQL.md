# Online Ordering

## General instructions:
Please keep in mind that this exercise is intended to be achievable in a couple of hours.  Assume that this will be part of a larger system.  If there are larger considerations, that would have affected decisions of what is in/out of scope, please make note of your assumptions.  We use SQL server for our database for Nuix Discover and would like this to be written in T-SQL.  Any middle tier/UI code is out of scope. Assume any data sent from middle tier/UI will be sent the way you require. Likewise returned data can be done any way you like. Security and error handling are out of scope. Assume the front end is sending you good data.

## Problem statement:
The company you are working for is building an online ordering system. They are expecting millions of orders in this system so scale and performance is a concern. Your tasks will be limited to a small subset of the database code.

## User stories:

1.	Products:
    - As an owner I would like to store a list of products I have to sell in the data base along with their current cost
    - For brevity just create the table(s) with bare minimum information. Assume another developer is handling the CRUD operations on the table(s). For the sake of this exercise manually enter a couple of rows.
    - Required fields: Product name, and cost. Add others as needed.

2.	Customers: 
    - As an owner I would like to store a list of customers who buy my products and their current discount percentage.
    - For brevity just create the table(s) with bare minimum information. Assume another developer is handling the CRUD operations on the table(s). For the sake of this exercise manually enter a couple of rows.
    - Required fields: Customer name, and discount percentage. Add others as needed.

3.	Orders 
    - As an owner I would like to store my customers orders.
    - For brevity just create the table(s) with bare minimum information. You will be tasked will a portion of the CRUD operations for this table(s) below.
    - Required fields: Order number, customer, order date/time, item(s), cost(s), quantity, total cost (after discount). Add others as needed.
    - For brevity ignore taxes and shipping. 

4.	Add item to order 
    - As a buyer I would like items added to an existing order or create a new order if it’s the first item.
    - Assume the middle tier/ UI will send one product at a time as opposed to the whole order at once.
    - Add item/Create new order based on input
    - You are responsible for creating unique order number for new orders and returning it to UI
    - Required results/return: order number

5.	Delete item from order 
    - As a buyer I would like to delete an item from an existing order.
    - Delete item and update order accordingly.
    - Required results/return: none

6.	Update quantity on order 
    - As a buyer I would like to change the quantity of a product I ordered.
    - Update quantity and order accordingly.
    - Required results/return: none

7.	Read order 
    - As a buyer I would like to see an existing/previous order
    - Take order number and return order in the same order it was entered.
    - Required results/return: Order number, customer, order date/time, item(s), cost(s), quantity, total cost, discount percentage.

8.	Calculate discount 
    - As a owner, once a quarter I want to update the customers discount percentage based on the prior quarters purchases.
    - Using the following formula update customers discount percentage.
        - Discount is based on the previous quarter's purchases before discount.
            - $0 to $1,000.00 = 0%
            - $1,000.01 to $4,999.99 = 2%
            - $5,000.00 to $9,999.99 = 4%
            - $10,000.00 to $24,999.99 = 5%
            - $25,000.00 to $99,999.99 = 6%
            - $100000.00 and up = 7%
    - This discount formula can change quarter to quarter.
