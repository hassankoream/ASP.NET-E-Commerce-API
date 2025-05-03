using System;

public class Class1
{
    public Class1()
    {
        #region Part 01 Revision

        /*
         
        Using Specification Design pattern in order to make our query Dynamic:

        -- Create ISpecification Interface and added our first parts of the query which is (Criteria and IncludeExpressions)
        -- Create BaseSpecification class in order to Implement the interface and to make any class that inherit from it and the properties needed for that  .
        -- Before we send the query to the database we need to update it in the IGenericRepository and GenericRepository, or send it with the required Specification to get the objects we need.. 
        
        --Create SpecificationsEvaluator as a helper class inside the same project beside the repositories folder.
        --Add new method in SpecificationsEvaluator to help us Create the query needed.

        --For each Entity or controller we need to ask if the query need to b updated with more specification from the user.
        --So we needed to create a new class for each time we need something new(new query) from the Products(ProductWithBrndAndTypeSpecifications,ProductCountSpecification) and both of them inherit from BaseSpecification
        --Update ProductService in order to have the new methods that have the Specification parameter.
        --Created ProductQuryParams to get more then three params from the user when he call the controller
        
        
        --Pagination need more parts in our creation of the query, so we added three more Properties in ISpecification.
        --Update BaseSpecification with the new Properties.
        --Update SpecificationsEvaluator with the new Properties.

        --I need Count to send it to the user to know how many products we have, so he can apply pagination settings.
        --So we added more class called ProductCountSpecification, having Only the Criteria, why? Criteria minimize the count of requested products. so NO need to add the other parts of the query



         */
        #endregion

        #region Part 02 Postman
        /*
         
        -open Postman
        -Workspace > MyWorkSpace
        -the plus sign > create new blank collection and name it
        - Add new  Folder named after the Module (products, Employees, so on)
        - Add new request inside the folder
        - Add your variables inside the collection
        - click right on the three dots and choose more to export collection
        - click on import to import new collection from someone else.
        variables Inside Environment are global in the workspace
        variables Inside Collection are local in the collection 



         
         */
        #endregion

        #region Part 03 Custom Exception Middleware

        /*
         
        ++Handling Server Error
        500 Internal Server Error: in the BackEnd or the server or the SQL service
        Go to Program and a costume Middleware that



        Inside your main web app:
        -create a folder called CustomMiddleWares and create CustomExceptionHandlerMiddleWare class.
        -Create Folder in the shared project called ErrorModels
        -Create class called ErrorToReturn inside the ErrorModels folder
        -Add the Custom Middleware in the main program function
         

         
         */
        #endregion


        #region Part 04 Result Not Found

        /*
         
        In order to return a new response with Not FOund product:
        -Go to Core/Domain/Exceptions and Create abstract class NotFoundException that inherit from Exception
        -Create another sealed class for the ProductNotFoundException that inherit from NotFoundException
        -Go to Product service and check if product is null to return a new Exception from ProductNotFoundException.
        -Go to CustomExceptionHandlerMiddleWare and fix the status Code that will be return.
         
         
         */

        #endregion
        #region Part 05 Not Found EndPoint

        /*
         
        Not Found EndPoint: is not Exception 
        -Inside the CustomExceptionHandlerMiddleWare, in the try edit the response to have the new error.
        -to make it more cleaner you can Select the code, Ctrl + dot, and Extract Method.

         
         
         */

        #endregion
        #region Part 06 Validation Error Response

        /*
         
        -Create 2 Error Models for validation in the shared folder(ValidationError, ValidationErrorToReturn).
        -Go to the program Class, and start to Configure the Service (ApiBehaviorOptions)
        -Test in PostMan
        -Clean: Create a helper class having a method to generate the error response
        in E-Commerce.Web/Factories Create ApiResponseFactory

         
         
         */

        #endregion
        #region Part 07 Refactor Program Class

        /*
        
        Fixing program class require steps to do:
        Start with Services registrations, ignore Domain layer
        -Create ApplicationServicesRegistration static class in the services project
        -remove the Registration of (AddAutoMapper, IServiceManager) from main function in the program class.
        - Add the new way of the registration.
        -Create InfrastructureServicesRegistration static class  in the persistence project.
        -Remove the Registration of (DataSeeding, UnitOfWork, StoreDbContext) from main function in the program class.
        - Add the new way of the registration.
        -Create a GlobalUsing Class to add the global using of the NameSpaces 
        -Using this package: using Microsoft.Extensions.Configuration; no need to add it, you have a reference form Services project
        - Create Extensions folder inside the Web app and Create ServiceRegistration static class
        - Add methods for services like (Swagger, ApiBehavior)
        - Remove the Registration of (Swagger, ApiBehavior) from main function in the program class. 
        - Add the new way of the registration.
        - Add new static class called WebApplicationRegistration.
        - Remove the Registration of (DataSeeding) from main function in the program class. 
        - Add the new way of the registration.
        - Add new method in the WebApplicationRegistration to replace CustomExceptionMiddleWare.
        - Remove the Registration of (CustomExceptionMiddleWare) from main function in the program class. 
        - Add new method in the WebApplicationRegistration to replace SwaggerUI.
        - Remove the Registration of (SwaggerUI) from main function in the program class. 
        


         
         */
        #endregion
        #region Part 08 Project Business

        /*
         
        Project is a web application to allow customers to search for products and buy them.
        
        -We will design the Product Module for (Listing products, and show them, and make Crud Operations on them).
        -We will design the Basket Module to allow customers to buy products.
         
         
         */

        #endregion
        #region Part 09 Basket Module - Entities
        /*
         
        -Create ProductModule folder inside Core/Domain/Models that has the entities related to the product.
        -Create basketModule folder to have the required classes for the Basket.
        -Create CustomerBasket class and BasketItem class as Entities stored in the Memory not in the database.

         
         
         */
        #endregion
        #region Part 10 Redis In Memory Database
        /*
         
        In Memory Database: Storing web app Data in the RAM.
        using Redis Service is a good approach for saving.
        Install Redis following this article: 
        https://redis.io/blog/install-redis-windows-11/

        --Install Redis Insight from windows store.
        --Add Redis Database and see if there is a way to test the connection.

         
         */
        #endregion
        #region Part 11 Basket Module - Repository
        /*
         
        -Create Interface IBasketRepository inside the Contracts folder.
        -Create Class BasketRepository Implement the interface IBasketRepository inside the Persistence/Repositories folder.
        -Install StackExchange.Redis package inside the persistence project to use IConnectionMultiplexer, and IDatabase.
        -Implement the interface for the crud operations on the baskeModule.
        -Register the service inside the Persistence/InfrastructureServicesRegistration
         -Add RedisConnection in the appsettings.json and register the connection in the InfrastructureServicesRegistration
         
         */
        #endregion
        #region Part 12 Basket Module - Service


        /*
        
        -Create BasketModuleDtos inside the shared folder
        -Create IBasketService Inside the ServiceAbstractions
        -Create BasketService inside the service project Implement the interface IBasketService
        -Create BasketProfile inside Services/MappingProflies to map the classes.
        -Create BasketNotFoundException Inside the Exceptions folder in the domain layer to use it in the BasketService
        -
         
         */
        #endregion
        #region Part 13 Basket Module - Controller

        /*
         
        -Create BasketController inherit from Controller base in the Infrastructure/presentation project/Controllers
        - Add IbasketService as a property inside the IServicemanager
        - Add the Implementation of the new property inside the ServiceManager  
         
         
         */
        #endregion


    }
