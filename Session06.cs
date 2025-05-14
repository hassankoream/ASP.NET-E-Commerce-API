using System;

public class Class1
{
	public Class1()
	{
        #region Part 01 Order Module Overview

        /*
        
        
        Responsible for handling all aspects of the customer's purchase after they add products to their cart and proceed to checkout
        it will manage the complete ordering process, from capturing user delivery information to managing payment and delivery status.

        EndPoints:
        -Create Order 
        -Get Delivery Methods
        -Get Order For Specific User 
        -Get Orders For Specific User 


         
         
         */
        #endregion

        #region Part 02 Order Module Entities

        /*
         
        Order Is a Complex Object: an Object consist of many other objects
        Each object in the complex object has its own properties 

        The Order is a complex object that aggregates other components
        like Address, DeliveryMethod, OrderStatus, and a collection of OrderItem objects

         

        -Create class named ProductItemOrdered Inside Core/Domain/Models/OrderModule/
        -Create class named OrderItem Inside Core/Domain/Models/OrderModule/
        -Create class named DeliveryMethod Inside Core/Domain/Models/OrderModule/
        -Create class named OrderAddress Inside Core/Domain/Models/OrderModule/
        -Create enum named OrderStatus Inside Core/Domain/Models/OrderModule/
        
        -Create the main class named Order Inside Core/Domain/Models/OrderModule/

         
         */
        #endregion

        #region Part 03 Order Configurations

        /*
         
        -Go to Persistence/Data/DataSeeding/Delivery and remove ids
        -Create DeliveryMethodConfigurations.cs inside Persistence/Data/Configuration  to add the required configuration for the table in the database
        -Create OrderConfigurations.cs inside Persistence/Data/Configuration  to add the required configuration for the table in the database
        -Create OrderItemConfigurations inside Persistence/Data/Configuration  to add the required configuration for the table in the database


        --GO to package Manager console to add the Migration
        
        Add-Migration "OrderEntitiesMigration" -Context StoreDbContext

        Update-Database -Context StoreDbContext


        --DataSeed the delivery.json file inside the DataSeeding class

         */
        #endregion

        #region Part 04 Order Service [ Create Order ]

        /*
         
        -Create IOrderService inside the ServiceAnstactions project.
        -Create OrderDto and OrderToReturnDto folder inside the shared/DataTransferObjects/ folder
         
        -Create class OrderService inside the services projects
        -Create class OrderProfile inside the Services/MappingProfiles folder
        -We will not trust the front end to send the product details, so we should get these info from the Database, we need a deal with the products table
         
        -Create DeliveryNotFoundException
        -Map what was not mapped

         */
        #endregion

        #region Part 05 Order Profile [ Mapping ]

        /*
         
         
        -We need to map from AddressDto to OrderAddress
        -from Order to OrderToReturnDto 
        -from OrderItem to OrderItemDto
        -we need another Picture Resolver
         
         */
        #endregion

        #region Part 06 Order Controller [ Create Order ]

        /*
         
        -Go to presentation/controller and create the first Action
        -Go to Postman to test it out, you would need (basket, and token) 
         
         */
        #endregion



        #region Part 07 Order Controller [ Get All Delivery Methods - Get All Orders - Get Order By Id ]
        /*
         
         
        -Create all the required Endpoints, Add [Authorize] at top of the Controller itself.
        
         
         */
        #endregion
    }
}
