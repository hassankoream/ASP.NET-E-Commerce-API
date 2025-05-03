using System;

public class Class1
{
	public Class1()
	{
        #region Part 01 Revision

        /*
         
        ++Async vs Sync Architecture
        ++Models or Entities
        ++IGenericRepository in Core/Domain/Contracts
        ++GenericRepository in Persistence/Repositories
        ++IUnitOfWork in Core/Domain/Contracts
        ++UnitOfWork in  Persistence/Repositories
        ++Work on ServicesAbstraction and ServicesImplementation
        ++Work on MappingProfiles
        ++Work On ServiceManager
        ++Work on Controllers
        ++Work on Resolver if needed to change the data while mapping

        

         
         */
        #endregion

        #region Part 02 Specifications


        /*
         
        What?
        Specifications Design pattern Used to make your Queries Dynamic.

        Why?
        We need it to be Dynamic in order to send other parameters to the query, Other Expressions Rather than Static ones.
        Rather then Filtering based on Price we could filter on any other property.
        Rather than Including Just specific tables we could Include any columns in the table.
        Rather than Sorting based on just a specific column, we could make it more Dynamic.
        
        _dbContext.Products.Where(P => P.Id == id).Include(P => P.ProductType).Include(P => P.ProductBrand);


        1. _dbContext.Products : Entry Point (Start Of Query)
        2. Where(P => P.Id == id) : Where Condition
        3. Include(P => P.ProductType).Include(P => P.ProductBrand) : List Of Includes


        How?
        -Create ISpecifications Interface in Core/Domain/Contracts
        -Create BaseSpecifications Class in Core/Services/Specifications
        -Add new methods to the IGenericRepository with the Specification
        -Create a Static class SpecificationsEvaluator as a helper class to create the query in GenericRepository
        -Implement the IGenericRepository with the new methods and 
         
         
         
         */

        /*
         
        Of course! Let’s simplify the **Specification Design Pattern** — especially when used for **queries** in an **ASP.NET Core API**.

---

### 🌟 First: Simple Answers to "What", "Why", and "How"

**1. What is Specification Pattern (in Queries)?**  
It is a way to **separate** *what you are asking for* (filtering, sorting, paginating) into a **reusable object**, instead of putting query logic all over your code.

---

**2. Why use it?**  
- To **reuse** common query conditions (e.g., "Active Products", "Customers with Orders")  
- To **keep controllers/services clean**  
- To **combine** small conditions into bigger ones easily  
- To **test** your query logic separately

---

**3. How does it work?**  
👉 You create a **Specification** class that **describes**:
- *Which entities to select*  
- *How to filter them*  
- *What related data to include*  
- *How to order or paginate them*

You then **pass the Specification** to a **repository** that knows how to turn it into a real database query (`IQueryable`).

---

---

### 🧠 Now, a Simple Visual Flow

```
Controller 
   --> calls Service
        --> asks Repository
              --> Repository reads Specification
                     --> builds IQueryable<Product> based on it
                            --> executes on Database
```

---

### 🛠 Basic Example

**Imagine**: You want to query for **Products** where:
- Price > 100
- Include their **Category**
- Order by Name

#### 1. Define the Specification

```csharp
public class ProductWithCategorySpecification : BaseSpecification<Product>
{
    public ProductWithCategorySpecification()
    {
        AddInclude(p => p.Category);
        AddCriteria(p => p.Price > 100);
        AddOrderBy(p => p.Name);
    }
}
```

You are **describing** the query — not writing it yet!

---

#### 2. BaseSpecification Class

Normally you create a base to store **criteria**, **includes**, etc.

```csharp
public abstract class BaseSpecification<T>
{
    public Expression<Func<T, bool>> Criteria { get; private set; }
    public List<Expression<Func<T, object>>> Includes { get; } = new();
    public Expression<Func<T, object>> OrderBy { get; private set; }

    protected void AddCriteria(Expression<Func<T, bool>> criteria) => Criteria = criteria;
    protected void AddInclude(Expression<Func<T, object>> include) => Includes.Add(include);
    protected void AddOrderBy(Expression<Func<T, object>> orderBy) => OrderBy = orderBy;
}
```

---

#### 3. SpecificationEvaluator (Helper to Apply It)

```csharp
public static class SpecificationEvaluator<T> where T : class
{
    public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, BaseSpecification<T> spec)
    {
        var query = inputQuery;

        if (spec.Criteria != null)
            query = query.Where(spec.Criteria);

        if (spec.OrderBy != null)
            query = query.OrderBy(spec.OrderBy);

        query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));

        return query;
    }
}
```

---

#### 4. Repository Uses Specification

```csharp
public async Task<IReadOnlyList<Product>> ListAsync(BaseSpecification<Product> spec)
{
    var query = SpecificationEvaluator<Product>.GetQuery(_context.Products.AsQueryable(), spec);
    return await query.ToListAsync();
}
```

---

### 🎯 Final in Controller

```csharp
[HttpGet]
public async Task<IActionResult> GetProducts()
{
    var spec = new ProductWithCategorySpecification();
    var products = await _repository.ListAsync(spec);
    return Ok(products);
}
```

---

### ✨ Summary
- A **Specification** = a **description** of a query
- Repository **reads** it and builds the query dynamically
- **No messy Linq** inside controller or service
- **Flexible** — easy to add new conditions without changing code everywhere

---

Would you like me next to show you a **simplified "dynamic" Specification** (where the controller can pass parameters like minPrice/maxPrice/sorting at runtime)? 🚀  
It makes things even more powerful!
         
         
         */

        //Real world Example

        /*
         
         
        Got it — no problem at all.  
Let’s **slow down** and explain it in a **simpler**, more **natural** way — **without code first**.

---

## Let's Start From a Real-Life Example

Imagine you own a **library**.  
You ask your assistant:

> "Bring me all the books **written after 2000**, **in English**, and **sort them by title**."

Instead of explaining this **every time**, you want to create a **note** that says:

✅ *"After 2000" filter*  
✅ *"Language is English" filter*  
✅ *"Sort by title" rule*

Your assistant will **read the note** and **find the books** exactly according to it — no confusion.

---

**In programming,**  
- **You** = Controller / Service  
- **Assistant** = Repository  
- **The Note** = Specification

---

> 📜 **The Specification = a simple instruction sheet for what you want to query.**

You **describe** what you want.  
Your repository **reads** it and **gets the right data**.

---

## Why Is It Useful?

Because sometimes:
- You want **different rules** for different cases.
- You want to **reuse** rules without writing them again.
- You want **small notes** (specifications) that you can **combine** later if needed.

---
  
## In ASP.NET Core APIs

- Your **Controller** says, "Get products."
- It sends a **Specification** saying, "Price > 100, Include Category, Sort by Name."
- Your **Repository** reads that **Specification** and builds the right query to the database.

---

# 🎨 Super Simple Diagram:

```
Controller 
   --> gives "Note" (Specification)
Repository 
   --> reads "Note"
        --> builds query 
             --> gets data
```

---

# 🌿 Even Simpler Words

| Part | Real Meaning |
|:----:|:------------:|
| Specification | A description of a query |
| Repository | A worker that knows how to read a description and execute it |
| Controller | A caller who gives the description |
| Data | The result of the worker following the description |

---

# 🧘‍♂️ Take Your Time

I completely understand — when I first learned Specification, it felt **abstract and unnecessary**.  
But once you think of it as "**a note**" or "**a description of the query**" — it becomes very natural.

---
  
# Would you like next:

- 🎯 **An even smaller example** with **only one condition**?  
- 🧩 **A picture/diagram** showing the connection between controller → specification → repository?  
- ✍️ **Step-by-step building** of a simple query with Specification in slow motion?

(You can choose — I will move at your pace!) 🌱  
Which way feels better for you right now?
         
         */


        #endregion

        #region Part 03 Specification [All Products - Product By Id]

        /*
         
        -Create Class ProductWithBrndAndTypeSpecifications in order to use GetAll with brand and type included
        -Make two constructors in that class for both methods
        -Go to Product Service and use the method that takes one parameter called Specifications
        
        **Start by asking what do you need?
        *I need to get products with brand and type
        *I need to make it Dynamic so I can Add more tables in the future without modifying the query over again.
        *I need to follow the Specification Design pattern as it is and see how they solved the problem.
        *Start with controller > service > repository > Add one more method that takes Specification as a parameter.
        *
        *

         
         */
        #endregion

        #region Part 04 Filtration Specifications

        /*
         
        -Modify the Controller Get All Products to have more parameters for filtration based on Brand and Type if needed
        -Modify Service to Include the new parameters.
        -Modify ProductWithBrndAndTypeSpecifications  to Include the new parameters.

         
         */

        #endregion
        #region Part 05 Sorting Specifications

        /*
         
        -From ISpecifications Add two more properties for ordering
        -Implement the two added properties into  BaseSpecifications
        -Add new enum for productSortingOptions in the shared program
        -Add this to Controller > service > ProductWithBrndAndTypeSpecifications
        -use switch case to Add the all sorting options required
        -Add OrderBy and  OrderByDesc to your query creation in the SpecificationsEvaluator before Include
   



         
         
         */

        #endregion
        #region Part 06 Product Query Parameters

        /*
        
        If your function has more than 3 params, start to make an object and add the params to it.

        -Create ProductQueryParams class in the shared folder
        -Send an object from this class to Controller > Service > ProductWithBrndAndTypeSpecifications
        - change the controller to accept fromQuery Annotation
         


         */

        #endregion
        #region Part 07 Search Specification
        /*
         
        Add new property inside the ProductQueryParams class for the search value
        -Add the search property to the Criteria
         
         
         */
        #endregion
        #region Part 08 Pagination

        /*
         
        those new properties will be  part of the query itself (Changes should be in ISpecifications, BaseSpecifications, )
        - Using keywords like (Skip and Take) to determine the page size and the page Index
        - Add two more properties to the ISpecifications interface
        - Go to BaseSpecifications and Add the Implementation, add new method to apply pagination.
        - Go to ProductWithBrndAndTypeSpecifications and add ApplyPagination after switch
        - Go to ProductQueryParams and Add two properties from the query
        - Go to SpecificationsEvaluator and Add the missing properties to update the query
         
         
         
         
         */

        #endregion

        #region Part 09 Paginated Result
        /*
         
        Create new class called PaginatedResult in the shared folder, holding data of pagination.
        Update IproductService and productService to match the return result of the new object.

         
         
         */

        #endregion

        #region Part 10 Count Specifications
        /*
         
         
        -Go to IGenericRepository and add new method for Count
        -Go to GenericRepository to Implement the method 
        -In ProductService Add the new way for the count
        -need another Specifications without the Pagination Applied to the result.
        -We need a new Specifications for the Count Method called ProductCountSpecification and inherit from BaseSpecifications
        -After that Modify the total count in the service class
         
         
         */

        #endregion
    }
}
