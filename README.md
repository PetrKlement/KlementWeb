# O webu 
Aplikace poslední z mých projektů a je založena na .NET frameworku, konkrétně při využití ASP.NET Core. Je rozdělena do částí články, o mně, kontakt, správy uživatele a administrátorské sekce. Web lze vyzkoušet na mých stránkách www.klementpetr.cz.

# Uživatel
Správa uživatelů, rolí a autorizace vychází z ASP.NET Identity. Návštěvník stránky po přihlášení (nebo prvotní registraci) může spravovat svůj účet. Práva jsou rozdělena do rolí, tedy například editace či vytváření článků může provádět pouze admin.

# Kontakt
Využívám nuget MailKit, který umožňuje uživatelům mě kontaktovat pomocí emailu. Pokud chcete ze stažené aplikace zasílat emaily, musíte v konfiguračním souboru vyplnit data vašeho emailového klienta.

# Články
Zde jsou vystaveny některé ze článků, kterých jsem autorem. Pro přístup k datům využívám LocalDB (lightverze SQL Server Express), takže pro nasazení programu je třeba ji založit (návod - viz. Spuštění aplikace). Na internetových stránkách je použito MS SQL. 

# Vyhledávání článků
V poli pro vyhledávání článků může uživatel fulltextově vyhledávat články dle názvu a obsahu.

# Databáze
V projektu je použita LocalDB (lightverze SQL Server Express). K editaci dat v Db využívám LINQ dotazy. Databáze jsou generovány za pomoci Entity Frameworku Core s metodikou Code First. Na stránkách je použit MS SQL Server 2014. 

# Technologie
Technologií je ASP.NET Core MVC. Integrační vrstva využívá návrhového vzoru Repository. Aplikace je rozdělena do datové (integrační), business a aplikační vrstvy. Závislosti jsou předávány na pomoci Dependency Injection. K databázím přistupuji pomocí Entity Framework Core. K odesílání emailů je využit MailKit. FE se skládá z kombinace HTML, CSS a JavaScriptu a využívám Bootstrap knihovny. Správu uživatelů zajišťuje ASP.NET Identity. 

# Návod na zprovoznění aplikace
Nejdříve je třeba založit databázi (LocalDB). Následně spustíte migrační skripty. V konzoli Package manager Console nastavte Default project na KlementWeb.Data. Migraci přidáte příkazem Add-migration názevVašíMigrace. Poté aplikujte migraci příkazem Update-Database. V SQL Server Object Explorer si můžete novou databázi rozkliknout. Ve složce C:\Users\vašeUživatelskéJméno se vygeneruje databázový soubor.
Jméno souboru vložte místo databaseName v appsettings.json. Dále je potřeba nastavit emailového klienta a emaily pro zprovoznění odesílaní emailů. To provedete nastavením emailReciever v kontroléru ContactController.cs a emailSender ve třídě Email.cs. Poté musíte nakonfigurovat data vašeho emailového klienta v appsettings.json. Nakonec je třeba nastavit administrátorský přístup v aplikaci. Ve třídě Startup.cs odkomentujte řádky 79 až 81 a místo adminEmail vložte email pod kterým chcete mít administrátorská práva. Poté se pod tímto emailem registrujte na webu.
