namespace Sklep_internetowy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Brands",
                c => new
                    {
                        BrandId = c.Int(nullable: false, identity: true),
                        brand = c.String(),
                    })
                .PrimaryKey(t => t.BrandId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        CategoryId = c.Int(nullable: false),
                        BrandId = c.Int(nullable: false),
                        GenderId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 100),
                        Description = c.String(nullable: false, maxLength: 1000),
                        isBestseller = c.Boolean(nullable: false),
                        isSize = c.Boolean(nullable: false),
                        isColor = c.Boolean(nullable: false),
                        AddingDate = c.DateTime(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        GraphicFileName = c.String(),
                        Color_ColorId = c.Int(),
                    })
                .PrimaryKey(t => t.ProductId)
                .ForeignKey("dbo.Brands", t => t.BrandId, cascadeDelete: true)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.Genders", t => t.GenderId, cascadeDelete: true)
                .ForeignKey("dbo.Colors", t => t.Color_ColorId)
                .Index(t => t.CategoryId)
                .Index(t => t.BrandId)
                .Index(t => t.GenderId)
                .Index(t => t.Color_ColorId);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        MainCategoryId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 100),
                        Description = c.String(nullable: false, maxLength: 300),
                    })
                .PrimaryKey(t => t.CategoryId)
                .ForeignKey("dbo.MainCategory", t => t.MainCategoryId, cascadeDelete: true)
                .Index(t => t.MainCategoryId);
            
            CreateTable(
                "dbo.MainCategory",
                c => new
                    {
                        MainCategoryId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.MainCategoryId);
            
            CreateTable(
                "dbo.Genders",
                c => new
                    {
                        GenderId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.GenderId);
            
            CreateTable(
                "dbo.ProductsVariant",
                c => new
                    {
                        ProductVariantId = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        SizeId = c.Int(nullable: false),
                        ColorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProductVariantId)
                .ForeignKey("dbo.Colors", t => t.ColorId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.Sizes", t => t.SizeId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.SizeId)
                .Index(t => t.ColorId);
            
            CreateTable(
                "dbo.Colors",
                c => new
                    {
                        ColorId = c.Int(nullable: false, identity: true),
                        color = c.String(),
                    })
                .PrimaryKey(t => t.ColorId);
            
            CreateTable(
                "dbo.Sizes",
                c => new
                    {
                        SizeId = c.Int(nullable: false, identity: true),
                        size = c.String(),
                    })
                .PrimaryKey(t => t.SizeId);
            
            CreateTable(
                "dbo.Stocks",
                c => new
                    {
                        ProductVariantId = c.Int(nullable: false),
                        CountOfStock = c.Int(nullable: false),
                        LastUpdateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ProductVariantId)
                .ForeignKey("dbo.ProductsVariant", t => t.ProductVariantId)
                .Index(t => t.ProductVariantId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Stocks", "ProductVariantId", "dbo.ProductsVariant");
            DropForeignKey("dbo.ProductsVariant", "SizeId", "dbo.Sizes");
            DropForeignKey("dbo.ProductsVariant", "ProductId", "dbo.Products");
            DropForeignKey("dbo.ProductsVariant", "ColorId", "dbo.Colors");
            DropForeignKey("dbo.Products", "Color_ColorId", "dbo.Colors");
            DropForeignKey("dbo.Products", "GenderId", "dbo.Genders");
            DropForeignKey("dbo.Products", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Categories", "MainCategoryId", "dbo.MainCategory");
            DropForeignKey("dbo.Products", "BrandId", "dbo.Brands");
            DropIndex("dbo.Stocks", new[] { "ProductVariantId" });
            DropIndex("dbo.ProductsVariant", new[] { "ColorId" });
            DropIndex("dbo.ProductsVariant", new[] { "SizeId" });
            DropIndex("dbo.ProductsVariant", new[] { "ProductId" });
            DropIndex("dbo.Categories", new[] { "MainCategoryId" });
            DropIndex("dbo.Products", new[] { "Color_ColorId" });
            DropIndex("dbo.Products", new[] { "GenderId" });
            DropIndex("dbo.Products", new[] { "BrandId" });
            DropIndex("dbo.Products", new[] { "CategoryId" });
            DropTable("dbo.Stocks");
            DropTable("dbo.Sizes");
            DropTable("dbo.Colors");
            DropTable("dbo.ProductsVariant");
            DropTable("dbo.Genders");
            DropTable("dbo.MainCategory");
            DropTable("dbo.Categories");
            DropTable("dbo.Products");
            DropTable("dbo.Brands");
        }
    }
}
