using System.Web.Mvc;
using Moq;
using Sklep_internetowy.Controllers;
using NUnit.Framework;
using Sklep_internetowy.DAL;
using Sklep_internetowy.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System;
using System.Linq;
using Sklep_internetowy.View;
using Sklep_internetowy.Infrastructure;
using Sklep_internetowy.DAL.Interfaces;

namespace Sklep_internetowy.Tests.Service
{
    class ContextServicesTest
    {
        private Mock<ICacheProvider> mockCache;
        private Mock<IClock> mockClock;

        [SetUp]
        public void SetUp()
        {
            mockCache = new Mock<ICacheProvider>();
            mockClock = new Mock<IClock>();
        }

        [Test]
        [TestCase(1)]
        public void GetProductsForGivenCategory_Should_ReturnAllProductsForGivenCategory(int CategoryId)
        {
            //Arrange
            
            var ListOfProducts = new List<Product>()
            {
                new Product() { ProductId = 1, CategoryId = 1 },
                new Product() { ProductId = 2, CategoryId = 2 },
                new Product() { ProductId = 3, CategoryId = 2 },
                new Product() { ProductId = 4, CategoryId = 1 },
                new Product() { ProductId = 5, CategoryId = 1 },

            }.AsQueryable();

            var mockSet = new Mock<IDbSet<Product>>();
            mockSet.As<IQueryable<Product>>().Setup(m => m.Provider).Returns(ListOfProducts.Provider);
            mockSet.As<IQueryable<Product>>().Setup(m => m.Expression).Returns(ListOfProducts.Expression);
            mockSet.As<IQueryable<Product>>().Setup(m => m.ElementType).Returns(ListOfProducts.ElementType);
            mockSet.As<IQueryable<Product>>().Setup(m => m.GetEnumerator()).Returns(ListOfProducts.GetEnumerator());

            var mockContext = new Mock<IProductDbContext>();
            mockContext.Setup(c => c.Products).Returns(mockSet.Object);

            var service = new ContextServices(mockContext.Object, mockCache.Object, mockClock.Object);

            //Act
            var result = service.GetProductsForGivenCategory(CategoryId);

            //Assert
            Assert.AreEqual(3, result.Count());
            Assert.IsInstanceOf<List<Product>>(result);
            Assert.IsNotNull(result);
        }

        [Test]
        public void GetBestsellersAndNewsForMainPage_Should_ReturnThreeBestsellersAndNewProducts()
        {
            //Arrange  
            var ListOfProducts = new List<Product>()
            {
                new Product() { ProductId = 1, Name = "a", isBestseller = true, AddingDate = new DateTime(2020,03,10,18,43,45) },
                new Product() { ProductId = 2, Name = "b", isBestseller = false, AddingDate = new DateTime(2020,03,10,18,41,35) },
                new Product() { ProductId = 3, Name = "c", isBestseller = true, AddingDate = new DateTime(2020,03,10,18,44,18) },
                new Product() { ProductId = 4, Name = "d", isBestseller = false, AddingDate = new DateTime(2020,03,10,18,42,33) },
                new Product() { ProductId = 5, Name = "e", isBestseller = true, AddingDate = new DateTime(2020,03,10,18,44,10) },
                new Product() { ProductId = 6, Name = "f", isBestseller = true, AddingDate = new DateTime(2020,03,10,18,42,21) },
            }.AsQueryable();

            mockCache.Setup(m => m.Get(It.IsAny<string>())).Returns(ListOfProducts);

            var mockSet = new Mock<IDbSet<Product>>();
            mockSet.As<IQueryable<Product>>().Setup(m => m.Provider).Returns(ListOfProducts.Provider);
            mockSet.As<IQueryable<Product>>().Setup(m => m.Expression).Returns(ListOfProducts.Expression);
            mockSet.As<IQueryable<Product>>().Setup(m => m.ElementType).Returns(ListOfProducts.ElementType);
            mockSet.As<IQueryable<Product>>().Setup(m => m.GetEnumerator()).Returns(ListOfProducts.GetEnumerator());

            var mockContext = new Mock<IProductDbContext>();
            mockContext.Setup(m => m.Products).Returns(mockSet.Object);

            var service = new ContextServices(mockContext.Object, mockCache.Object, mockClock.Object);

            //Act
            var result = service.GetBestsellersAndNewsForMainPage();

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<HomeViewModel>(result);
            Assert.AreEqual(3, result.Bestsellers.Count());
            Assert.AreEqual(3, result.New.Count());
            Assert.AreEqual("c", result.New.OrderByDescending(s => s.AddingDate).Select(x => x.Name).First());          
        }

        [Test]
        public void GetAllMainCategories_Should_ReturnAllMainCategories()
        {
            //Arrange          
            var ListOfMainCategories = new List<MainCategory>()
            {
                new MainCategory() { MainCategoryId = 1, Name = "a"},
                new MainCategory() { MainCategoryId = 2, Name = "b"},
                new MainCategory() { MainCategoryId = 3, Name = "c"},
                new MainCategory() { MainCategoryId = 4, Name = "d"},
                new MainCategory() { MainCategoryId = 5, Name = "e"},
            }.AsQueryable();

            var mockSet = new Mock<IDbSet<MainCategory>>();
            mockSet.As<IQueryable<MainCategory>>().Setup(m => m.Provider).Returns(ListOfMainCategories.Provider);
            mockSet.As<IQueryable<MainCategory>>().Setup(m => m.Expression).Returns(ListOfMainCategories.Expression);
            mockSet.As<IQueryable<MainCategory>>().Setup(m => m.ElementType).Returns(ListOfMainCategories.ElementType);
            mockSet.As<IQueryable<MainCategory>>().Setup(m => m.GetEnumerator()).Returns(ListOfMainCategories.GetEnumerator());

            var mockContext = new Mock<IProductDbContext>();
            mockContext.Setup(x => x.MainCategories).Returns(mockSet.Object);

            var service = new ContextServices(mockContext.Object, mockCache.Object, mockClock.Object);

            //Act
            var result = service.GetAllMainCategories();

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<List<MainCategory>>(result);
            Assert.AreEqual(5, result.Count);
        }

        [Test]
        [TestCase(1)]
        public void GetProductsForGivenCategoryWithFilter_NoFilter_ReturnAllProductForGivenCategory(int idCategory)
        {
            //Arrange
            var listOfBrands = new List<Brand>()
            {
                new Brand() { BrandId = 1, brand = "x"},
                new Brand() { BrandId = 2, brand = "y"},
                new Brand() { BrandId = 3, brand = "z"}

            }.AsQueryable();

            var listOfProducts = new List<Product>()
            {
                new Product() { ProductId = 1, CategoryId = 1, Name = "abc", Brand = listOfBrands.Where(b => b.BrandId == 1).SingleOrDefault() },
                new Product() { ProductId = 2, CategoryId = 2, Name = "bac", Brand = listOfBrands.Where(b => b.BrandId == 2).SingleOrDefault() },
                new Product() { ProductId = 3, CategoryId = 2, Name = "bab", Brand = listOfBrands.Where(b => b.BrandId == 3).SingleOrDefault() },
                new Product() { ProductId = 4, CategoryId = 1, Name = "acb", Brand = listOfBrands.Where(b => b.BrandId == 1).SingleOrDefault() },
                new Product() { ProductId = 5, CategoryId = 1, Name = "cab", Brand = listOfBrands.Where(b => b.BrandId == 2).SingleOrDefault() }

            }.AsQueryable();

            var mockSet = new Mock<IDbSet<Product>>();
            mockSet.As<IQueryable<Product>>().Setup(m => m.Provider).Returns(listOfProducts.Provider);
            mockSet.As<IQueryable<Product>>().Setup(m => m.Expression).Returns(listOfProducts.Expression);
            mockSet.As<IQueryable<Product>>().Setup(m => m.ElementType).Returns(listOfProducts.ElementType);
            mockSet.As<IQueryable<Product>>().Setup(m => m.GetEnumerator()).Returns(listOfProducts.GetEnumerator());

            var mockContext = new Mock<IProductDbContext>();
            mockContext.Setup(s => s.Products).Returns(mockSet.Object);

            var mockCache = new Mock<ICacheProvider>();

            var service = new ContextServices(mockContext.Object, mockCache.Object, mockClock.Object);

            //Act
            var result = service.GetProductsForGivenCategoryWithFilter(idCategory, null);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<List<Product>>(result);
            Assert.AreEqual(3, result.Count);

        }

        [Test]
        [TestCase(1, "ab")]
        public void GetProductsForGivenCategoryWithFilter_StringContainsFilterToLower_ReturnAllProductForGivenCategory(int idCategory, string searchTerm)
        {
            //Arrange
            var listOfBrands = new List<Brand>()
            {
                new Brand() { BrandId = 1, brand = "ABB"},
                new Brand() { BrandId = 2, brand = "BAB"},
                new Brand() { BrandId = 3, brand = "CBA"}

            }.AsQueryable();

            var listOfProducts = new List<Product>()
            {
                new Product() { ProductId = 1, CategoryId = 1, Name = "ABC", Brand = listOfBrands.Where(b => b.BrandId == 1).SingleOrDefault() },
                new Product() { ProductId = 2, CategoryId = 2, Name = "BAC", Brand = listOfBrands.Where(b => b.BrandId == 3).SingleOrDefault() },
                new Product() { ProductId = 3, CategoryId = 2, Name = "BAB", Brand = listOfBrands.Where(b => b.BrandId == 1).SingleOrDefault() },
                new Product() { ProductId = 4, CategoryId = 1, Name = "ACB", Brand = listOfBrands.Where(b => b.BrandId == 3).SingleOrDefault() },
                new Product() { ProductId = 5, CategoryId = 1, Name = "CAB", Brand = listOfBrands.Where(b => b.BrandId == 2).SingleOrDefault() }

            }.AsQueryable();

            var mockSet = new Mock<IDbSet<Product>>();
            mockSet.As<IQueryable<Product>>().Setup(m => m.Provider).Returns(listOfProducts.Provider);
            mockSet.As<IQueryable<Product>>().Setup(m => m.Expression).Returns(listOfProducts.Expression);
            mockSet.As<IQueryable<Product>>().Setup(m => m.ElementType).Returns(listOfProducts.ElementType);
            mockSet.As<IQueryable<Product>>().Setup(m => m.GetEnumerator()).Returns(listOfProducts.GetEnumerator());

            var mockContext = new Mock<IProductDbContext>();
            mockContext.Setup(s => s.Products).Returns(mockSet.Object);         

            var service = new ContextServices(mockContext.Object, mockCache.Object, mockClock.Object);

            //Act
            var result = service.GetProductsForGivenCategoryWithFilter(idCategory, searchTerm);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<List<Product>>(result);
            Assert.AreEqual(2, result.Count);

        }

        [Test]
        [TestCase(1, "BA")]
        public void GetProductsForGivenCategoryWithFilter_StringContainsFilterToUpper_ReturnAllProductForGivenCategory(int idCategory, string searchTerm)
        {
            //Arrange
            var listOfBrands = new List<Brand>()
            {
                new Brand() { BrandId = 1, brand = "abb"},
                new Brand() { BrandId = 2, brand = "bab"},
                new Brand() { BrandId = 3, brand = "cba"}

            }.AsQueryable();

            var listOfProducts = new List<Product>()
            {
                new Product() { ProductId = 1, CategoryId = 1, Name = "abc", Brand = listOfBrands.Where(b => b.BrandId == 1).SingleOrDefault() },
                new Product() { ProductId = 2, CategoryId = 2, Name = "bac", Brand = listOfBrands.Where(b => b.BrandId == 3).SingleOrDefault() },
                new Product() { ProductId = 3, CategoryId = 2, Name = "bab", Brand = listOfBrands.Where(b => b.BrandId == 1).SingleOrDefault() },
                new Product() { ProductId = 4, CategoryId = 1, Name = "acb", Brand = listOfBrands.Where(b => b.BrandId == 3).SingleOrDefault() },
                new Product() { ProductId = 5, CategoryId = 1, Name = "cab", Brand = listOfBrands.Where(b => b.BrandId == 2).SingleOrDefault() }

            }.AsQueryable();

            var mockSet = new Mock<IDbSet<Product>>();
            mockSet.As<IQueryable<Product>>().Setup(m => m.Provider).Returns(listOfProducts.Provider);
            mockSet.As<IQueryable<Product>>().Setup(m => m.Expression).Returns(listOfProducts.Expression);
            mockSet.As<IQueryable<Product>>().Setup(m => m.ElementType).Returns(listOfProducts.ElementType);
            mockSet.As<IQueryable<Product>>().Setup(m => m.GetEnumerator()).Returns(listOfProducts.GetEnumerator());

            var mockContext = new Mock<IProductDbContext>();
            mockContext.Setup(s => s.Products).Returns(mockSet.Object);

            var service = new ContextServices(mockContext.Object, mockCache.Object, mockClock.Object);

            //Act
            var result = service.GetProductsForGivenCategoryWithFilter(idCategory, searchTerm);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<List<Product>>(result);
            Assert.AreEqual(2, result.Count);

        }

        [Test]
        [TestCase(1)]
        public void GetProductsForGivenMainCategoryWithFilter_NoFilter_ReturnAllProductForGivenMainCategory(int idMainCategory)
        {
            //Arrange
           
            var ListOfCategories = new List<Category>()
            {
                new Category() { CategoryId = 1, MainCategoryId = 1, Name = "a"},
                new Category() { CategoryId = 2, MainCategoryId = 2, Name = "b"},
                new Category() { CategoryId = 3, MainCategoryId = 2, Name = "c"},

            }.AsQueryable();

            var ListOfProducts = new List<Product>()
            {
                new Product() { ProductId = 1, Category = ListOfCategories.Where(c => c.MainCategoryId == 1).First()},
                new Product() { ProductId = 2, Category = ListOfCategories.Where(c => c.MainCategoryId == 2).First()},
                new Product() { ProductId = 3, Category = ListOfCategories.Where(c => c.MainCategoryId == 1).First()},

            }.AsQueryable();


            var mockSet = new Mock<IDbSet<Product>>();
            mockSet.As<IQueryable<Product>>().Setup(m => m.Provider).Returns(ListOfProducts.Provider);
            mockSet.As<IQueryable<Product>>().Setup(m => m.Expression).Returns(ListOfProducts.Expression);
            mockSet.As<IQueryable<Product>>().Setup(m => m.ElementType).Returns(ListOfProducts.ElementType);
            mockSet.As<IQueryable<Product>>().Setup(m => m.GetEnumerator()).Returns(ListOfProducts.GetEnumerator());


            var mockContext = new Mock<IProductDbContext>();
            mockContext.Setup(c => c.Products).Returns(mockSet.Object);

            var service = new ContextServices(mockContext.Object, mockCache.Object, mockClock.Object);

            //Act
            var result = service.GetProductsForGivenMainCategoryWithFilter(idMainCategory, null);


            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<List<Product>>(result);
            Assert.AreEqual(2, result.Count);
        }

        [Test]
        [TestCase(1, "ab")]
        public void GetProductsForGivenMainCategoryWithFilter_FilterContainsStringToLower_ReturnAllProductForGivenMainCategoryAndFromGivenFilter(int idMainCategory, string searchContain)
        {
            //Arrange
            
            var listOfBrands = new List<Brand>()
            {
                new Brand() { BrandId = 1, brand = "ab"},
                new Brand() { BrandId = 2, brand = "bc"},
                new Brand() { BrandId = 3, brand = "cd"},

            }.AsQueryable();

            var listOfCategories = new List<Category>()
            {
                new Category() { CategoryId = 1, MainCategoryId = 1},
                new Category() { CategoryId = 2, MainCategoryId = 2},
                new Category() { CategoryId = 3, MainCategoryId = 1}

            }.AsQueryable();

            var listOfProducts = new List<Product>()
            {
                new Product() { ProductId = 1, Name = "ABC", Brand = listOfBrands.Where(b => b.BrandId == 1).FirstOrDefault(), Category = listOfCategories.Where(c => c.CategoryId == 1).First() },
                new Product() { ProductId = 2, Name = "ACB", Brand = listOfBrands.Where(b => b.BrandId == 2).FirstOrDefault(), Category = listOfCategories.Where(c => c.CategoryId == 2).First() },
                new Product() { ProductId = 3, Name = "ACC", Brand = listOfBrands.Where(b => b.BrandId == 1).FirstOrDefault(), Category = listOfCategories.Where(c => c.CategoryId == 1).First() },
                new Product() { ProductId = 4, Name = "ABB", Brand = listOfBrands.Where(b => b.BrandId == 3).FirstOrDefault(), Category = listOfCategories.Where(c => c.CategoryId == 2).First() },
                new Product() { ProductId = 5, Name = "CAB", Brand = listOfBrands.Where(b => b.BrandId == 3).FirstOrDefault(), Category = listOfCategories.Where(c => c.CategoryId == 2).First() }

            }.AsQueryable();

            var mockSet = new Mock<IDbSet<Product>>();
            mockSet.As<IQueryable<Product>>().Setup(m => m.Provider).Returns(listOfProducts.Provider);
            mockSet.As<IQueryable<Product>>().Setup(m => m.Expression).Returns(listOfProducts.Expression);
            mockSet.As<IQueryable<Product>>().Setup(m => m.ElementType).Returns(listOfProducts.ElementType);
            mockSet.As<IQueryable<Product>>().Setup(m => m.GetEnumerator()).Returns(listOfProducts.GetEnumerator());

            var mockContext = new Mock<IProductDbContext>();
            mockContext.Setup(s => s.Products).Returns(mockSet.Object);

            var service = new ContextServices(mockContext.Object, mockCache.Object, mockClock.Object);

            //Act
            var result = service.GetProductsForGivenMainCategoryWithFilter(idMainCategory, searchContain);

            //Assert
            Assert.AreEqual(4, result.Count());
            Assert.IsInstanceOf<List<Product>>(result);
        }

        [Test]
        [TestCase(1, "AB")]
        public void GetProductsForGivenMainCategoryWithFilter_FilterContainsStringToUpper_ReturnAllProductForGivenMainCategoryAndFromGivenFilter(int idMainCategory, string searchContain)
        {
            //Arrange
            
            var listOfBrands = new List<Brand>()
            {
                new Brand() { BrandId = 1, brand = "ab"},
                new Brand() { BrandId = 2, brand = "bc"},
                new Brand() { BrandId = 3, brand = "cd"},

            }.AsQueryable();

            var listOfCategories = new List<Category>()
            {
                new Category() { CategoryId = 1, MainCategoryId = 1},
                new Category() { CategoryId = 2, MainCategoryId = 2},
                new Category() { CategoryId = 3, MainCategoryId = 1}

            }.AsQueryable();

            var listOfProducts = new List<Product>()
            {
                new Product() { ProductId = 1, Name = "abc", Brand = listOfBrands.Where(b => b.BrandId == 1).FirstOrDefault(), Category = listOfCategories.Where(c => c.CategoryId == 1).First() },
                new Product() { ProductId = 2, Name = "acb", Brand = listOfBrands.Where(b => b.BrandId == 2).FirstOrDefault(), Category = listOfCategories.Where(c => c.CategoryId == 2).First() },
                new Product() { ProductId = 3, Name = "acc", Brand = listOfBrands.Where(b => b.BrandId == 1).FirstOrDefault(), Category = listOfCategories.Where(c => c.CategoryId == 1).First() },
                new Product() { ProductId = 4, Name = "abb", Brand = listOfBrands.Where(b => b.BrandId == 3).FirstOrDefault(), Category = listOfCategories.Where(c => c.CategoryId == 2).First() },
                new Product() { ProductId = 5, Name = "cab", Brand = listOfBrands.Where(b => b.BrandId == 3).FirstOrDefault(), Category = listOfCategories.Where(c => c.CategoryId == 2).First() }

            }.AsQueryable();

            var mockSet = new Mock<IDbSet<Product>>();
            mockSet.As<IQueryable<Product>>().Setup(m => m.Provider).Returns(listOfProducts.Provider);
            mockSet.As<IQueryable<Product>>().Setup(m => m.Expression).Returns(listOfProducts.Expression);
            mockSet.As<IQueryable<Product>>().Setup(m => m.ElementType).Returns(listOfProducts.ElementType);
            mockSet.As<IQueryable<Product>>().Setup(m => m.GetEnumerator()).Returns(listOfProducts.GetEnumerator());

            var mockContext = new Mock<IProductDbContext>();
            mockContext.Setup(s => s.Products).Returns(mockSet.Object);

            var service = new ContextServices(mockContext.Object, mockCache.Object, mockClock.Object);

            //Act
            var result = service.GetProductsForGivenMainCategoryWithFilter(idMainCategory, searchContain);

            //Assert
            Assert.AreEqual(4, result.Count());
            Assert.IsInstanceOf<List<Product>>(result);
        }

        [Test]
        public void GetProductsWhichAreBestsellers_Should_ReturnAllBestsellersProducts()
        {
            //Arrange
            
            var ListOfProducts = new List<Product>()
            {
                new Product() { ProductId = 1, isBestseller = true },
                new Product() { ProductId = 2, isBestseller = false },
                new Product() { ProductId = 3, isBestseller = false },
                new Product() { ProductId = 4, isBestseller = false },
                new Product() { ProductId = 5, isBestseller = true },

            }.AsQueryable();

            var mockSet = new Mock<IDbSet<Product>>();
            mockSet.As<IQueryable<Product>>().Setup(m => m.Provider).Returns(ListOfProducts.Provider);
            mockSet.As<IQueryable<Product>>().Setup(m => m.Expression).Returns(ListOfProducts.Expression);
            mockSet.As<IQueryable<Product>>().Setup(m => m.ElementType).Returns(ListOfProducts.ElementType);
            mockSet.As<IQueryable<Product>>().Setup(m => m.GetEnumerator()).Returns(ListOfProducts.GetEnumerator());

            var mockContext = new Mock<IProductDbContext>();
            mockContext.Setup(c => c.Products).Returns(mockSet.Object);

            var service = new ContextServices(mockContext.Object, mockCache.Object, mockClock.Object);

            //Act
            var result = service.GetProductsWhichAreBestsellers();

            //Assert
            Assert.AreEqual(2, result.Count());
            Assert.IsInstanceOf<List<Product>>(result);
            Assert.IsNotNull(result);
        }

        [Test]
        public void GetProductsWhichAreNew_NotOlderThan30Days_ReturnAllNewProduct()
        {
            //Arrange
            
            
            var mockClock = new Mock<IClock>();
            mockClock.Setup(s => s.Now).Returns(new DateTime(2020, 4, 10));

            var listOfProducts = new List<Product>()
            {
                new Product() { ProductId = 1, AddingDate = new DateTime(2020, 4, 10) },
                new Product() { ProductId = 2, AddingDate = new DateTime(2020, 4, 2) },
                new Product() { ProductId = 3, AddingDate = new DateTime(2020, 3, 22) },
                new Product() { ProductId = 4, AddingDate = new DateTime(2020, 3, 7) },
                new Product() { ProductId = 5, AddingDate = new DateTime(2020, 2, 27) }

            }.AsQueryable();

            var mockSet = new Mock<IDbSet<Product>>();
            mockSet.As<IQueryable<Product>>().Setup(m => m.Provider).Returns(listOfProducts.Provider);
            mockSet.As<IQueryable<Product>>().Setup(m => m.Expression).Returns(listOfProducts.Expression);
            mockSet.As<IQueryable<Product>>().Setup(m => m.ElementType).Returns(listOfProducts.ElementType);
            mockSet.As<IQueryable<Product>>().Setup(m => m.GetEnumerator()).Returns(listOfProducts.GetEnumerator());

            var mockContext = new Mock<IProductDbContext>();
            mockContext.Setup(s => s.Products).Returns(mockSet.Object);

            var service = new ContextServices(mockContext.Object, mockCache.Object, mockClock.Object);

            //Act
            var result = service.GetProductsWhichAreNew();

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<List<Product>>(result);
            Assert.AreEqual(3, result.Count);
           
        }

        [Test]
        [TestCase(1)]
        public void GetCategoriesForGivenMainCategory_Should_ReturnAllCategoriesForGivenMainCategory(int idMainCategory)
        {
            //Arrange
            
            var listOfMainCategories = new List<MainCategory>()
            {
                new MainCategory() { MainCategoryId = 1, Name = "a"},
                new MainCategory() { MainCategoryId = 2, Name = "a"}

            }.AsQueryable();

            var listOfCategory = new List<Category>()
            {
                new Category() { CategoryId = 1, MainCategory = listOfMainCategories.Where(mc => mc.MainCategoryId == 1).FirstOrDefault()},
                new Category() { CategoryId = 2, MainCategory = listOfMainCategories.Where(mc => mc.MainCategoryId == 2).FirstOrDefault()},
                new Category() { CategoryId = 3, MainCategory = listOfMainCategories.Where(mc => mc.MainCategoryId == 1).FirstOrDefault()},
                new Category() { CategoryId = 4, MainCategory = listOfMainCategories.Where(mc => mc.MainCategoryId == 2).FirstOrDefault()},
                new Category() { CategoryId = 5, MainCategory = listOfMainCategories.Where(mc => mc.MainCategoryId == 1).FirstOrDefault()}

            }.AsQueryable();

            var mockSet = new Mock<IDbSet<Category>>();
            mockSet.As<IQueryable<Category>>().Setup(m => m.Provider).Returns(listOfCategory.Provider);
            mockSet.As<IQueryable<Category>>().Setup(m => m.Expression).Returns(listOfCategory.Expression);
            mockSet.As<IQueryable<Category>>().Setup(m => m.ElementType).Returns(listOfCategory.ElementType);
            mockSet.As<IQueryable<Category>>().Setup(m => m.GetEnumerator()).Returns(listOfCategory.GetEnumerator());

            var mockContext = new Mock<IProductDbContext>();
            mockContext.Setup(s => s.Categories).Returns(mockSet.Object);

            var service = new ContextServices(mockContext.Object, mockCache.Object, mockClock.Object);

            //Act
            var result = service.GetCategoriesForGivenMainCategory(idMainCategory);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<List<Category>>(result);
            Assert.AreEqual(3, result.Count);
        }

        [Test]
        [TestCase(1)]
        public void GetAllDetailsForGivenProductId_Should_ReturnProductViewModel(int id)
        {
            //Arrange
            
            var listOfProducts = new List<Product>()
            {
                new Product() { ProductId = 1 },
                new Product() { ProductId = 2 },
                new Product() { ProductId = 3 },
                new Product() { ProductId = 4 },
                new Product() { ProductId = 5 },

            }.AsQueryable();

            var listOfProductsVariant = new List<ProductVariant>()
            {
                new ProductVariant() { ProductVariantId = 1, ProductId = 1},
                new ProductVariant() { ProductVariantId = 2, ProductId = 2},
                new ProductVariant() { ProductVariantId = 3, ProductId = 4},
                new ProductVariant() { ProductVariantId = 4, ProductId = 1},
                new ProductVariant() { ProductVariantId = 5, ProductId = 5},

            }.AsQueryable();

            var mockSet = new Mock<IDbSet<Product>>();
            mockSet.As<IQueryable<Product>>().Setup(m => m.Provider).Returns(listOfProducts.Provider);
            mockSet.As<IQueryable<Product>>().Setup(m => m.Expression).Returns(listOfProducts.Expression);
            mockSet.As<IQueryable<Product>>().Setup(m => m.ElementType).Returns(listOfProducts.ElementType);
            mockSet.As<IQueryable<Product>>().Setup(m => m.GetEnumerator()).Returns(listOfProducts.GetEnumerator());

            var mockSet2 = new Mock<IDbSet<ProductVariant>>();
            mockSet2.As<IQueryable<ProductVariant>>().Setup(m => m.Provider).Returns(listOfProductsVariant.Provider);
            mockSet2.As<IQueryable<ProductVariant>>().Setup(m => m.Expression).Returns(listOfProductsVariant.Expression);
            mockSet2.As<IQueryable<ProductVariant>>().Setup(m => m.ElementType).Returns(listOfProductsVariant.ElementType);
            mockSet2.As<IQueryable<ProductVariant>>().Setup(m => m.GetEnumerator()).Returns(listOfProductsVariant.GetEnumerator());

            var mockContext = new Mock<IProductDbContext>();
            mockContext.Setup(m => m.Products).Returns(mockSet.Object);
            mockContext.Setup(m => m.ProductsVariant).Returns(mockSet2.Object);

            var service = new ContextServices(mockContext.Object, mockCache.Object, mockClock.Object);

            //Act
            var result = service.GetAllDetailsForGivenProductId(id);

            //Assert
            Assert.IsInstanceOf<ProductViewModel>(result);          
        }










    }
}
