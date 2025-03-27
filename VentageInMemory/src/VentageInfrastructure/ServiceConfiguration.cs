using Microsoft.Data.Sqlite;
using System.Data;
using Microsoft.Extensions.DependencyInjection;
using Dapper;
using Microsoft.Extensions.Configuration;
using VentageInfrastructure.DataAccess;
using VentageApplication.IRepository;
using VentageInfrastructure.Repository;
using VentageApplication.StandardResponse;
using FluentValidation;
using MediatR;
using VentageApplication.Features.Customer.Command.Validations;

namespace VentageInfrastructure
{
    public static class ServiceConfiguration
    {
        public static IServiceCollection InfrastructureSerives(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddTransient<Customer>()
                 .AddTransient<ICustomerRepository, CustomerRepository>()
                 .AddTransient<Response>()
                 .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationsPipeline<,>))
                 .AddValidatorsFromAssemblyContaining<CustomerRequestValidation>()
                 .AddValidatorsFromAssemblyContaining<CustomerUpdateRequestValidations>()
                 .AddSingleton<IDbConnection>(sp =>
            {
                var connection = new SqliteConnection(configuration.GetConnectionString("InMemory"));
                connection.Open();

                // Enable foreign key constraints in SQLite
                connection.Execute("PRAGMA foreign_keys = ON;");

                // Create tables
                connection.Execute(@"
                    CREATE TABLE IF NOT EXISTS Customers (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Name TEXT NOT NULL,
                        PhoneNumber TEXT,
                        Website TEXT,
                        IsDeleted BOOLEAN NOT NULL DEFAULT 0,
                        DateCreated TEXT DEFAULT (datetime('now'))
                    );

                    CREATE TABLE IF NOT EXISTS Contacts (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        FirstName TEXT,
                        LastName TEXT,
                        EmailAddress TEXT,
                        PhoneNumber TEXT,
                        CustomerId INTEGER,
                        IsDeleted BOOLEAN NOT NULL DEFAULT 1,
                        FOREIGN KEY (CustomerId) REFERENCES Customers (Id)
                    );

                    CREATE TABLE IF NOT EXISTS CustomerAddress (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        CustomerId INTEGER,
                        CountryId INTEGER,
                        Address TEXT,
                        PostCode TEXT,
                        IsDeleted BOOLEAN NOT NULL DEFAULT 1,
                        FOREIGN KEY (CustomerId) REFERENCES Customers (Id),
                        FOREIGN KEY (CountryId) REFERENCES Country(Id)
                    );

                    CREATE TABLE IF NOT EXISTS Country (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Name TEXT
                    );
                ");

                // Insert default countries
                connection.Execute(@"
                    INSERT INTO Country (Name) VALUES ('United Kingdom');
                    INSERT INTO Country (Name) VALUES ('United States');
                    INSERT INTO Country (Name) VALUES ('Nigeria');
                    INSERT INTO Country (Name) VALUES ('South Africa');
                ");

                return connection;
            });
        }
    }
}
