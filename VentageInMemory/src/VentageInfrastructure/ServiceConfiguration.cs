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
using VentageApplication.Features.Gender.Command.Validations;

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
                 .AddTransient<Gender>()
                 .AddTransient<IGenderRepository, GenderRepository>()
                 .AddValidatorsFromAssemblyContaining<GenderRequestValidation>()
                 .AddValidatorsFromAssemblyContaining<GenderRequestValidator>()
                 .AddTransient<ICustomerAddressRepository, CustomerAddressRepository>()
                 .AddTransient<CustomerAddress>()
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
                        FirstName TEXT NOT NULL,
                        LastName TEXT NOT NULL,
                        GenderId INTEGER,
                        Website TEXT,
                        IsDeleted BOOLEAN NOT NULL DEFAULT 0,
                        DateCreated TEXT DEFAULT (datetime('now')),
                        FOREIGN KEY (GenderId) REFERENCES Gender(Id) ON DELETE SET NULL
                    );

                    CREATE TABLE IF NOT EXISTS Contacts (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        EmailAddress TEXT,
                        PhoneNumber TEXT,
                        CustomerId INTEGER,
                        IsDeleted BOOLEAN NOT NULL DEFAULT 0,
                        FOREIGN KEY (CustomerId) REFERENCES Customers (Id)
                    );

                    CREATE TABLE IF NOT EXISTS CustomerAddress (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        CustomerId INTEGER,
                        CountryId INTEGER,
                        Address TEXT,
                        PostCode TEXT,
                        IsDeleted BOOLEAN NOT NULL DEFAULT 0,
                        FOREIGN KEY (CustomerId) REFERENCES Customers (Id),
                        FOREIGN KEY (CountryId) REFERENCES Country(Id)
                    );

                     CREATE TABLE IF NOT EXISTS UserAuthentication (
                     Id INTEGER PRIMARY KEY AUTOINCREMENT,
                     CustomerId INTEGER,                    
                     Username TEXT NOT NULL UNIQUE,
                     Password_hash TEXT NOT NULL,
                     Lastlogin TEXT DEFAULT (datetime('now')),           
                     FailedAttempts INTEGER DEFAULT 0,
                     IsDisabled BOOLEAN NOT NULL DEFAULT 0,
                     FOREIGN KEY (CustomerId) REFERENCES Customers(Id) ON DELETE CASCADE
                     );

                    CREATE TABLE IF NOT EXISTS Country (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Name TEXT UNIQUE,
                        IsDeleted BOOLEAN NOT NULL DEFAULT 0
                    );

                    CREATE TABLE IF NOT EXISTS Gender (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Name TEXT UNIQUE,
                        IsDeleted BOOLEAN NOT NULL DEFAULT 0
                    );
                ");

                // Insert default countries
                connection.Execute("INSERT OR IGNORE INTO Country (Name) VALUES ('United Kingdom')");
                connection.Execute("INSERT OR IGNORE INTO Country (Name) VALUES ('United States America')");
                connection.Execute("INSERT OR IGNORE INTO Country (Name) VALUES ('Nigeria')");
                connection.Execute("INSERT OR IGNORE INTO Country (Name) VALUES ('South Africa')");

                // Insert default genders 
                connection.Execute("INSERT OR IGNORE INTO Gender (Name) VALUES ('Male')");
                connection.Execute("INSERT OR IGNORE INTO Gender (Name) VALUES ('Female')");
                connection.Execute("INSERT OR IGNORE INTO Gender (Name) VALUES ('Prefer Not to say')");



                return connection;
            });
        }
    }
}
