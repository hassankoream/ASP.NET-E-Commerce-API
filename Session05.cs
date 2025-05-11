using System;

public class Class1
{
	public Class1()
	{
        #region Part 01 Revision
        /*
         
        Exception Middleware added at first before any other middleware.
        Custom Middlewares added at last after Authentication and Authorization.

        -we first changed the way we got the internal server error to get a json data with the required response.
        -we did that by creating the custom middleware and added it to the program class
        -Creating the NotFoundException, ex: product not found
        -Handle NotFoundEndPoint Exception
        -Handle Validation Error
        -Add in program containers for the services that you have 

        -Basket Module
        -In Memory Database using (Redis)

         
         */
        #endregion

        #region Part 02 Identity Module Overview

        /*
         
        ++UserManager<T> : it is a class provided from ASP.NET to Handle user-related operations
        -it has many methods to deal with Users and their roles.


        ++SignInManager<T> : Handles login/logout and sign-in logic

        -It has many methods to make the created user By UserManager class to sign in into my application.

        ++RoleManager<T> : Manages role creation and assignment

        -It has the required Methods to add roles to the users
         
         ++Identity Module EndPoints:

            Login: This EndPoint Will Handle User Login Take Email and Password Then Return Token ,  Email and DisplayName To Client  

            Register: This EndPoint Will Handle User Registration Will Take Email , Password  , UserName , Display Name And Phone Number Then Return Token , Email and Display Name To Client  

            Check Email : This EndPoint Will Handle Checking if User Email Is Exists Or Not Will Take Email Then Return boolean To Client  

            Get Current User Address : This EndPoint Will Take Email Then Return Address of Current Logged in User To Client  

            Update Current User Address: This EndPoint Will Handle Updating User Address Take Updated Address and Email Then Return Address after Update To Client  

            Get Current User: This EndPoint Will Take Email Then Return Token , Email and Display Name To Client  



         */

        #endregion
        #region Part 03 Entities & Identity DbContext
        /*
         
        -Create Identity Module folder inside the Core/Domain/Models
        -Create a Public class called ApplicationUser Inside the Identity Module folder
        -Install aspnetcore.identity.entityframeworkcore package inside the domain layer.
        -Make ApplicationUser inherit from IdentityUser.
        -Add more extended properties other that the (Id, UserName, Email, ...) like(DisplayName)
        -Add another Entity for the Address inside the Identity Module.
        -make a navigation property one to one (one user has one address) you can change this anytime.
        -make a new folder called Identity Inside the Infrastructure/persistence
        -Create a new class called StoreIdentityDbContext inherit from IdentityDbContext
        -Make your Configuration and choose which DbSets will be added inside the SQL Database and which will be ignored.
        -Register the service inside the InfrastructureServicesRegistration
        -Add IdentityConnection inside the appsettings.json
        -Add Migration in the Package manager Console: Default project is the Persistence
        -Add Migration "IdentityInitailCreate" -Context "StoreIdentityDbContext" -OutputDir "Identity/Migrations"
        -Update DataBase -Context "StoreIdentityDbContext"
         
         */
        #endregion

        #region Part 04 Seeding Default users & roles
        /*
         
        -Add IdentityDataSeedAsync method inside IDataSeeding which is in the Core/Domain//Contracts 
        -Go to persistence and Implement the new method:
        We need to check if there are users or not using UserManager in the constructor
        We need to check if there are roles or not using RoleManager in the constructor
        We need to register those two services inside the program class, or now inside the InfrastructureServicesRegistration 
        we need to include AddIdentityCore
        use try and catch
        Use IdentityDbcontext to save the changes
         
        
        -Go to E_Commerce.Web.Extensions to WebApplicationRegistration and update SeedDataBaseAsync method 
        
        
         
         */

        #endregion

        #region Part 05 Authentication Service [Login & Register]

        /*
         
         
        -Create Interface called IAuthenticationService Inside Core/ServiceAbstractions
        -Create DTos classes to show to user in E-Commerce.Web/DataTransferObjects/IdentityDtos and the Dto to return
        -write the signatures in the interface  IAuthenticationService
        -Create AuthenticationService inside services that implement IAuthenticationService and write the implementation 
        -Did you registered the services?
        -Create a new Exception class UserNotFoundException inside Domain/Exceptions inherit from NotFoundException
        -Create a new Exception class UnauthorizedException inside Domain/Exceptions inherit from Exception
        -Go to CustomExceptionHandlerMiddleWare and add inside the HandleException method UnauthorizedException => StatusCodes.Status401Unauthorized,
        -Implement the login method.
        

        -Implement the Register method.
        -For token we need a private method to create it, rather than repeating the same code.
        -Create new Exception called BadRequestException 
        -Go to Custom Exception Middleware and add the exception in the handleException method 
        -Go to ErrorToRetuen and a new list of string called errors (shared/ErrorModles/ErrorToRetrun)
        -Add new method to handle the bad request exception
        -Go to IServiceManager and the AuthenticationService.
        -Go to ServiceManager and the Implementation of AuthenticationService.
         */

        #endregion

        #region Part 06 Authentication Controller [Login & Register]
        /*
         
         
        -Go to The Presentation layer and add AuthenticationController class
        -to avoid repeating same attributes over and over, we will create a new class for that name is ApiBaseController
        -Implement the two actions of the controller 
         
         
         */

        #endregion

        #region Part 07 JWT & Token Creation
        /*
         
        JWT: JSON WEB Token go  to read more about JWT
        JWT consists of three main parts:
        1- Header : The header typically consists of two parts: the type of the token, which is JWT, and the signing algorithm being used, such as HMAC SHA256 or RSA.
        {
          "alg": "HS256",
          "typ": "JWT"
        }

        2-Payload: which contains the claims. Claims are statements about an entity (typically, the user) and additional data. There are three types of claims: registered, public, and private claims.
        {
          "sub": "1234567890",
          "name": "John Doe",
          "admin": true
        } 

        3-Signature: To create the signature part you have to take the encoded header, the encoded payload, a secret, the algorithm specified in the header, and sign that.
         
         

        How?
        -Go Services and Install package Microsoft.AspNetCore.Authentication.JwtBearer
        -Go to AuthenticationService and Implement the method create token, see what the steps we took for that.
        -Go to appsettings.json and new property JwtOptions and its parameters(SecretKey, issuer)
        -Get any generated key from any website. 
        -Add IConfiguration inside the constructor to add what you typed inside the appsettings.json 
        --Go to Service Manager and the IConfiguration to send it to the AuthService 
         
         */

        #endregion


        #region Part 08 Jwt Options & Token Validation
        /*
         
        -Add [Authorize] attribute before GetAllProducts to ask if they are authorized to see or not
        -Go to E-commerce.web/Extensions/ ServiceRegistration class, and add new method to register JWT service
        -Go to Program and the new added services 
        -Add use routing to middlewares 
        - Add app.UseRouting(); at the program class
        -in postman send token in header or authorization tab
        -if the user has no token , you get 401 unauthorized, 403: Forbidden
         
         
         */

        #endregion

        #region Part 09 Authentication Service [Address , Email And Get User ]
        /*
         
        -Go to IAuthenticationService and add a new signatures for the email and address methods
        -GO shared/DTos/Identity/ and add a new class for the addressDto  
        -Go to AuthenticationService and Implement the new methods.
        -Add new Exception called AddressNotFoundException
        -Map between Address and AddressDto inside the Services project inside MappingProfiles
         
         
         */

        #endregion

        #region Part 10 Authentication Controller [Address , Email And Get User ]
        /*
         
         
        
        -Go to Authentication Controller and add th new four methods
         
         

         
         
         
         
         
         
         */


        #endregion


    }
}
