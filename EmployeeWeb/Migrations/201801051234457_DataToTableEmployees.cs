namespace EmployeeWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DataToTableEmployees : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Employees (Name, Address1, Address2, City, State, Country, Zipcode, Email) VALUES ('John','aa','aa','aa','aa','aa','aa','aa')");
            Sql("INSERT INTO Employees (Name, Address1, Address2, City, State, Country, Zipcode, Email) VALUES ('Mary','aa','aa','aa','aa','aa','aa','aa')");
        }
        
        public override void Down()
        {
        }
    }
}
