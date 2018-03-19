# PhotoHub
> PhotoHub Site on ASP.NET MVC CORE 2 and Three Layer Architecture
## ASP.NET Core MVC and Three-Layer Architecture
![image about 3-layer](https://morecoding.files.wordpress.com/2015/01/3tier_2.jpg)
#### Data Layer

A DAL contains methods that helps the Business Layer to connect the data and perform required actions, whether to return data or to manipulate data (insert, update, delete and so on).
 
#### Business Layer

A BAL contains business logic, validations or calculations related to the data.

Though a web site could talk to the data access layer directly, it usually goes through another layer called the Business Layer. The Business Layer is vital in that it validates the input conditions before calling a method from the data layer. This ensures the data input is correct before proceeding, and can often ensure that the outputs are correct as well. This validation of input is called business rules, meaning the rules that the Business Layer uses to make “judgments” about the data.
 
#### Presentation Layer

The Presentation Layer contains pages like .aspx or Windows Forms forms where data is presented to the user or input is taken from the user. The ASP.NET web site or Windows Forms application (the UI for the project) is called the Presentation Layer. The Presentation Layer is the most important layer simply because it’s the one that everyone sees and uses. Even with a well structured business and data layer, if the Presentation Layer is designed poorly, this gives the users a poor view of the system.
## Vue JS 2 Front-End
![image about vue js](https://cdn.shopify.com/s/files/1/0533/2089/files/vuejs-tutorial_2d2a853c-aa2f-44b0-80df-933b495f77f8.png?v=1509478492)
