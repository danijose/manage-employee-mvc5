namespace EmployeeWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreatedTableEmployees1 : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Employees (Name, Address1, Address2, City, State, Country, Zipcode, Email) VALUES ('Alex','aa','aa','aa','aa','aa','aa','aa')");
            Sql("INSERT INTO Employees (Name, Address1, Address2, City, State, Country, Zipcode, Email) VALUES ('Clara','aa','aa','aa','aa','aa','aa','aa')");
        }
        
        public override void Down()
        {
            Sql("DELETE FROM Employees where Name = 'Alex'");
            Sql("DELETE FROM Employees where Name = 'Clara'");
        }
    }
}
