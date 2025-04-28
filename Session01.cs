using System;

public class Class1
{
    public Class1()
    {
        #region Part 01 API Project Structure

        /*
         
        AOT: Ahead Of Time

        Native AOT: compile to native code without IL file.

        minimalAPI: one controller which is the program , we write actions inside it

        LaunchUrl: Swagger: which is a way to test your API, like views in the MVC.

        naming convention of designing the route of the api: BaseUrl/Api/Controller
        

         sending request could from E-Commerce.Web.http


         
         
         */
        #endregion

        #region Part 02 APIs Types

        /*
         

        API: Application Programming Interface
        Endpoint: A URL that represents a resource or Function in the API

        RESTful API: [REpresentational State Transfer] an architectural style for an application programming interface[API] that uses HTTP requests to access and use data
         
        Endpoint needs you to identify the method: which could be get, post, whatever you need in order to go walk
       

---

**1. RESTful API**  
- **Stands for**: Representational State Transfer  
- **Style**: Web standard using HTTP methods (GET, POST, PUT, DELETE)  
- **Data format**: Usually JSON or XML  
- **How it works**:  
  - You have "resources" (like users, products)  
  - You access them via URLs like `/users/1`, `/products/5`  
  - Each URL represents a thing. You GET, POST, DELETE on it.  
- **Good for**: Simplicity, standard CRUD apps.

---

**2. SOAP API**  
- **Stands for**: Simple Object Access Protocol  
- **Style**: A strict protocol with a full specification (unlike REST)  
- **Data format**: Always XML  
- **How it works**:  
  - You send an XML message following a very detailed structure (called an "envelope").  
  - Needs special tools to parse and send (not just simple browser requests).  
- **Good for**: Enterprise apps where strict contracts, security, and heavy operations are needed (like banks, insurance systems).

---

**3. GraphQL API**  
- **Stands for**: Graph Query Language  
- **Style**: You ask exactly for the data you want  
- **Data format**: JSON  
- **How it works**:  
  - You send a *query* to the server describing exactly what fields you want.  
  - No fixed endpoints like `/users` or `/products`; you query the graph.  
  - One request can fetch deeply related data (like user + posts + comments) all at once.  
- **Good for**: Frontend apps needing flexibility and efficiency (no overfetching/underfetching data).

---

**Quick Example to visualize**:

| Feature | REST | SOAP | GraphQL |
|:-------|:-----|:-----|:--------|
| Data format | JSON, XML | XML | JSON |
| Style | Resources via URL | Operations via XML envelopes | Queries over a schema |
| Flexibility | Medium | Low (strict) | Very high |
| Good for | Web apps | Enterprises (banking, secure apps) | Modern web/mobile apps |

---

Would you like a simple real-world example (like "getting a list of books") in each style to see them side by side? 📚✨  
It will make it even clearer!
         
         
         */
        #endregion


        #region Part 03 Onion Architecture

        /*
         
        Layers Depends on Layers (Two Main Projects)
        ask where to start?
        ask which depends on which?
        ask and make reference from other projects like we did in this one.


         
         
         */
        #endregion

        #region Part 04 Product Module

        /*
         

        Building the Product Module

        We Start layer by layer, from Inner to outer layers
        Each product has connection with the brand that it is belong to and the type so we will create three entities to represent it with its own connections

        Layer one (Domain layer)
        
        Entities or Models:
        product, productBrand, ProductType
        
        
         
         
         
         
         */
        #endregion'

        #region Part 05 Product Configurations & Db Context

        /*
         
        Create the First DbContext Class (StoreDbContext)
        use Microsoft.EntityFrameworkCore and EntityFrameworkCore.sqlserver  into the project where the DbContext exists.
        Override void OnModelCreating Function
        use Configuration Classes for every entity 
        Add Assembly class to Persistence
        Add ApplyConfigurationsFromAssembly in the OnModelCreating inside DbContext
        Add ConnectionString to the App setting
        Add DbContext Service using the SqlServer and ConnectionString
        Add Migration using InitialCrreate Comment

        

         
         
         */
        #endregion

        #region Part 06 Products-Brands-Types Data Seeding

        /*
         
        Add Interface IDataSeeding in the Contracts folder
        Add Class DataSeeding in the Persistence Project that Implement the Interface IDataSeeding
        Ensure that All Migrations Applied before Seeding the Data by Injecting the DBContext in the DataSeeding class
        We Only Seed the Data At the first request in the production or when using Update-database in development


        Start to Add Data if no elements Are there in the database.

        Create a new folder where re JSON files located
        Add them into DataSeeding Class with Validations
        

        Go to 
         
         
         
         */
        #endregion
    }
}
