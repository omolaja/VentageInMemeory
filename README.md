VentageInMemory

VentageInMemory is an in-memory database implementation for handling customer-related data in a lightweight and efficient manner. This project leverages C#, Dapper, FluentValidation, and CQRS to provide a structured and scalable approach to data management.

🚀 Features

In-Memory Data Storage: Uses a dictionary to store customer data instead of a persistent database.

Dapper Integration: Simulates SQL-like queries with in-memory data.

CQRS Architecture: Separates read and write operations for better performance.

FluentValidation: Ensures data integrity and validation before processing requests.

MediatR Pipeline: Implements middleware for request validation.

🛠️ Technologies Used

.NET Core 8

Dapper (for querying in-memory data)

FluentValidation (for request validation)

MediatR (for CQRS pattern)

ASP.NET Core Web API

📌 Installation

Clone the repository:

git clone https://github.com/omolaja/VentageInMemeory.git

Navigate to the project directory:

cd VentageInMemory

Restore dependencies:

dotnet restore

Run the application:

dotnet run

🔄 API Endpoints

Customer Management

Method

Endpoint

Description

POST

/api/customers

Add a new customer

GET

/api/customers

Retrieve all customers

GET

/api/customers/{id}

Retrieve a specific customer by ID

PUT

/api/customers/{id}

Update customer details

DELETE

/api/customers/{id}

Delete a customer

🔍 Request Validation

Validation is implemented using FluentValidation. Example validation for customer ID:

RuleFor(x => x.Id)
    .GreaterThan(0)
    .WithMessage("Id must be greater than 0.");

📜 License

This project is licensed under the MIT License - see the LICENSE file for details.

🤝 Contributing

Contributions are welcome! To contribute:

Fork the repository.

Create a new branch: git checkout -b feature-branch-name

Make your changes and commit: git commit -m "Add new feature"

Push to the branch: git push origin feature-branch-name

Open a Pull Request.

📞 Contact

For any issues or feature requests, feel free to open an issue or contact me via [GitHub](https://github.com/omolaja).
