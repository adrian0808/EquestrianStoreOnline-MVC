namespace Sklep_internetowy.Migrations
{
    using Sklep_internetowy.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Validation;
    using System.Linq;


    internal sealed class Configuration : DbMigrationsConfiguration<Sklep_internetowy.ProductDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "Sklep_internetowy.ProductDbContext";
        }

        protected override void Seed(Sklep_internetowy.ProductDbContext context)
        {
            var ListOfMainCategories = new List<MainCategory>()
            {
                new MainCategory() { MainCategoryId = 1, Name = "Dla jeźdzca"},
                new MainCategory() { MainCategoryId = 2, Name = "Dla konia"},
                new MainCategory() { MainCategoryId = 3, Name = "Stajnia"},
                new MainCategory() { MainCategoryId = 4, Name = "Pasze i suplementy"},
            };

            ListOfMainCategories.ForEach(c => context.MainCategories.AddOrUpdate(c));
            context.SaveChanges();

            var ListOfCategories = new List<Category>()
            {
                 new Category() { CategoryId = 1, MainCategoryId = 2, Name = "Siodło", Description = "Część rzędu końskiego stanowiąca siedzenie przeznaczone dla jeźdźca." },
                 new Category() { CategoryId = 2, MainCategoryId = 2, Name = "Ochraniacze", Description = "Ochraniacze na końskie kończyny stosuje się wtedy, gdy zachodzi taka potrzeba, bo koń ma skłonności do ranienia własnych nóg." },
                 new Category() { CategoryId = 3, MainCategoryId = 2, Name = "Kaloszki", Description = "Kalosze dla koni zostały zaprojektowane po to, by chronić koronkę i piętkę kopyta przed urazami." },
                 new Category() { CategoryId = 4, MainCategoryId = 2, Name = "Owijki", Description = "Owijki służą do wspierania ścięgien w treningu, który wiąże się z większym ich obciążeniem, a także zapobiegania drobnym urazom." },
                 new Category() { CategoryId = 5, MainCategoryId = 2, Name = "Podkładka", Description = "Podkładki mają za zadanie poprawić sposób, w jakim sprzęt leży na końskim grzbiecie." },
                 new Category() { CategoryId = 6, MainCategoryId = 2, Name = "Kantar", Description = "Kantar umożliwia czyszczenie, prowadzenie i wykonywanie innych prac przy zwierzęciu." },
                 new Category() { CategoryId = 7, MainCategoryId = 2, Name = "Derka", Description = "Okrycie konia, zakrywające jego cały tułów (kłodę), zad i czasem szyję, wykonane z różnych materiałów w zależności od funkcji, jaką mają spełniać. Derki mocuje się paskami przebiegającymi w okolicy mocowania popręgu oraz między tylnymi nogami konia." },
                 new Category() { CategoryId = 8, MainCategoryId = 2, Name = "Ogłowie", Description = "Ogłowie to główna część rzędu jeździeckiego lub uprzęży. Głównym zadaniem ogłowia jest utrzymanie kiełzna (lub paska naciskającego na nos w wypadku ogłowi bezwędzidłowych) we właściwym miejscu w pysku konia" },
                 new Category() { CategoryId = 9, MainCategoryId = 2, Name = "Popręg", Description = "Element rzędu końskiego. Przebiega on pod brzuchem konia, łącząc obie strony terlicy. Jego głównym zadaniem jest utrzymanie siodła we właściwym położeniu na grzbiecie konia. " },
                 new Category() { CategoryId = 10, MainCategoryId = 2, Name = "Czaprak", Description = "Płócienna, filcowa, sukienna lub niekiedy futrzana podkładka pod siodło chroniąca grzbiet konia przed obtarciami, a siodło przed końskim potem; nazywana też potnikiem" },
                 new Category() { CategoryId = 11, MainCategoryId = 1, Name = "Bryczesy", Description = "Bryczesy z HKM" },
                 new Category() { CategoryId = 12, MainCategoryId = 1, Name = "Kask", Description = "Toczek z Samshielda" },
                 new Category() { CategoryId = 13, MainCategoryId = 1, Name = "Podkolanówki", Description = "Podkolanówki z Pikeura" }
            };

            ListOfCategories.ForEach(k => context.Categories.AddOrUpdate(k));
            context.SaveChanges();

            var ListOfBrands = new List<Brand>()
            {
                 new Brand() { BrandId = 1, brand = "Eskadron"},
                 new Brand() { BrandId = 2, brand = "Hippica"},
                 new Brand() { BrandId = 3, brand = "York"},
                 new Brand() { BrandId = 4, brand = "WINDEREN"},
                 new Brand() { BrandId = 5, brand = "EQUICK"},
                 new Brand() { BrandId = 6, brand = "DAW MAG"},
                 new Brand() { BrandId = 7, brand = "HKM"},
                 new Brand() { BrandId = 8, brand = "TORPOL"},
                 new Brand() { BrandId = 9, brand = "BR"},
                 new Brand() { BrandId = 10, brand = "Prestige"},
            };

            ListOfBrands.ForEach(b => context.Brands.AddOrUpdate(b));
            context.SaveChanges();

            var ListOfGenders = new List<Gender>()
            {
                 new Gender() { GenderId = 1, Name = "Women"},
                 new Gender() { GenderId = 2, Name = "Man"},
                 new Gender() { GenderId = 3, Name = "Human"},
                 new Gender() { GenderId = 4, Name = "Horse"},

            };

            ListOfGenders.ForEach(g => context.Genders.AddOrUpdate(g));
            context.SaveChanges();

            var ListOfColors = new List<Color>()
            {
                 new Color() { ColorId = 1, color = "Czerwony"},
                 new Color() { ColorId = 2, color = "Czarny"},
                 new Color() { ColorId = 3, color = "Brazowy"},
                 new Color() { ColorId = 4, color = "Bialy"},
                 new Color() { ColorId = 5, color = "Niebieski"},
                 new Color() { ColorId = 6, color = "Szary"},
                 new Color() { ColorId = 7, color = "Różowy"},
                 new Color() { ColorId = 8, color = "Złoty"},
                 new Color() { ColorId = 9, color = "Granatowy"},
            };

            ListOfColors.ForEach(c => context.Colors.AddOrUpdate(c));
            context.SaveChanges();

            var ListOfSizes = new List<Size>()
            {
                 new Size() { SizeId = 1, size = "brak"},
                 new Size() { SizeId = 2, size = "XS"},
                 new Size() { SizeId = 3, size = "S"},
                 new Size() { SizeId = 4, size = "M"},
                 new Size() { SizeId = 5, size = "L"},
                 new Size() { SizeId = 6, size = "XL"},
                 new Size() { SizeId = 7, size = "XXL"},
                 new Size() { SizeId = 8, size = "17"},
                 new Size() { SizeId = 9, size = "18"},
                 new Size() { SizeId = 10, size = "19"},
                 new Size() { SizeId = 11, size = "35-37"},
                 new Size() { SizeId = 12, size = "38-40"},
                 new Size() { SizeId = 13, size = "40-42"},
                 new Size() { SizeId = 14, size = "400cm"},
                 new Size() { SizeId = 15, size = "FULL"},
                 new Size() { SizeId = 16, size = "SHEETY"},
                 new Size() { SizeId = 17, size = "100"},
                 new Size() { SizeId = 18, size = "105"},
                 new Size() { SizeId = 19, size = "110"},
                 new Size() { SizeId = 20, size = "125"},
            };

            ListOfSizes.ForEach(s => context.Sizes.AddOrUpdate(s));
            context.SaveChanges();


            var ListOfProducts = new List<Product>()
            {
                new Product() { ProductId = 1, CategoryId = 1, BrandId = 6, GenderId = 4, Name = "Siodło Rajdowe MAX Standard",
                    Description = "Zaprojektowane specjalnie z myślą o rajdach siodło Max posiada bardzo głębokie siedzisko dodatkowo przeszywane, co zapobiega przesuwaniu się jeźdzca w siodle w trudnym terenie oraz zapewnia komfortowa miękkość. Dzięki umieszczeniu siedziska na długich ławkach z miękkim podbiciem uzyskano konieczną podczas długich rajdów wentylację pod siodłem oraz przyleganie do grzbietu konia na większej powierzchni, co zapewnia zwięrzeciu wysoki komfort. Dodatkowo w tylnych częściach ławek znajdują się uchwyty ze stali nierdzewnej, do których można przytroczyć sakwy.Siodło wyposażone jest w wysokie przednie i tylne klocki oraz miękka poduszkę kolanową. Posiada także elastyczną terlicę oraz mocno osadzony uchwyt do przytrzymania się w razie konieczności. Siedzisko i poduszki kolanowe wykonane są z wysokiej jakości skóry antypoślizgowej.",
                    Price = 3370, GraphicFileName = "SiodloRajdoweMAXStandard.png", AddingDate = DateTime.Now, isBestseller = true, isSize = true, isColor = true },

                new Product() { ProductId = 2, CategoryId = 2, BrandId = 5, GenderId = 4, Name = "Ochraniacze eSHOCK, PRZEDNIE",
                    Description = "Wysokiej jakości ochraniacze wyposażone w system eFLUIDGEL. Wykonane z wysokiej jakości trwałych materiałów. Anatomiczny kształt doskonale chroni ścięgna przed urazami i zapewnia maksimum komfortu podczas użytkowania. Z zewnątrz wzmocnione tworzywem TPU. Wewnątrz wyściełanie miękkim neoprenem zapobiegającym otarciom i odparzeniom. Łatwa regulacja za pomocą pasków zapinanych na kołki.",
                    Price = 545, GraphicFileName = "EQUICK_Ochraniacze_eSHOCK_przednie.png", AddingDate = DateTime.Now, isBestseller = false, isSize = true, isColor = true },

                new Product() { ProductId = 3, CategoryId = 3, BrandId = 7, GenderId = 4, Name = "Kaloszki SPACE",
                    Description = "Wysokiej jakości kaloszki firmy HKM.",
                    Price = 113, GraphicFileName = "HKM_Space_Kaloszki_Rozowe.png", AddingDate = DateTime.Now, isBestseller = false, isSize = true, isColor = true },

                new Product() { ProductId = 4, CategoryId = 4, BrandId = 7, GenderId = 4, Name = "Owijki wełniane WOOL",
                    Description = "TORPOL, Owijki wełniane WOOL. Świetne właściwości termoregulacyjne",
                    Price = 279, GraphicFileName = "TORPOL_Owijki_WOOL.png", AddingDate = DateTime.Now, isBestseller = false, isSize = true, isColor = true },

                new Product() { ProductId = 5, CategoryId = 5, BrandId = 4, GenderId = 4, Name = "Podkładka skokowa BACK PROTECT SOLUTION",
                    Description = "Zawaansowana technologicznie podkładka firmy Winderen. Innowacyjna warstowa budowa zapewnia wyjątkowy komfort nie tylko jeźdźcom ale i koniom.",
                    Price = 956, GraphicFileName = "WINDEREN_Podkladka_skokowa_back_protect_solution.png", AddingDate = DateTime.Now, isBestseller = true, isSize = true, isColor = true },

                new Product() { ProductId = 6, CategoryId = 6, BrandId = 9, GenderId = 4, Name = "Kantar EQUISITE",
                    Description = "Kantar wykonany z nylonowej taśmy.",
                    Price = 95, GraphicFileName = "BR_Kantar_Equisite_Zloty.png", AddingDate = DateTime.Now, isBestseller = true, isSize = true, isColor = true },

                new Product() { ProductId = 7, CategoryId = 7, BrandId = 7, GenderId = 4, Name = "Derka podszyta polarem",
                    Description = "Wiatroodporna, wodoodporna. Pasy krzyzowe,klapka na ogon. 100% poliester, 600D material poliester, 600D mocny material zewnetrzny.",
                    Price = 95, GraphicFileName = "Derka_podszyta_HKM_Granatowa.png", AddingDate = DateTime.Now, isBestseller = false, isSize = true, isColor = true },

                new Product() { ProductId = 8, CategoryId = 8, BrandId = 7, GenderId = 4, Name = "Ogłowie LOU",
                    Description = "Ogłowie firmy HKM. Wykonane ze skóry bydlęcej",
                    Price = 147, GraphicFileName = "HKM_Oglowie_LUO_Czarne.png", AddingDate = DateTime.Now, isBestseller = false, isSize = true, isColor = true },

                new Product() { ProductId = 9, CategoryId = 9, BrandId = 10, GenderId = 4, Name = "Popręg anatomiczny symetryczny z gumami",
                    Description = "Popręg wykonany z wysokiej jakości impregnowanej skóry, co zapobiega wchłanianiu potu.",
                    Price = 950, GraphicFileName = "PRESTIGE_Popreg_Czarny_Zgumami.png", AddingDate = DateTime.Now, isBestseller = false, isSize = true, isColor = true },

                new Product() { ProductId = 10, CategoryId = 10, BrandId = 7, GenderId = 4, Name = "Czaprak ujeżdżeniowy ANATOMIC TOP LINE",
                    Description = "Wysokiej jakości czaprak firmy Winderen. Anatomicznie profil zaprojektowany aby dopasować do krzywizm grzbietu konia.",
                    Price = 315, GraphicFileName = "Czaprak_Ujezdzeniowy_WINDEREN_TOPLine.png", AddingDate = DateTime.Now, isBestseller = false, isSize = false, isColor = true },

                new Product() { ProductId = 11, CategoryId = 11, BrandId = 7, GenderId = 1, Name = "Bryczesy JEANS 56 damskie",
                    Description = "Bryczesy HKM  KINGSTON JEANS 3/4 ALOS +SILIKON",
                    Price = 390, GraphicFileName = "Bryczesy_HKM_Kingston_Jeans56.png", AddingDate = DateTime.Now, isBestseller = false, isSize = true, isColor = true },

                new Product() { ProductId = 12, CategoryId = 12, BrandId = 3, GenderId = 3, Name = "Kaskotoczek z regulacją czarny",
                    Description = "Nowy model kaskoteczka chroniacy glowe jezdzca. Wyposazony w pokretlo umozliwiajace dopasowanie rozmiaru",
                    Price = 109, GraphicFileName = "Kaskotoczek_York_Regulacja_Czarny.png", AddingDate = DateTime.Now, isBestseller = true, isSize = true, isColor = true },

                new Product() { ProductId = 13, CategoryId = 13, BrandId = 7, GenderId = 3, Name = "Podkolanówki WINDSOR",
                    Description = "HKM, Podkolanówki WINDSOR. Podkolanówki fitrmy HKM. Skład: 80% bawełna, 17% poliester, 3% elastan.",
                    Price = 39, GraphicFileName = "HKM_Podkolanowki_Windsor.png", AddingDate = DateTime.Now, isBestseller = false, isSize = true, isColor = true }

            };

            ListOfProducts.ForEach(p => context.Products.AddOrUpdate(p));
            context.SaveChanges();

            var listOfProductsVariant = new List<ProductVariant>()
            {
                new ProductVariant() { ProductVariantId = 1, ProductId = 1, ColorId = 2, SizeId = 8},
                new ProductVariant() { ProductVariantId = 2, ProductId = 1, ColorId = 3, SizeId = 9},
                new ProductVariant() { ProductVariantId = 3, ProductId = 2, ColorId = 2, SizeId = 3},
                new ProductVariant() { ProductVariantId = 4, ProductId = 2, ColorId = 3, SizeId = 5},
                new ProductVariant() { ProductVariantId = 5, ProductId = 3, ColorId = 5, SizeId = 4},
                new ProductVariant() { ProductVariantId = 6, ProductId = 3, ColorId = 7, SizeId = 6},
                new ProductVariant() { ProductVariantId = 7, ProductId = 4, ColorId = 2, SizeId = 14},
                new ProductVariant() { ProductVariantId = 8, ProductId = 4, ColorId = 4, SizeId = 14},
                new ProductVariant() { ProductVariantId = 9, ProductId = 5, ColorId = 2, SizeId = 9},
                new ProductVariant() { ProductVariantId = 10, ProductId = 5, ColorId = 5, SizeId = 10},
                new ProductVariant() { ProductVariantId = 11, ProductId = 6, ColorId = 9, SizeId = 15},
                new ProductVariant() { ProductVariantId = 12, ProductId = 6, ColorId = 6, SizeId = 16},
                new ProductVariant() { ProductVariantId = 13, ProductId = 7, ColorId = 5, SizeId = 18},
                new ProductVariant() { ProductVariantId = 14, ProductId = 7, ColorId = 5, SizeId = 20},
                new ProductVariant() { ProductVariantId = 15, ProductId = 8, ColorId = 2, SizeId = 15},
                new ProductVariant() { ProductVariantId = 16, ProductId = 8, ColorId = 2, SizeId = 16},
                new ProductVariant() { ProductVariantId = 17, ProductId = 9, ColorId = 2, SizeId = 20},
                new ProductVariant() { ProductVariantId = 18, ProductId = 9, ColorId = 3, SizeId = 20},
                new ProductVariant() { ProductVariantId = 19, ProductId = 10, ColorId = 5, SizeId = 1},
                new ProductVariant() { ProductVariantId = 20, ProductId = 10, ColorId = 8, SizeId = 1},
                new ProductVariant() { ProductVariantId = 21, ProductId = 11, ColorId = 4, SizeId = 3},
                new ProductVariant() { ProductVariantId = 22, ProductId = 11, ColorId = 6, SizeId = 4},
                new ProductVariant() { ProductVariantId = 23, ProductId = 12, ColorId = 2, SizeId = 3},
                new ProductVariant() { ProductVariantId = 24, ProductId = 12, ColorId = 2, SizeId = 5},
                new ProductVariant() { ProductVariantId = 25, ProductId = 13, ColorId = 2, SizeId = 11},
                new ProductVariant() { ProductVariantId = 26, ProductId = 13, ColorId = 9, SizeId = 12},
            };

            listOfProductsVariant.ForEach(p => context.ProductsVariant.AddOrUpdate(p));
            context.SaveChanges();

            var listOfStocks = new List<Stock>()
            {
                new Stock() { ProductVariantId = 1, CountOfStock = 6, LastUpdateTime = DateTime.Now},
                new Stock() { ProductVariantId = 2, CountOfStock = 3, LastUpdateTime = DateTime.Now},
                new Stock() { ProductVariantId = 3, CountOfStock = 11, LastUpdateTime = DateTime.Now},
                new Stock() { ProductVariantId = 4, CountOfStock = 2, LastUpdateTime = DateTime.Now},
                new Stock() { ProductVariantId = 5, CountOfStock = 1, LastUpdateTime = DateTime.Now},
                new Stock() { ProductVariantId = 6, CountOfStock = 9, LastUpdateTime = DateTime.Now},
                new Stock() { ProductVariantId = 7, CountOfStock = 2, LastUpdateTime = DateTime.Now},
                new Stock() { ProductVariantId = 8, CountOfStock = 4, LastUpdateTime = DateTime.Now},
                new Stock() { ProductVariantId = 9, CountOfStock = 13, LastUpdateTime = DateTime.Now},
                new Stock() { ProductVariantId = 10, CountOfStock = 11, LastUpdateTime = DateTime.Now},
                new Stock() { ProductVariantId = 11, CountOfStock = 7, LastUpdateTime = DateTime.Now},
                new Stock() { ProductVariantId = 12, CountOfStock = 2, LastUpdateTime = DateTime.Now},
                new Stock() { ProductVariantId = 13, CountOfStock = 3, LastUpdateTime = DateTime.Now},
                new Stock() { ProductVariantId = 14, CountOfStock = 1, LastUpdateTime = DateTime.Now},
                new Stock() { ProductVariantId = 15, CountOfStock = 19, LastUpdateTime = DateTime.Now},
                new Stock() { ProductVariantId = 16, CountOfStock = 12, LastUpdateTime = DateTime.Now},
                new Stock() { ProductVariantId = 17, CountOfStock = 16, LastUpdateTime = DateTime.Now},
                new Stock() { ProductVariantId = 18, CountOfStock = 5, LastUpdateTime = DateTime.Now},
                new Stock() { ProductVariantId = 19, CountOfStock = 12, LastUpdateTime = DateTime.Now},
                new Stock() { ProductVariantId = 20, CountOfStock = 2, LastUpdateTime = DateTime.Now},
                new Stock() { ProductVariantId = 21, CountOfStock = 1, LastUpdateTime = DateTime.Now},
                new Stock() { ProductVariantId = 22, CountOfStock = 13, LastUpdateTime = DateTime.Now},
                new Stock() { ProductVariantId = 23, CountOfStock = 11, LastUpdateTime = DateTime.Now},
                new Stock() { ProductVariantId = 24, CountOfStock = 9, LastUpdateTime = DateTime.Now},
                new Stock() { ProductVariantId = 25, CountOfStock = 14, LastUpdateTime = DateTime.Now},
                new Stock() { ProductVariantId = 26, CountOfStock = 7, LastUpdateTime = DateTime.Now},
            };                
        }                     
    }                         
}                             
                              
                              