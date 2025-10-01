# Investment Performance Web API

## General instructions:
Please fork this project and submit a pull request when completed.  You should submit a working piece of software that is tested and can be run.  We will review your pull request and execute your code, please provide instructions on how to do so.

Please keep in mind that this exercise is intended to be achievable in a couple of hours.  We expect a production ready submission that demonstrates not only the code you write but quality controls such as exception handling, logging, unit tests, etc.  Assume that this api will be part of a larger system.  If there are larger considerations, that would have affected decisions of what is in/out of scope, please make note of your assumptions.  The majority of the tech stack in use at Nuix Discover is SQLServer, C#.NET, ExtJs, Vue.js.  We prefer the use of that stack, but we want you to showcase your abilities.  If you don't know those specific technologies, you can substitute.



## Problem statement
The company you are working for is building an investment trading platform for stock, bond and mutual funds.  The platform will have various functionality to buy, sell, deposit, withdrawal, and report on investments.  For this part of the product, they need you to create a web api to return data for investment performance.  The type of api to create is your decision, but it must be hosted on a webserver and accessed via web request.

## Problem details/user stories:
- ### Get a list of current investments for the user 

    As an API user, I want to be able to query the list of investments for a user.  The query should return the investment id and name.

- ### Get details for a user's investment: 

    As an API user, I want to be able to query the details of a specific investment for a user.  The query should return number of shares, cost basis per share, current value, current price, term, and total gain/loss.

#### Definitions:

- Cost basis per share: this is the price of 1 share of stock at the time it was purchased

- Current value: this is the number of shares multiplied by the current price per share

- Current price: this is the current price of 1 share of the stock

- Term: this is how long the stock has been owned.  <=1 year is short term, >1 year is long term

- Total gain or loss: this is the difference between the current value, and the amount paid for all shares when they were purchased



