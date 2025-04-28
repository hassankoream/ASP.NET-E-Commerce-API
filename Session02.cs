using System;

public class Class1
{
    public Class1()
    {

        #region Part 01 Revision

        /*
         

         
         
         */
        #endregion
        #region Part 02 Asynchronous And Synchronous Programming

        /*
         
        What is Synchronous and Asynchronous?
        
        Synchronous is a blocking architecture As a single-thread model, it follows a strict set of sequences, which means that operations are performed one at a time, in order.While one operation is being performed, other operations’ instructions are blocked. The completion of the first task triggers the next, and so on

        
        
        Asynchronous is a non-blocking architecture, so the execution of one task isn’t dependent on another , Increases Performance because multiple operations can run at the same time.


        Why we should use them?

        Synchronous is simpler for CPU-bound logic.

        Asynchronous improves scalability for I/O-bound tasks by not blocking threads.


        How?



        - `async` – Marks method as asynchronous.
        - `await` – Waits for an async task to finish.
        - `Task` – Represents an async operation.
        - `Task<T>` – Represents an async operation that returns a value of type `T`.
        - Use "Async" at the and of the Method Name



         
         */


        /*
         
        Great! Let's break down **Synchronous** vs **Asynchronous** in C# using your **What, Why, How** method — with *moderated examples* for each keyword.

---

### 🟠 **Synchronous vs Asynchronous in C#**

---

### 🟢 **1. What**
- **Synchronous** code runs step by step. One thing must finish before the next starts.
- **Asynchronous** code lets the program keep running while waiting for something (like a database or API).

---

### 🟡 **2. Why**
- **Synchronous** is simpler for CPU-bound logic.
- **Asynchronous** improves scalability for I/O-bound tasks by not blocking threads.

---

### 🔵 **3. How**

#### 👉 SYNC EXAMPLE: Simple & Blocking

```csharp
public string GetMessage()
{
    Thread.Sleep(3000); // Simulates a delay (blocking)
    return "Hello from Sync!";
}
```

- ✅ Use for: Quick, CPU-only logic.
- ❌ Avoid for: I/O like DB or HTTP calls.

---

#### 👉 ASYNC EXAMPLE: Using `async`, `await`, `Task`, `Task<T>`

```csharp
public async Task<string> GetMessageAsync()
{
    await Task.Delay(3000); // Simulates delay without blocking thread
    return "Hello from Async!";
}
```

- ✅ Use for: I/O tasks like API calls, DB access.
- `async` – Marks method as asynchronous.
- `await` – Waits for an async task to finish.
- `Task` – Represents an async operation.
- `Task<T>` – Represents an async operation that returns a value of type `T`.

---

### 🔡 **More Examples for Each Keyword**

#### ✅ `async` + `await`:
```csharp
public async Task DoWorkAsync()
{
    await Task.Delay(1000);
    Console.WriteLine("Finished async work.");
}
```

#### ✅ `Task` returning no result:
```csharp
public Task SimpleDelayAsync()
{
    return Task.Delay(500); // No need for async/await if not doing extra logic
}
```

#### ✅ `Task<T>` returning result:
```csharp
public async Task<int> GetNumberAsync()
{
    await Task.Delay(100);
    return 42;
}
```

#### ✅ Call from Controller in ASP.NET Core:

```csharp
[HttpGet("sync")]
public string GetSync() => GetMessage(); // blocks

[HttpGet("async")]
public async Task<string> GetAsync() => await GetMessageAsync(); // non-blocking
```

---

Want to move to file I/O, DB calls, or external API next?
         
         
         
         */

        #endregion


        #region Part 03 Generic Repository


        /*
         ### 🧠 What is a Generic Repository?

A **Generic Repository** is a reusable class that handles common database operations like `Add`, `Update`, `Delete`, and `Get` for any entity type.

---

### ❓ Why use it?

- **Avoid repetition**: Write common data logic once for all entities.  
- **Cleaner architecture**: Separation of concerns (Controller → Service → Repository → DbContext).  
- **Easier testing**: You can mock the repository.

        How?
        Create interface IGeneric Repository
        Create Class Generic Repository
         

         
         
         */
        /*
         
         Great! Let’s break down **Generic Repository** using the **three-question method**, with **clear C# and ASP.NET Core keywords and examples**.

---

### 🧠 What is a Generic Repository?

A **Generic Repository** is a reusable class that handles common database operations like `Add`, `Update`, `Delete`, and `Get` for any entity type.

---

### ❓ Why use it?

- **Avoid repetition**: Write common data logic once for all entities.  
- **Cleaner architecture**: Separation of concerns (Controller → Service → Repository → DbContext).  
- **Easier testing**: You can mock the repository.

---

### 🛠️ How to implement it (with full C# & ASP.NET Core keywords and usage)

---

#### 1. **Create a base interface** `IGenericRepository<T>`

```csharp
// Interface - IGenericRepository.cs
using System.Linq.Expressions;

public interface IGenericRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int id);
    Task AddAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
}
```

✅ **Keywords** used:
- `Task<T>`: async return types.
- `where T : class`: generic constraint for EF Core.
- `IEnumerable<T>`: generic collection.

---

#### 2. **Implement the generic class** `GenericRepository<T>`

```csharp
// GenericRepository.cs
using Microsoft.EntityFrameworkCore;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly AppDbContext _context;
    private readonly DbSet<T> _dbSet;

    public GenericRepository(AppDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync(); // async DB call
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public void Update(T entity)
    {
        _dbSet.Update(entity);
    }

    public void Delete(T entity)
    {
        _dbSet.Remove(entity);
    }
}
```

✅ **Keywords** used:
- `DbSet<T>`: EF Core table set.
- `context.Set<T>()`: dynamic EF set for any entity.
- `await`: used with async DB calls like `ToListAsync()`.
- `FindAsync`, `AddAsync`, `Update`, `Remove`.

---

#### 3. **Inject the repository into your service**

```csharp
// IProductService.cs
public interface IProductService
{
    Task<IEnumerable<Product>> GetAllProductsAsync();
}
```

```csharp
// ProductService.cs
public class ProductService : IProductService
{
    private readonly IGenericRepository<Product> _repository;

    public ProductService(IGenericRepository<Product> repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        return await _repository.GetAllAsync();
    }
}
```

---

#### 4. **Register in DI container**

```csharp
// In Program.cs or Startup.cs
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IProductService, ProductService>();
```

✅ **Keywords**:
- `AddScoped`: Dependency injection lifetime.
- `typeof`: generic type resolution.

---

#### 5. **Use in controller**

```csharp
// ProductController.cs
[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _service;

    public ProductController(IProductService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var products = await _service.GetAllProductsAsync();
        return Ok(products);
    }
}
```

---

### ✅ Summary

| Concept              | Keywords                         | Example Use                        |
|----------------------|----------------------------------|-------------------------------------|
| Async DB ops         | `Task<T>`, `await`               | `await _dbSet.ToListAsync()`        |
| Reusability          | `T`, `where T : class`           | `IGenericRepository<T>`             |
| EF Core access       | `DbSet<T>`                       | `_context.Set<T>()`                 |
| DI registration      | `AddScoped(typeof(...))`         | DI for open generic repository      |

---

Want me to extend this with **Unit of Work**, **custom query methods**, or **async transaction handling** next?
         
         
         */
        #endregion

        #region Part 04 UnitOfWork

        /*
         
        
### 🧠 What is Unit of Work?

It’s a pattern that groups one or more **repository operations** into a **single transaction**.

---

### ❓ Why use it?

- To **ensure atomicity**: All DB operations succeed or fail together.  
- To **coordinate multiple repositories** cleanly.  
- To keep **DbContext lifecycle** in one place.
         
         
         
         */
        /*
         
         
        Perfect! Let’s break down **Unit of Work** using the **What, Why, How** method—short and simple for What & Why, then full code examples with keywords in C# and ASP.NET Core.

---

### 🧠 What is Unit of Work?

It’s a pattern that groups one or more **repository operations** into a **single transaction**.

---

### ❓ Why use it?

- To **ensure atomicity**: All DB operations succeed or fail together.  
- To **coordinate multiple repositories** cleanly.  
- To keep **DbContext lifecycle** in one place.

---

### 🛠️ How to implement it

---

### 🧩 1. Interface: `IUnitOfWork`

```csharp
// IUnitOfWork.cs
public interface IUnitOfWork : IDisposable
{
    IGenericRepository<Product> Products { get; }
    IGenericRepository<Category> Categories { get; }

    Task<int> SaveAsync();
}
```

✅ **Keywords**:
- `IDisposable`: for cleanup.
- `Task<int>`: async saving.
- Properties return typed repos.

---

### ⚙️ 2. Implementation: `UnitOfWork`

```csharp
// UnitOfWork.cs
public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public IGenericRepository<Product> Products { get; }
    public IGenericRepository<Category> Categories { get; }

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
        Products = new GenericRepository<Product>(context);
        Categories = new GenericRepository<Category>(context);
    }

    public async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync(); // commit all changes
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
```

✅ **Keywords**:
- `GenericRepository<T>`: per-entity repository.
- `SaveChangesAsync()`: EF commit.
- `Dispose()`: free up DB connection.

---

### 🧪 3. Service Layer Usage

```csharp
// ProductService.cs
public class ProductService
{
    private readonly IUnitOfWork _unitOfWork;

    public ProductService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task AddProductWithCategory(Product product, Category category)
    {
        await _unitOfWork.Categories.AddAsync(category);
        await _unitOfWork.Products.AddAsync(product);

        await _unitOfWork.SaveAsync(); // one commit for both
    }
}
```

---

### 🔗 4. Dependency Injection in Program.cs

```csharp
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
```

---

### 🧠 Keywords Recap

| Keyword                 | Used For                            | Example                                  |
|------------------------|--------------------------------------|------------------------------------------|
| `IDisposable`          | Dispose DbContext                    | `public void Dispose()`                  |
| `Task<T>` / `await`    | Async DB saving                      | `await _context.SaveChangesAsync()`      |
| `AddScoped`            | Register UoW in DI                   | `builder.Services.AddScoped...`          |
| `IGenericRepository<T>`| Strongly typed repo properties       | `IGenericRepository<Product> Products`   |

---

### ✅ Summary

**Unit of Work**:
- Manages **all repositories and saves** together.
- Promotes **clean, transaction-safe architecture**.
- Keeps business logic focused and testable.

---

Want me to show how to extend this with **transactions**, **validation**, or **custom repository logic**?
         
         
         */
        #endregion

        #region Part 05 Product Module - Product Service

        /*
         
        -Create IProductService inside Core/ServicesAbstraction
        -Create DTOs in the Shared folder
        -Create ProductService in  Core/Services
        -Install AutoMapper in Services
        -Implement the Interface and start Code.



         
         
         
         
         */
        #endregion

        #region Part 06 Auto Mapper

        /*
         

        -Create MappingProfiles Folder inside the Service
        -Add new Class for ProductProfile

         
         
         
         */


        //Assembly Reference



        /*
         
         Alright, sticking exactly to your current learning method (What - Why - How, clear and simple):

---

## 🧠 What is Assembly Reference?

An **Assembly Reference** means:  
➔ **One project (or file) points to another compiled assembly (DLL)** so it can **use its classes, interfaces, methods**.

---

## ❓ Why do we need Assembly References?

- To **reuse** code from other projects.
- To **split** big applications into **smaller, organized projects** (like Data, Services, API).
- To **share** common libraries without rewriting them.

---

## 🛠 How to create and use an Assembly Reference?

### In Visual Studio (or .NET CLI):

| Step | Action |
|:----|:------|
| 1 | **Right-click** on the project → **Add** → **Project Reference**. |
| 2 | **Choose** the project you want to reference. |
| 3 | Now you can **use** its classes by **adding `using NamespaceName;`** on top. |

---

## ✨ Simple Example:

### You have two projects:

- `MyApp.Core` → (holds Models, Interfaces)
- `MyApp.API` → (your main Web API)

---

1. **Add a Reference:**

In `MyApp.API`, add **Reference** to `MyApp.Core`.

2. **Use classes/interfaces:**
```csharp
using MyApp.Core.Models;

public class ProductsController : ControllerBase
{
    private readonly Product _product;
}
```
✅ Now `Product` is accessible because of **Assembly Reference**.

---

## ⚡ In CLI (.NET command):

```bash
dotnet add MyApp.API reference MyApp.Core
```

---

## 🧠 Extra Tip (important):

- If you don't reference the assembly ➔ you **can't use** its code.
- Always **reference from higher to lower layer**:
  - API ➔ Services ➔ Repositories ➔ Core
  - **Never in reverse!** (no Data accessing API)

---

**In Short:**  
AssemblyReference = "Hey Project A! I need to use things from Project B!"

---

Would you also like a diagram 📈 showing how assemblies typically reference each other in a real app (layered architecture)? It would make it super visual. 🎯  
Want me to?
         
         
         
         */
        #endregion

        #region Part 07 Service Manager

        /*
         
        -Add Controllers Folder inside the Presentation Layer

        -Add ProductsController inside the folder

        -Add Interface called IserviceManger inside the ServicesAbstraction layer to handle All services as properties




         
         
         
         */


        //Lazy Unit Of work?

        /*
         
         Alright!  
We'll use the **same structure** you like:  

— **What** (short)  
— **Why** (short)  
— **How** (full example using Lazy)  

---

## 🧠 What is Unit of Work with Lazy?

It means **creating repositories only when needed** (on first use), instead of at UnitOfWork construction.

---

## ❓ Why use Lazy?

- **Performance**: don’t create things you don't use.  
- **Memory**: saves RAM by delaying heavy object creation.  
- **Speed**: speeds up startup.

---

## 🛠️ How to implement Lazy Unit of Work

---

### 🧩 1. Interface: `IUnitOfWork` (Same as before)

```csharp
// IUnitOfWork.cs
public interface IUnitOfWork : IDisposable
{
    IGenericRepository<Product> Products { get; }
    IGenericRepository<Category> Categories { get; }
    
    Task<int> SaveAsync();
}
```

✅ (No changes needed in the interface.)

---

### ⚙️ 2. Implementation: `UnitOfWork` (Lazy way)

```csharp
// UnitOfWork.cs
public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    private Lazy<IGenericRepository<Product>> _products;
    private Lazy<IGenericRepository<Category>> _categories;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;

        _products = new Lazy<IGenericRepository<Product>>(() => new GenericRepository<Product>(_context));
        _categories = new Lazy<IGenericRepository<Category>>(() => new GenericRepository<Category>(_context));
    }

    public IGenericRepository<Product> Products => _products.Value;
    public IGenericRepository<Category> Categories => _categories.Value;

    public async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
```

✅ **Keywords used**:
- `Lazy<T>`: creates repo only when accessed.
- `.Value`: accesses the actual repository.
- `Task`, `await`: async save.
- `IDisposable`: dispose context.

---

### 🧪 3. Usage (Same Service Example)

```csharp
// ProductService.cs
public class ProductService
{
    private readonly IUnitOfWork _unitOfWork;

    public ProductService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task AddProductWithCategory(Product product, Category category)
    {
        await _unitOfWork.Categories.AddAsync(category);
        await _unitOfWork.Products.AddAsync(product);

        await _unitOfWork.SaveAsync();
    }
}
```

---
 
### 🔗 4. Dependency Injection (Same)

```csharp
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
```

---

## 🧠 Keywords Table Recap:

| Keyword            | Used For                             | Example                                  |
|---------------------|---------------------------------------|------------------------------------------|
| `Lazy<T>`           | Delay creation until needed           | `new Lazy<IGenericRepository<Product>>()` |
| `.Value`            | Access the actual object              | `_products.Value`                        |
| `Task<T>` / `await` | Async save operations                 | `await SaveChangesAsync()`               |
| `IDisposable`       | Dispose context properly              | `_context.Dispose()`                     |

---

✅ **Summary**:  
Lazy + UnitOfWork =  
- Repositories are **created only if used**.  
- **Faster** startup.  
- **Cleaner** memory management.  

---

Would you also like me to show a **Full Working Example** (Controller + DbContext + Models) after that? 🚀  
It'll be super clear!
         
         
         
         
         */

        //Compare lazy and unit of work
        /*
         
        Perfect, let's compare **Unit of Work normal vs Lazy** clearly — the way you like:  

---

## 🧠 What?

| Version | Meaning |
|:--------|:--------|
| **Normal Unit of Work** | Create *all* repositories immediately. |
| **Lazy Unit of Work** | Create repository *only when accessed*. |

---

## ❓ Why does it matter?

- If you have **many repositories** but **use only a few** during a request → Lazy **saves memory and CPU**.  
- If you **always** use **all repositories** → no big difference.

---

## 📈 Performance and Memory Comparison:

| Factor            | Normal UoW                  | Lazy UoW                           |
|-------------------|------------------------------|------------------------------------|
| **Object Creation** | Create **everything immediately** when UnitOfWork is created | Create **only what you use** |
| **Memory Usage**    | **Higher** (all repos live in memory even unused) | **Lower** (only used repos exist) |
| **Speed at Startup** | **Slower** (constructs all repos) | **Faster** (constructs on first use) |
| **Speed during usage** | **Faster** if all repos needed (already created) | **Slightly slower** (first access cost of `.Value`) |
| **Best For** | Small apps, few repos | Big apps, many repos, heavy repositories |

---

## 🎯 Real World Example:

Imagine you have 20 repositories:  
- In **normal Unit of Work**, all 20 are created even if you only use 1!  
- In **lazy Unit of Work**, **only the 1 you actually use** is created.

> ➔ **Less memory**, **less garbage collection**, **better scalability**.

---

## 🛠 Code Behavior

| Normal |
```csharp
public IGenericRepository<Product> Products { get; }
```
- Products repo is **instantly created** when UnitOfWork is created.

| Lazy |
```csharp
public IGenericRepository<Product> Products => _products.Value;
```
- Products repo is **created only when you call** `.Products`.

---

## 🧠 Simple Memory Visual:

| Stage        | Normal                            | Lazy                              |
|--------------|------------------------------------|----------------------------------|
| App Start    | All repositories created          | Nothing created yet              |
| User Request (needs 1 repo) | All repos already in memory | Only needed repo created         |

---

## ✅ Summary

| If... | Then Use |
|:-----|:---------|
| App has **few repositories**, always used together | Normal Unit of Work |
| App has **many repositories**, often unused | Lazy Unit of Work |

---

Would you like me to give a **small Benchmark Code Example** too (measuring memory and speed between normal vs lazy)? 🚀 It will make the difference even more *visible*!  
(It's simple!)  
Would you? 🎯
         
         
         
         */
        #endregion


        #region Part 08 Product Module -  Product Controller

        /*
         
        in order to have a controller we need :
        1-our class should inherit from  ControllerBase which is located in Microsoft.AspNetCore.App namespace
        so we will Add reference of the framework itself and we will not install it from NuGet
        How?
        Go to the project or the class library(presentation) and double click on it and add:
         	<ItemGroup>
		        <FrameworkReference Include="Microsoft.AspNetCore.App"/>
	        </ItemGroup>


        2-     [ApiController]
               [Route("api/[Controller]")] //BaseUrl/api/Products

        3- Build the requests (GetAll, GetByID, Create, Remove and so on)



        Run the Application on (Swagger, Postman, E_Commerce.Web_HostAddress)
         
         
         */
        #endregion

        #region Part 09 Product Module -  Product Picture Url Resolver

        /*
         
        in order to make the FrontEnd developer see the Images you need to add them in your project.

        Add wwwroot folder then add images folder then add products folder.

        Add the route required to access the images:
        - In program Add this after UseHttpsRedirection:  app.UseStaticFiles();
        - make the JSON Data Response see the correct URl (baseUrl+Picture Url)
        - Go to Product profile to map the Url by adding the baseUrl, but the problem is the BaseUrl is static
        - Add new class in the profiles internal class PictureUrlResolver: IValueResolver<>
        - Add this package in order to make the Url non static and have it in appsettings.json: Microsoft.Extensions.Configuration;
        - make your constructor to add object form IConfiguration to access the appsettings.json and use the URl

         
         
         */
        #endregion
    }
}
